using RedOctober.DAL.Abstraction;
using RedOctober.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Repositories.Abstractions
{
    public interface IEmployeesReposytory : IRepository<Employee, int>
    {
        /// <summary>
        /// Получение всех значений с фильтром
        /// </summary>
        /// <param name="filter">условия фильтрации</param>
        /// <returns>Отфильтрованный список сотрудников</returns>
        Task<List<Employee>> GetFilteredEmployeesAsync(Expression<Func<Employee, bool>>? filter);
    }
}
