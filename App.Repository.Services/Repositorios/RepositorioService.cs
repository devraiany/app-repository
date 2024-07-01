using App.Repository.Data.Databases;
using App.Repository.Models.Cadastro;
using App.Repository.Models.Enums;
using App.Repository.Models.Repositorio;
using App.Repository.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Repository.Services.Repositorios
{
    public class RepositorioService : IRepositorioService
    {
        public RepositoryDbContext _context;
        public readonly IHttpContextAccessor _contextAccessor;

        public RepositorioService(RepositoryDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        #region Cadastrar/Atualizar
        // --  Responsável por Cadastrar um repositório.
        public async Task Cadastrar(RepositorioModel model)
        {
            // -- Obtém o Id do usuário autenticado na sessão
            var usuarioId = ObterUsuarioIdDaSessao();

            var repositorio = new Domain.Entities.Repositorios.Repositorios
            {
                Id = model.Id ?? Guid.NewGuid(),
                Nome = model.Nome,
                Descricao = model.Descricao,
                Linguagem = model.Linguagem,
                UsuarioId = usuarioId,
                SysDateInsert = DateTime.UtcNow,
                SysDateUpdate = DateTime.UtcNow
            };

            _context.Repositorios.Add(repositorio);

            await _context.SaveChangesAsync();
        }

        // -- Responsável por atualizar um repositório.
        public async Task Atualizar(RepositorioModel model)
        {
            var repositorio = await _context.Repositorios.FirstOrDefaultAsync(item => item.Id == model.Id)
                 ?? throw new Exception("Repositório informado não encontrado");

            repositorio.Atualizar(model);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Obter Repositórios, com ou sem filtros.

        // -- Responsável por retornar todos os repositórios, ou filtrados pelo usuário.
        public async Task<ICollection<RepositorioModel>> Obter(string? nome = null)
        {
            // -- Obtém o Id do usuário autenticado na sessão.
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Inicia a pesquisa no banco de dados.
            var query = _context.Repositorios
                .Where(r => r.UsuarioId == usuarioId)
                .AsQueryable();

            // --- Caso seja passado, filtra por nome.
            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(n => n.Nome.Contains(nome));
            }

            var repositorios = await query.ToListAsync();

            // -- Busca o usuário para obter nome e sobrenome.
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            // -- Define o fuso horário para o Brasil (Horário de Brasília)
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // -- Mapeia para RepositorioModel incluindo informações do usuário.
            var repositoriosModel = repositorios.Select(r => new RepositorioModel
            {
                Id = r.Id,
                Nome = r.Nome,
                Descricao = r.Descricao,
                Linguagem = r.Linguagem,
                UltimaAlteracao = TimeZoneInfo.ConvertTimeFromUtc(r.SysDateUpdate, timeZoneInfo),
                Usuario = new UsuarioModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email
                }
            }).ToList();

            return repositoriosModel;
        }

        // -- Responsável por retornar um repositório.
        public async Task<RepositorioModel> ObterPorId(Guid repositorioId)
        {
            // -- Obtém o Id do usuário autenticado na sessão.
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Busca o repositório pelo Id e verifica se pertence ao usuário.
            var repositorio = await _context.Repositorios
                .FirstOrDefaultAsync(r => r.Id == repositorioId && r.UsuarioId == usuarioId)
                ?? throw new Exception("Repositório não encontrado ou não pertence ao usuário.");

            // -- Busca o usuário para obter nome e sobrenome.
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId)
                ?? throw new Exception("Usuário não encontrado.");

            // -- Define o fuso horário para o Brasil (Horário de Brasília)
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // -- Mapeia para RepositorioModel incluindo informações do usuário.
            var repositorioModel = new RepositorioModel
            {
                Id = repositorio.Id,
                Nome = repositorio.Nome,
                Descricao = repositorio.Descricao,
                Linguagem = repositorio.Linguagem,
                UltimaAlteracao = TimeZoneInfo.ConvertTimeFromUtc(repositorio.SysDateUpdate, timeZoneInfo),
                Usuario = new UsuarioModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email
                }
            };

            return repositorioModel;
        }

        // -- Responsável por obter apenas os favoritos.
        public async Task<ICollection<RepositorioModel>> ObterFavoritos()
        {
            // -- Obtém o Id do usuário autenticado na sessão.
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Busca os favoritos do usuário.
            var favoritos = await _context.Favoritos
                .Where(f => f.UsuarioId == usuarioId)
                .ToListAsync();

            // -- Obtém os Ids dos repositórios favoritos.
            var favoritosIds = favoritos.Select(f => f.RepositorioId).ToList();

            // -- Busca os repositórios que estão nos favoritos.
            var repositorios = await _context.Repositorios
                .Where(r => favoritosIds.Contains(r.Id))
                .ToListAsync();

            // -- Busca o usuário para obter nome e sobrenome.
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Id == usuarioId)
                ?? throw new Exception("Usuário não encontrado.");

            // -- Define o fuso horário para o Brasil (Horário de Brasília)
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

            // -- Mapeia para RepositorioModel incluindo informações do usuário.
            var repositoriosModel = repositorios.Select(r => new RepositorioModel
            {
                Id = r.Id,
                Nome = r.Nome,
                Descricao = r.Descricao,
                Linguagem = r.Linguagem,
                UltimaAlteracao = TimeZoneInfo.ConvertTimeFromUtc(r.SysDateUpdate, timeZoneInfo),
                Usuario = new UsuarioModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email
                }
            }).ToList();

            return repositoriosModel;
        }
        #endregion

        #region Adicionar/Remover dos favoritos.
        // -- Responsável por adicionar um repositório aos favoritos.
        public async Task AdicionarAosFavoritos(Guid repositorioId)
        {
            // -- Obtém o Id do usuário autenticado na sessão
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Verifica se o repositório existe.
            Domain.Entities.Repositorios.Repositorios repositorio = await _context.Repositorios
                .FirstOrDefaultAsync(x => x.Id == repositorioId)
                ?? throw new Exception("Repositório informado não encontrado");

            // -- Verifica se o repositório já está nos favoritos do usuário.
            var repositorioFavorito = await _context.Favoritos
                .FirstOrDefaultAsync(x => x.RepositorioId == repositorioId && x.UsuarioId == usuarioId);

            if (repositorioFavorito != null)
                throw new Exception("Este repositório já está nos favoritos do usuário");

            // -- Adiciona o repositório aos favoritos
            var favorito = new Domain.Entities.Favoritos.Favoritos
            {
                Id = Guid.NewGuid(),
                UsuarioId = usuarioId,
                RepositorioId = repositorioId,
            };

            _context.Favoritos.Add(favorito);
            await _context.SaveChangesAsync();
        }

        // -- Responsável por remover um repositório dos favoritos.
        public async Task RemoverDosFavoritos(Guid repositorioId)
        {
            // -- Obtém o Id do usuário autenticado na sessão
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Verifica se o relacionamento de favoritos existe
            var favorito = await _context.Favoritos
                .FirstOrDefaultAsync(x => x.RepositorioId == repositorioId && x.UsuarioId == usuarioId)
                ?? throw new Exception("Este repositório não está nos favoritos do usuário");

            // -- Remove o relacionamento dos favoritos
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Deletar repositórios.

        // -- Responsável por remover permanentemente todos os repositórios cadastrados do usuário.
        public async Task DeletarTodos()
        {
            // -- Obtém o Id do usuário autenticado na sessão.
            var usuarioId = ObterUsuarioIdDaSessao();

            // -- Busca os repositórios pelo ID do usuário autenticado
            var repositorios = await _context.Repositorios
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();

            // -- Obtém os IDs dos repositórios a serem removidos
            var repositorioIds = repositorios.Select(r => r.Id).ToList();

            // -- Remove todos os favoritos associados aos repositórios do usuário
            var favoritos = await _context.Favoritos
                .Where(f => repositorioIds.Contains(f.RepositorioId.Value))
                .ToListAsync();

            _context.Favoritos.RemoveRange(favoritos);

            // -- Remove os repositórios do usuário
            _context.Repositorios.RemoveRange(repositorios);

            // -- Salva as mudanças no banco de dados
            await _context.SaveChangesAsync();
        }

        // -- Responsável por remover um repositório permanentemente.
        public async Task Deletar(Guid id)
        {
            var repositorio = await _context.Repositorios.FindAsync(id);

            if (repositorio != null)
            {
                // -- Remove todos os favoritos associados ao repositório
                var favoritos = await _context.Favoritos
                    .Where(x => x.RepositorioId == id)
                    .ToListAsync();

                _context.Favoritos.RemoveRange(favoritos);

                // -- Remove o próprio repositório
                _context.Repositorios.Remove(repositorio);

                // -- Salva as mudanças no banco de dados
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Obter UsuarioId.
        // -- Método auxiliar para obter o Id do usuário da sessão.
        private Guid ObterUsuarioIdDaSessao()
        {
            var usuarioIdString = _contextAccessor.HttpContext.Session.GetString(RepositoryKeys.UID.ToString());

            if (!string.IsNullOrEmpty(usuarioIdString) && Guid.TryParse(usuarioIdString, out Guid usuarioId))
            {
                return usuarioId;
            }
            throw new Exception("Usuário não autenticado ou sessão expirada.");
        }
        #endregion
    }
}
