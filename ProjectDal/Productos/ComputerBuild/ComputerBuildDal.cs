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
        private static readonly decimal increaseSearch =  new Decimal(1.10);
        private static readonly ConfiguracionComputerOperation configXD = new ConfiguracionComputerOperation(presupuesto);

        private static List<Procesador> ProcesadoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Procesador> procesadors = configXD.ProcesadoresRecomendados(tipoComputer.Procesador);
            while (procesadors is null)
            {
                tipoComputer.Procesador.PrecioUnidad.max *= increaseSearch;//AUMENTAMOS EL MAXIMO en un 20%
                procesadors = configXD.ProcesadoresRecomendados(tipoComputer.Procesador);
            }
            return procesadors;
        }
        private static List<PlacaBase> PlacasBasesRecomendadas(Requirements.TipoComputer tipoComputer, Procesador procesador)
        {
            List<PlacaBase> placaBases = configXD.PlacaBaseRecomendados(tipoComputer.PlacaBase, procesador);
            while (placaBases is null)
            {
                tipoComputer.PlacaBase.PrecioUnidad.max *= increaseSearch;//AUMENTAMOS EL MAXIMO en un 20%
                placaBases = configXD.PlacaBaseRecomendados(tipoComputer.PlacaBase, procesador);
            }
            return placaBases;
        }
        private static List<Ram> RamsRecomdadas(Requirements.TipoComputer tipoComputer)
        {
            List<Ram> rams = configXD.RamsRecomendados(tipoComputer.Ram);
            while (rams is null)
            {
                tipoComputer.Ram.PrecioUnidad.max *= increaseSearch;//10% extra
                rams = configXD.RamsRecomendados(tipoComputer.Ram);
            }
            return rams;
        }
        private static List<Almacenamiento> AlmacenamientoRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Almacenamiento> almacenamientos = configXD.AlmacenamientoRecomendados(tipoComputer.Almacenamiento);
            while (almacenamientos is null)
            {
                tipoComputer.Almacenamiento.PrecioUnidad.max *= increaseSearch;//10% extra
                almacenamientos = configXD.AlmacenamientoRecomendados(tipoComputer.Almacenamiento);
            }
            return almacenamientos;
        }
        private static List<Monitor> MonitoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Monitor> monitores = configXD.MonitorRecomendados(tipoComputer.Monitor);
            while (monitores is null)
            {
                tipoComputer.Monitor.PrecioUnidad.max *= increaseSearch;//10% extra
                monitores = configXD.MonitorRecomendados(tipoComputer.Monitor);
            }
            return monitores;
        }
        private static List<Gabinete> GabinetesRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Gabinete> gabinetes = configXD.GabinetesRecomendados(tipoComputer.Gabinete);
            while (gabinetes is null)
            {
                tipoComputer.Gabinete.PrecioUnidad.max *= increaseSearch;//10% extra
                gabinetes = configXD.GabinetesRecomendados(tipoComputer.Gabinete);
            }
            return gabinetes;
        }
        private static List<Computadora> FuentesRecomendados(Requirements.TipoComputer tipoComputer, List<Grafica> graficas, List<Computadora> placaProceRam, List<List<Almacenamiento>> almacenamientos)
        {
            List<Computadora> compis = new List<Computadora>();
            List<Fuente> fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
            while (fuentes is null)
            {
                tipoComputer.Fuente.PrecioUnidad.max *= increaseSearch;//10% extra
                fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
            }
            fuentes = fuentes.Where(x => x.PrecioUnidad >= tipoComputer.Fuente.PrecioUnidad.min).ToList();
            if (tipoComputer.TarjetaGrafica.PrecioUnidad.min == 0)
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
            List<Computadora> computadorita = new List<Computadora>();
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
            return computadorita;
        }
        private static List<Grafica> GraficasRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Grafica> graficas = configXD.TarjetaGraficaRecomendados(tipoComputer.TarjetaGrafica);
            if (tipoComputer.TarjetaGrafica.PrecioUnidad.min != 0)
            {
                while (graficas is null)
                {
                    tipoComputer.TarjetaGrafica.PrecioUnidad.max *= increaseSearch;//10% extra
                    graficas = configXD.TarjetaGraficaRecomendados(tipoComputer.TarjetaGrafica);
                }
            }
            return graficas;
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
                        while ((ramNew.Memoria * cantidadRams) <= tipoComputer.Ram.Capacidad.max && cantidadRams < cantidadRamxMax)
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
            auxProcesadorPlacaBase = auxProcesadorPlacaBase.Where(x => (double)x.CantidadMemoriaRam >= tipoComputer.Ram.Capacidad.min).OrderByDescending(x => x.CantidadMemoriaRam).ToList();
            List<Almacenamiento> almacenamientos = AlmacenamientoRecomendados(tipoComputer).OrderByDescending(x => x.PrecioUnidad).ToList();
            List<List<Almacenamiento>> almacenamientoConcat = CombinarAlmacenamiento(almacenamientos, tipoComputer.Almacenamiento.Capacidad.min, tipoComputer.Almacenamiento.Capacidad.max)
                .Select(x =>
                {
                    var f = 0;
                    foreach (var item in x)
                    {
                        f += item.Capacidad;
                    }
                    if (f < tipoComputer.Almacenamiento.Capacidad.min)
                    {
                        x = null;
                    }
                    return x;
                }).Where(x => !(x is null)).ToList();
            List<Monitor> monitores = MonitoresRecomendados(tipoComputer).Where(x => x.PrecioUnidad >= tipoComputer.Monitor.PrecioUnidad.min).OrderBy(x => x.PrecioUnidad).ToList();
            List<Gabinete> gabinetes = GabinetesRecomendados(tipoComputer).Where(x => x.PrecioUnidad >= tipoComputer.Gabinete.PrecioUnidad.min).OrderBy(x => x.PrecioUnidad).ToList();
            List<Grafica> graficas = GraficasRecomendados(tipoComputer);
            if (!(graficas is null))
            {
                graficas = graficas.Where(x => x.PrecioUnidad >= tipoComputer.TarjetaGrafica.PrecioUnidad.min).OrderBy(x => x.PrecioUnidad).ToList();
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

        private static List<List<Almacenamiento>> CombinarAlmacenamiento(List<Almacenamiento> almacenamientos, double capacidadMin, double capacidadMax)
        {
            List<List<Almacenamiento>> almacenamientos1 = new List<List<Almacenamiento>>();
            List<Almacenamiento> almacenamientos2 = new List<Almacenamiento>();
            foreach (var item in almacenamientos)
            {
                // MIN -> 1tb MAX -> 1.5tb
                // ACTUAL -> 1tb
                int capacidadTotal = item.Capacidad;
                almacenamientos2.Add(item);
                if (capacidadTotal < capacidadMin || capacidadTotal < capacidadMax)
                {
                    foreach (var item1 in almacenamientos)
                    {
                        // +1tb
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
            return almacenamientos1;
        }
    }
}
