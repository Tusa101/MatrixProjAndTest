using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace MatrixProjAndTest
{
    public class Program
    {
        static void Main(string[] args)
        {


            //for (int i = 0; i < matr.Rows; i++)
            //{
            //    for (int j = 0; j < matr.Cols; j++)
            //    {
            //        Console.WriteLine(matr[i, j]);
            //    }
            //}

            //arrange
            Matrix mExpected = Matrix.GetUnity(3);
            bool expected = true;
            bool actual;
            Matrix mActual;
            string inputForTryParse = "1 0 0, 0 1 0, 0 0 1";
            //act
            actual = Matrix.TryParse(inputForTryParse, out mActual);
            //assert

            // Matrix actual = mOne * mTwo;
            // Console.WriteLine(matr.ToString());
            Console.ReadKey();
        }
    }
}