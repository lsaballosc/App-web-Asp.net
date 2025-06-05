using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Net.Mail; // Importar para enviar correos electrónicos
using System.Net; // Importar para usar NetworkCredential y SmtpClient
using System.IO; // Importar para manejar archivos y directorios

namespace CapaNegocio
{
    public class CN_Recursos
    {

        //metodo para generar clave aleatoria
        public static string GenerarClave()
        {
            //un GUID es un identificador único global que se puede usar para generar una clave aleatoria
            string clave = Guid.NewGuid().ToString("N").Substring(0, 6); // Generar un GUID y tomar los primeros 8 caracteres
            return clave; // Retornar la clave generada
        }





        //Metodo para encriptar la clave con SHA256
        public static string EncriptarSHA256(string texto)
        {
            StringBuilder sb = new StringBuilder(); // Usar StringBuilder para construir el hash en formato hexadecimal
            // usar la referencia System.Security.Cryptography para usar SHA256
            using (SHA256 hash = SHA256Managed.Create())// Crear una instancia de SHA256Managed para calcular el hash
            {
                Encoding enc = Encoding.UTF8; // Usar UTF8 para codificar el texto a bytes
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));// Calcular el hash del texto convertido a bytes

                foreach (byte b in result) // Recorrer cada byte del resultado del hash
                {
                    sb.Append(b.ToString("x2")); // Convertir cada byte a hexadecimal y agregarlo al StringBuilder
                }
            }

            return sb.ToString(); // Retornar el hash en formato hexadecimal
        }


        public static bool EnviarCorreo(string correo, string asunto, string mensaje)
        {

            bool resultado = false; // Variable para almacenar el resultado del envío de correo

            try
            {
                MailMessage mail = new MailMessage(); // Crear una instancia de MailMessage para configurar el correo
                mail.To.Add(correo); // Agregar el destinatario al correo
                mail.From = new MailAddress("jzelacrochet@gmail.com");
                mail.Subject = asunto; // Establecer el asunto del correo
                mail.Body = mensaje; // Establecer el cuerpo del correo
                mail.IsBodyHtml = true; // Indicar que el cuerpo del correo es HTML

                var servidorSmtp = new SmtpClient("smtp.gmail.com", 587)
                //si da error probar poner la contraseña pegada sin espacios ni comillas
                {
                    Credentials = new NetworkCredential("jzelacrochet@gmail.com", "mshigfbajjrvtpwr"), // configurar las credenciales del servidor SMTP de Gmail
                    // Configurar las credenciales del servidor SMTP de Gmail
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true // Habilitar SSL para el envío seguro de correos

                };// Configurar el servidor SMTP de Gmail, se encarga // de enviar el correo

                servidorSmtp.Send(mail); // Enviar el correo utilizando el servidor SMTP configurado
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false; // Si ocurre una excepción, establecer el resultado como false
            }

            return resultado; // Retornar el resultado del envío de correo
        }




        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty; // Inicializar la variable para almacenar el texto en Base64
            conversion = true; // Inicializar la variable de conversión como verdadera


            try
            {
                byte[] bytes = File.ReadAllBytes(ruta); // Leer todos los bytes del archivo en la ruta especificada
                textoBase64 = Convert.ToBase64String(bytes); // Convertir los bytes a una cadena en Base64
            }
            catch (Exception ex)
            {
                conversion = false; // Si ocurre una excepción, establecer la conversión como falsa
            }
            return textoBase64; // Retornar la cadena en Base64
        }
    }
}