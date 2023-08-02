using AlunosAPI.Context;
using AlunosAPI.Models;
using AlunosAPI.Pagination;
using AlunosAPI.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Repository
{
    public class AlunosRepository : IAlunoRepository, Repository<Aluno>
    {
        public AlunosRepository(AppDbContext context) : base(context) { }

        public async Task<PagedList<Aluno>> GetAlunos(AlunosParameter alunosParameter)
        {
            return await PagedList<Aluno>.ToPagedList(
                GetAll().OrderBy(on => on.Id), alunosParameter.PageNumber);
        }

        public async Task<Aluno> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var aluno = await GetAll().Where(aluno => aluno.Nome.Contains(nome)).ToListAsync();
                return aluno;
            }
            else
            {
                var aluno = await GetAll().ToListAsync();
                return aluno;
            }
        }

        public Task CreateAluno(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAluno(Aluno aluno)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAluno(Aluno aluno)
        {
            throw new NotImplementedException();
        }
    }
}
