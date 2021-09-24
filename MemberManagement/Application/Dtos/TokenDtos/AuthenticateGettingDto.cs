namespace Application.Dtos.TokenDtos
{
    public class AuthenticateGettingDto
    {
        public string TokenCode { get; set; }
        public string TokenRefeshCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
