using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
//    IdProducto
//Nombre
//Descripción
//IdMarca
//IdCategoria
//Precio
//Stock
//RutaImagen
//NombreImagen
//Activo
//Fecha Registro
    public class Producto
    {

        public int IdProducto { get; set; }
        public  String Nombre { get; set; }
        public Marca oMarca{ get; set; }

        public Categoria oCategoria { get; set; }

        public decimal Precio { get; set; }

        public int Stock { get; set; }
        public String RutaImagen { get; set; }

        public String NombreImagen { get; set; }

        public bool Activo { get; set; }




    }
}
