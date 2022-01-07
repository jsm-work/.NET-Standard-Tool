using System;
using System.Collections.Generic;

namespace Matlab_Function
{
    public class Matrix_Size
    {
        public Matrix_Size(int r, int c)
        {
            Row = r;
            Column = c;
        }
        public int Row { get; set; }
        public int Column { get; set; }
    }

    public static class Matlab
    {
        #region Zeros / Ones / Nums
        public static double[,] Zeros(int size, int size2 = -1)
        {
            double[,] result = null;
            if (size2 == -1)
            {
                result = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        result[i, j] = 0;

            }
            else
            { 
                result = new double[size, size2];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size2; j++)
                        result[i, j] = 0;
            }
            
            return result;
        }

        public static double[,] Ones(int size, int size2 = -1)
        {
            double[,] result = null;
            if (size2 == -1)
            {
                result = new double[size, size];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size; j++)
                        result[i, j] = 1;
            }
            else
            {
                result = new double[size, size2];
                for (int i = 0; i < size; i++)
                    for (int j = 0; j < size2; j++)
                        result[i, j] = 1;
            }
            return result;
        }

        public static double[,] Nums(int row_size, int col_size, double value)
        {
            double[,] result = new double[row_size, col_size];
            for (int i = 0; i < row_size; i++)
                for (int j = 0; j < col_size; j++)
                    result[i, j] = value;
            
            return result;
        }
        #endregion

        #region Matrix Size
        public static Matrix_Size Matrix_Size(double[,] target)
        {
            if (target.Rank == 2)
                return new Matrix_Size(target.GetLength(0), target.GetLength(1));
            else 
                return null;
        }

        public static int Matrix_Size_row(double[,] target)
        {
            int row = 0;
            int col = 0;

            if (target.Rank == 2)
            {
                row = target.GetLength(0);
                col = target.GetLength(1);
            }
            return row;
        }
        public static int Matrix_Size_col(double[,] target)
        {
            int row = 0;
            int col = 0;

            if (target.Rank == 2)
            {
                row = target.GetLength(0);
                col = target.GetLength(1);
            }
            return col;
        }
        #endregion

        #region Matrix Select
        public static double[,] Matrix_Select_Row(double[,] target, int col)
        {
            var size = Matrix_Size(target);

            if (size.Column < col)
                return null;

            double[,] result = new double[size.Row, 1];
            for (int i = 0; i < size.Row; i++)
            {
                result[i, 0] = target[i, col];
            }

            return result;
        }
        public static double[,] Matrix_Select_Col(double[,] target, int row)
        {
            var size = Matrix_Size(target);

            if (size.Row < row)
                return null;

            double[,] result = new double[1, size.Column];
            for (int i = 0; i < size.Column; i++)
                result[0, i] = target[row, i];

            return result;
        }
        #endregion

        #region Floor - 내림 (음수 무한대 방향으로 가장 가까운 정수로 내림합니다.)
        /// <summary>
        /// 음수 무한대 방향으로 가장 가까운 정수로 내림합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double[,] Floor(double[,] target)
        {
            var size = Matrix_Size(target);

            for (int i = 0; i < size.Row; i++)
                for (int j = 0; j < size.Column; j++)
                    target[i, j] = Math.Floor(target[i, j]);
            return target;
        }

        /// <summary>
        /// 음수 무한대 방향으로 가장 가까운 정수로 내림합니다.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double Floor(double target)
        {
            return Math.Floor(target);
        }
        #endregion

        public static double Rem(double num1, double num2)
        {
            return num1 % num2; 
        }

        public static double[,] Rem(double[,] target, int value)
        {
            var size = Matrix_Size(target);
            for (int i = 0; i < size.Row; i++)
                for (int j = 0; j < size.Column; j++)
                {
                    int result = 0;
                    Math.DivRem((int)target[i, j], value,out result);
                    target[i, j] = result;
                }

            return target;
        }

        public static double JulianDate(int year, int month, int day, int hour, int minute, int second, JulianDateType type = JulianDateType.JD)
        {
            int y = year;
            int m = month;
            int d = day;
            int h = hour + minute / 60 + second / 3600;
            if (y < 1901 || y > 2099)
                throw new Exception(" not in allowed range: 1900 < year < 2100");

            int i2 = (m <= 2) ? 1 : 0;

            if (m <= 2)
            {
                m += 12;
                y -= 1;
            }

            double jd = Math.Floor(365.25 * y) + Math.Floor(30.6001 * (m + 1)) - 15 + 1720996.5 + d + h / 24;
            double mjd = jd - 2400000.5;
            return (type == JulianDateType.JD) ? jd : mjd;
        }

        public static double[] JulianDate(int[,] utcTable)
        {
            if (utcTable.Length % 6 == 0)
            {
                double[] result = new double[utcTable.Length / 6];

                for (int i = 0; i < utcTable.Length / 6; i++)
                {
                    result[i] = JulianDate(utcTable[i, 0], utcTable[i, 1], utcTable[i, 2], utcTable[i, 3], utcTable[i, 4], utcTable[i, 5]);
                }

                return result;
            }
            else
                return null;
        }

        public static double LeapSeconds(int year, int month, int day, int hour, int minute, int second)
        {
            int[,] utcTable = new int[18, 6] {
               {1982, 1, 1, 0, 0, 0},
               {1982, 7, 1, 0, 0, 0},
               {1983, 7, 1, 0, 0, 0},
               {1985, 7, 1, 0, 0, 0},
               {1988, 1, 1, 0, 0, 0},
               {1990, 1, 1, 0, 0, 0},
               {1991, 1, 1, 0, 0, 0},
               {1992, 7, 1, 0, 0, 0},
               {1993, 7, 1, 0, 0, 0},
               {1994, 7, 1, 0, 0, 0},
               {1996, 1, 1, 0, 0, 0},
               {1997, 7, 1, 0, 0, 0},
               {1999, 1, 1, 0, 0, 0},
               {2006, 1, 1, 0, 0, 0},
               {2009, 1, 1, 0, 0, 0},
               {2012, 7, 1, 0, 0, 0},
               {2015, 7, 1, 0, 0, 0},
               {2017, 1, 1, 0, 0, 0}
            };
            //% when a new leap second is announced in IERS Bulletin C
            // % update the table with the UTC time right after the new leap second
            //IERS Bulletin C에 새로운 윤초가 발표되었을 때
            //새로운 윤초 직후 UTC 시간으로 표를 업데이트합니다.

            double[] tableJDays = JulianDate(utcTable);
            for (int i = 0; i < tableJDays.Length; i++) 
                tableJDays[i] -= GPSConstant.GPSEPOCHJD;



            //   % tableSeconds = tableJDays * GpsConstants.DAYSEC + utcTable(:, 4:6) *[3600; 60; 1];
            //% NOTE: JulianDay returns a realed value number, corresponding to days and
            // % fractions thereof, so we multiply it by DAYSEC to get the full time in seconds
            double[] tableSeconds = new double[18];
            for (int i = 0; i < tableJDays.Length; i++)
                tableSeconds[i] = tableJDays[i] * GPSConstant.DAYSEC;

            double jDays = JulianDate(year, month, day, hour, minute, second) - GPSConstant.GPSEPOCHJD; //% days since GPS Epoch
            //              % timeSeconds = jDays * GpsConstants.DAYSEC + utcTime(:, 4:6) *[3600; 60; 1];
            double timeSeconds = jDays * GPSConstant.DAYSEC;
            //% tableSeconds and timeSeconds now contain number of seconds since the GPS epoch

            int leapSecs = 0;
            foreach (var item in tableSeconds)
            {
                leapSecs += item <= timeSeconds ? 1 : 0;
            }
            return leapSecs;
        }


        public static double[] Find(double[,] target, Operator oper, int value)
        {
            System.Collections.Generic.List<double> result = new System.Collections.Generic.List<double>();

            int i = 1;
            foreach (var item in target)
            {
                switch (oper)
                {
                    case Operator.Inequality_Left:
                        if (item > value)
                            result.Add(i);
                        break;
                    case Operator.Inequality_Right:
                        if (item < value)
                            result.Add(i);
                        break;
                }
                i++;
            }

            return result.ToArray();
        }

        /// <summary>
        /// 외적
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double[,] Cross(double[,] v1, double[,] v2)
        {
            try
            {
                var size1 = Matlab.Matrix_Size(v1);
                var size2 = Matlab.Matrix_Size(v2);

                if (size1.Row != size2.Row && size1.Column != size2.Column)
                    throw new Exception("행렬의 크기가 동일해야 합니다.");

                double[] _v1 = new double[3] { v1[0, 0], v1[1, 0] , v1[2, 0] };
                double[] _v2 = new double[3] { v2[0, 0], v2[1, 0] , v2[2, 0] };

                double x, y, z;
                x = _v1[1] * _v2[2] - _v2[1] * _v1[2];
                y = (_v1[0] * _v2[2] - _v2[0] * _v1[2]) * -1;
                z = _v1[0] * _v2[1] - _v2[0] * _v1[1];

                return new double[1, 3] { { x, y, z } };
            }
            catch (System.IndexOutOfRangeException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 전치 행렬
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static double[,] Transposed(double[,] target)
        {
            int r = target.GetLength(0);
            int c = target.GetLength(1);
            double[,] result = new double[c, r];
            for (int i = 0; i < c; i++)
            {
                for (int j = 0; j < r; j++)
                {

                    result[i, j] = target[j, i];
                }
            }
            return result;
        }

        //        public static double JulianDate_To_LeapSEC(int tsec, double julianDate)
        //        {
        //            //function leapsec = SubGPSLeap(tsec, mjd)
        //            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //            //% 윤초(Leap seconds)를 계산하는 함수
        //            //% --------------------------------------------------------------------------
        //            //% INPUT
        //            //% jd          : Time of seconds(0~86400)
        //            //% --------------------------------------------------------------------------
        //            //% OUTPUT
        //            //% leapsec     : 윤초
        //            //% --------------------------------------------------------------------------
        //            //% Notes
        //            //% -input time is GPS seconds --initialized by setjd0()
        //            //% -does * *NOT * * return the full TAI-UTC delta
        //            //  % -Y2K-- only functional between 1980jan06 - 00:00:00(GPS time start)
        //            //  % and hard - coded date
        //            //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        //            if (julianDate > 2400000.5)
        //                julianDate -= 2400000.5;



        //            while (tsec >= 86400)
        //            { 
        //                tsec = tsec - 86400;
        //                julianDate = julianDate + 1;
        //            }

        //            while (tsec < 0)
        //            { 
        //                tsec = tsec + 86400;
        //                julianDate = julianDate - 1;
        //            }

        //            if (julianDate < 44244)
        ////                keyboard;

        ////if (mjd >= 57754)        % 2017 JAN 1 = 57754
        ////tai_utc = 37;
        ////            elseif(mjd >= 57204) % 2015 JUL 1 = 57204
        ////tai_utc = 36;
        ////            elseif(mjd >= 56109) % 2012 JUL 1 = 56109
        ////tai_utc = 35;
        ////            elseif(mjd >= 54832) % 2009 JAN 1 = 54832
        ////tai_utc = 34;
        ////            elseif(mjd >= 53736) % 2006 JAN 1 = 53736
        ////tai_utc = 33;
        ////            elseif(mjd >= 51179) % 1999 JAN 1 = 51179
        ////tai_utc = 32;
        ////            elseif(mjd >= 50630) % 1997 JUL 1 = 50630
        ////tai_utc = 31;
        ////            elseif(mjd >= 50083) % 1996 JAN 1 = 50083
        ////tai_utc = 30;
        ////            elseif(mjd >= 49534) % 1994 JUL 1 = 49534
        ////tai_utc = 29;
        ////            elseif(mjd >= 49169) % 1993 JUL 1 = 49169
        ////tai_utc = 28;
        ////            elseif(mjd >= 48804) % 1992 JUL 1 = 48804
        ////tai_utc = 27;
        ////            elseif(mjd >= 48257) % 1991 JAN 1 = 48257
        ////tai_utc = 26;
        ////            elseif(mjd >= 47892) % 1990 JAN 1 = 47892
        ////tai_utc = 25;
        ////            elseif(mjd >= 47161) % 1988 JAN 1 = 47161
        ////tai_utc = 24;
        ////            elseif(mjd >= 46247) % 1985 JUL 1 = 46247
        ////tai_utc = 23;
        ////            elseif(mjd >= 45516) % 1983 JUL 1 = 45516
        ////tai_utc = 22;
        ////            elseif(mjd >= 45151) % 1982 JUL 1 = 45151
        ////tai_utc = 21;
        ////            elseif(mjd >= 44786) % 1981 JUL 1 = 44786
        ////tai_utc = 20;
        ////            elseif(mjd >= 44239) % 1980 JAN 1 = 44239
        ////tai_utc = 19;

        ////% should never get here

        ////else
        ////                keyboard;
        ////            end

        ////            % convert TAI - UTC into leap seconds

        ////              leapsec = tai_utc - 19;

        //        }




    }

    public enum Operator
    {
        /// <summary>
        /// >
        /// </summary>
        Inequality_Left,
        /// <summary>
        /// <
        /// </summary>
        Inequality_Right
    }
}
