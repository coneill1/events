namespace Events.Data
{
    public class DataCredentials : IDataCredentials
    {
        public string Database { get; set; }
        public string Host { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
    }

    public interface IDataCredentials
    {
        string Database { get; set; }
        string Host { get; set; }
        string UserId { get; set; }
        string Password { get; set; }
    }
}
