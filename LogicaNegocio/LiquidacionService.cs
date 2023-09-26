using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AccesoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LiquidacionService
    {
        private readonly LiquidacionRepository liquidacionRepository;
        public LiquidacionService()
        {
            liquidacionRepository = new LiquidacionRepository();
        }

        decimal salarioMinimo = 1160000;


        public decimal CalcularCuotaModeradoraContributivo(decimal valorServicio, decimal salarioDevengado)
        {
            decimal tarifa = 0;

            if (salarioDevengado < 2 * salarioMinimo)
            {
                tarifa = 0.15m; // 15%
            }
            else if (salarioDevengado >= 2 * salarioMinimo && salarioDevengado <= 5 * salarioMinimo)
            {
                tarifa = 0.20m; // 20%
            }
            else
            {
                tarifa = 0.25m; // 25%
            }

            decimal cuotaModeradora = valorServicio * tarifa;

            // Aplicar el tope máximo si es necesario
            decimal topeMaximo = ObtenerTopeMaximoContributivo(salarioDevengado);
            if (cuotaModeradora > topeMaximo)
            {
                cuotaModeradora = topeMaximo;
            }

            return cuotaModeradora;
        }

        private decimal ObtenerTopeMaximoContributivo(decimal salarioDevengado)
        {
            if (salarioDevengado < 2 * salarioMinimo)
            {
                return 250000; 
            }
            else if (salarioDevengado >= 2 * salarioMinimo && salarioDevengado <= 5 * salarioMinimo)
            {
                return 900000; 
            }
            else
            {
                return 1500000; 
            }
        }
        public decimal CalcularCuotaModeradoraSubsidiado(decimal valorServicio)
        {
            decimal tarifa = 0.05m; // 5%

            decimal cuotaModeradora = valorServicio * tarifa;

            decimal topeMaximo = ObtenerTopeMaximoSubsidiado();
            if (cuotaModeradora > topeMaximo)
            {
                cuotaModeradora = topeMaximo;
            }

            return cuotaModeradora;
        }

        private decimal ObtenerTopeMaximoSubsidiado()
        {
            return 200000; 
        }

        public string Guardar(Liquidacion liquidacion)
        {
            try
            {

                if (liquidacionRepository.Buscar(liquidacion.Identificacion) == null)
                {
                    liquidacionRepository.Guardar(liquidacion);
                    return $"se han guardado Satisfactoriamente los datos de: {liquidacion.NombrePaciente} ";
                }
                else
                {
                    return $"Lo sentimos, con la Identificación {liquidacion.Identificacion} ya se encuentra registrada";
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }
        }

        public string Eliminar(string numLiquidacion)
        {
            try
            {
                if (liquidacionRepository.Buscar(numLiquidacion) != null)
                {
                    liquidacionRepository.Eliminar(numLiquidacion);
                    return ($"se han Eliminado Satisfactoriamente los datos de la persona con numero de liquidacion: {numLiquidacion} ");
                }
                else
                {
                    return ($"Lo sentimos, no se encuentra registrada una persona con de liquidacion: {numLiquidacion}");
                }
            }
            catch (Exception e)
            {

                return $"Error de la Aplicacion: {e.Message}";
            }

        }
        public ConsultaLiquidacionesResponse ConsultarTodos()
        {

            try
            {
                List<Liquidacion> liquidaciones = liquidacionRepository.ConsultarTodos();
                if (liquidaciones != null)
                {
                    return new ConsultaLiquidacionesResponse(liquidaciones);
                }
                else
                {
                    return new ConsultaLiquidacionesResponse("No hay liquidaciones registradas");
                }

            }
            catch (Exception e)
            {

                return new ConsultaLiquidacionesResponse("Error de Aplicacion: " + e.Message);
            }
        }
        public LiquidacionReponse BuscarLiquidacion(string numLiquidacion)
        {
            try
            {
                Liquidacion liquidacion = liquidacionRepository.Buscar(numLiquidacion);

                if (liquidacion != null)
                {
                    return new LiquidacionReponse(liquidacion);
                }
                else
                {
                    return new LiquidacionReponse($"No se encontró una liquidación con número de liquidación {numLiquidacion}");
                }
            }
            catch (Exception e)
            {
                return new LiquidacionReponse($"Error de la Aplicación: {e.Message}");
            }
        }
        public Dictionary<string, decimal> CalcularValorTotalCuotasModeradorasPorTipoAfiliacion()
        {
            try
            {
                return liquidacionRepository.CalcularValorTotalPorTipoAfiliacion();
            }
            catch (Exception)
            {
          
                return new Dictionary<string, decimal>();
            }
        }
        public Dictionary<string, decimal> FiltrarYTotalizarCuotasPorMesYAnio(int mes, int anio)
        {
            try
            {
                return liquidacionRepository.FiltrarYTotalizarPorMesYAnio(mes, anio);
            }
            catch (Exception)
            {

                return new Dictionary<string, decimal>();
            }
        }

        public ConsultaLiquidacionesResponse FiltrarLiquidacionesPorNombre(string palabraClave)
        {
            try
            {
                List<Liquidacion> liquidacionesFiltradas = liquidacionRepository.FiltrarPorNombre(palabraClave);

                if (liquidacionesFiltradas != null && liquidacionesFiltradas.Count > 0)
                {
                    return new ConsultaLiquidacionesResponse(liquidacionesFiltradas);
                }
                else
                {
                    return new ConsultaLiquidacionesResponse($"No se encontraron liquidaciones con el nombre de paciente que contenga '{palabraClave}'");
                }
            }
            catch (Exception e)
            {
                return new ConsultaLiquidacionesResponse($"Error de la Aplicación: {e.Message}");
            }
        }

        public decimal CalcularCuotaModeradora(decimal valorServicio, decimal salario, decimal tarifa, char tipoAfiliacion)
        {
            throw new NotImplementedException();
        }

        public Liquidacion Buscar(string numLiquidacionBuscar)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, int> TotalizarLiquidacionesPorTipoAfiliacion()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, decimal> CalcularValorTotalPorTipoAfiliacion()
        {
            throw new NotImplementedException();
        }

        public List<Liquidacion> FiltrarPorNombre(string palabraClave)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, decimal> FiltrarYTotalizarPorMesYAnio(int mes, int anio)
        {
            throw new NotImplementedException();
        }

        public static List<Liquidacion> ConsultarTodos(List<Liquidacion> todasLasLiquidaciones)
        {
            throw new NotImplementedException();
        }
    }
    public class LiquidacionReponse
    {
        public Liquidacion Liquidacion{ get; set; }
        public string Message { get; set; }
        public bool Encontrado { get; set; }

        public LiquidacionReponse(Liquidacion liquidacion)
        {
            Liquidacion = new Liquidacion();
            Liquidacion = liquidacion;
            Encontrado = true;
        }
        public LiquidacionReponse(string message)
        {
            Message = message;
            Encontrado = false;
        }
    }
    public class ConsultaLiquidacionesResponse
    {
        public List<Liquidacion> Liquidaciones { get; set; }
        public string Message { get; set; }
        public bool Encontrado { get; set; }

        public ConsultaLiquidacionesResponse(List<Liquidacion> liquidaciones)
        {
            Liquidaciones = new List<Liquidacion>();
            Liquidaciones = liquidaciones;
            Encontrado = true;
        }
        public ConsultaLiquidacionesResponse(string message)
        {
            Message = message;
            Encontrado = false;
        }
    }
}
 