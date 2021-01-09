using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repository;

        public ProfessorController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/<ProfessorController>
        [HttpGet]
        public IActionResult Get()
        {
            var prof = _repository.GetAllProfessor(true);
            if (prof == null) return BadRequest("Não há nenhum professor");
            return Ok(prof);
        }

        // GET api/<ProfessorController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Favor informe um Id valido.");
            var prof = _repository.GetProfessorById(id, true);
            if(prof == null) return BadRequest("Professor não encontrado");
            return Ok(prof);
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            if (professor == null) return BadRequest("Favor informar um professor");
            _repository.Add(professor);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não cadastrado");
        }

        // PUT api/<ProfessorController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Professor professor)
        {
            if (id == 0) return BadRequest("Favor informe um Id");
            if (professor == null) return BadRequest("Informe um professor.");
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");
            professor.Id = prof.Id;

            _repository.Update(professor);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, [FromBody] Professor professor)
        {
            if (id == 0) return BadRequest("Favor informe um Id");
            if (professor == null) return BadRequest("Informe um professor.");
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");
            professor.Id = prof.Id;

            _repository.Update(professor);
            if (_repository.SaveChanges())
                return Ok(professor);

            return BadRequest("Professor não atualizado");
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Favor informe um Id");
            var prof = _repository.GetProfessorById(id, false);
            if (prof == null) return BadRequest("Professor não encontrado");

            _repository.Delete(prof);
            if (_repository.SaveChanges())
                return Ok("Professor Deletado");

            return BadRequest("Professor não Deletado");
        }
    }
}
