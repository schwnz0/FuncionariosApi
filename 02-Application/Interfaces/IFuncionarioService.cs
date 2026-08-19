using _02_Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_Application.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<FuncionarioOutputDto>> GetAllAsync();
        Task<FuncionarioOutputDto?> GetByIdAsync(int id);
        Task<FuncionarioOutputDto> CreateAsync(FuncionarioInputDto dto);
        Task UpdateAsync(int id, FuncionarioInputDto dto);
        Task DeleteAsync(int id);
    }
}
