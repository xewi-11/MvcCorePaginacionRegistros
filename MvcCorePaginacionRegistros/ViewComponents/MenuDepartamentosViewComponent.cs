using Microsoft.AspNetCore.Mvc;
using MvcCorePaginacionRegistros.Repositories;
using MvcCorePaginacionRegistros.Repositories;

namespace MvcCorePaginacionRegistros.ViewComponents
{
    public class MenuDepartamentosViewComponent : ViewComponent
    {
        private RepositoryHospital repo;

        public MenuDepartamentosViewComponent(RepositoryHospital repositoryHospital)
        {
            this.repo = repositoryHospital;
        }
        //Podemos TENER TODOS LOS METOSOAS QUE QUERAMOS

        //PERO SI QUEREMOS DEVOOLVER DATOS TENE;OS QUE USAR EL METODO DE INVOKEASYNC

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var departamentos =await  this.repo.GetAllDepartamentosNameAsync();
            return View(departamentos);
        }
    }
}
