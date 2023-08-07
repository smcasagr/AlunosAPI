using AlunosAPI.DTOs;
using AlunosAPI.Pagination;
using AlunosAPI.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AlunosAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        
        public AlunosController(IUnitOfWork context, IMapper mapper)
        {
            _uof = context;
            _mapper = mapper;
        }

        // /alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoDTO>>> Get([FromQuery] AlunosParameter alunosParameter)
        {
            var alunos = await _uof.AlunoRepository.GetAlunos(alunosParameter);

            // Dados paginação
            var metadata = new
            {
                alunos.TotalCount,
                alunos.PageSize,
                alunos.CurrentPage,
                alunos.TotalPages,
                alunos.HasNext,
                alunos.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var alunosDTO = _mapper.Map<List<AlunoDTO>>(alunos);
            return alunosDTO;
        }

        [HttpGet("{id}", Name = "ObterAluno")]
        public async Task<ActionResult<AlunoDTO>> Get(int id)
        {
            var aluno = await _uof.AlunoRepository.GetById(p => p.Id == id);

            if (aluno == null)
                return NotFound($"Aluno ID: {id} não encontrado!");

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);
            return alunoDTO;
        }
    }
}
