using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace dz1chernovik.DataBase
{
    public interface IEmployeeRepository
    {
        Task<List<Sotrudnik>> GetAllAsync();
        Task<Sotrudnik?> GetByIdAsync(int id);
        Task AddSotrudnikAsync(Sotrudnik employee);
        Task UpdateSotrudnikAsync(Sotrudnik employee);
        Task DeleteSotrudnikAsync(int id);
    }
}
