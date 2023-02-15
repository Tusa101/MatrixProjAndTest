using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            Matrix temp = new Matrix(m1.Rows, m2.Cols);
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Cols; j++)
                {
                    temp[i, j] = 0;
                    for (int k = 0; k < m1.Cols; k++)
                    {
                        temp[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return temp;
        }
        public static explicit operator Matrix(double[,] arr)
        {
            if (arr == null)
                return new Matrix();
            return new Matrix(arr);
        }
        public double Trace()
        {
            double temp = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (i == j)
                    {
                        temp += _data[i * j];
                    }
                }
            }
            return temp;
        }
        public override string ToString()
        {
            string temp = "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    temp = temp + _data[i * j].ToString() + " "; 
                }
                temp = temp.Trim() + "\n";
            }   
            return sb.ToString();
        }

    }
}
