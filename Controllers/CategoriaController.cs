using GestionZapateria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace GestionZapateria.Controllers
{
    public class CategoriaController : Controller
    {
        public ActionResult Index(int Id_Categoria)
        {
            Categoria cat = new(Id_Categoria);
            return View(cat);
        }


        [HttpPost]
        public ActionResult Index(Categoria cat, string accion)
        {
            switch (accion)
            {
                case "Buscar":
                    cat.GetCategoriaByNombre(cat.Nombre_Categoria); //Cambiar
                    ViewData["listCategoria"] = cat.GetCategoriaByNombre(cat.Nombre_Categoria); //Cambiar

                    return View("Consulta");
                case "Actualizar":
                    cat.UPDATE(cat);
                    new EnviarEmail().enviaMail("categoria", cat.Id_Categoria, "Actualizado");
                    return RedirectToAction("Index");
                case "Eliminar":
                    cat.DELETE(cat);
                    new EnviarEmail().enviaMail("categoria", cat.Id_Categoria, "Eliminado");
                    return RedirectToAction("Index");
                case "Insertar":

                    cat.Add(cat);
                    new EnviarEmail().enviaMail("categoria", cat.Id_Categoria, "Agregado");
                    return RedirectToAction("Index");


            }

            return View();
        }
    }
}
