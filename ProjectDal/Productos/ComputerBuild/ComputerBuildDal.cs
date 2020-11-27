using System;
using System.Collections.Generic;
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
        private static ConfiguracionComputerOperation configXD = new ConfiguracionComputerOperation(presupuesto);

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
        private static List<Computadora> FuentesRecomendados(Requirements.TipoComputer tipoComputer, double potenciaActual, List<Grafica> graficas)
        {
            List<Computadora> compis = new List<Computadora>();
            double potenciaActuali = tipoComputer.Fuente.Potencia.min;
            if (potenciaActual > potenciaActuali)
            {
                potenciaActuali = potenciaActual;
            }
            tipoComputer.Fuente.Potencia.min = potenciaActuali;
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
                    if ((potenciaActual + nuevaGrafica.Consumo) > potenciaActuali)
                    {
                        potenciaActuali = potenciaActual + nuevaGrafica.Consumo;
                    }
                    tipoComputer.Fuente.Potencia.min = potenciaActuali;
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
                    auxProcesadorPlacaBase.Add(new Computadora()
                    {
                        Procesador = procesador,
                        PlacaBase = placaBase
                    });
                }
            }
            List<Ram> rams = RamsRecomdadas(tipoComputer);
            List<List<Ram>> ramsConact = new List<List<Ram>>();
            foreach (var ramNew in rams)
            {
                double cantidadRams = tipoComputer.Ram.Cantidad.min;
                if (((double)ramNew.PrecioUnidad * 2.0) <= tipoComputer.Ram.PrecioUnidad.max)
                {
                    if ((ramNew.Memoria * 2) <= tipoComputer.Ram.Capacidad.max)
                    {
                        cantidadRams++;
                    }
                }
                List<Ram> nuevasRams = new List<Ram>();
                for (int i = 0; i < cantidadRams; i++)
                {
                    nuevasRams.Add(ramNew);
                }
                ramsConact.Add(nuevasRams);
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
                foreach (var ramsita in ramsConact)
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
                                    Rams = ramsita,
                                    Procesador = procePlaca.Procesador,
                                    PlacaBase = procePlaca.PlacaBase
                                };
                                nuevita.Add(nuevitaC);
                            }
                        }
                    }
                }
            }
            List<Computadora> compusFinales = new List<Computadora>();
            foreach (var compu in nuevita)
            {
                double presupuestoGrafica = presupuesto - compu.CostoTotal;
                if (tipoComputer.TarjetaGrafica.PrecioUnidad.max < presupuestoGrafica)
                {
                    tipoComputer.TarjetaGrafica.PrecioUnidad.max = presupuestoGrafica;//10% extra
                }
                if (presupuestoGrafica <= 0)
                {
                    tipoComputer.TarjetaGrafica.PrecioUnidad.max = 0;
                }
                List<Grafica> graficas = null;
                if (tipoComputer.TarjetaGrafica.PrecioUnidad.max > 0)
                {
                    graficas = GraficasRecomendados(tipoComputer);
                }
                double potenciaActual = compu.Procesador.Consumo + (24.0 * compu.Rams.Count);//-> SUmar tarjeta grafica
                List<Computadora> fuentesGraficas = FuentesRecomendados(tipoComputer, potenciaActual, graficas);
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
            }
            return compusFinales;
        }
    }
}
