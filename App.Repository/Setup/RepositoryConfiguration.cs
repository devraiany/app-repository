using App.Repository.Models.Configuration;

namespace App.Repository.Setup
{
    public static class RepositoryConfiguration
    {
        public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
        {
            // -- Buscando variavel de ambiente
            string environmentVariable = Environment
                .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
            
            // -- Encontrando arquivo de acordo com o ambiente
            builder.Configuration.AddJsonFile($"appsettings.{environmentVariable}.json");

            // -- Injetando configuração
            builder.Services.Configure<AppRepositoryConfiguration>(builder.Configuration);

            return builder;
        }
    }
}
