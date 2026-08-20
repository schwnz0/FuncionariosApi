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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuncionarioInputDto dto)
        {
            var resultado = await _funcionarioService.CreateAsync(dto);

            return StatusCode(201, resultado);
        }
    }
}