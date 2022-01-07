using System;
using System.Collections.Generic;
using System.Text;

namespace Matlab_Function
{
    public static class GPSConstant
    {
        /// <summary>Clock relativity parameter, (s/m^1/2)</summary>
        public static double FREL = -4.442807633e-10;

        /// <summary>WGS84 Earth's gravitational constant for GPS user [m^3/s^2]</summary>
        public static double GM = Math.Pow(3.986005 * 10, 14);

        /// <summary>WGS84 Rate of Right Acsention in semi-circle/sec</summary>
        public static double OMEGADOTe = Math.Pow(7.2921151467 * 10, -5);

        /// <summary>Equatorial Radius(meter)</summary>
        public static double E_Re = 6378137.0;

        /// <summary>Speed of Light in m/sec</summary>
        public static double Light_Speed = 299792458;

        /// <summary>Radian to Degree</summary>
        public static double R2D = 180 / Math.PI;

        /// <summary>Degree to Radian</summary>
        public static double D2R = Math.PI / 180;

        /// <summary>Pi* 2</summary>
        public static double PI2 = Math.PI * 2;

        /// <summary>GPS(PRN01 ~PRN32)</summary>
        public static double MAXGPS = 32;

        /// <summary>Galileo(PRN01 ~PRN32)</summary>
        public static double MAXGAL = 36;

        /// <summary>Julian date(1980.1.6)</summary>
        public static double JAN61980 = 44244;

        /// <summary>Julian date(1980.1.6)</summary>
        public static double GPSEPOCHJD = 2444244.5;

        /// <summary>Julian date(1901.1.1)</summary>
        public static double JAN11901 = 15385;

        public static double SEC_PER_DAY = 86400;

        public static double Fe = Math.Pow(-4.442807633 * 10, -10);

        /// <summary>a numerical eccentricity</summary>
        public static double e2 = 6.69438002290341574957e-3;

        /// <summary>GPS L1 frequency</summary>
        public static double GPSL1FREQ = 1.57542e9;
        /// <summary>GPS L2 frequency</summary>
        public static double GPSL2FREQ = 1.22760e9;
        /// <summary>GPS L1 lambda</summary>
        public static double GPSL1LAMBDA = 0.19029367279836;
        /// <summary>GPS L2 lambda</summary>
        public static double GPSL2LAMBDA = 0.24421021342457;
        /// <summary></summary>
        public static double gamma = GPSL1FREQ / GPSL2FREQ;

        public static double MINSEC = 60;
        public static double HOURSEC = 3600;
        public static double DAYSEC = 86400;
        public static double WEEKSEC = 604800;

    }
}
