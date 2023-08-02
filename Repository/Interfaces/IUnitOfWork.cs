using AlunosAPI.Repository.Services;

namespace AlunosAPI.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IAlunoRepository AlunoRepository { get; }
        
        Task Commit();

        void Dispose();
    }
}
