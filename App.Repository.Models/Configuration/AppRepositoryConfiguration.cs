namespace App.Repository.Models.Configuration
{
    public class AppRepositoryConfiguration
    {
        public AppRepositoryConfigurationConnectionStrings? ConnectionStrings { get; set; }
    }

    public class AppRepositoryConfigurationConnectionStrings
    {
        public string? BancoDeDadosConexao { get; set; }
    }
}
