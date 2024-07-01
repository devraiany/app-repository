using App.Repository.Domain.Entities.Default;

namespace App.Repository.Domain.Entities.Usuarios
{
    public class Usuarios : RepositoryEntityModel
    {
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        // -- Indica o relacionamento 1:N com os repositórios.
        public virtual ICollection<Repositorios.Repositorios>? Repositorios { get; set; }

        // -- Indica o relacionamento com os Favoritos.
        public virtual ICollection<Favoritos.Favoritos>? Favoritos { get; set; }
    }
}
