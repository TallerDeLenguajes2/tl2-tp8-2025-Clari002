using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_Clari002.Models;
using tl2_tp8_2025_Clari002.Repositorios;

namespace tl2_tp8_2025_Clari002.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ProductosRepository _repo;

        public ProductosController()
        {
            _repo = new ProductosRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Productos> productos = _repo.ListarProductos();
            return View(productos);
        }

        
    }
}
