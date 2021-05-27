using FMCW.Common.Results;

namespace FMCW.Common.Jwt
{
    public interface IJwtManager
    {
        public JwtDTO GenerateToken(int idUsuario);
        public IntResult ValidateToken(string token);
    }
}