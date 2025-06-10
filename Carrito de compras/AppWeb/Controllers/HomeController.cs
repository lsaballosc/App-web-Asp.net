using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using System.Data;
using ClosedXML.Excel;
using System.IO;

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
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

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








        [HttpGet]
        public JsonResult ListaReporte(string fechainicio, string fechafin, string idtransaccion)
        {

            List<Reporte> olista = new List<Reporte>();



            // Retorno un JsonResult con la vista del dashboard
            olista = new CN_DashBoard().Ventas(fechainicio, fechafin, idtransaccion);



            return Json(new { data = olista }, JsonRequestBehavior.AllowGet);
        }





        [HttpGet]
        public JsonResult VistaDashboard()
        {
            // Retorno un JsonResult con la vista del dashboard
            CN_DashBoard oDashBoard = new CN_DashBoard();

            Dashboard result = oDashBoard.VerDashboard();

            return Json(new { resultado = result }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public FileResult ExportarVenta(string fechainicio, string fechafin, string idtransaccion)
        {
            List<Reporte> olista = new List<Reporte>();

            olista = new CN_DashBoard().Ventas(fechainicio, fechafin, idtransaccion);
            // Crear un DataTable para almacenar los datos del reporte
            DataTable dt = new DataTable();

            dt.Locale = new System.Globalization.CultureInfo("es-CR");

            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Cliente", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Precio", typeof(decimal));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Total", typeof(decimal));
            dt.Columns.Add("Id Transacción", typeof(string));

            foreach(Reporte rp in olista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FechaVenta,
                    rp.Cliente,
                    rp.Producto,
                    rp.Precio,
                    rp.Cantidad,
                    rp.Total,
                    rp.IdTransaccion
                });
            }

            dt.TableName = "Reporte Ventas";


            // Exportar a Excel
            using(XLWorkbook wb = new XLWorkbook())
            {
                // Agregar el DataTable al libro de trabajo
                wb.Worksheets.Add(dt);
                // Configurar el estilo de la hoja de trabajo
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    byte[] fileBytes = stream.ToArray();
                    string fileName = $"Reporte_Ventas_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.xlsx";
                    return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                }
            }
        }

    }
}