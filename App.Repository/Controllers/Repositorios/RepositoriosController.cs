using App.Repository.Default;
using App.Repository.Services.Auth;
using App.Repository.Views.Home;
using App.Repository.Views.Repositorios;
using Microsoft.AspNetCore.Mvc;

namespace App.Repository.Controllers.Repositorios
{
    [Route("repository")]
    public class RepositoriosController : RepositoryController
    {
        private readonly IRepositorioService _repositorioService;
        public RepositoriosController(IRepositorioService repositorioService)
        {
            _repositorioService = repositorioService;
        }

        /// <summary>
        /// Responsável por deletar um repositório.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // --== Controlando o acesso do usuário.
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Obtendo repositório.
            await _repositorioService.Deletar(id);

            // -- Redirecionando para a tela home.
            return Redirect("/home/landing");
        }

        /// <summary>
        /// Responsável por favoritar um repositório.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("favoritar/{id}")]
        public async Task<IActionResult> Favoritar(Guid id)
        {
            // --== Controlando o acesso do usuário.
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Obtendo repositório.
            await _repositorioService.AdicionarAosFavoritos(id);

            // -- Redirecionando para a tela de favoritos.
            return Redirect($"/home/favoritos");
        }

        [HttpGet("editar/{id}")]
        public async Task<IActionResult> Put(Guid id, string? message = null)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Passando mensagem
            ViewBag.Message = message;

            // -- Obtendo repositório
            var repo = await _repositorioService.ObterPorId(id);

            // -- Gerando viewModel
            var viewModel = new PutRepositorioViewModel
            {
                Repositorio = repo
            };

            return View(viewModel);
        }

        /// <summary>
        /// Responsável por editar um repositório.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost("editar/{id}")]
        public async Task<IActionResult> Put(Guid id, PutRepositorioViewModel model)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Validando se os campos foram preenchidos corretamente
            if (!ModelState.IsValid) return View(model);

            model.Repositorio.Id = id;

            // --== Cadastrando novo repositório
            await _repositorioService.Atualizar(model.Repositorio);

            // -- Isso evita que ao recarregar a página eu envie o formulário novamente (padrão PRG)
            return RedirectToAction("Put", new { message = "Repositório atualizado com sucesso!" });
        }

        [HttpGet("novo-repositorio")]
        public async Task<IActionResult> Create(string? message = null)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Passando mensagem
            ViewBag.Message = message;

            return View();
        }

        /// <summary>
        /// Responsável por criar um novo repositório.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("novo-repositorio")]
        public async Task<IActionResult> Create(CreateRepositorioViewModel model)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Validando se os campos foram preenchidos corretamente
            if (!ModelState.IsValid) return View(model);

            // --== Cadastrando novo repositório
            await _repositorioService.Cadastrar(model.Repositorio);

            // -- Isso evita que ao recarregar a página eu envie o formulário novamente (padrão PRG)
            return RedirectToAction("Create", new { message = "Repositório cadastrado com sucesso!" });
        }

        /// <summary>
        /// Responsável por obter detalhes de um repositório.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("repositorios/{id}")]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            // -- Obtendo repositório
            var repo = await _repositorioService.ObterPorId(id);

            // -- Gerando viewModel
            var viewModel = new DetalhesRepositorioViewModel
            {
                Repositorio = repo
            };

            return View(viewModel);
        }

        [Route("meus-repositorios")]
        public async Task<IActionResult> Get(MeusRepositoriosViewModel? model = null)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            var res = await _repositorioService.Obter(model?.Query);

            var viewModel = new MeusRepositoriosViewModel()
            {
                Repositorios = res.ToList()
            };

            return View(viewModel);
        }

        /// <summary>
        /// Responsável por deletar todos os repositórios.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("deletar-todos")]
        public async Task<IActionResult> DeletarTodos(MeusRepositoriosViewModel? model = null)
        {
            // --== Controlando o acesso do usuário
            if (!this.IsAuthorized())
                return Redirect("/auth/login");

            await _repositorioService.DeletarTodos();

            return Redirect("/home/landing");
        }
    }
}
