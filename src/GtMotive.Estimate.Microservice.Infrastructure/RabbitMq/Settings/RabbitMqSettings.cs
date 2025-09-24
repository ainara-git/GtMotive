namespace GtMotive.Estimate.Microservice.Infrastructure.RabbitMq.Settings
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = "localhost";

        public int Port { get; set; } = 5672;

        public string UserName { get; set; } = "admin";

        public string Password { get; set; } = "password";
    }
}
