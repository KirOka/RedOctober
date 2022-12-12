using RedOctober.DAL.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Entities
{
    /// <summary>
    /// Подразделение
    /// </summary>
    public class Department : IEntity<int>, INamed
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
        /// Вышестоящее подразделение
        /// </summary>
        public int? ParentDepartment { get; set; }

        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
