using App.Repository.Models.Repositorio;

namespace App.Repository.Views.Home
{
    public class MeusRepositoriosViewModel
    {
        public List<RepositorioModel> Repositorios { get; set; } = new List<RepositorioModel>();

        public string? Query { get; set; }
    }
}
