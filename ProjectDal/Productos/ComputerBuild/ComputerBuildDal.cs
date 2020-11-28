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
        private static double presupuesto;
        public static double Presupuesto
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

        private static List<Procesador> ProcesadoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Procesador> procesadors = configXD.ProcesadoresRecomendados(tipoComputer.Procesador);
            while (procesadors is null)
            {
                tipoComputer.Procesador.PrecioUnidad.max *= 1.20;//AUMENTAMOS EL MAXIMO en un 20%
                procesadors = configXD.ProcesadoresRecomendados(tipoComputer.Procesador);
            }
            return procesadors;
        }
        private static List<PlacaBase> PlacasBasesRecomendadas(Requirements.TipoComputer tipoComputer, Procesador procesador)
        {
            List<PlacaBase> placaBases = configXD.PlacaBaseRecomendados(tipoComputer.PlacaBase, procesador);
            while (placaBases is null)
            {
                tipoComputer.PlacaBase.PrecioUnidad.max *= 1.1;//AUMENTAMOS EL MAXIMO en un 20%
                placaBases = configXD.PlacaBaseRecomendados(tipoComputer.PlacaBase, procesador);
            }
            return placaBases;
        }
        private static List<Ram> RamsRecomdadas(Requirements.TipoComputer tipoComputer)
        {
            List<Ram> rams = configXD.RamsRecomendados(tipoComputer.Ram);
            while (rams is null)
            {
                tipoComputer.Ram.PrecioUnidad.max *= 1.1;//10% extra
                rams = configXD.RamsRecomendados(tipoComputer.Ram);
            }
            return rams;
        }
        private static List<Almacenamiento> AlmacenamientoRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Almacenamiento> almacenamientos = configXD.AlmacenamientoRecomendados(tipoComputer.Almacenamiento);
            while (almacenamientos is null)
            {
                tipoComputer.Almacenamiento.PrecioUnidad.max *= 1.1;//10% extra
                almacenamientos = configXD.AlmacenamientoRecomendados(tipoComputer.Almacenamiento);
            }
            return almacenamientos;
        }
        private static List<Monitor> MonitoresRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Monitor> monitores = configXD.MonitorRecomendados(tipoComputer.Monitor);
            while (monitores is null)
            {
                tipoComputer.Monitor.PrecioUnidad.max *= 1.10;//10% extra
                monitores = configXD.MonitorRecomendados(tipoComputer.Monitor);
            }
            return monitores;
        }
        private static List<Gabinete> GabinetesRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Gabinete> gabinetes = configXD.GabinetesRecomendados(tipoComputer.Gabinete);
            while (gabinetes is null)
            {
                tipoComputer.Gabinete.PrecioUnidad.max *= 1.10;//10% extra
                gabinetes = configXD.GabinetesRecomendados(tipoComputer.Gabinete);
            }
            return gabinetes;
        }
        private static List<Computadora> FuentesRecomendados(Requirements.TipoComputer tipoComputer, List<Grafica> graficas)
        {
            List<Computadora> compis = new List<Computadora>();
            List<Fuente> fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
            while (fuentes is null)
            {
                tipoComputer.Fuente.PrecioUnidad.max *= 1.1;//10% extra
                fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
            }
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
                    List<Fuente> fuentesExtra = configXD.FuenteRecomendados(tipoComputer.Fuente);
                    while (fuentesExtra is null)
                    {
                        tipoComputer.Fuente.PrecioUnidad.max *= 1.1;//10% extra
                        fuentes = configXD.FuenteRecomendados(tipoComputer.Fuente);
                    }
                    foreach (var fuentesita in fuentesExtra)
                    {
                        compis.Add(new Computadora()
                        {
                            Fuente = fuentesita,
                            TarjetaGrafica = nuevaGrafica

                        });
                    }
                }
            }
            return compis;
        }
        private static List<Grafica> GraficasRecomendados(Requirements.TipoComputer tipoComputer)
        {
            List<Grafica> graficas = configXD.TarjetaGraficaRecomendados(tipoComputer.TarjetaGrafica);
            if (tipoComputer.TarjetaGrafica.PrecioUnidad.min != 0)
            {
                while (graficas is null)
                {
                    tipoComputer.TarjetaGrafica.PrecioUnidad.max *= 1.1;//10% extra
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
                    //List<List<Ram>> ramsConact = new List<List<Ram>>();
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
                        //ramsConact.Add(nuevasRams);
                        auxProcesadorPlacaBase.Add(new Computadora()
                        {
                            Procesador = procesador,
                            PlacaBase = placaBase,
                            Rams = nuevasRams
                        });
                    }
                }
            }

            List<Almacenamiento> almacenamientos = AlmacenamientoRecomendados(tipoComputer);
            List<List<Almacenamiento>> almacenamientoConcat = new List<List<Almacenamiento>>();
            foreach (var nuevoAlmacenamiento in almacenamientos)
            {
                double verificarCapacidad = tipoComputer.Almacenamiento.Capacidad.min;
                double verificarCapacidadMax = tipoComputer.Almacenamiento.Capacidad.max;
                double capacidadEntre2 = nuevoAlmacenamiento.Capacidad;
                List<Almacenamiento> nuevitoAlmacenamiento = new List<Almacenamiento>();
                nuevitoAlmacenamiento.Add(nuevoAlmacenamiento);
                while (verificarCapacidad > capacidadEntre2)
                {
                    foreach (var nuevo2AL in almacenamientos)
                    {
                        double aux = capacidadEntre2 + nuevo2AL.Capacidad;
                        if (verificarCapacidad <= aux && aux <= verificarCapacidadMax)
                        {
                            capacidadEntre2 += nuevo2AL.Capacidad;
                            nuevitoAlmacenamiento.Add(nuevo2AL);
                        }
                    }
                }
                almacenamientoConcat.Add(nuevitoAlmacenamiento);
            }
            List<Monitor> monitores = MonitoresRecomendados(tipoComputer);
            List<Gabinete> gabinetes = GabinetesRecomendados(tipoComputer);
            List<Computadora> nuevita = new List<Computadora>();
            foreach (var procePlaca in auxProcesadorPlacaBase)//JUNTAR TODO->
            {
                foreach (var alma in almacenamientoConcat)
                {
                    foreach (var moni in monitores)
                    {
                        foreach (var gabi in gabinetes)
                        {
                            Computadora nuevitaC = new Computadora()
                            {
                                Almacenamientos = alma,
                                Gabinete = gabi,
                                Monitor = moni,
                                Rams = procePlaca.Rams,
                                Procesador = procePlaca.Procesador,
                                PlacaBase = procePlaca.PlacaBase
                            };
                            nuevita.Add(nuevitaC);
                        }
                    }
                }
            }
            List<Grafica> graficas = GraficasRecomendados(tipoComputer);
            List<Computadora> compusFinales = new List<Computadora>();
            List<Computadora> fuentesGraficas = FuentesRecomendados(tipoComputer, graficas);
            nuevita = nuevita.Where(x => x.CostoTotal <= presupuesto && x.CostoTotal >= tipoComputer.CostoMinimo).ToList();
            nuevita = nuevita.Where(x => (double)x.CantidadMemoriaRam >= tipoComputer.Ram.Capacidad.min).OrderByDescending(x => x.CantidadMemoriaRam).ToList();
            double cantidadesCOUNT = 0;
            foreach (var compu in nuevita)
            {
                foreach (var nuevaFuente in fuentesGraficas)
                {
                    compusFinales.Add(new Computadora()
                    {
                        Almacenamientos = compu.Almacenamientos,
                        Fuente = nuevaFuente.Fuente,
                        Gabinete = compu.Gabinete,
                        Monitor = compu.Monitor,
                        PlacaBase = compu.PlacaBase,
                        Procesador = compu.Procesador,
                        Rams = compu.Rams,
                        TarjetaGrafica = nuevaFuente.TarjetaGrafica
                    });


                }
                cantidadesCOUNT++;
            }
            compusFinales = compusFinales.Where(x =>
            x.ConsumoEstimado <= x.Fuente.Potencia).OrderBy(x => x.CostoTotal).ToList();
            return compusFinales;
        }
    }
}
