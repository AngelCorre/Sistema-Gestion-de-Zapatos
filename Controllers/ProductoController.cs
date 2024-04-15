using GestionZapateria.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace GestionZapateria.Controllers
{
    public class ProductoController : Controller
    {
        public ActionResult Index(int Id_Producto)
        {
            Producto por = new(Id_Producto);
            return View(por);
        }


        [HttpPost]
        public ActionResult Index(Producto por, string accion)
        {
            switch (accion)
            {
                case "Buscar":
                    por.GetProductoByNombre(por.Modelo, por.Talla, por.Precio); //Cambiar
                    ViewData["listProducto"] = por.GetProductoByNombre(por.Modelo, por.Talla, por.Precio); //Cambiar
                    return View("Consulta");
                case "Actualizar":
                    por.UPDATE(por);
                    new EnviarEmail().enviaMail("producto",por.Id_Producto, "Actualizado");
                    return RedirectToAction("Index");
                case "Eliminar":
                    por.DELETE(por);
                    new EnviarEmail().enviaMail("producto", por.Id_Producto, "Eliminado");
                    return RedirectToAction("Index");
                case "Insertar":

                    por.Add(por);
                    new EnviarEmail().enviaMail("producto", por.Id_Producto, "Agregado");
                    return RedirectToAction("Index");


            }

            return View();
        }
    }
}
