using RedOctober.DAL.Abstraction;
using RedOctober.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Repositories.Abstractions
{
    public interface IDepartmentsReposytory : IRepository<Department, int>
    {
        /// <summary>
        /// Получить дочерние подразделения
        /// </summary>
        /// <param name="id">ID родительского подразделения</param>
        /// <returns>список дочерних подразделений</returns>
        Task<List<Department>> GetAffiliateDepartmentsAsync(int? id);

        /// <summary>
        /// Каскадное удаление подразделений
        /// </summary>
        /// <param name="entity">Удаляемое подразделение</param>
        /// <returns>была ли сущность удалена</returns>
        Task<bool> DeleteDepartmentsCascadeAsync(Department entity);
    }
}
