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
        
    }
    
}

