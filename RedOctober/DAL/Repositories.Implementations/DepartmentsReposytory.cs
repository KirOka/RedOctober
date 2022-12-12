using Microsoft.EntityFrameworkCore;
using RedOctober.DAL.Entities;
using RedOctober.DAL.EntityFramework;
using RedOctober.DAL.Implementation;
using RedOctober.DAL.Repositories.Abstractions;

namespace RedOctober.DAL.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий работы с подразделениями
    /// </summary>
    public class DepartmentsReposytory : Repository<Department, int>, IDepartmentsReposytory
    {
        public DepartmentsReposytory(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id">ID удалённой сущности</param>
        /// <returns>была ли сущность удалена</returns>
        public override bool Delete(int id)
        {
            var obj = EntitySet.Find(id);
            if (obj == null)
            {
                return false;
            }
            List<Department> departments = this.GetAffiliateDepartmentsAsync(obj.Id).Result;
            foreach (Department department in departments)
            {
                this.Delete(department.Id);
            }
            EntitySet.Remove(obj);
            Context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">сущность для удаления</param>
        /// <returns>была ли сущность удалена</returns>
        public override bool Delete(Department entity)
        {
            if (entity == null)
            {
                return false;
            }
            Context.Entry(entity).State = EntityState.Deleted;
            Context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Каскадное удаление подразделений
        /// </summary>
        /// <param name="entity">Удаляемое подразделение</param>
        /// <returns>была ли сущность удалена</returns>
        public virtual async Task<bool> DeleteDepartmentsCascadeAsync(Department entity)
        {
            if (entity == null)
            {
                return false;
            }
            List<Department> departments = await this.GetAffiliateDepartmentsAsync(entity.Id);
            foreach (Department department in departments)
            {
                if (!await this.DeleteDepartmentsCascadeAsync(department))
                {
                    return false;
                }
            }
            this.Delete(entity);
            return true;
        }

        /// <summary>
        /// Запросить все дочерние подразделения
        /// </summary>
        /// <param name="id">ID подразделения</param>
        /// <returns>сущность</returns>
        public virtual async Task<List<Department>> GetAffiliateDepartmentsAsync(int? id)
        {
            return await EntitySet.Where(p => p.ParentDepartment == id).ToListAsync();
        }
    }
}
