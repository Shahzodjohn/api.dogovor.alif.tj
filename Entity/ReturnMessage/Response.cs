

namespace Entity.ReturnMessage
{
    public class Response : HttpResponseMessage
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
