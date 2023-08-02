using AlunosAPI.Context;
using AlunosAPI.Repository.Interfaces;
using AlunosAPI.Repository.Services;

namespace AlunosAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AlunosRepository _alunoRepo;

        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IAlunoRepository AlunoRepository
        {
            get
            {
                return _alunoRepo = _alunoRepo ?? new AlunosRepository(_context);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
