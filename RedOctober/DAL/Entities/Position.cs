using RedOctober.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Entities
{
    /// <summary>
    /// Должность
    /// </summary>
    public class Position : IEntity<int>, INamed
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Id подразделения
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        /// Подразделение
        /// </summary>
        public Department Department { get; set; }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public int? EmployeeId { get; set; }

        /// <summary>
        /// дата трудоустройства
        /// </summary>
        public DateTime DateOfEmployment { get; set; }
    }
}
