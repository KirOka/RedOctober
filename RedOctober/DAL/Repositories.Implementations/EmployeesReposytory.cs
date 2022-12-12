using Microsoft.EntityFrameworkCore;
using RedOctober.DAL.Abstraction;
using RedOctober.DAL.Entities;
using RedOctober.DAL.EntityFramework;
using RedOctober.DAL.Implementation;
using RedOctober.DAL.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RedOctober.DAL.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с сотрудниками
    /// </summary>
    public class EmployeesReposytory : Repository<Employee, int>, IEmployeesReposytory
    {
        public EmployeesReposytory(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Получение всех значений с фильтром
        /// </summary>
        /// <param name="filter">условия фильтрации</param>
        /// <returns>Отфильтрованный список сотрудников</returns>
        public async virtual Task<List<Employee>> GetFilteredEmployeesAsync(Expression<Func<Employee, bool>>? filter)
        {
            IQueryable<Employee> query = this.EntitySet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync();
        }
    }
}
