using App.Repository.Models.Repositorio;

namespace App.Repository.Services.Auth
{
    public interface IRepositorioService
    {
        Task Cadastrar(RepositorioModel model);
        Task Atualizar(RepositorioModel model);
        Task<ICollection<RepositorioModel>> Obter(string? nome = null);
        Task AdicionarAosFavoritos(Guid repositorioId);
        Task RemoverDosFavoritos(Guid repositorioId);
        Task DeletarTodos();
        Task Deletar(Guid id);
        Task<RepositorioModel> ObterPorId(Guid repositorioId);
        Task<ICollection<RepositorioModel>> ObterFavoritos();
    }
}
