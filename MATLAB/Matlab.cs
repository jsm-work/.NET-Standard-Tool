using System;

namespace Matlab_Function
{
    public static class Matlab
    {
        #region Zeros / Ones
        public static double[,] Zeros(int size)
        {
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0;
                }
            }
            return result;
        }
        public static double[,] Zeros(int size1, int size2)
        {
            double[,] result = new double[size1, size2];
            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    result[i, j] = 0;
                }
            }
            return result;
        }


        public static double[,] Ones(int size)
        {
            double[,] result = new double[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 1;
                }
            }
            return result;
        }
        public static double[,] Ones(int size1, int size2)
        {
            double[,] result = new double[size1, size2];
            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    result[i, j] = 1;
                }
            }
            return result;
        }

        /// <summary>
        /// 지정한 크기로 행렬을 생성 후 입력된 값으로 모든 항목을 초기화 한다.
        /// </summary>
        /// <param name="row_size"></param>
        /// <param name="col_size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double[,] Nums(int row_size, int col_size, double value)
        {
            double[,] result = new double[row_size, col_size];
            for (int i = 0; i < row_size; i++)
            {
                for (int j = 0; j < col_size; j++)
                {
                    result[i, j] = value;
                }
            }
            return result;
        }
        #endregion

        #region Matrix Size
        public static void Matrix_Size(double[,] target, ref int row, ref int col)
        {
            if (target.Rank == 2)
            {
                row = target.GetLength(0);
                col = target.GetLength(1);
            }
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
            int size_col = 0, size_row = 0;
            Matrix_Size(target, ref size_row, ref size_col);

            if (size_col < col)
                return null;

            double[,] result = new double[size_row, 1];
            for (int i = 0; i < size_row; i++)
            {
                result[i, 0] = target[i, col];
            }

            return result;
        }
        public static double[,] Matrix_Select_Col(double[,] target, int row)
        {
            int size_col = 0, size_row = 0;
            Matrix_Size(target, ref size_row, ref size_col);

            if (size_row < row)
                return null;

            double[,] result = new double[1, size_col];
            for (int i = 0; i < size_col; i++)
            {
                result[0, i] = target[row, i];
            }

            return result;
        }
        #endregion


        public static double[,] Floor(double[,] target)
        {
            int r = 0, c = 0;

            Matrix_Size(target, ref r, ref c);

            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    target[i, j] = Math.Floor(target[i, j]);
                }
            }
            return target;
        }

        public static double[,] Rem(double[,] target, int value)
        {
            int r = 0, c = 0;

            Matrix_Size(target, ref r, ref c);
            for (int i = 0; i < r; i++)
            {
                for (int j = 0; j < c; j++)
                {
                    int result = 0;
                    Math.DivRem((int)target[i, j], value,out result);
                    target[i, j] = result;
                }
            }
            

            return target;
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
