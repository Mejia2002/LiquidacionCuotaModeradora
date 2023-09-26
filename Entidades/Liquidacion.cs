using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Liquidacion
    {
        public string NombrePaciente { get; set; }
        public string NumLiquidacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Identificacion { get; set; }
        public char TipoAfiliacion { get; set; }
        public decimal Salario { get; set; }
        public decimal ValorServicio { get; set; }
        public decimal Tarifa { get; set; }

        public decimal CuotaModeradora { get; set; }

        public Liquidacion()
        {
        }

        public Liquidacion(string numLiquidacion, string nombrePaciente, DateTime fecha, string identificacion, char tipoAfiliacion, decimal salario, decimal valorServicio, decimal tarifa, decimal cuotaModeradora)
        {
            NumLiquidacion = numLiquidacion;
            NombrePaciente = nombrePaciente;
            Fecha = fecha;
            Identificacion = identificacion;
            TipoAfiliacion = tipoAfiliacion;
            Salario = salario;
            ValorServicio = valorServicio;
            Tarifa = tarifa;
            CuotaModeradora = cuotaModeradora;
            
        }
        public override string ToString()
        {
            return $"{NumLiquidacion};{NombrePaciente}{Fecha};{Identificacion};{TipoAfiliacion};{Salario};{ValorServicio};{Tarifa};{CuotaModeradora}";
        }

    }
}
