using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;
namespace CapaDatos
{
    public class CD_Dashboard
    {
        public Dashboard VerDashboard()
        {
            Dashboard objeto = new Dashboard();


            try
            {
                //La cadena de conexión se obtiene desde la clase Conection, en la propiedad cn que obtiene la conexión
                using (SqlConnection oconexion = new SqlConnection(Conection.cn)) // Uso la conexión definida en la clase Conection

                {
                    //hago un Query
                  


                    // hago un comando SQL con la conexión y el query
                    SqlCommand cmd = new SqlCommand("sp_ReporteDashboard", oconexion);
                    //le digo que tipo de comando es, en este caso es tipo texto
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Abro la conexión
                    oconexion.Open();
                    // Aqui leemos todos los resultados de la ejecucion del Query
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //Mientras este leyendo, almacena en la lista que hice
                        while (reader.Read())
                        {
                            objeto = new Dashboard()
                            {
                                totalCliente = Convert.ToInt32(reader["TotalCliente"]),
                                totalVenta = Convert.ToInt32(reader["TotalVenta"]),
                                totalProducto = Convert.ToInt32(reader["TotalProducto"])
                               
                            };

                        }
                    }
                }
            }
            // Si ocurre un error, lo capturo y no hago nada, solo retorno la lista vacía
            catch
            {
                objeto = new Dashboard();

            }
            return objeto;
        }

    }
}
