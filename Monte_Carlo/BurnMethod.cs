using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monte_Carlo
{
    internal class BurnMethod
    {
        List<int[]> list = new List<int[]>();
        private int[] MixPosition(int[] matrix, Random randA, Random randB)
        {
            while (true)
            {
                int pos1;
                int pos2;
                while (true)
                {
                    pos1 = randA.Next(0, matrix.Length);
                    pos2 = randB.Next(0, matrix.Length);
                    if (pos1 != pos2) break;
                }

                int temp = matrix[pos1];
                matrix[pos1] = matrix[pos2];
                matrix[pos2] = temp;

                if (!list.Contains(matrix))
                {
                    list.Add(matrix);
                    break;
                }
            }
            return matrix;
        }

        internal class Output
        {
            public int[] ResultMatrix { get; set; }
            public bool Fail { get; set; } = true;
            public int NumberOfTrying { get; set; }
            //public Output() => Fail = true;
        }

        public Output LetBurn(int[] matrix, double k, double T=10000000)
        {
            
            Random randA = new Random();
            Thread.Sleep(100);
            Random randB = new Random();

            int[] oldMatrix = new int[matrix.Length];
            oldMatrix = matrix;
            int[] nextMatrix = new int[matrix.Length];

            Random rand = new Random();
            CalcEnergy calc = new CalcEnergy();
            //BurnMethod burn = new BurnMethod();
            int numberOfTry = 1;
            double P = 0;
            double chance = 0;
            int nextE = 0;
            int dE = 0;
            int currentE = 0;

            while (T > 1)
            {
                currentE = calc.FindEnergy(oldMatrix); //подсчитываем энергию расположения фигур
                if (currentE == 0) return new Output
                {
                    ResultMatrix = oldMatrix,
                    Fail = false,
                    NumberOfTrying = numberOfTry
                }; //если энергия =0 цель достигнута

                else
                {
                    var temp = new int[matrix.Length];
                    Array.Copy(oldMatrix, temp, matrix.Length);
                    nextMatrix = MixPosition(temp, randA, randB);
                }

                nextE = calc.FindEnergy(nextMatrix);// подсчитываем новую энергию
                dE = nextE - currentE;
                if (dE < 0)
                {
                    Array.Copy(nextMatrix, oldMatrix, matrix.Length);
                    P = 1;
                }
                else
                {
                    P = Math.Exp(-dE / T)/10; //подсчет вероятности прохождения
                    chance = rand.NextDouble();
                    if (P > chance) Array.Copy(nextMatrix, oldMatrix, matrix.Length);
                }
                T = T * k; // понижаем температуру
                numberOfTry++;
            }
            //при неудачном исходе метода
            return new Output { ResultMatrix = null, Fail = true, NumberOfTrying = numberOfTry };
        }
    }
}
