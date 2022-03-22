using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {

        private FilmeContext _context;
        private IMapper _mapper;
        //private object filmeNovo;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*private static List<Filme> filmes = new List<Filme>();
        private static int id = 1;*/
        
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            /*filme.Id = id++;
            filmes.Add(filme);

            Filme filme = new Filme
            {
                Titulo = filmeDto.Titulo,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,
                Diretor = filmeDto.Diretor
            };*/

            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperaFilmesId), new { Id = filme.Id }, filme);
        }

        [HttpGet]
        //public IActionResult RecuperaFilmes()
        public IEnumerable<Filme> RecuperaFilmes()
        {
            //return Ok(filmes);
            //return Ok(_context.Filmes);
            return _context.Filmes;
        }

        [HttpGet("{id}")]
        public IActionResult RecuperaFilmesId(int id)
        {
            /*foreach (Filme filme in filmes){
                if(filme.Id == id)
                {
                    return filme;
                }
            }
            return null;*/

            //Filme filme =  filmes.FirstOrDefault(filme => filme.Id == id);
            Filme filme =  _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme != null)
            {
                /*ReadFilmeDto filmeDto = new ReadFilmeDto
                {
                    Titulo = filme.Titulo,
                    Genero = filme.Genero,
                    Duracao = filme.Duracao,
                    Diretor = filme.Diretor,
                    Id = filme.Id,
                    HoraConsulta = DateTime.Now
                };*/

                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

                //return Ok(filme);
                return Ok(filmeDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if(filme == null)
            {
                return NotFound();
            }
            /*
            filme.Titulo = filmeDto.Titulo;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;
            filme.Diretor = filmeDto.Diretor;
            */

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
