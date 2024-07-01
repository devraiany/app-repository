using App.Repository.Domain.Entities.Default;
using App.Repository.Models.Repositorio;

namespace App.Repository.Domain.Entities.Repositorios
{
    public class Repositorios : RepositoryEntityModel
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Linguagem { get; set; }
        public Guid? UsuarioId { get; set; }

        public void Atualizar(RepositorioModel model)
        {
            if (model != null)
            {
                Nome = model.Nome ?? Nome;
                Descricao = model.Descricao ?? Descricao;
                Linguagem = model.Linguagem ?? Linguagem;
            }
        }
    }
}
