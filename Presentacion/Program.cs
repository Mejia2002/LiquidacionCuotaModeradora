using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio;
using Entidades;

namespace Presentacion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var liquidacionService = new LogicaNegocio.LiquidacionService();

            while (true)
            {
                Console.Clear();

                Console.WriteLine("Menú:");
                Console.WriteLine("1. Guardar liquidación");
                Console.WriteLine("2. Eliminar liquidación");
                Console.WriteLine("3. Buscar liquidación por Número de Liquidación");
                Console.WriteLine("4. Consultar todas las liquidaciones");
                Console.WriteLine("5. Totalizar Liquidaciones por Tipo de Afiliación");
                Console.WriteLine("6. Calcular Valor Total por Tipo de Afiliación");
                Console.WriteLine("7. Filtrar y Totalizar por Mes y Año");
                Console.WriteLine("8. Filtrar por Nombre del Paciente");
                Console.WriteLine("9. Salir");

                Console.Write("Ingrese la opción deseada: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.WriteLine("Ingrese los datos de la nueva liquidación:");
                        Console.Write("Nombre del paciente: ");
                        string nombrePaciente = Console.ReadLine();
                        Console.Write("Número de liquidación: ");
                        string numLiquidacion = Console.ReadLine();
                        Console.Write("Fecha (YYYY-MM-DD): ");
                        DateTime fecha = DateTime.Parse(Console.ReadLine());
                        Console.Write("Identificación: ");
                        string identificacion = Console.ReadLine();
                        Console.Write("Tipo de afiliación (S para Subsidiado, C para Contributivo): ");
                        char tipoAfiliacion = Console.ReadLine().ToUpper()[0];
                        Console.Write("Salario: ");
                        decimal salario = decimal.Parse(Console.ReadLine());
                        Console.Write("Valor del servicio: ");
                        decimal valorServicio = decimal.Parse(Console.ReadLine());
                        Console.Write("Tarifa: ");
                        decimal tarifa = decimal.Parse(Console.ReadLine());

                        // Calcular la cuota moderadora llamando a liquidacionService.CalcularCuotaModeradora
                        decimal cuotaModeradora = liquidacionService.CalcularCuotaModeradora(valorServicio, salario, tarifa, tipoAfiliacion);

                        var nuevaLiquidacion = new Liquidacion
                        {
                            NombrePaciente = nombrePaciente,
                            NumLiquidacion = numLiquidacion,
                            Fecha = fecha,
                            Identificacion = identificacion,
                            TipoAfiliacion = tipoAfiliacion,
                            Salario = salario,
                            ValorServicio = valorServicio,
                            Tarifa = tarifa,
                            CuotaModeradora = cuotaModeradora
                        };

                        string resultado = liquidacionService.Guardar(nuevaLiquidacion);

                        Console.WriteLine(resultado);
                        Console.ReadKey();
                        break;


                    case "2":
                        Console.Write("Ingrese el número de liquidación a eliminar: ");
                        string numLiquidacionEliminar = Console.ReadLine();
                        string resultadoEliminar = liquidacionService.Eliminar(numLiquidacionEliminar);
                        Console.WriteLine(resultadoEliminar);
                        Console.ReadKey();

                        break;

                    case "3":
                        Console.Write("Ingrese el número de liquidación a buscar: ");
                        string numLiquidacionBuscar = Console.ReadLine();
                        Liquidacion liquidacionEncontrada = liquidacionService.Buscar(numLiquidacionBuscar);

                        if (liquidacionEncontrada != null)
                        {
                            Console.WriteLine("Liquidación encontrada:");
                            Console.WriteLine($"Nombre del paciente: {liquidacionEncontrada.NombrePaciente}");
                            Console.WriteLine($"Número de liquidación: {liquidacionEncontrada.NumLiquidacion}");
                            Console.WriteLine($"Fecha: {liquidacionEncontrada.Fecha}");
                            Console.WriteLine($"Identificación: {liquidacionEncontrada.Identificacion}");
                            Console.WriteLine($"Tipo de afiliación: {liquidacionEncontrada.TipoAfiliacion}");
                            Console.WriteLine($"Salario: {liquidacionEncontrada.Salario}");
                            Console.WriteLine($"Valor del servicio: {liquidacionEncontrada.ValorServicio}");
                            Console.WriteLine($"Tarifa: {liquidacionEncontrada.Tarifa}");
                            Console.WriteLine($"Cuota moderadora: {liquidacionEncontrada.CuotaModeradora}");
                        }
                        else
                        {
                            Console.WriteLine("Liquidación no encontrada.");
                        }
                        Console.ReadKey();
                        break;


                    case "4":

                        List<Liquidacion> todasLasLiquidaciones = LiquidacionService.ConsultarTodos();


                        if (todasLasLiquidaciones.Count > 0)
                        {
                            Console.WriteLine("Todas las liquidaciones:");
                            foreach (var liquidacion in todasLasLiquidaciones)
                            {
                                Console.WriteLine($"Nombre del paciente: {liquidacion.NombrePaciente}");
                                Console.WriteLine($"Número de liquidación: {liquidacion.NumLiquidacion}");
                                Console.WriteLine($"Fecha: {liquidacion.Fecha}");
                                Console.WriteLine($"Identificación: {liquidacion.Identificacion}");
                                Console.WriteLine($"Tipo de afiliación: {liquidacion.TipoAfiliacion}");
                                Console.WriteLine($"Salario: {liquidacion.Salario}");
                                Console.WriteLine($"Valor del servicio: {liquidacion.ValorServicio}");
                                Console.WriteLine($"Tarifa: {liquidacion.Tarifa}");
                                Console.WriteLine($"Cuota moderadora: {liquidacion.CuotaModeradora}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay liquidaciones registradas.");
                        }
                        Console.ReadKey();
                        break;

                    case "5":
                        Dictionary<string, int> totalLiquidacionesPorTipoAfiliacion = liquidacionService.TotalizarLiquidacionesPorTipoAfiliacion();

                        if (totalLiquidacionesPorTipoAfiliacion.Count > 0)
                        {
                            Console.WriteLine("Total de liquidaciones por tipo de afiliación:");
                            foreach (var kvp in totalLiquidacionesPorTipoAfiliacion)
                            {
                                Console.WriteLine($"Tipo de afiliación: {kvp.Key}, Total: {kvp.Value}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay liquidaciones registradas.");
                        }
                        Console.ReadKey();
                        break;


                    case "6":
                        Dictionary<string, decimal> valorTotalPorTipoAfiliacion = liquidacionService.CalcularValorTotalPorTipoAfiliacion();

                        if (valorTotalPorTipoAfiliacion.Count > 0)
                        {
                            Console.WriteLine("Valor total de liquidaciones por tipo de afiliación:");
                            foreach (var kvp in valorTotalPorTipoAfiliacion)
                            {
                                Console.WriteLine($"Tipo de afiliación: {kvp.Key}, Valor Total: {kvp.Value:C}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay liquidaciones registradas.");
                        }
                        Console.ReadKey();
                        break;


                    case "7":
                        Console.Write("Ingrese el mes (1-12): ");
                        int mes = int.Parse(Console.ReadLine());
                        Console.Write("Ingrese el año (YYYY): ");
                        int anio = int.Parse(Console.ReadLine());

                        Dictionary<string, decimal> resultadoFiltradoPorMesAnio = liquidacionService.FiltrarYTotalizarPorMesYAnio(mes, anio);

                        if (resultadoFiltradoPorMesAnio.Count > 0)
                        {
                            Console.WriteLine($"Total de liquidaciones para el mes {mes}/{anio}:");
                            foreach (var kvp in resultadoFiltradoPorMesAnio)
                            {
                                if (kvp.Key == "TotalLiquidaciones")
                                {
                                    Console.WriteLine($"Total de liquidaciones encontradas: {kvp.Value}");
                                }
                                else if (kvp.Key == "TotalCuotasModeradoras")
                                {
                                    Console.WriteLine($"Total de cuotas moderadoras: {kvp.Value:C}");
                                }
                                else
                                {
                                    Console.WriteLine($"Tipo de afiliación: {kvp.Key}, Cuota Moderadora: {kvp.Value:C}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No hay liquidaciones registradas para el mes y año especificados.");
                        }
                        Console.ReadKey();
                        break;


                    case "8":
                        Console.Write("Ingrese una palabra clave para filtrar por nombre del paciente: ");
                        string palabraClave = Console.ReadLine();

                        List<Liquidacion> liquidacionesFiltradas = liquidacionService.FiltrarPorNombre(palabraClave);

                        if (liquidacionesFiltradas.Count > 0)
                        {
                            Console.WriteLine($"Liquidaciones encontradas que contienen la palabra clave '{palabraClave}':");
                            foreach (var liquidacion in liquidacionesFiltradas)
                            {
                                Console.WriteLine($"Nombre del paciente: {liquidacion.NombrePaciente}");
                                Console.WriteLine($"Número de liquidación: {liquidacion.NumLiquidacion}");
                                Console.WriteLine($"Fecha: {liquidacion.Fecha}");
                                Console.WriteLine($"Identificación: {liquidacion.Identificacion}");
                                Console.WriteLine($"Tipo de afiliación: {liquidacion.TipoAfiliacion}");
                                Console.WriteLine($"Salario: {liquidacion.Salario}");
                                Console.WriteLine($"Valor del servicio: {liquidacion.ValorServicio}");
                                Console.WriteLine($"Tarifa: {liquidacion.Tarifa}");
                                Console.WriteLine($"Cuota moderadora: {liquidacion.CuotaModeradora}");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No se encontraron liquidaciones que contengan la palabra clave '{palabraClave}'.");
                        }
                        Console.ReadKey();
                        break;


                    case "9":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para continuar.");
                        Console.ReadLine();
                        break;
                }
            }





        }
            
    }   
    
}
