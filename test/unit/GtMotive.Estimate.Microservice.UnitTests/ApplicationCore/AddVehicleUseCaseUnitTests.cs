using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.AddVehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    public class AddVehicleUseCaseUnitTests
    {
        private readonly Mock<IVehicleRepository> _mockVehicleRepository;
        private readonly Mock<IAddVehicleOutputPort> _mockOutputPort;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBusFactory> _mockBusFactory;
        private readonly Mock<IBus> _mockBus;
        private readonly AddVehicleUseCase _addVehicleUseCase;

        public AddVehicleUseCaseUnitTests()
        {
            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockOutputPort = new Mock<IAddVehicleOutputPort>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockBusFactory = new Mock<IBusFactory>();
            _mockBus = new Mock<IBus>();

            _mockBusFactory.Setup(f => f.GetClient(It.IsAny<Type>())).Returns(_mockBus.Object);

            _addVehicleUseCase = new AddVehicleUseCase(
                _mockVehicleRepository.Object,
                _mockOutputPort.Object,
                _mockUnitOfWork.Object,
                _mockBusFactory.Object);
        }

        [Fact]
        public async Task ExecuteWhenVehicleDoesNotExistShouldAddVehicleAndPublishEvent()
        {
            // Arrange
            var makeAndModel = "Toyota Corolla";
            var licensePlate = new LicensePlate("1234ABC");
            var manufacturingDate = DateTime.Today.AddYears(-1);
            var input = new AddVehicleInput(makeAndModel, licensePlate, manufacturingDate);

            _mockVehicleRepository.Setup(r => r.GetByLicensePlateAsync(licensePlate)).Returns(Task.FromResult<Vehicle>(null));
            _mockVehicleRepository.Setup(r => r.AddAsync(It.IsAny<Vehicle>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.Save()).ReturnsAsync(1);
            _mockBus.Setup(b => b.Send(It.IsAny<VehicleAddedEvent>())).Returns(Task.CompletedTask);

            // Act
            await _addVehicleUseCase.Execute(input);

            // Assert
            _mockVehicleRepository.Verify(r => r.GetByLicensePlateAsync(licensePlate), Times.Once);
            _mockVehicleRepository.Verify(r => r.AddAsync(It.IsAny<Vehicle>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
            _mockBusFactory.Verify(f => f.GetClient(typeof(VehicleAddedEvent)), Times.Once);
            _mockBus.Verify(b => b.Send(It.IsAny<VehicleAddedEvent>()), Times.Once);
            _mockOutputPort.Verify(p => p.StandardHandle(It.IsAny<AddVehicleOutput>()), Times.Once);
            _mockOutputPort.Verify(p => p.ErrorHandle(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task ExecuteWhenVehicleAlreadyExistsShouldCallErrorHandleAndNotAddVehicle()
        {
            // Arrange
            var makeAndModel = "Toyota Corolla";
            var licensePlate = new LicensePlate("1234ABC");
            var manufacturingDate = DateTime.Today.AddYears(-1);
            var input = new AddVehicleInput(makeAndModel, licensePlate, manufacturingDate);

            _mockVehicleRepository.Setup(r => r.GetByLicensePlateAsync(licensePlate)).ReturnsAsync(new Vehicle(makeAndModel, licensePlate, manufacturingDate));

            // Act
            await _addVehicleUseCase.Execute(input);

            // Assert
            _mockVehicleRepository.Verify(r => r.GetByLicensePlateAsync(licensePlate), Times.Once);
            _mockVehicleRepository.Verify(r => r.AddAsync(It.IsAny<Vehicle>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Never);
            _mockBusFactory.Verify(f => f.GetClient(It.IsAny<Type>()), Times.Never);
            _mockBus.Verify(b => b.Send(It.IsAny<IDomainEvent>()), Times.Never);
            _mockOutputPort.Verify(p => p.StandardHandle(It.IsAny<AddVehicleOutput>()), Times.Never);
            _mockOutputPort.Verify(p => p.ErrorHandle("ERROR: Vehicle already exists."), Times.Once);
        }
    }
}
