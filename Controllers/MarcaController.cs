using GestionZapateria.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionZapateria.Controllers
{
    public class MarcaController : Controller
    {
        public ActionResult Index(int Id_Marca)
        {
            Marca mar = new(Id_Marca);
            return View(mar);
        }


        [HttpPost]
        public ActionResult Index(Marca mar, string accion)
        {
            switch (accion)
            {

                case "Buscar":

                    mar.GetMarcaByNombre(mar.Nombre_Marca); //Cambiar
                    ViewData["listMarca"] = mar.GetMarcaByNombre(mar.Nombre_Marca); //Cambiar

                    return View("Consulta");

                case "Actualizar":

                    mar.UPDATE(mar);
                    new EnviarEmail().enviaMail("marca", mar.Id_Marca, "Actualizado");
                    return RedirectToAction("Index");

                case "Eliminar":

                    mar.DELETE(mar);
                    new EnviarEmail().enviaMail("marca", mar.Id_Marca, "Eliminado");
                    return RedirectToAction("Index");

                case "Insertar":

                    mar.Add(mar);
                    new EnviarEmail().enviaMail("marca", mar.Id_Marca, "Agregado");
                    return RedirectToAction("Index");


            }

            return View();
        }
    }
}
