namespace DocRecycle.Models
{
    public class AuthResult
    {
        private AuthResult()
        {
        }

        public string Token { get; set; }
        public string Message { get; set; }

        public static AuthResult FromSuccess(string token)
        {
            var obj = new AuthResult {Token = token};
            return obj;
        }

        public static AuthResult FromError(string message)
        {
            var obj = new AuthResult {Message = message};
            return obj;
        }
    }
}