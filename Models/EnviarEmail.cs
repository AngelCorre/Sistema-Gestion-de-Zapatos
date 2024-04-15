using System.Net.Mail;
using System.Net;

namespace GestionZapateria.Models
{
    public class EnviarEmail
    {

        public bool enviaMail(string tipo, int id, string subject)
        {

            string From = "pelonchascorr12@gmail.com"; //de quien procede, puede ser un alias
            string[] To = { "erickadrian255@gmail.com", "angelcorr435@gmail.com", "pelonchascorr12@gmail.com" };  //a quien vamos a enviar el mail
            string Subject = "Tipo: " + tipo + " ,Id: " + id + " ," + subject;  //mensaje
            string Message = "";

            if (tipo == "producto")
            {

                Producto por = new Producto(id);

                Message = "Modelo: " + por.Modelo + ", Talla: " + por.Talla + ", Precio: " + por.Precio;

            } else if(tipo == "categoria")
            {

                Categoria cat = new Categoria(id);

                Message = "Nombre: " + cat.Nombre_Categoria;

            } else if(tipo == "marca")
            {

                Marca mar = new Marca(id);

                Message = "Nombre: " + mar.Nombre_Marca;

            }



            MailMessage Email;
            int lenght = To.Length;
            //una validación básica
            //Se inicia el proceso de envio.
            //comienza-------------------------------------------------------------------------
            try
            {

                for (int i = 0; i <= lenght; i++)
                {
                    if (To[i].Trim().Equals("") || Message.Trim().Equals("") || Subject.Trim().Equals(""))
                    {
                        return false;
                    }
                    //creamos un objeto tipo MailMessage
                    //este objeto recibe el sujeto o persona que envia el mail,
                    //la direccion de procedencia, el asunto y el mensaje
                    Email = new MailMessage(From, To[i], Subject, Message);
                    MailAddress copy = new MailAddress("ecastellanosa1992@gmail.com");
                    Email.Bcc.Add(copy);




                    Email.From = new MailAddress(From); //definimos la direccion de procedencia

                    //aqui creamos un objeto tipo SmtpClient el cual recibe el servidor que utilizaremos como smtp
                    System.Net.Mail.SmtpClient smtpMail = new SmtpClient();

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    smtpMail.EnableSsl = true;//le definimos si es conexión ssl
                    smtpMail.UseDefaultCredentials = false; //le decimos que no utilice la credencial por defecto
                    smtpMail.Host = "smtp.gmail.com"; //agregamos el servidor smtp
                    smtpMail.Port = 587; //le asignamos el puerto
                    smtpMail.Credentials = new System.Net.NetworkCredential("pelonchascorr12@gmail.com", "dalb cfxi fnbk uxxy"); //agregamos nuestro usuario y pass de correo


                    //enviamos el mail
                    smtpMail.Send(Email);

                    //eliminamos el objeto
                    Email.Dispose();
                    smtpMail.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {

                //si ocurre un error regresamos false y el error
                return false;
            }
        }


    }
}
