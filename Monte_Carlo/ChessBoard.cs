using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Monte_Carlo
{
    //Визуализация шахматной доски
    internal class ChessBoard : Form
    {
        readonly int size;
        private TableLayoutPanel table;
        
        public ChessBoard(BurnMethod.Output output)
        {
            size = output.ResultMatrix.Length;//размерность доски
            Text = string.Format("Правильная расстановка {0} ферзей", size);
            //Font = new Font("Arial", 20);
            DoubleBuffered = true;
            table = new TableLayoutPanel();
            for (int i = 0; i < size; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / size));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / size));
            }
            ClientSize = new Size(650, 500);
            // расставляем ферзей
            for (int column = 0; column < size; column++)
                for (int row = 0; row < size; row++)
                {
                    var button = new Button();
                    button.BackColor = (row + column) % 2 == 0 ? Color.White : Color.Black;
                    if (row == output.ResultMatrix[column]-1)
                    {
                        button.BackgroundImage = Image.FromFile("Queen.bmp");
                        button.BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    button.Dock = DockStyle.Fill;
                    table.Controls.Add(button, column, row);
                }
            table.Dock = DockStyle.Fill;
            Controls.Add(table);

            MessageBox.Show(string.Format("Получилось!\n"+ "Количество итераций: {0}",
                    output.NumberOfTrying.ToString()));
        }
    }
}
