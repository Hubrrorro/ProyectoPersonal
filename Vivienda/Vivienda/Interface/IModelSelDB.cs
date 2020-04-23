using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vivienda.Interface
{
    public interface IModelSelDB
    {
        int Id { get; set; }
        string CtrEditar { get; set; }
        string CtrEstatus { get; set; }
        int Activo { get; set; }

    }
}
