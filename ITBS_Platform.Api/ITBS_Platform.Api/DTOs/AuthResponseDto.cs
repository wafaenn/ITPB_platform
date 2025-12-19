namespace ITBS_Platform.Api.DTOs
{
    public class AuthResponseDto
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

}
