using FMCW.Common.Results;

namespace FMCW.Common.Jwt
{
    public interface IJwtManager
    {
        public JwtDTO GenerateToken(string userId);
        public IntResult ValidateToken(string token);
    }
}