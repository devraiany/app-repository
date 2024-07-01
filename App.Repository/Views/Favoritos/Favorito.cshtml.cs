using App.Repository.Models.Repositorio;

namespace App.Repository.Views.Favoritos
{
    public class FavoritoViewModel
    {
        public List<RepositorioModel> Repositorios { get; set; } = new List<RepositorioModel>();
    }
}
