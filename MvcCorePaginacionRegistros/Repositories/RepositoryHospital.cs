using Microsoft.EntityFrameworkCore;
using MvcCorePaginacionRegistros.Data;
using MvcCorePaginacionRegistros.Models;
using System.Diagnostics.Metrics;
using System.Threading;

namespace MvcCorePaginacionRegistros.Repositories
{
    #region VISTAS PROCEDIMIENTOS ALMACENADOS

//    alter view V_DEPARTAMENTOS_INDIVIDUAL
//as
//  select Cast(ROW_NUMBER() OVER (ORDER BY DEPT_NO) as int)
//  as POSICION
//, DEPT_NO,DNOMBRE,LOC FROM DEPT
//go

    #endregion
    public class RepositoryHospital
    {
        private HospitalContext context;

        public RepositoryHospital(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<int> GetNumeroRegistrosVistaDepartamentosASync()
        {
            return await context.VistaDepartamentos.CountAsync();
        }
        public async Task<VistaDepartamento>GetVistaDepartamentoAsync(int posicion)
        {
            VistaDepartamento departamento = await context.VistaDepartamentos.Where(x => x.posicion == posicion).FirstOrDefaultAsync();
            return departamento;
        }
        public async Task<List<VistaDepartamento>>GetGrupoDepartamentosAsync(int posicion)
        {
            var consulta = from datos in this.context.VistaDepartamentos 
                           where datos.posicion >= posicion && datos.posicion < (posicion + 2) select datos;


            return await consulta.ToListAsync();
        }
    }
}
