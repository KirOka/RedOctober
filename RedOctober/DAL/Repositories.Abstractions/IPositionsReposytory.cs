using RedOctober.DAL.Abstraction;
using RedOctober.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Repositories.Abstractions
{
    public interface IPositionsReposytory : IRepository<Position, int>
    {
        /// <summary>
        /// Получить должности в подразделении
        /// </summary>
        /// <param name="id">ID подразделения</param>
        /// <returns>список должностей подразделения</returns>
        Task<List<Position>> GetPositionsOfDepartmentAsync(int id);
    }
}
