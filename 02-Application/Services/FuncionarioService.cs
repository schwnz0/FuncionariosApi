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
            private readonly IFuncionarioRepository _repository;

            public FuncionarioService(IFuncionarioRepository repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync()
            {
                var funcionarios = await _repository.GetAllAsync();
                return funcionarios.Select(f => new FuncionarioOutputDto
                {
                    Id = f.Id,
                    Nome = f.Nome,
                    Cargo = f.Cargo,
                    Salario = f.Salario,
                    Departamento = f.Departamento,
                    Ativo = f.Ativo
                });
            }

            public async Task<FuncionarioOutputDto?> GetByIdAsync(int id)
            {
                var funcionario = await _repository.GetByIdAsync(id);
                if (funcionario == null)
                    throw new KeyNotFoundException($"Funcionário com ID {id} não encontrado.");

                return new FuncionarioOutputDto
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Cargo = funcionario.Cargo,
                    Salario = funcionario.Salario,
                    Departamento = funcionario.Departamento,
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
                    Departamento = dto.Departamento,
                    Ativo = dto.Ativo
                };

                await _repository.AddAsync(funcionario);
                await _repository.SaveChangesAsync();

                return new FuncionarioOutputDto
                {
                    Id = funcionario.Id,
                    Nome = funcionario.Nome,
                    Cargo = funcionario.Cargo,
                    Salario = funcionario.Salario,
                    Departamento = funcionario.Departamento,
                    Ativo = funcionario.Ativo
                };
            }

            public async Task UpdateAsync(int id, FuncionarioInputDto dto)
            {
                var funcionario = await _repository.GetByIdAsync(id);
                if (funcionario == null)
                    throw new KeyNotFoundException($"Funcionário com ID {id} não encontrado.");

                funcionario.Nome = dto.Nome;
                funcionario.Cargo = dto.Cargo;
                funcionario.Salario = dto.Salario;
                funcionario.Departamento = dto.Departamento;
                funcionario.Ativo = dto.Ativo;

                _repository.Update(funcionario);
                await _repository.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var funcionario = await _repository.GetByIdAsync(id);
                if (funcionario == null)
                    throw new KeyNotFoundException($"Funcionário com ID {id} não encontrado.");

                _repository.Delete(funcionario);
                await _repository.SaveChangesAsync();
            }
      }
    
}
