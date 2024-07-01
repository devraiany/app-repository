using App.Repository.Default;
using App.Repository.Models.Enums;
using App.Repository.Services.Auth;
using App.Repository.Views.Home;
using Microsoft.AspNetCore.Mvc;

namespace App.Repository.Controllers.Home
{
    [Route("home")]
    public class HomeController : RepositoryController
    {
        private readonly IRepositorioService _repositorioService;
        public HomeController(IRepositorioService repositorioService)
        {
            _repositorioService = repositorioService;
        }

        [Route("landing")]
        public async Task<IActionResult> Home()
        {
            // -- Gerando nova ViewModel.
            var viewModel = new HomeViewModel();

            // --== Controlando o acesso do usuário.
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // --== Enviando nome de usuário para View.
            ViewData["Usuario"] = HttpContext!.Session!.GetString(RepositoryKeys.Username.ToString());

            // --== Obtendo repositórios.
            var repos = await _repositorioService.Obter();
            viewModel.Repositorios = repos.ToList();

            return View(viewModel);
        }
    }
}
