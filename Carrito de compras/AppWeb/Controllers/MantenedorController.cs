using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace AppWeb.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor

        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Marca()
        {
            return View();
        }
        public ActionResult Producto()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarCategorias()
        {
            // Creo un objeto de la capa de Entidad para acceder a los metodos de la capa de negocio
            List<Categoria> olista = new List<Categoria>();
            olista = new CN_Categoria().Listar();

            // Retorno un JsonResult con la lista de usuarios
            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public JsonResult GuardarCategoria(Categoria obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.idCategoria == 0)
            {
                // Si el IdCategoria es 0, significa que es un nuevo usuario
                resultado = new CN_Categoria().Registrar(obj, out mensaje);
            }
            else
            {
                // Si el IdUsuario es mayor a 0, significa que es un usuario existente
                resultado = new CN_Categoria ().Editar(obj, out mensaje);
            }

            // Retorno un JsonResult con el resultado de la operacion
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            // hago 2 variables para el resultado y el mensaje
            string mensaje = string.Empty;

            bool respuesta = false;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);
            // Retorno un JsonResult con el resultado de la operacion
            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}