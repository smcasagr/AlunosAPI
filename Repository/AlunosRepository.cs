using AlunosAPI.Context;
using AlunosAPI.Models;
using AlunosAPI.Repository.Services;

namespace AlunosAPI.Repository
{
    public class AlunosRepository : IAlunoRepository, Repository<Aluno>
    {
        public AlunosRepository(AppDbContext context) : base(context) { }

        public Task<IEnumerable<Aluno>> GetAlunos()
        {
            throw new NotImplementedException();
        }

        public Task<Aluno> GetAlunoById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Aluno>> GetAlunosByNome(string nome)
        {
            throw new NotImplementedException();
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
