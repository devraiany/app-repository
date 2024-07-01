using App.Repository.Domain.Entities.Default;

namespace App.Repository.Domain.Entities.Favoritos
{
    public class Favoritos : RepositoryEntityModel
    {
        public Guid? UsuarioId { get; set; }
        public Guid? RepositorioId { get; set; }
    }
}
