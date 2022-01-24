namespace GISA.EventBusRabbitMQ.Settings
{
    public class RabbitOptionsSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; } = 5672;
        public string VHost { get; set; } = "/";
    }
}
