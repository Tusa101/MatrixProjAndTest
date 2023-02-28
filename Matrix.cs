using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatrixProjAndTest
{
    public class Matrix
    {
        private static double[] _data;
        private static int _rows ;
        private static int _cols;
        public Matrix ()
        {
            _data = new double[0];
            _rows = 0;
            _cols = 0;
        }
        public Matrix(int nRows, int nCols)
        {
            _data = new double[nRows*nCols];
            _rows = nRows;
            _cols = nCols;
        }
        
        public Matrix(double[,] initData)
        {
            _rows = initData.GetLength(0);
            _cols = initData.GetLength(1);
            _data = new double[Rows * Cols];
            for (int i = 0; i < Rows ; i++)
            {
                for (int j = 0; j < Cols ; j++)
                {
                    _data[i*Cols + j] = initData[i,j];
                }
            }
            
        }
        public double this[int i, int j] 
        {
            get 
            {
                if (i < 0 || j < 0)
                    throw new IndexOutOfRangeException("Index is less than a zero.");
                if (i > Rows || j > Cols)
                    throw new IndexOutOfRangeException("Index is more than possible.");
                return _data[i * Cols + j]; 
            } 
            set 
            {
                if (i < 0 || j < 0)
                    throw new IndexOutOfRangeException("Index is less than a zero.");
                if (i > Rows || j > Cols)
                    throw new IndexOutOfRangeException("Index is more than possible.");
                _data[i * Cols + j] = value; 
            }
        }
        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }
        public int Cols
        {
            get { return _cols; }
            set { _cols = value; }
        }
        public int? Size
        {
            get
            {
                if (IsSquared)
                {
                    return Cols;
                }
                return null;
            }
        }
        /// <summary>
        /// Проверка на квадратную матрицу
        /// </summary>
        public bool IsSquared
        {
            get
            {
                if(Rows == Cols)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Проверка на нулевую матрицу
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (Rows == 0 && Cols ==0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Проверка на едииничную матрицу
        /// </summary>
        public bool IsUnity
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (!((i != j && _data[i * Cols + j] == 0)||( i==j && _data[i * Cols + j] == 1)))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        /// <summary>
        /// ПРоверка матрицы на симментричность относительно главной диагонали
        /// </summary>
        public bool IsSymmetric 
        {
            get
            {
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Cols; j++)
                    {
                        if (!(_data[i*Cols + j] == _data[j*Cols + i]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public static  Matrix operator* (Matrix m1, Matrix m2)
        {
            if (m1.Cols != m2.Rows)
                return null;
            int rows = m1.Rows;
            int cols = m2.Cols;
            double[,] temp = new double[rows,cols];
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    for (int k = 0; k < m1.Cols; k++)
                    {
                        temp[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return new Matrix(temp);
        }

        public static explicit operator Matrix(double[,] arr)
        {
            if (arr == null)
                return new Matrix();
            return new Matrix(arr);
        }
        /// <summary>
        /// Вычисление следа матрицы
        /// </summary>
        /// <returns> Возвращает сумму элементов главной диаагонали</returns>
        public double Trace()
        {
            double temp = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i == j)
                    {
                        temp += _data[i * Cols + j];
                    }
                }
            }
            return temp;
        }
        /// <summary>
        /// Конвертация матрицы в формат строки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string temp = "";
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    temp = temp + _data[i * Cols + j].ToString() + " "; 
                }
                temp = temp.Trim() + "\n";
            }   
            return temp;
        }
        /// <summary>
        /// Создает едининую матрицу размера size x size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Matrix GetUnity(int size)
        {
            Matrix m = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i==j)
                    {
                        m[i, j] = 1;
                    }
                    else
                    {
                        m[i, j] = 0;
                    }
                }
            }
            return new Matrix();
        }
        /// <summary>
        /// Создает нулевую матрицу размера size x size
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Matrix GetEmpty(int size)
        {
            Matrix result = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result[i, j] = 0;
                }
            }
            return result;
        }
        /// <summary>
        /// Описание метода конвертации строки в тип Matrix
        /// </summary>
        /// <param name="s"> Входная строка формата "1 2 3, 4 5 6, 7 8 9"</param>
        /// <param name="m"> Выходные даннные типа Matrix</param>
        /// <returns> возвращает true, если конвертация удалась, иначе - false</returns>
        public static bool TryParse(string s, out Matrix m)
        {
            if (s.Trim().Equals(String.Empty) || (s.Trim().Equals("\n")))
            {
                m = new Matrix();
                return false;
            }
            //соответствие формату 1 2 3 ..., 1 2 3 ..., 1 2 3 ...
            List<string> lines;
            int sizeCols = -1;
            int sizeRows = 1;
            try
            {
                lines = new List<string>(s.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].EndsWith(','))
                    {
                        lines[i] = lines[i].Trim(',');
                        sizeRows++;
                        if (sizeCols==-1)
                        {
                            sizeCols = i+1;
                        }
                    }
                    
                }
            }
            catch (Exception)
            {
                m = new Matrix();
                return false;
            }
            m = new Matrix(sizeRows, sizeCols);
            try
            {
                for (int i = 0; i < sizeRows; i++)
                {
                    for (int j = 0; j < sizeCols; j++)
                    {
                        m[i, j] = double.Parse(lines[i * sizeCols + j]);
                    }
                }
            }
            catch (Exception)
            {
                m = new Matrix();
                return false;
            }

            return true;
        }
    }
}
