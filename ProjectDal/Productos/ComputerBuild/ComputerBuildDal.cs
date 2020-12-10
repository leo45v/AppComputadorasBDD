using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Configuracion;
using Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.ProjectDal.Personas.Productos.ComputerBuild
{
    public class ComputerBuildDal
    {
        private static decimal presupuesto;
        public static decimal Presupuesto
        {
            get
            {
                return presupuesto;
            }
            set
            {
                presupuesto = value;
                configXD.presupuesto = value;
            }
        }
        private static readonly ConfiguracionComputerOperation configXD = new ConfiguracionComputerOperation(presupuesto);


        public static Computadora ObtenerComputadoraRecomendada(Requirements.TipoComputer tipoComputer)
        {
            List<Computadora> computadoras = GetComputersBuild(tipoComputer);
            Computadora computadora = null;
            if (computadoras.Count > 0)
            {
                computadora = computadoras.Last();
            }
            computadoras.Clear();
            GC.Collect();
            return computadora;
        }

        public static List<Computadora> GetComputersBuild(Requirements.TipoComputer tipoComputer)
        {
            List<Procesador> procesadors = ProcesadoresRecomendados(tipoComputer);
            List<Computadora> auxProcesadorPlacaBase = new List<Computadora>();
            foreach (var procesador in procesadors)
            {
                List<PlacaBase> plaquitasList = PlacasBasesRecomendadas(tipoComputer, procesador);
                foreach (var placaBase in plaquitasList)
                {
                    List<Ram> rams = RamsRecomdadas(tipoComputer);
                    foreach (var ramNew in rams)
                    {
                        double cantidadRams = 1;
                        double cantidadRamxMax = placaBase.NumeroDims;
                        while ((ramNew.Memoria * cantidadRams) <= tipoComputer.Ram.Capacidad.Max && cantidadRams < cantidadRamxMax)
                        {
                            cantidadRams++;
                        }
                        if (cantidadRams == 3)
                        {
                            cantidadRams++;
                        }
                        List<Ram> nuevasRams = new List<Ram>();
                        for (int i = 0; i < cantidadRams; i++)
                        {
                            nuevasRams.Add(ramNew);
                        }
                        auxProcesadorPlacaBase.Add(new Computadora()
                        {
                            Procesador = procesador,
                            PlacaBase = placaBase,
                            Rams = nuevasRams
                        });
                        nuevasRams = null;
                    }
                    rams = null;
                }
                plaquitasList = null;
            }
            auxProcesadorPlacaBase = auxProcesadorPlacaBase.Where(x => (double)x.CantidadMemoriaRam >= tipoComputer.Ram.Capacidad.Min).OrderByDescending(x => x.CantidadMemoriaRam).ToList();
            List<Almacenamiento> almacenamientos = AlmacenamientoRecomendados(tipoComputer).OrderByDescending(x => x.PrecioUnidad).ToList();
            List<List<Almacenamiento>> almacenamientoConcat = CombinarAlmacenamiento(almacenamientos, tipoComputer.Almacenamiento.Capacidad.Min, tipoComputer.Almacenamiento.Capacidad.Max)
                .Select(x =>
                {
                    var f = 0;
                    foreach (var item in x)
                    {
                        f += item.Capacidad;
                    }
                    if (f < tipoComputer.Almacenamiento.Capacidad.Min)
                    {
                        x = null;
                    }
                    return x;
                }).Where(x => !(x is null)).ToList();
            List<Monitor> monitores = MonitoresRecomendados(tipoComputer).Where(x => x.PrecioUnidad >= tipoComputer.Monitor.PrecioUnidad.Min).OrderBy(x => x.PrecioUnidad).ToList();
            List<Gabinete> gabinetes = GabinetesRecomendados(tipoComputer).Where(x => x.PrecioUnidad >= tipoComputer.Gabinete.PrecioUnidad.Min).OrderBy(x => x.PrecioUnidad).ToList();
            List<Grafica> graficas = GraficasRecomendados(tipoComputer);
            if (graficas.Count > 0)
            {
                graficas = graficas.Where(x => x.PrecioUnidad >= tipoComputer.TarjetaGrafica.PrecioUnidad.Min).OrderBy(x => x.PrecioUnidad).ToList();
            }



            List<Computadora> fuentesGraficasProceMotherRam = FuentesRecomendados(tipoComputer, graficas, auxProcesadorPlacaBase, almacenamientoConcat)
                .Where(x => x.ConsumoEstimado <= x.Fuente.Potencia
                && x.CostoTotal < presupuesto).OrderBy(x => x.CostoTotal)
                .ToList();

            Decimal partialFraction = new Decimal(0.3);
            if (fuentesGraficasProceMotherRam.Count > 0)
            {
                decimal minByPresupuesto = presupuesto;
                decimal precioAlto = fuentesGraficasProceMotherRam.Last().CostoTotal;
                decimal precioBajo = fuentesGraficasProceMotherRam.First().CostoTotal;

                if ((1 - (precioBajo / precioAlto)) < partialFraction)
                {
                    fuentesGraficasProceMotherRam = fuentesGraficasProceMotherRam.Where(x => x.CostoTotal <= precioBajo + (precioBajo * partialFraction)).ToList();
                }
                else
                {
                    fuentesGraficasProceMotherRam = fuentesGraficasProceMotherRam.Where(x => x.CostoTotal >= precioAlto - (precioAlto * partialFraction)).ToList();
                }
            }
            auxProcesadorPlacaBase = null;
            List<Computadora> nuevita = new List<Computadora>();
            foreach (var nuevaFuenteGraficaProMbRam in fuentesGraficasProceMotherRam)
            {
                foreach (var moni in monitores)
                {
                    foreach (var gabi in gabinetes)
                    {
                        decimal costeParcial = nuevaFuenteGraficaProMbRam.CostoTotal + moni.PrecioUnidad + gabi.PrecioUnidad;
                        if (costeParcial <= presupuesto && costeParcial >= tipoComputer.CostoMinimo)
                        {
                            Computadora nuevitaC = new Computadora()
                            {
                                Almacenamientos = nuevaFuenteGraficaProMbRam.Almacenamientos,
                                Gabinete = gabi,
                                Monitor = moni,
                                Rams = nuevaFuenteGraficaProMbRam.Rams,
                                Procesador = nuevaFuenteGraficaProMbRam.Procesador,
                                PlacaBase = nuevaFuenteGraficaProMbRam.PlacaBase,
                                Fuente = nuevaFuenteGraficaProMbRam.Fuente,
                                TarjetaGrafica = nuevaFuenteGraficaProMbRam.TarjetaGrafica
                            };
                            nuevita.Add(nuevitaC);
                            nuevitaC = null;
                        }
                    }
                }
            }
            almacenamientos = null;
            almacenamientoConcat = null;
            monitores = null;
            gabinetes = null;
            graficas = null;
            fuentesGraficasProceMotherRam = null;
            procesadors = null;
            procesadors = null;
            GC.Collect();
            nuevita = nuevita
                .OrderBy(x => x.CostoTotal)
                .ToList();

            return nuevita;
        }
        private static List<Procesador> ProcesadoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Procesador> procesadors = configXD.ProcesadoresRecomendados(tipoComputer.Procesador);
            if (procesadors is null)
            {
                return new List<Procesador>();
            }
            return procesadors;
        }
        private static List<PlacaBase> PlacasBasesRecomendadas(Requirements.TipoComputer tipoComputer, Procesador procesador)
        {
            List<PlacaBase> placaBases = configXD.PlacaBaseRecomendados(tipoComputer.PlacaBase, procesador);
            if (placaBases is null)
            {
                return new List<PlacaBase>();
            }
            return placaBases;
        }
        private static List<Ram> RamsRecomdadas(Requirements.TipoComputer tipoComputer)
        {
            List<Ram> rams = configXD.RamsRecomendados(tipoComputer.Ram);
            if (rams is null)
            {
                return new List<Ram>();
            }
            return rams;
        }
        private static List<Grafica> GraficasRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Grafica> graficas = configXD.TarjetaGraficaRecomendados(tipoComputer.TarjetaGrafica);
            if (graficas is null)
            {
                return new List<Grafica>();
            }
            return graficas;
        }
        private static List<Almacenamiento> AlmacenamientoRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Almacenamiento> almacenamientos = configXD.AlmacenamientoRecomendados(tipoComputer.Almacenamiento);
            if (almacenamientos is null)
            {
                return new List<Almacenamiento>();
            }
            return almacenamientos;
        }
        private static List<Monitor> MonitoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Monitor> monitores = configXD.MonitorRecomendados(tipoComputer.Monitor);
            if (monitores is null)
            {
                return new List<Monitor>();
            }
            return monitores;
        }
        private static List<Gabinete> GabinetesRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Gabinete> gabinetes = configXD.GabinetesRecomendados(tipoComputer.Gabinete);
            if (gabinetes is null)
            {
                return new List<Gabinete>();
            }
            return gabinetes;
        }
        private static List<Computadora> FuentesRecomendados(Requirements.TipoComputer tipoComputer, List<Grafica> graficas, List<Computadora> placaProceRam, List<List<Almacenamiento>> almacenamientos)
        {
            List<Computadora> compis = new List<Computadora>();
            List<Fuente> fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
            List<Computadora> computadorita = new List<Computadora>();
            if (!(fuentes is null))
            {
                fuentes = fuentes.Where(x => x.PrecioUnidad >= tipoComputer.Fuente.PrecioUnidad.Min).ToList();
                if (tipoComputer.TarjetaGrafica.PrecioUnidad.Min == 0)
                {
                    foreach (var fuentesita in fuentes)
                    {
                        compis.Add(new Computadora()
                        {
                            Fuente = fuentesita,
                            TarjetaGrafica = null
                        });
                    }
                }
                if (!(graficas is null))
                {
                    foreach (var nuevaGrafica in graficas)
                    {
                        foreach (var fuentesita in fuentes)
                        {
                            compis.Add(new Computadora()
                            {
                                Fuente = fuentesita,
                                TarjetaGrafica = nuevaGrafica

                            });
                        }
                    }
                }

                foreach (var placaPorceRamX in placaProceRam)
                {
                    foreach (var graficaFuente in compis)
                    {
                        foreach (var almacenamientoX in almacenamientos)
                        {
                            computadorita.Add(new Computadora()
                            {
                                Fuente = graficaFuente.Fuente,
                                PlacaBase = placaPorceRamX.PlacaBase,
                                Procesador = placaPorceRamX.Procesador,
                                Rams = placaPorceRamX.Rams,
                                TarjetaGrafica = graficaFuente.TarjetaGrafica,
                                Almacenamientos = almacenamientoX
                            });
                        }
                    }
                }
            }
            return computadorita;
        }

        private static List<List<Almacenamiento>> CombinarAlmacenamiento(List<Almacenamiento> almacenamientos, double capacidadMin, double capacidadMax)
        {
            List<List<Almacenamiento>> almacenamientos1 = new List<List<Almacenamiento>>();
            List<Almacenamiento> almacenamientos2 = new List<Almacenamiento>();
            foreach (var item in almacenamientos)
            {
                int capacidadTotal = item.Capacidad;
                almacenamientos2.Add(item);
                if (capacidadTotal < capacidadMin || capacidadTotal < capacidadMax)
                {
                    foreach (var item1 in almacenamientos)
                    {
                        capacidadTotal += item1.Capacidad;
                        almacenamientos2.Add(item1);
                        if (capacidadTotal < capacidadMin || capacidadTotal < capacidadMax)
                        {
                            foreach (var item2 in almacenamientos)
                            {
                                capacidadTotal += item2.Capacidad;
                                almacenamientos2.Add(item2);
                                if (capacidadTotal < capacidadMin && capacidadTotal < capacidadMax)
                                {
                                    foreach (var item3 in almacenamientos)
                                    {
                                        capacidadTotal += item3.Capacidad;
                                        if (capacidadTotal >= capacidadMin && capacidadTotal <= capacidadMax)
                                        {
                                            almacenamientos2.Add(item3);
                                            almacenamientos1.Add(almacenamientos2);
                                            almacenamientos2 = new List<Almacenamiento>();
                                        }
                                    }
                                }
                                else
                                {
                                    if (item2.Capacidad <= capacidadMax)
                                    {
                                        almacenamientos1.Add(almacenamientos2);
                                    }
                                    almacenamientos2 = new List<Almacenamiento>();
                                }
                            }
                        }
                        else
                        {
                            if (item1.Capacidad <= capacidadMax)
                            {
                                almacenamientos1.Add(almacenamientos2);
                            }
                            almacenamientos2 = new List<Almacenamiento>();
                        }
                    }
                }
                else
                {
                    if (item.Capacidad <= capacidadMax)
                    {
                        almacenamientos1.Add(almacenamientos2);
                    }
                    almacenamientos2 = new List<Almacenamiento>();
                }
            }
            if (almacenamientos1 is null)
            {
                return new List<List<Almacenamiento>>();
            }
            return almacenamientos1;
        }
    }
}
