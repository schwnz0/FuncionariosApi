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

        public async Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            return funcionarios
                .Select(f => new FuncionarioOutputDto
                {
                    Id = f.Id,
                    Ativo = f.Ativo
                })
                .ToList();
        }

        public async Task<FuncionarioOutputDto?> GetByIdAsync(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
                return null;

            return new FuncionarioOutputDto
            {
                Id = funcionario.Id,
                Ativo = funcionario.Ativo
            };
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

        public async Task UpdateAsync(int id, FuncionarioInputDto dto)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
                throw new KeyNotFoundException($"Funcionário com id {id} não encontrado.");

            funcionario.Nome = dto.Nome;
            funcionario.Cargo = dto.Cargo;
            funcionario.Salario = dto.Salario;
            funcionario.Departamento = dto.Departamento;

            _funcionarioRepository.Update(funcionario);
            await _funcionarioRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
                throw new KeyNotFoundException($"Funcionário com id {id} não encontrado.");

            _funcionarioRepository.Delete(funcionario);
            await _funcionarioRepository.SaveChangesAsync();
        }
    }
}

