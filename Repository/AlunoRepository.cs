using AlunosAPI.Context;
using AlunosAPI.Models;
using AlunosAPI.Pagination;
using AlunosAPI.Repository.Services;
using Microsoft.EntityFrameworkCore;

namespace AlunosAPI.Repository
{
    public class AlunoRepository : Repository<Aluno>, IAlunoRepository
    {
        public AlunoRepository(AppDbContext context) : base(context) { }

        public async Task<PagedList<Aluno>> GetAlunos(AlunosParameter alunosParameter)
        {
            return await PagedList<Aluno>.ToPagedList(
                Get().OrderBy(on => on.Id), alunosParameter.PageNumber);
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            if (!string.IsNullOrWhiteSpace(nome))
            {
                var aluno = await Get().Where(aluno => aluno.Nome.Contains(nome)).ToListAsync();
                return aluno;
            }
            else
            {
                var aluno = await Get().ToListAsync();
                return aluno;
            }
        }
    }
}
