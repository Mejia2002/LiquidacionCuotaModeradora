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
                    return new ConsultaLiquidacionesResponse("La Persona buscada no se encuentra Registrada");
                }

            }
            catch (Exception e)
            {

                return new ConsultaLiquidacionesResponse("Error de Aplicacion: " + e.Message);
            }
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
        public List<Persona> Personas { get; set; }
        public string Message { get; set; }
        public bool Encontrado { get; set; }

        public ConsultaLiquidacionesResponse(List<Persona> personas)
        {
            Personas = new List<Persona>();
            Personas = personas;
            Encontrado = true;
        }
        public ConsultaLiquidacionesResponse(string message)
        {
            Message = message;
            Encontrado = false;
        }
    }
}
 