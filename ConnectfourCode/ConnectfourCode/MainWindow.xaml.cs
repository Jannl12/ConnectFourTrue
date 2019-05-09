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
    /// //test
    public partial class MainWindow : Window
    {
        Ellipse[,] ellipseGameBoard;
        SolidColorBrush yellowColor, redColor, emptyColor, blackColor;
        const int rowCount = 6, columnCount = 7, ellipseSize = 100;
        Negamax negamaxTest = new Negamax();
        
        public MainWindow()
        {
            //Negamax test = new Negamax();
            //test.bitGameBoard[0] = 0x4081; //gul (lige)
            //test.bitGameBoard[1] = 0x70008102; //rød (ulige)
            //test.EvaluateBoard();
            //test.NegaMax(test, int.MinValue, int.MaxValue, 3, 1);

            InitializeComponent();
            Grid gameGrid = new Grid();
            this.Width = ellipseSize * columnCount; this.Height = ellipseSize * rowCount;
            gameGrid.Height = this.Height; gameGrid.Width = this.Width;
            
            //Define colours used for the users. 
            yellowColor = new SolidColorBrush();
            yellowColor.Color = Color.FromArgb(255, 255, 0, 0);
            redColor = new SolidColorBrush();
            redColor.Color = Color.FromArgb(255, 255, 255, 0);
            emptyColor = new SolidColorBrush();
            emptyColor.Color = Color.FromArgb(0, 0, 0, 0);
            blackColor = new SolidColorBrush();
            blackColor.Color = Color.FromArgb(255, 0, 0, 0);

            //Intializes the array of ellipses, that are put into the Grid.
            ellipseGameBoard = new Ellipse[rowCount, columnCount];

            //Creates the rows and columns for the Grid, which are added using the two loops.
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

            /*Defines and specializes the Ellipses, which are put into the Grid. 
             * Here the colours, width, height and the eventhandlers are added to the
             * Ellipses using delegates, since it is required to target af specific
             * column (bit of a workaround). Finally adds the the Grid to the Form.
             * */
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
                        MouseEnterHandler(g);
                    };
                    bufferEllipse.MouseLeave += delegate (object sender, MouseEventArgs e)
                    {
                        MouseLeaveHandler(g);
                    };
                    bufferEllipse.MouseDown += delegate (object sender, MouseButtonEventArgs e)
                    {
                        ColumnClick(g, true);
                    };
                    ellipseGameBoard[i, j] = bufferEllipse;
                    gameGrid.Children.Add(bufferEllipse);
                }
            }
            this.Content = gameGrid;
            ColumnClick(negamaxTest.GetBestMove(10), false);
        }
        private void MouseEnterHandler(int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                ellipseGameBoard[i, targetColumn].Stroke = (negamaxTest.IsPlayerMove()) ? redColor : yellowColor;
            }
        }
        private void MouseLeaveHandler(int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                ellipseGameBoard[i, targetColumn].Stroke = blackColor;
            }
        }
        /*Loops from the bottom of the column, and if it finds an empty cell, it fills it with 
         * the current players color. Also makes a move on the bitboard.
         */
        private void ColumnClick(int targetColumn, bool playerMove)
        {
            
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (ellipseGameBoard[i, targetColumn].Fill == emptyColor)
                {
                    ellipseGameBoard[i, targetColumn].Fill = negamaxTest.IsPlayerMove() ? redColor : yellowColor;
                    negamaxTest.MakeMove(targetColumn);
                    if (negamaxTest.IsWin())
                    {
                        MessageBox.Show((negamaxTest.IsPlayerMove() ? "Player one" : "Player two") + " won!");
                        ResetGame();
                    }
                    if (playerMove)
                    {
                        ColumnClick(negamaxTest.GetBestMove(10), false);
                    } 
                    break;
                }
            }   
        }

        private void ResetGame()
        {
            foreach (Ellipse ellipse in ellipseGameBoard)
            {
                ellipse.Fill = emptyColor;
            }
            negamaxTest.ResetGame();
        }
    }

}
