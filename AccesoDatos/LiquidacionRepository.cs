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
            liquidacion.NumLiquidacion = (matrizLiquidacion[0]);
            liquidacion.Identificacion = matrizLiquidacion[1];
            liquidacion.TipoAfiliacion = Convert.ToChar(matrizLiquidacion[2]);
            liquidacion.Salario = Convert.ToDecimal(matrizLiquidacion[3]);
            liquidacion.ValorServivio= Convert.ToDecimal(matrizLiquidacion[4]);
            liquidacion.Tarifa = Convert.ToDecimal(matrizLiquidacion[5]);
            liquidacion.CuotaModeradora = Convert.ToDecimal(matrizLiquidacion[6]);
            liquidacion.Fecha = Convert.ToDateTime(matrizLiquidacion[7]);
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
    }
}
