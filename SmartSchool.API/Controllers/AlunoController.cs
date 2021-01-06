using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {

        private readonly DataContext _context;

        public AlunoController(DataContext context)
        {
            _context = context;
        }


        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Alunos);
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var aluno = _context.Alunos.FirstOrDefault(p => p.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(aluno);
        }
        [HttpGet("ByName")]
        public IActionResult GetByName(string nome, string sobrenome)
        {
            if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(sobrenome))
            {
                var filtro = _context.Alunos.FirstOrDefault(p => p.Nome == nome && p.Sobrenome == sobrenome);
                if (filtro == null) return BadRequest("Aluno não localizado com os parametros informados!");
                return Ok(filtro);
            }
            else if (string.IsNullOrEmpty(nome)) return BadRequest("Informe um nome");
            else
            {
                var filtro = _context.Alunos.FirstOrDefault(p => p.Nome == nome);
                if (filtro == null) return BadRequest("Ocorreu um erro");
                return Ok(filtro);
            }
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            if (aluno == null) return BadRequest("Favor informar um aluno");
            _context.Add(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            if (aluno == null) return BadRequest("Favor informar um aluno");
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");
            if (alu.Id != id) return BadRequest("Id informado não corresponde ao Id encontrado no Banco");
            if (aluno.Id == 0)
                aluno.Id = alu.Id;
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            if (aluno == null) return BadRequest("Favor informar um aluno");
            var alu = _context.Alunos.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (alu == null) return BadRequest("Aluno não encontrado");
            if (alu.Id != id) return BadRequest("Id informado não corresponde ao Id encontrado no Banco");
            if (aluno.Id == 0)
                aluno.Id = alu.Id;
            _context.Update(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            var aluno = _context.Alunos.FirstOrDefault(p => p.Id == id);
            if (aluno == null) return BadRequest("Aluno não encontrado.");
            _context.Remove(aluno);
            _context.SaveChanges();
            return Ok(aluno);
        }
    }
}
