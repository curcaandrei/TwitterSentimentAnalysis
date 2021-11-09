namespace Application.Responses
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = Success;
            Message = message;
        }

        public BaseResponse(bool success, string message)
        {
            this.Success = success;
            this.Message = message;
        }
        
    }
}