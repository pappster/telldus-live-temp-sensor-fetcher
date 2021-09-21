namespace SensorPoller
{
    public class EmailSettings
    {
        public string SenderEmail { get; set; }
        public string SenderPassword { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpServerPort { get; set; }
        public string ReceiverEmail { get; set; }
    }
}
