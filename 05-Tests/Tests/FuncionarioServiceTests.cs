using _02_Application.DTOs;
using _02_Application.Services;
using _03_Infrastructure.Data;
using _03_Infrastructure.Repositories;
using _04_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace _05_Tests.Tests
{
    public class FuncionarioServiceTests
    {
        private AppDbContext CriarContextoDeTeste()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task GetAllAsync_DeveRetornarFuncionariosCadastrados()
        {
            using var context = CriarContextoDeTeste();

            context.Funcionarios.AddRange(
                new Funcionario { Id = 1, Nome = "João Silva", Cargo = "Desenvolvedor", Salario = 5000, Departamento = "TI", Ativo = true },
                new Funcionario { Id = 2, Nome = "Maria Souza", Cargo = "Analista", Salario = 6000, Departamento = "RH", Ativo = true }
            );
            await context.SaveChangesAsync();

            var repository = new FuncionarioRepository(context);
            var service = new FuncionarioService(repository);

            // Act 
            var resultado = await service.GetAllAsync();

            // Assert 
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task GetByIdAsync_IdInexistente_DeveLancarKeyNotFoundException()
        {
            // Arrange
            using var context = CriarContextoDeTeste();
            var repository = new FuncionarioRepository(context);
            var service = new FuncionarioService(repository);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetByIdAsync(999));
        }

        [Fact]
        public async Task CreateAsync_DeveSalvarERetornarFuncionario()
        {
            // Arrange
            using var context = CriarContextoDeTeste();
            var repository = new FuncionarioRepository(context);
            var service = new FuncionarioService(repository);

            var inputDto = new FuncionarioInputDto
            {
                Nome = "Carlos Andrade",
                Cargo = "Gerente de Projetos",
                Salario = 8500,
                Departamento = "Gestão"
            };

            // Act
            var resultado = await service.CreateAsync(inputDto);

            // Assert
            Assert.NotNull(resultado);
            Assert.True(resultado.Id > 0, "O ID retornado deve ser maior que 0.");
            Assert.Equal(inputDto.Nome, resultado.Nome);
        }
    }
}
