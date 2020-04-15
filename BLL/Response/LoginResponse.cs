namespace BLL.Response
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int Expire { get; set; }
        
    }
}