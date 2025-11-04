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
        //paso 1 para mostrar los productos

        [HttpGet]
        public IActionResult Index()
        {
            List<Productos> productos = _repo.ListarProductos();
            return View(productos);
        }

         //paso 3, agrego metodo Create
        [HttpGet] //creo el formulario vacio
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]//recibo los datos del form y los guardo en la base con _repo.CrearProducto()
        public IActionResult Create(Productos producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }
            _repo.CrearProducto(producto);
            return RedirectToAction("Index");
        }
        //agrego metodo Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var producto = _repo.BuscarProductoId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [HttpPost]
        public IActionResult Edit(int id, Productos producto)
        {
            if (!ModelState.IsValid)
            {
                return View(producto);
            }
            _repo.ModificarProducto(id, producto);
            return RedirectToAction("Index");
        }

        //agrego metodo de eliminar
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var producto = _repo.BuscarProductoId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.EliminarProducto(id);
            return RedirectToAction("Index");
        }

        //agrego para ver detalle
        [HttpGet]
        public IActionResult Details(int id)
        {
            var producto = _repo.BuscarProductoId(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        
    }
}
