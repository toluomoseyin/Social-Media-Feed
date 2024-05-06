namespace SocialMediaApp.Application.DTOs
{
    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static BaseResponse<T> Success(string message,T data = default)
        {
            return new BaseResponse<T>
            {
                Status = true,
                Message = message,
                Data = data
            };
        }

        public static BaseResponse<T> Failure(string message)
        {
            return new BaseResponse<T>
            {
                Status = false,
                Message = message
            };
        }
    }
}
