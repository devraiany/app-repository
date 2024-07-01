using App.Repository.Models.Auth;
using App.Repository.Models.Cadastro;

namespace App.Repository.Services.Auth
{
    public interface IAuthService
    {
        Task Cadastrar(UsuarioModel model);
        Task<UsuarioModel> Autenticar(AuthModel model);
    }
}
