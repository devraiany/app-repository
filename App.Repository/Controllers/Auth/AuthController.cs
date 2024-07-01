using App.Repository.Default;
using App.Repository.Models.Attributes;
using App.Repository.Models.Configuration;
using App.Repository.Services.Auth;
using App.Repository.Views.Auth;
using App.Repository.Views.Cadastro;
using Microsoft.AspNetCore.Mvc;

namespace App.Repository.Controllers.Auth
{
    [Route("auth")]
    public class AuthController : RepositoryController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Route("login")]
        public IActionResult Login()
        {
            // --== Controlando o acesso do usuário
            if (this.IsAuthorized())
                return Redirect("/home/landing");

            return View();
        }

        /// <summary>
        /// Responsável por realizar o login do usuário.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            // --== Controlando o acesso do usuário
            if (this.IsAuthorized())
                return Redirect("/home/landing");

            if (!ModelState.IsValid) return View(model);
            else
            {
                try
                {
                    // -- Procurando usuário
                    var res = await _authService.Autenticar(model.AuthModel);

                    // -- Usuário autenticado, levando para LandingPage
                    return Redirect("/home/landing");
                }
                catch (RepositoryErrorException ex)
                {
                    // -- Um erro esperado aconteceu
                    ViewBag.Error = ex.Message;
                    return View(model);
                }
            }
        }

        [Route("cadastro")]
        public IActionResult Cadastro()
        {
            // --== Controlando o acesso do usuário
            if (this.IsAuthorized())
                return Redirect("/home/landing");

            return View();
        }

        /// <summary>
        /// Responsável por realizar o cadastro do usuário.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("cadastro")]
        public async Task<IActionResult> Cadastro(CadastroViewModel model)
        {
            // --== Controlando o acesso do usuário
            if (this.IsAuthorized())
                return Redirect("/home/landing");

            try
            {
                await _authService.Cadastrar(model.UsuarioModel);

                return Redirect("/auth/login");
            }
            catch (RepositoryErrorException ex)
            {
                // --== Enviando feedback para a View
                ViewBag.Error = ex.Message;
                return View(model);
            }
            catch (Exception)
            {
                ViewBag.Error = AppRepositoryDefaultMessages.InternalServerError;
                return View(model);
            }
        }
    }
}
