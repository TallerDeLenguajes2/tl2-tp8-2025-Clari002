using Microsoft.AspNetCore.Mvc;
using tl2_tp8_2025_Clari002.Models;
using tl2_tp8_2025_Clari002.Repositorios;

namespace tl2_tp8_2025_Clari002.Controllers
{
    
    public class PresupuestosController : Controller
    {
        private readonly PresupuestosRepository _repo;

        public PresupuestosController()
        {
            _repo = new PresupuestosRepository();
        }

        //en paso 1 Controllers/PresupuestosController
        [HttpGet]
        public IActionResult Index()
        {
            List<Presupuestos> presupuestos = _repo.ListarPresupuestos();
            return View(presupuestos);
        }

        //en paso 2 , agrego metodo details que llamara al repositorio BuscarPresupuestoPorId 
        [HttpGet]
        public IActionResult Details(int id)
        {
            var presupuesto = _repo.BuscarPresupuestoPorId(id);
            if (presupuesto == null)
            {
                return NotFound();//error 404
            }
            return View(presupuesto);
        }

        //agrego acciones paso 3
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Presupuestos presupuesto)
        {
            if (!ModelState.IsValid)
            {
                return View(presupuesto);
            }
            _repo.CrearPresupuesto(presupuesto);
            return RedirectToAction("Index");
        }
        //acciones para eliminar presupuesto
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var presupuesto = _repo.BuscarPresupuestoPorId(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _repo.EliminarPresupuesto(id);
            return RedirectToAction("Index");
        }
             //agrego metodo Edit
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var presupuesto = _repo.BuscarPresupuestoPorId(id);
            if (presupuesto == null)
            {
                return NotFound();
            }
            return View(presupuesto);
        }
        [HttpPost]
        public IActionResult Edit(int id, Presupuestos presupuesto)
        {
            if (!ModelState.IsValid)
            {
                return View(presupuesto);
            }
            _repo.ModificarPresupuesto(id, presupuesto);
            return RedirectToAction("Index");
        }
    }
    
}

