using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_dapper.Models;

namespace dotnet_dapper.Repository
{
    public interface IFilmeRepository
    {
        Task<IEnumerable<FilmeResponse>> BuscaFilmesAsync();
        Task<FilmeResponse> BuscaFilmeAsync(int id);
        Task<bool> AdicionaAsync(FilmeRequest request);
        Task<bool> AtualizaAsync(FilmeRequest request, int id);
        Task<bool> DeletarAsync(int id);

    }
}