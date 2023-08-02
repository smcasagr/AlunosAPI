using AlunosAPI.Models;
using AlunosAPI.Pagination;
using AlunosAPI.Repository.Interfaces;

namespace AlunosAPI.Repository.Services
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Task<PagedList<Aluno>> GetAlunos(AlunosParameter alunosParameter);
        Task<IEnumerable<Aluno>> GetAlunoById(int id);
        Task<IEnumerable<Aluno>> GetAlunosByNome(string nome);
        Task CreateAluno(Aluno aluno);
        Task UpdateAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
    }
}
