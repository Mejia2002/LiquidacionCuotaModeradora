using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Entidades;

namespace AccesoDatos
{
    public class LiquidacionRepository
    {
        string Archivo = "LiquidacionCuotaModeradora.txt";
        public string Guardar(Liquidacion liquidacion)
        {
            StreamWriter escritor = new StreamWriter(Archivo, true);
            escritor.WriteLine(liquidacion.ToString());
            escritor.Close();
            return "Datos guardados";
        }

        public string Guardar(List<Liquidacion> liquidaciones) 
        {
            StreamWriter escritor = new StreamWriter(Archivo, false);
            foreach (var item in liquidaciones)
            {
                escritor.WriteLine(item.ToString());
            }
            escritor.Close();
            return $"Datos actualizados";
        }

        private Liquidacion Map(string linea)
        {

            Liquidacion liquidacion = new Liquidacion();
            char delimiter = ';';
            string[] matrizLiquidacion = linea.Split(delimiter);
            establecimiento.IdentificacionEstablecimiento = (matrizLiquidacion[0]);
            establecimiento.NombreEstablecimiento = matrizLiquidacion[1];
            establecimiento.IngresosAnuales = Convert.ToDecimal(matrizLiquidacion[2]);
            establecimiento.GastosAnuales = Convert.ToDecimal(matrizLiquidacion[3]);
            establecimiento.TiempoFuncionamiento = int.Parse(matrizLiquidacion[4]);
            establecimiento.TipoResposabilidad = Convert.ToChar(matrizLiquidacion[5]);
            establecimiento.Ganancias = Convert.ToDecimal(matrizLiquidacion[6]);
            establecimiento.ValorUvt = Convert.ToDecimal(matrizLiquidacion[7]);
            establecimiento.Tarifa = Convert.ToDecimal(matrizLiquidacion[8]);
            establecimiento.ValorImpuesto = Convert.ToDecimal(matrizLiquidacion[9]);
            return establecimiento;

        }

        public void Eliminar(string numLiquidacion)
        {
            List<Liquidacion> establecimientos = new List<Liquidacion>();
            liquidaciones = ConsultarTodos();
            FileStream file = new FileStream(Archivo, FileMode.Create);
            file.Close();
            foreach (var item in establecimientos)
            {
                if (!EsEncontrado(item.NumLiquidacion, numLiquidacion))
                {
                    Guardar(item);
                }

            }

        }
    }
}
