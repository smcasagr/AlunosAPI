using AlunosAPI.Context;
using AlunosAPI.Models;
using AlunosAPI.Pagination;
using AlunosAPI.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Repository
{
    public class AlunosRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunosRepository(AppDbContext context) : base(context) { }

        public async Task<PagedList<Aluno>> GetAlunos(AlunosParameter alunosParameter)
        {
            return await PagedList<Aluno>.ToPagedList(
                GetAll().OrderBy(on => on.Id), alunosParameter.PageNumber);
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
    }
}
