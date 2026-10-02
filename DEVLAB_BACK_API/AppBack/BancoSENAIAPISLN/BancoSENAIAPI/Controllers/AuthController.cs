using BancoSENAIAPI.Data;
using BancoSENAIAPI.Dtos;
using BancoSENAIAPI.Models;
using BancoSENAIAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TokenContext _tokenServico;

        public AuthController(AppDbContext context, TokenContext tokenServico)
        {
            _context = context;
            _tokenServico = tokenServico;
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterRequest dto)
        {
            if (await _context.Usuarios.AnyAsync(u => u.NomeUsuario == dto.NomeUsuario))
            {
                return BadRequest(new { message = "Este nome de usuário já está em uso" });
            }

            var usuario = new Usuario
            {
                NomeUsuario = dto.NomeUsuario,
                SenhaRash = BCrypt.Net.BCrypt.HashPassword(dto.Senha)
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Created("", new { usuario.Id, usuario.NomeUsuario });
        }
    }
}
