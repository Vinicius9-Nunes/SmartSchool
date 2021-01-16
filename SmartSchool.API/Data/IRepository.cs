using SmartSchool.API.Helpers;
using SmartSchool.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.API.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        //Métodos de Aluno
        Task<PageList<Aluno>> GetAllAlunos(PageParams pageParams, bool includeProfessor = false);
        Aluno[] GetAllAlunosByDisciplina(int id, bool includeProfessor = false);
        Aluno GetAlunoById(int id, bool includeProfessor = false);


        //Métodos de Professo
        Professor[] GetAllProfessor(bool includeAlunos = false);
        Professor[] GetAllProfessorByDisciplina(int id, bool includeAlunos = false);
        Professor GetProfessorById(int id, bool includeAlunos = false);




    }
}
