using App.Repository.Models.Repositorio;

namespace App.Repository.Views.Home
{
    public class HomeViewModel
    {
        public List<RepositorioModel> Repositorios { get; set; } = new List<RepositorioModel>();
    }
}
