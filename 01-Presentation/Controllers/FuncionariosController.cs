using _02_Application.DTOs;      
using _02_Application.Interfaces;
using _02_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace _01_Presentation.Controllers
{
    [ApiController]
    [Route("api/funcionarios")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        /// <summary>
        /// Obtém a lista completa de funcionários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<FuncionarioOutputDto>))]
        public async Task<IActionResult> GetAll()
        {
            var funcionarios = await _funcionarioService.GetAllAsync();
            return Ok(funcionarios);
        }

        /// <summary>
        /// Obtém um funcionário específico pelo seu ID.
        /// </summary>
        /// <param name="id">ID do funcionário</param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FuncionarioOutputDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var funcionario = await _funcionarioService.GetByIdAsync(id);
                return Ok(funcionario);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Cadastra um novo funcionário.
        /// </summary>
        /// <param name="dto">Dados do novo funcionário</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(FuncionarioOutputDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] FuncionarioInputDto dto)
        {
            var resultado = await _funcionarioService.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created, resultado);
        }

        /// <summary>
        /// Atualiza os dados de um funcionário existente.
        /// </summary>
        /// <param name="id">ID do funcionário a ser atualizado</param>
        /// <param name="dto">Novos dados do funcionário</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] FuncionarioInputDto dto)
        {
            try
            {
                await _funcionarioService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        /// <summary>
        /// Remove um funcionário pelo seu ID.
        /// </summary>
        /// <param name="id">ID do funcionário a ser removido</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _funcionarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }
    }
}