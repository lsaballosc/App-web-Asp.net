using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{
    public class CN_Usuarios
    {
        // creo objeto de la capa de datos para poder acceder a los metodos de la capa de datos
        private CD_Usuarios  objCdUsuario = new CD_Usuarios();



        // metodo para listar los usuarios
        public List<Usuario> Listar()
        {
            //llamo al metodo de la capa de datos que me devuelve una lista de usuarios
            return objCdUsuario.Listar();
        }

        public int Registrar(Usuario obj, out string Mensaje)
        {
            //llamo al metodo de la capa de datos que me devuelve el id generado y un mensaje de error si ocurre

            Mensaje = string.Empty; // Inicializo el mensaje como vacío
            // Validar campos obligatorios
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres ))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
               
            }else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
                
                }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
               
            }
            //si el mensaje esta vacio, significa que no hay errores en los campos obligatorios
            if (string.IsNullOrEmpty(Mensaje))
            {

                //Voy a mandar la clave por correo más adelante
                string clave = "test123";
                // encripto la clave con sha256
                obj.Clave = CN_Recursos.EncriptarSHA256(clave);// Encripto la clave con SHA256 antes de enviarla a la base de datos
                return objCdUsuario.Registrar(obj, out Mensaje); // Llamo al método de la capa de datos para registrar el usuario y obtengo el mensaje de error si ocurre
            }
            else {
                return 0; }// Retorno 0 si hay un mensaje de error, indicando que no se pudo registrar
                                             // }
        }


        // Metodo para actualizar un usuario
        public bool Editar(Usuario obj, out string Mensaje)
        {
            Mensaje = string.Empty; // Inicializo el mensaje como vacío
            // Validar campos obligatorios
            if (string.IsNullOrEmpty(obj.Nombres) || string.IsNullOrWhiteSpace(obj.Nombres))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
                return false;
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
                return false;
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "Los campos Nombres, Apellidos, Correo y Clave son obligatorios.";
                return false;
            }
            //si el mensaje esta vacio, significa que no hay errores en los campos obligatorios
            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCdUsuario.Editar(obj, out Mensaje); // Llamo al método de la capa de datos para editar el usuario y obtengo el mensaje de error si ocurre
            }
            else
            {
                return false; // Retorno false si hay un mensaje de error, indicando que no se pudo editar
            }
        }



        // Metodo para eliminar un usuario
        public bool Eliminar(int idUsuario, out string Mensaje)
        {
            return objCdUsuario.Eliminar(idUsuario, out Mensaje); // Llamo al método de la capa de datos para eliminar el usuario y obtengo el mensaje de error si ocurre
        }

    }
}
