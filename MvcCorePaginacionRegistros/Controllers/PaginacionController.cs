using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Models;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.Controllers
{
    public class PaginacionController : Controller
    {
        private RepositoryHospital repo;

        public PaginacionController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RegistroVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentosAsync();

            int siguiente = posicion.Value + 1;

            if (siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;

            VistaDepartamento vistaDepartamento = await this.repo.GetVistaDepartamentoAsync(posicion.Value);

            return View(vistaDepartamento);
        }

        public async Task<IActionResult> GrupoVistaDepartamentos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentosAsync();

            ViewData["NUMEROREGISTROS"] = numeroRegistros;
            ViewData["POSICIONACTUAL"] = posicion.Value;

            List<VistaDepartamento> departamentos = await this.repo.GetGrupoVistaDepartamentoAsync(posicion.Value);

            return View(departamentos);
        }

        public async Task<IActionResult> GrupoDepartamentos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentosAsync();

            ViewData["NUMEROREGISTROS"] = numeroRegistros;
            ViewData["POSICIONACTUAL"] = posicion.Value;

            List<Departamento> departamentos = await this.repo.GetGrupoDepartamentosAsync(posicion.Value);

            return View(departamentos);
        }

        public async Task<IActionResult> PaginarGrupoEmpleados(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numRegistros = await this.repo.GetEmpleadosCountAsync();

            ViewData["REGISTROS"] = numRegistros;

            List<Empleado> empleados = await this.repo.GetGrupoEmpleadosAsync(posicion.Value);

            return View(empleados);
        }

        public async Task<IActionResult>PaginarGrupoEmpleadosOficio(int? posicion,string oficio)
        {
         
            if(posicion == null )
            {
                posicion = 1;
                return View();
            }
            else
            {
                List<Empleado> empleados = await this.repo.GetGrupoEmpleadosOficioAsync(posicion.Value, oficio);
                int registros = await this.repo.GetEmpleadosOficioCountAsync(oficio);
                ViewData["REGISTROS"] = registros;
                ViewData["OFICIO"] = oficio;
                return View(empleados);

            }

               
        }
        [HttpPost]
        public async Task<IActionResult>PaginarGrupoEmpleadosOficio(string? oficio)
        {
            ModelEmpleadosOficio model = new ModelEmpleadosOficio();
            List <Empleado> empleados= await this.repo.GetGrupoEmpleadosOficioAsync(1, oficio);
            int registros = await this.repo.GetEmpleadosOficioCountAsync(oficio);
            ViewData["REGISTROS"] = registros;
            ViewData["OFICIO"] = oficio;
            return View(empleados);
        }
        public async Task<IActionResult>PaginarGrupoEmpleadosOficioOut(int? posicion,string oficio)
        {
         
            if(posicion == null )
            {
                posicion = 1;
                return View();
            }
            else
            {
                ModelEmpleadosOficio model = await this.repo.GetGrupoEmpleadosOficioOutAsync(posicion.Value, oficio);
                ViewData["OFICIO"] = oficio;
                return View(model);

            }

               
        }
        [HttpPost]
        public async Task<IActionResult>PaginarGrupoEmpleadosOficioOut(string? oficio)
        {
            ModelEmpleadosOficio model = new ModelEmpleadosOficio();
            model= await this.repo.GetGrupoEmpleadosOficioOutAsync(1, oficio);
            ViewData["OFICIO"] = oficio;
            return View(model);
        }

        public async Task<IActionResult> DetailsEmpleadoByIdDept(int iddepartamento, int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            // Obtener el modelo completo (empleado, total registros y departamento)
            ModelEmpleadosDepartamento model = await this.repo.GetEmpleadoByDepartamentoYPosicionEntityfRameworkAsync(posicion.Value, iddepartamento);

            // El número de registros ya viene en el modelo
            int numeroRegistros = model.numregistros;

            // Calcular siguiente posición
            int siguiente = posicion.Value + 1;
            if (siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }

            // Calcular anterior posición
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["POSICIONACTUAL"] = posicion.Value;
            ViewData["IDDEPARTAMENTO"] = iddepartamento;

            return View(model);
        }
    }
}