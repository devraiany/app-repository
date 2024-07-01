using App.Repository.Data.Databases;
using App.Repository.Domain.Entities.Usuarios;
using App.Repository.Models.Attributes;
using App.Repository.Models.Auth;
using App.Repository.Models.Cadastro;
using App.Repository.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace App.Repository.Services.Auth
{
    public class AuthService : IAuthService
    {
        public RepositoryDbContext _context;
        public IHttpContextAccessor _contextAccessor;

        public AuthService(RepositoryDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task Cadastrar(UsuarioModel model)
        {
            // -- Verificando se o usuário já existe
            if (_context.Usuarios.Any(x => x.Email == model.Email))
                throw new RepositoryErrorException("E-mail utilizado já está cadastrado");

            // --== Criando novo usuário
            var usuario = new Usuarios()
            {
                Id = Guid.NewGuid(),
                Email = model.Email,
                Nome = model.Nome,
                Senha = model.Senha,
                Sobrenome = model.Sobrenome,
            };

            // --== Gerando alteração na base de dados
            _context.Add(usuario);

            // --== Enviando alterações
            await _context.SaveChangesAsync();
        }

        // TODO: Passar para automapper depois
        public async Task<UsuarioModel> Autenticar(AuthModel model)
        {
            // -- Procurando usuário
            var user = await _context.Usuarios
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Senha == model.Senha)
                    ?? throw new RepositoryErrorException("E-mail e/ou senha incorretos!");
            
            // --== Inserindo o Id do usuário na sessão
            _contextAccessor.HttpContext.Session.SetString(RepositoryKeys.UID.ToString(), user.Id.ToString());
            _contextAccessor.HttpContext.Session.SetString(RepositoryKeys.Username.ToString(), 
                $"{user.Nome} {user.Sobrenome}");

            return new UsuarioModel()
            {
                Email = user.Email,
                Senha = user.Senha,
                Id = user.Id,
                Nome = user.Nome,
                Sobrenome = user.Sobrenome
            };
        }
    }
}
