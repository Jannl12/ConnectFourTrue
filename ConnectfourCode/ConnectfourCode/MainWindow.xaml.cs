using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ConnectfourCode
{
    // TODO: Code needs commment. ALLE
    // TODO: Check om det er muligt at lave en klasse med handlers
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ellipse[,] gameBoard;
        SolidColorBrush yellowColor, redColor, emptyColor, blackColor;
        const int rowCount = 6, columnCount = 7, ellipseSize = 100;
        int moves = 0;
        BitBoard gameBitBoard = new BitBoard();
        public MainWindow()
        {
            InitializeComponent();
            gameBitBoard.ResetBitBoard();
            Grid gameGrid = new Grid();
            this.Width = ellipseSize * columnCount; this.Height = ellipseSize * rowCount;
            gameGrid.Height = this.Height; gameGrid.Width = this.Width;
            //testComment
            yellowColor = new SolidColorBrush();
            yellowColor.Color = Color.FromArgb(255, 255, 0, 0);
            redColor = new SolidColorBrush();
            redColor.Color = Color.FromArgb(255, 255, 255, 0);
            emptyColor = new SolidColorBrush();
            emptyColor.Color = Color.FromArgb(0, 0, 0, 0);
            blackColor = new SolidColorBrush();
            blackColor.Color = Color.FromArgb(255, 0, 0, 0);
            gameBoard = new Ellipse[rowCount, columnCount];
            ColumnDefinition[] columns = new ColumnDefinition[columnCount];
            RowDefinition[] rows = new RowDefinition[rowCount];


            foreach (ColumnDefinition col in columns)
            {
                ColumnDefinition bufferCol = new ColumnDefinition();
                gameGrid.ColumnDefinitions.Add(bufferCol);
            }
            foreach (RowDefinition row in rows)
            {
                RowDefinition bufferRow = new RowDefinition();
                gameGrid.RowDefinitions.Add(bufferRow);
            }
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int g = j;
                    Ellipse bufferEllipse = new Ellipse();
                    bufferEllipse.Stroke = blackColor;
                    bufferEllipse.Fill = emptyColor;
                    bufferEllipse.Height = gameGrid.Height / rowCount - 5;
                    bufferEllipse.Width = gameGrid.Width / columnCount - 5;
                    Grid.SetColumn(bufferEllipse, j);
                    Grid.SetRow(bufferEllipse, i);

                    bufferEllipse.MouseEnter += delegate (object sender, MouseEventArgs e)
                    {
                        MouseEnterHandler(sender, e, g);
                    };
                    bufferEllipse.MouseLeave += delegate (object sender, MouseEventArgs e)
                    {
                        MouseLeaveHandler(sender, e, g);
                    };
                    bufferEllipse.MouseDown += delegate (object sender, MouseButtonEventArgs e)
                    {
                        ColumnClick(sender, e, g);
                    };
                    gameBoard[i, j] = bufferEllipse;
                    gameGrid.Children.Add(bufferEllipse);
                }
            }
            this.Content = gameGrid;
        }
        private void MouseEnterHandler(object sender, EventArgs e, int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                gameBoard[i, targetColumn].Stroke = ((moves & 1) == 0) ? redColor : yellowColor;
            }
        }
        private void MouseLeaveHandler(object sender, EventArgs e, int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                gameBoard[i, targetColumn].Stroke = blackColor;
            }
        }
        private void ColumnClick(object sender, EventArgs e, int targetColumn)
        {
            
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (gameBoard[i, targetColumn].Fill == emptyColor)
                {
                    gameBoard[i, targetColumn].Fill = ((++moves & 1) == 1) ? redColor : yellowColor;
                    gameBitBoard.MakeMove(targetColumn, moves);
                    if (gameBitBoard.IsWin(moves))
                    {
                        MessageBox.Show(((moves & 1) == 1 ? "Player one" : "Player two") + " won!");
                        ResetBoard();
                    }
                   // MessageBox.Show(gameBitBoard.EvaluateBoard(moves).ToString());
                    MouseEnterHandler(sender, e, targetColumn);
                    break;
                }
            }
        }

        private void ResetBoard()
        {
            foreach (Ellipse ellipse in gameBoard)
            {
                ellipse.Fill = emptyColor;
            }
            gameBitBoard.ResetBitBoard();
            moves = 0;
        }
    }

}
