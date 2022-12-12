using Microsoft.EntityFrameworkCore;
using RedOctober.DAL.Entities;
using RedOctober.DAL.EntityFramework;
using RedOctober.DAL.Implementation;
using RedOctober.DAL.Repositories.Abstractions;

namespace RedOctober.DAL.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с должностями
    /// </summary>
    public class PositionsReposytory : Repository<Position, int>, IPositionsReposytory
    {
        public PositionsReposytory(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Запрос всех должностей в подразделении
        /// </summary>
        /// <param name="id">Id подразделения</param>
        /// <returns>сущность</returns>
        public virtual async Task<List<Position>> GetPositionsOfDepartmentAsync(int id)
        {
            return await EntitySet.Where(p => p.DepartmentId == id).ToListAsync();
        }
    }
}
