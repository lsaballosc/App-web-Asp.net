using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace AppWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        // retorno un JsonResult para que pueda ser consumido por el cliente
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            // Creo un objeto de la capa de Entidad para acceder a los metodos de la capa de negocio
            List<Usuario> olista = new List<Usuario>();
            olista = new CN_Usuarios().Listar();

            // Retorno un JsonResult con la lista de usuarios
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]

        public JsonResult GuardarUsuario(Usuario obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.IdUsuario == 0)
            {
                // Si el IdUsuario es 0, significa que es un nuevo usuario
                resultado = new CN_Usuarios().Registrar(obj, out mensaje);
            }
            else
            {
                // Si el IdUsuario es mayor a 0, significa que es un usuario existente
                resultado = new CN_Usuarios().Editar(obj, out mensaje);
            }

            // Retorno un JsonResult con el resultado de la operacion
            return Json(new { resultado = resultado, mensaje= mensaje }, JsonRequestBehavior.AllowGet);

        }






        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            // hago 2 variables para el resultado y el mensaje
            string mensaje = string.Empty;
   
            bool respuesta = false;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);
            // Retorno un JsonResult con el resultado de la operacion
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

    }
}