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

        public async Task<IActionResult> RegistroVistaDepartamento(int? posicion)
        {
            if (posicion == null)
            {
                
                posicion = 1;
            }
            int numeroRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentosASync();
            //PRIMERO=1
            //ULTIMO=4
            //ANTERIOR=POSICION-1
            //SIGUIENTE=POSICION+1
            int siguiente = posicion.Value + 1;
            if(siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }
            int anterior = posicion.Value - 1;
            if(anterior < 1)
            {
                anterior = 1;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["ULTIMO"] = numeroRegistros;

            VistaDepartamento departamento = await this.repo.GetVistaDepartamentoAsync(posicion.Value);
            return View(departamento);

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task< IActionResult> GrupoVIstaDepartamentos(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            //Lo SIGUIENTE SERA QUE DEBEMOS DIBUJAR DE LOS NUMEROS DE PAGINA EN LOS LINKS
            //NECESITAMOS UNA VARIABLE PARA EL NUMERO DE PAGINA
            //VOY A REALIZAR EL DIBUJO DESDES AQUI,NO DESDE RAZOR.
            int numPag = 1;
            int numRegistros = await this.repo.GetNumeroRegistrosVistaDepartamentosASync();
            string html = "<div>";
            for (int i = 1; i <= numRegistros; i+=2)
            {
                html += "<a href='GrupoVIstaDepartamentos?posicion=" + i + "'>Pagina "+ numPag + "</a> |";
                numPag++;
            }
            html += "</div>";
            ViewData["LINKS"] = html;
            List<VistaDepartamento> departamentos = await this.repo.GetGrupoDepartamentosAsync(posicion.Value);


            return View(departamentos);
        }


    }
}
