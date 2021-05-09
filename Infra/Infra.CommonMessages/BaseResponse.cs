namespace Infra.CommonMessages
{
    public class BaseResponse
    {
        public static BaseResponse Success => new(string.Empty);
        public static BaseResponse Fail => new("Something went wrong");
        public static BaseResponse Create(string message) => new(message);

        private BaseResponse(string message)
        {
            IsError = !string.IsNullOrWhiteSpace(message);
            Message = message;
        }

        public bool IsError { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public int AccessTokenExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string RefreshTokenExpiresIn { get; set; }
    }
}
