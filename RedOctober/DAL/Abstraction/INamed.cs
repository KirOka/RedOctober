using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedOctober.DAL.Abstraction
{
    /// <summary>
    /// Интерфейс сущьности с именем
    /// </summary>
    public interface INamed
    {
        /// <summary>
        /// Имя сущьности
        /// </summary>
        string? Name { get; set; }
    }
}
