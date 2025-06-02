using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    //    IdCliente
    //Nombres
    //Apellidos
    //Correo
    //Clave
    //Reestablecer
    //FechaRegistro
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Apelllidos { get; set; }
        public int MyProperty { get; set; }

        public string Correo { get; set; }

        public string Reestablecer{ get; set; }

    }
}
