namespace ProvaUnitTests
{
    public class LogInfo
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Source { get; set; }
        public string Type { get; set; } = "ERROR";
        public int Position { get; set; } = 0;
        public System.Collections.IDictionary Data { get; set; }
        public Guid Code { get; set; }

        public LogInfo() { }

        public LogInfo(string message)
        {
            Message = message;
            StackTrace = "";
            Source = "";
            Type = "";
            Position = 0;
            Data = new Dictionary<string, string>();
            Code = new Guid();
        }
    }
}
