using App.Repository.Default;
using App.Repository.Services.Auth;
using App.Repository.Views.Favoritos;
using Microsoft.AspNetCore.Mvc;

namespace App.Repository.Controllers.Favoritos
{
    [Route("home")]
    public class FavoritosController : RepositoryController
    {
        private readonly IRepositorioService _repositorioService;
        public FavoritosController(IRepositorioService repositorioService)
        {
            _repositorioService = repositorioService;
        }

        /// <summary>
        /// Responsável por obter todos os repositórios favoritos.
        /// </summary>
        /// <returns></returns>
        [Route("favoritos")]
        public async Task<IActionResult> Favorito()
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Obtendo repositórios favoritos.
            var res = await _repositorioService.ObterFavoritos();

            // -- Listando repositórios.
            var viewModel = new FavoritoViewModel()
            {
                Repositorios = res.ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Responsável por remover um item dos favoritos.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("favoritos/{id}/remover")]
        public async Task<IActionResult> Remover(Guid id)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // --== Removendo.
            await _repositorioService.RemoverDosFavoritos(id);
        
            // -- Redirecionando para a tela de favorito.
            return RedirectToAction("Favorito");
        }
    }
}
