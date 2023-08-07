using AlunosAPI.DTOs;
using AlunosAPI.Models;
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
            if (alunos is null)
                return NotFound("Nenhum aluno cadastrado!");

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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AlunoDTO alunoDTO)
        {
            var aluno = _mapper.Map<Aluno>(alunoDTO);

            if (aluno == null)
                return BadRequest("Erro ao tentar salvar o aluno!");

            _uof.AlunoRepository.Add(aluno);
            await _uof.Commit();

            var _alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            // informa o aluno salvo no header
            // Aciona a rota informada, com o ID informado
            return new CreatedAtRouteResult("ObterAluno",
                new { id = aluno.Id }, _alunoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, AlunoDTO alunoDTO)
        {
            if (id != alunoDTO.Id)
                return BadRequest();

            var aluno = _mapper.Map<Aluno>(alunoDTO);

            _uof.AlunoRepository.Update(aluno);
            await _uof.Commit();

            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<AlunoDTO>> Delete(int id)
        {
            var aluno = await _uof.AlunoRepository.GetById(p => p.Id == id);
            if (aluno is null)
                return NotFound($"Aluno ID {id} não localizado!");

            _uof.AlunoRepository.Delete(aluno);
            await _uof.Commit();

            var alunoDTO = _mapper.Map<AlunoDTO>(aluno);

            return Ok(alunoDTO);
        }

    }
}
