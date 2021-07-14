using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monte_Carlo
{
    internal class CalcEnergy
    {
        public int FindEnergy(int[] matrix) //подсчет энергии расположения ферзей
        {
            int energy=0; //чем больше фигур под ударом, тем больше энергия
            int dx;
            int dy;
            for (int i=0; i<matrix.Length; i++)
                for (int j=0; j<matrix.Length; j++)
                {
                    if (i == j) continue;
                    dx = Math.Abs(matrix[j]- matrix[i]);
                    dy = Math.Abs(j - i);
                    if (dx == dy) energy++;
                }
            return energy;
        }
    }
}
