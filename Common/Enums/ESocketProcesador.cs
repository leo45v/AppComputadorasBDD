using System;
using System.Collections.Generic;
using System.Text;

namespace Univalle.Fie.Sistemas.BaseDeDatos2.AppComputadorasBDD.Common.Enums
{
    public enum ESocketProcesador
    {
        Ninguno = 0,
        /// <summary>
        /// Intel Xeon <br/>
        /// Año 2002
        /// </summary>
        Socket_604_2002 = 1,
        /// <summary>
        /// AMD Athlon 64 <br/>
        /// AMD Sempron <br/>
        /// AMD Turion 64 <br/>
        /// Año 2003
        /// </summary>
        Socket_754_2003 = 2,
        /// <summary>
        /// INTEL 2da GENERACION <br/>
        /// Año 2011
        /// </summary>
        LGA_1155_2011 = 102,
        /// <summary>
        /// INTEL 3ra GENERACION <br/>
        /// Año 2012
        /// </summary>
        LGA_1155_2012 = 103,
        /// <summary>
        /// INTEL 4ta GENERACION <br/>
        /// Año 2013
        /// </summary>
        LGA_1150_2013 = 104,
        /// <summary>
        /// INTEL 5ta GENERACION <br/>
        /// Año 2014
        /// </summary>
        LGA_1150_2014 = 105,
        /// <summary>
        /// INTEL 6ta GENERACION <br/>
        /// Año 2015
        /// </summary>
        LGA_1151_2015 = 106,
        /// <summary>
        /// INTEL 7ma GENERACION <br/>
        /// Año 2016
        /// </summary>
        LGA_1151_2016 = 107,
        /// <summary>
        /// INTEL 8va GENERACION <br/>
        /// Año 2017
        /// </summary>
        LGA_1151_2017 = 108,
        /// <summary>
        /// INTEL 9na GENERACION <br/>
        /// Año 2018
        /// </summary>
        LGA_1151_2018 = 109,
        /// <summary>
        /// INTEL 10ma GENERACION <br/>
        /// Año 2020
        /// </summary>
        LGA_1200_2020 = 110,



        ///->
        /// INTEL X START IN 200
        /// 


        /// <summary>
        /// AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS) <br/>
        /// NO OVERCLOCK <br/>
        /// Año 2017 
        /// </summary>
        AM4_A320 = 300,
        /// <summary>
        /// AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS) <br/>
        /// Año 2017 
        /// </summary>
        AM4_B350 = 301,
        /// <summary>
        /// AMD SOPORTE ZEN, ZEN+, ZEN 2 (ALGUNOS) <br/>
        /// Año 2017 
        /// </summary>
        AM4_X370 = 302,
        /// <summary>
        /// AMD SOPORTE ZEN, ZEN+, ZEN 2, ZEN 3 (ALGUNOS) <br/>
        /// Año 2018
        /// </summary>
        AM4_B450 = 303,
        /// <summary>
        /// AMD SOPORTE ZEN, ZEN+, ZEN 2, ZEN 3 (ALGUNOS) <br/>
        /// Año 2018
        /// </summary>
        AM4_X470 = 304,
        /// <summary>
        /// AMD SOPORTE ZEN 2, ZEN 3 <br/>
        /// NO OVERCLOCK <br/>
        /// Año 2020
        /// </summary>
        AM4_A520 = 305,
        /// <summary>
        /// AMD SOPORTE ZEN 2, ZEN 3 <br/>
        /// Año 2020
        /// </summary>
        AM4_B550 = 306,
        /// <summary>
        /// AMD SOPORTE ZEN+, ZEN 2, ZEN 3 <br/>
        /// Año 2019
        /// </summary>
        AM4_X570 = 307,



        /// <summary>
        /// AMD Threadripper GEN 1 <br/>
        /// Año 2017
        /// </summary>
        sTR4_2017 = 400,
        /// <summary>
        /// AMD Threadripper GEN 2 <br/>
        /// Año 2018
        /// </summary>
        sTR4_2018 = 401,
        /// <summary>
        /// AMD Threadripper GEN 3 <br/>
        /// Año 2020
        /// </summary>
        sTRX4_2020 = 402
    }
}
