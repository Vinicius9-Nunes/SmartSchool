using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.API.Data;
using SmartSchool.API.DTOs;
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
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public AlunoController(IRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/<AlunoController>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repository.GetAllAlunos(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDTO>>(result));
        }

        // GET api/<AlunoController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0) return BadRequest("Informe um Id");
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            return Ok(_mapper.Map<AlunoDTO>(aluno));
        }

        // POST api/<AlunoController>
        [HttpPost]
        public IActionResult Post(Aluno aluno)
        {
            if (aluno == null) return BadRequest("Favor informar um aluno");
            _repository.Add(aluno);
            if(_repository.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}",_mapper.Map<AlunoDTO>(aluno));
            else
                return BadRequest("Aluno não cadastrado");
        }

        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, Aluno aluno)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            if (aluno == null) return BadRequest("Favor informar um aluno");
            var alu = _repository.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");
            if (alu.Id != id) return BadRequest("Id informado não corresponde ao Id encontrado no Banco");
            if (aluno.Id == 0)
                aluno.Id = alu.Id;

            _repository.Update(aluno);
            if (_repository.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Aluno aluno)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            if (aluno == null) return BadRequest("Favor informar um aluno");
            var alu = _repository.GetAlunoById(id, false);
            if (alu == null) return BadRequest("Aluno não encontrado");
            if (alu.Id != id) return BadRequest("Id informado não corresponde ao Id encontrado no Banco");
            if (aluno.Id == 0)
                aluno.Id = alu.Id;

            _repository.Update(aluno);
            if (_repository.SaveChanges())
                return Created($"/api/aluno/{aluno.Id}", _mapper.Map<AlunoDTO>(aluno));

            return BadRequest("Aluno não atualizado");
        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest("Favor informar o Id do aluno");
            var aluno = _repository.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado.");

            _repository.Delete(aluno);
            if (_repository.SaveChanges())
                return Ok("Aluno Deletado");

            return BadRequest("Aluno não Deletado");
        }
    }
}
