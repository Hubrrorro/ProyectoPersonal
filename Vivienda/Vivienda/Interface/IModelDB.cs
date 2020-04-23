using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivienda.Interface
{
    public interface IModelDB
    {
        int Id {get;set;}
        bool Activo { get; set; }
    }
}
