using dotnet_dapper.Models;
using dotnet_dapper.Repository;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_dapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmesController : ControllerBase
    {
        private readonly IFilmeRepository _repository;

        public FilmesController(IFilmeRepository repository)
        {
            _repository = repository;
        }    

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var filmes = await _repository.BuscaFilmesAsync();

            return filmes.Any() ? Ok(filmes) : NoContent();
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var filme = await _repository.BuscaFilmeAsync(id);

            return filme 
            != null ? Ok(filme) 
            : NotFound("Filme Desconhecido");
        }

        [HttpPost]
        public async Task<IActionResult> Post(FilmeRequest request)
        {
            if(string.IsNullOrEmpty(request.Nome) || request.Ano <= 0 || request.ProdutoraId <= 0)
            {
                return BadRequest("Informações inválidas");
            }

            var adicionado = await _repository.AdicionaAsync(request);

            return adicionado 
                ? Ok("Filme adicionado com sucesso") 
                : BadRequest("Erro ao adicionar Filme");
        }

        [HttpPut("id")]
        public async Task <IActionResult> Put(FilmeRequest request, int id)
        {
            if(id <= 0) return BadRequest("Filme Inválido");

            var filme = await _repository.BuscaFilmeAsync(id);

            if(filme == null) NotFound("Filme não exite");

            if(string.IsNullOrEmpty(request.Nome)) request.Nome = filme.Nome;
            if(request.Ano <= 0 ) request.Ano = filme.Ano;

            var atualizado = await _repository.AtualizaAsync(request, id);

            return atualizado 
                    ? Ok("Filme atualizado com sucesso") 
                    : BadRequest("Erro ao atualizado Filme");

        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            if(id <= 0) return BadRequest("Filme Inválido");

            var filme = await _repository.BuscaFilmeAsync(id);

            if(filme == null) NotFound("Filme não exite");

            var deletado = await _repository.DeletarAsync(id);
            return deletado 
                ? Ok("Filme deletado com sucesso") 
                : BadRequest("Erro ao deletado Filme");
        }
    }
}