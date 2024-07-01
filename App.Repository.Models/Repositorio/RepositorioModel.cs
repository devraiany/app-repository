using App.Repository.Models.Cadastro;
using System.ComponentModel.DataAnnotations;

namespace App.Repository.Models.Repositorio
{
    public class RepositorioModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O nome é um campo obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "A descrição é um campo obrigatório")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A Linguagem é um campo obrigatório")]
        public string? Linguagem { get; set; }

        public DateTime UltimaAlteracao { get; set; }

        public UsuarioModel? Usuario { get; set; }
    }
}
