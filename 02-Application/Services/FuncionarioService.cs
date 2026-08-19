using _02_Application.DTOs;
using _02_Application.Interfaces;
using _04_Domain.Entities;
using _04_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Application.Services
{
    public class FuncionarioService : IFuncionarioService 
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<FuncionarioOutputDto> CreateAsync(FuncionarioInputDto dto)
        {
            var funcionario = new Funcionario
            {
                Nome = dto.Nome,
                Cargo = dto.Cargo,
                Salario = dto.Salario,
                Departamento = dto.Departamento
            };

            await _funcionarioRepository.AddAsync(funcionario);

            await _funcionarioRepository.SaveChangesAsync();

            return new FuncionarioOutputDto
            {
                Id = funcionario.Id, 
                Ativo = funcionario.Ativo
            };
        }

        public async Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<FuncionarioOutputDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FuncionarioOutputDto> UpdateAsync(int id, FuncionarioInputDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

