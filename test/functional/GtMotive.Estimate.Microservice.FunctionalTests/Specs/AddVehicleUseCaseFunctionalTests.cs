using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.Domain.Common;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    public class AddVehicleUseCaseFunctionalTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task AddVehicleUseCaseWithValidInputShouldCreateVehicleSuccessfully()
        {
            // Arrange
            var testDate = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            DateTimeProvider.UtcNow = () => testDate;

            try
            {
                await Fixture.UsingHandlerForRequestResponse<AddVehicleCommand, ICommandResult>(
                    async handler =>
                    {
                        // Arrange
                        var command = new AddVehicleCommand(
                            makeAndModel: "Honda Civic 2023",
                            licensePlate: "9876XYZ",
                            manufacturingDate: testDate.AddYears(-2));

                        // Act
                        var result = await handler.Handle(command, CancellationToken.None);

                        // Assert
                        Assert.NotNull(result);

                        // Verify the result is a presenter
                        var webPresenter = Assert.IsType<IWebApiPresenter>(result, exactMatch: false);
                        Assert.NotNull(webPresenter.ActionResult);

                        // Verify it's a Created result (201)
                        var createdResult = Assert.IsType<CreatedResult>(webPresenter.ActionResult);
                        Assert.Contains("/api/vehicles/", createdResult.Location, StringComparison.InvariantCulture);

                        // Verify response data structure
                        var responseData = createdResult.Value;
                        Assert.NotNull(responseData);

                        // Verify response structure
                        var vehicleIdProperty = responseData.GetType().GetProperty("VehicleId");
                        var makeAndModelProperty = responseData.GetType().GetProperty("MakeAndModel");
                        var licensePlateProperty = responseData.GetType().GetProperty("LicensePlate");
                        var manufacturingDateProperty = responseData.GetType().GetProperty("ManufacturingDate");

                        Assert.NotNull(vehicleIdProperty);
                        Assert.NotNull(makeAndModelProperty);
                        Assert.NotNull(licensePlateProperty);
                        Assert.NotNull(manufacturingDateProperty);

                        // Verify actual values
                        var vehicleId = (Guid)vehicleIdProperty.GetValue(responseData)!;
                        var makeAndModel = (string)makeAndModelProperty.GetValue(responseData);
                        var licensePlate = (string)licensePlateProperty.GetValue(responseData);

                        Assert.NotEqual(Guid.Empty, vehicleId);
                        Assert.Equal("Honda Civic 2023", makeAndModel);
                        Assert.Equal("9876XYZ", licensePlate);
                    });
            }
            finally
            {
                // Cleanup
                DateTimeProvider.UtcNow = () => DateTime.UtcNow;
            }
        }

        [Fact]
        public async Task AddVehicleUseCaseCompleteFlowVerifiesAllIntegrationLayers()
        {
            // Arrange
            var testDate = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc);
            DateTimeProvider.UtcNow = () => testDate;

            try
            {
                await Fixture.UsingHandlerForRequestResponse<AddVehicleCommand, ICommandResult>(
                    async handler =>
                    {
                        // Arrange
                        var command = new AddVehicleCommand(
                            makeAndModel: "Integration Test Vehicle",
                            licensePlate: "1111AAA",
                            manufacturingDate: testDate.AddYears(-3));

                        // Act
                        var result = await handler.Handle(command, default);

                        // Assert

                        // 1. Command Handler Layer
                        Assert.NotNull(result);
                        var presenter = Assert.IsType<IWebApiPresenter>(result, exactMatch: false);

                        // 2. Use Case Layer (business logic executed)
                        var createdResult = Assert.IsType<CreatedResult>(presenter.ActionResult);

                        // 3. Domain Layer (Value Objects created and validated)
                        var responseData = createdResult.Value;
                        var licensePlateValue = (string)responseData.GetType().GetProperty("LicensePlate").GetValue(responseData);
                        Assert.Equal("1111AAA", licensePlateValue);

                        // 4. Repository Layer (data persistence completed)
                        var vehicleId = (Guid)responseData.GetType().GetProperty("VehicleId").GetValue(responseData);
                        Assert.NotEqual(Guid.Empty, vehicleId);

                        // 5. Output Port Layer (presentation)
                        Assert.Contains("/api/vehicles/", createdResult.Location, StringComparison.InvariantCulture);
                        Assert.Equal(201, createdResult.StatusCode);

                        // 6.domain → DTO conversion
                        var makeAndModel = (string)responseData.GetType().GetProperty("MakeAndModel").GetValue(responseData);
                        Assert.Equal("Integration Test Vehicle", makeAndModel);
                    });
            }
            finally
            {
                // Cleanup
                DateTimeProvider.UtcNow = () => DateTime.UtcNow;
            }
        }
    }
}
