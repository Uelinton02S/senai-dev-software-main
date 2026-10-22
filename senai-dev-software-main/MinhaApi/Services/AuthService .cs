using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _repo;
    private readonly IConfiguration _config;

    public AuthService(IUsuarioRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public LoginResponse Login(LoginRequest req)
    {
        var usuario = _repo.BuscarPorEmail(req.Email)
            ?? throw new UnauthorizedAccessException("Credenciais inválidas.");

        bool senhaOk = BCrypt.Net.BCrypt.Verify(req.Senha, usuario.SenhaHash);

        if (!senhaOk)
            throw new UnauthorizedAccessException("Credenciais inválidas.");

        return new LoginResponse
        {
            Token = GerarToken(usuario),
            Nome = usuario.Nome,
            Email = usuario.Email
        };
    }

    private string GerarToken(Usuario u)
    {
        var chave = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("Chave JWT não configurada.");

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(chave));

        var creds = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, u.Id.ToString()),
            new Claim(ClaimTypes.Name, u.Nome),
            new Claim(ClaimTypes.Email, u.Email)
        };

        var expira = DateTime.UtcNow.AddMinutes(
            double.Parse(_config["Jwt:ExpiresInMinutes"] ?? "60"));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: expira,
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}