
namespace Portal.Infrustructure.Class
{
    public enum StatusCode
    {
        Succeed = 200,
        Failed = -1
    }
    public class ApiOutput
    {
        public string Message { get; set; }
        public object Data { get; set; }
        public StatusCode StatusCode { get; set; }
    }
}
