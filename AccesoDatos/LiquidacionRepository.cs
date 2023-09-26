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
            liquidacion.NombrePaciente = (matrizLiquidacion[0]);
            liquidacion.NumLiquidacion = (matrizLiquidacion[1]);
            liquidacion.Identificacion = matrizLiquidacion[2];
            liquidacion.TipoAfiliacion = Convert.ToChar(matrizLiquidacion[3]);
            liquidacion.Salario = Convert.ToDecimal(matrizLiquidacion[4]);
            liquidacion.ValorServicio = Convert.ToDecimal(matrizLiquidacion[5]);
            liquidacion.Tarifa = Convert.ToDecimal(matrizLiquidacion[6]);
            liquidacion.CuotaModeradora = Convert.ToDecimal(matrizLiquidacion[7]);
            liquidacion.Fecha = Convert.ToDateTime(matrizLiquidacion[8]);
            return liquidacion;

        }

        public List<Liquidacion> ConsultarTodos()
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();
            FileStream file = new FileStream(Archivo, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(file);
            string leerlinea = string.Empty;
            while ((leerlinea = reader.ReadLine()) != null)
            {

                Liquidacion liquidacion = Map(leerlinea);
                liquidaciones.Add(liquidacion);
            }
            reader.Close();
            file.Close();
            return liquidaciones;
        }

        private bool EsEncontrado(string liquidacionRegistrada, string liquidacionBuscada)
        {
            return liquidacionRegistrada == liquidacionBuscada;
        }

        public Liquidacion Buscar(string numLiquidacion)
        {
            List<Liquidacion> liquidaciones = ConsultarTodos();
            foreach (var item in liquidaciones)
            {
                if (EsEncontrado(item.NumLiquidacion, numLiquidacion))
                {
                    return item;
                }
            }
            return null;
        }

        public void Eliminar(string numLiquidacion)
        {
            List<Liquidacion> liquidaciones = new List<Liquidacion>();
            liquidaciones = ConsultarTodos();
            FileStream file = new FileStream(Archivo, FileMode.Create);
            file.Close();
            foreach (var item in liquidaciones)
            {
                if (!EsEncontrado(item.NumLiquidacion, numLiquidacion))
                {
                    Guardar(item);
                }

            }

        }
        public Dictionary<string, int> TotalizarLiquidacionesPorTipoAfiliacion()
        {
            Dictionary<string, int> totalizacion = new Dictionary<string, int>();
            List<Liquidacion> liquidaciones = ConsultarTodos();

            foreach (var liquidacion in liquidaciones)
            {
                string tipoAfiliacion = liquidacion.TipoAfiliacion == 'S' ? "Subsidiado" : "Contributivo";

                if (totalizacion.ContainsKey(tipoAfiliacion))
                {
                    totalizacion[tipoAfiliacion]++;
                }
                else
                {
                    totalizacion[tipoAfiliacion] = 1;
                }
            }

            return totalizacion;
        }

        public Dictionary<string, decimal> CalcularValorTotalPorTipoAfiliacion()
        {
            Dictionary<string, decimal> valorTotalPorTipoAfiliacion = new Dictionary<string, decimal>();
            List<Liquidacion> liquidaciones = ConsultarTodos();

            foreach (var liquidacion in liquidaciones)
            {
                string tipoAfiliacion = liquidacion.TipoAfiliacion == 'S' ? "Subsidiado" : "Contributivo";

                if (valorTotalPorTipoAfiliacion.ContainsKey(tipoAfiliacion))
                {
                    valorTotalPorTipoAfiliacion[tipoAfiliacion] += liquidacion.CuotaModeradora;
                }
                else
                {
                    valorTotalPorTipoAfiliacion[tipoAfiliacion] = liquidacion.CuotaModeradora;
                }
            }

            return valorTotalPorTipoAfiliacion;
        }

        public Dictionary<string, decimal> FiltrarYTotalizarPorMesYAnio(int mes, int anio)
        {
            Dictionary<string, decimal> resultado = new Dictionary<string, decimal>();
            List<Liquidacion> liquidaciones = ConsultarTodos();

            decimal totalCuotasModeradoras = 0;
            int totalLiquidaciones = 0;

            foreach (var liquidacion in liquidaciones)
            {
                if (liquidacion.Fecha.Month == mes && liquidacion.Fecha.Year == anio)
                {
                    string tipoAfiliacion = liquidacion.TipoAfiliacion == 'S' ? "Subsidiado" : "Contributivo";
                    totalCuotasModeradoras += liquidacion.CuotaModeradora;
                    totalLiquidaciones++;

                    if (resultado.ContainsKey(tipoAfiliacion))
                    {
                        resultado[tipoAfiliacion] += liquidacion.CuotaModeradora;
                    }
                    else
                    {
                        resultado[tipoAfiliacion] = liquidacion.CuotaModeradora;
                    }
                }
            }

            resultado["TotalLiquidaciones"] = totalLiquidaciones;
            resultado["TotalCuotasModeradoras"] = totalCuotasModeradoras;

            return resultado;
        }

        public List<Liquidacion> FiltrarPorNombre(string palabraClave)
        {
            List<Liquidacion> liquidaciones = ConsultarTodos();
            List<Liquidacion> resultado = new List<Liquidacion>();

            foreach (var liquidacion in liquidaciones)
            {
 
                if (liquidacion.NombrePaciente != null && liquidacion.NombrePaciente.Contains(palabraClave))
                {
                    resultado.Add(liquidacion);
                }
            }

            return resultado;
        }
       
    }
}
