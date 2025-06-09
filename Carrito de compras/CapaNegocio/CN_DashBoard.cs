using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_DashBoard
    {

        private CD_Dashboard objCdDash = new CD_Dashboard();



        // metodo para listar los usuarios
        public Dashboard VerDashboard()
        {
            //llamo al metodo de la capa de datos que me devuelve una lista de usuarios
            return objCdDash.VerDashboard();
        }


    }
}
