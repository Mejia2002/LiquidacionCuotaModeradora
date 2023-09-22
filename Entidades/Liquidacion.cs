using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class Liquidacion
    {
        public string NumLiquidacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Identificacion { get; set; }
        public char TipoAfiliacion { get; set; }
        public decimal Salario { get; set; }
        public decimal ValorServivio { get; set; }
        public decimal Tarifa { get; set; }

        public decimal CuotaModeradora { get; set; }

        public Liquidacion()
        {
        }

        public Liquidacion(string numLiquidacion, DateTime fecha, string identificacion, char tipoAfiliacion, decimal salario, decimal valorServivio, decimal tarifa, decimal cuotaModeradora)
        {
            NumLiquidacion = numLiquidacion;
            Fecha = fecha;
            Identificacion = identificacion;
            TipoAfiliacion = tipoAfiliacion;
            Salario = salario;
            ValorServivio = valorServivio;
            Tarifa = tarifa;
            CuotaModeradora = cuotaModeradora;
            
        }
        public override string ToString()
        {
            return $"{NumLiquidacion};{Fecha};{Identificacion};{TipoAfiliacion};{Salario};{ValorServivio};{Tarifa};{CuotaModeradora}";
        }

    }
}
