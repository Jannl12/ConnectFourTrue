using NegamaxTest;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class startMenu : Window
    {
        StackPanel windowContent = new StackPanel();
        StackPanel exitAndStartButton = new StackPanel();
        StackPanel playerTypes = new StackPanel();
        Slider plyDepth = new Slider();
        Button startButton, exitButton, playerOneModeButton, playerTwoModeButton;
        Label title = new Label(), plyDepthTitle = new Label();
        Thickness buttonThickness = new Thickness(10, 20, 10, 20);
        private bool playerOneIsComputerControlled = true, playerTwoIsComputerControlled = false;
        public bool PlayerOneIsComputerControlled
        {
            get { return playerOneIsComputerControlled; }
        }

        public bool PlayerTwoIsComputerControlled
        {
            get { return playerTwoIsComputerControlled; }
        }


        Grid BoxGrid = new Grid();
        public startMenu()
        {
            windowContent.Orientation = Orientation.Vertical;
                title.Content = "Welcome to ConnectFour!";
                title.FontSize = 30;
                title.Margin = new Thickness(0, 20, 0, 20);
                title.HorizontalAlignment = HorizontalAlignment.Center;
            windowContent.Children.Add(title);
                plyDepthTitle.Content = "Select Plydepth: " + 10;
                plyDepthTitle.FontSize = 16;
                plyDepthTitle.Margin = new Thickness(0, 20, 0, 20);
                plyDepthTitle.HorizontalAlignment = HorizontalAlignment.Center;
            windowContent.Children.Add(plyDepthTitle);


                plyDepth.Name = "Hello";
                plyDepth.Minimum = 1;
                plyDepth.Maximum = 20;
                plyDepth.Width = 300;
                plyDepth.Value = 10;
                plyDepth.Height = 100;
                plyDepth.IsSnapToTickEnabled = true;
                plyDepth.Orientation = Orientation.Horizontal;
                plyDepth.TickPlacement = System.Windows.Controls.Primitives.TickPlacement.BottomRight;
                plyDepth.TickFrequency = 1;
                plyDepth.ValueChanged += (sender, e) =>
                {
                    plyDepthTitle.Content = "Select Plydepth: " + plyDepth.Value;
                };
            windowContent.Children.Add(plyDepth);

                playerTypes.Orientation = Orientation.Horizontal;
                playerTypes.HorizontalAlignment = HorizontalAlignment.Center;
                    playerOneModeButton = new Button();
                    playerOneModeButton.HorizontalAlignment = HorizontalAlignment.Center;
                    playerOneModeButton.Content = PlayerOneIsComputerControlled ? "AI Player" : "Human Player" ;
                    playerOneModeButton.Margin = buttonThickness;
                    
                    playerOneModeButton.Width = 150;
                    playerOneModeButton.Height = 100;
                    playerOneModeButton.Click += (sender, e) =>
                    {
                        playerOneIsComputerControlled = !playerOneIsComputerControlled;
                        playerOneModeButton.Content = playerOneIsComputerControlled ? "AI Player" : "Human Player";
                        
                    };
                playerTypes.Children.Add(playerOneModeButton);

                    playerTwoModeButton = new Button();
                    playerTwoModeButton.HorizontalAlignment = HorizontalAlignment.Center;
                    playerTwoModeButton.Content = playerTwoModeButton.Content = PlayerTwoIsComputerControlled ? "AI Player" : "Human Player"; ;
                    playerTwoModeButton.Margin = buttonThickness;
                    playerTwoModeButton.Width = 150;
                    playerTwoModeButton.Height = 100;
                    playerTwoModeButton.Click += (sender, e) =>
                    {
                        playerTwoIsComputerControlled = !playerTwoIsComputerControlled;
                        playerTwoModeButton.Content = playerTwoIsComputerControlled ? "AI Player" : "Human Player";

                    };
                playerTypes.Children.Add(playerTwoModeButton);

            windowContent.Children.Add(playerTypes);

                exitAndStartButton.Orientation = Orientation.Horizontal;
                exitAndStartButton.HorizontalAlignment = HorizontalAlignment.Center;
                    startButton = new Button();
                    startButton.HorizontalAlignment = HorizontalAlignment.Center;
                    startButton.Content = "Start Game";
                    startButton.Margin = buttonThickness;
                    startButton.Width = 150;
                    startButton.Height = 100;
                exitAndStartButton.Children.Add(startButton);

                    exitButton = new Button();
                    exitButton.HorizontalAlignment = HorizontalAlignment.Center;
                    exitButton.Content = "Exit";
                    exitButton.Width = 150;
                    exitButton.Margin = buttonThickness;
                    exitButton.Height = 100;
                    exitButton.Click += (sender, e) =>
                    {
                        System.Windows.Application.Current.Shutdown();
                    };
                exitAndStartButton.Children.Add(exitButton);

            windowContent.Children.Add(exitAndStartButton);

            this.Title = "New Game";
            this.Width = 400;
            this.Height = 600;
            this.Content = BoxGrid;
            this.Content = windowContent;
        }

    }
    public partial class MainWindow : Window
    {
        Ellipse[,] ellipseGameBoard;
        Button resetButton, undoMove;

        SolidColorBrush emptyColor, blackColor;
        List<SolidColorBrush> playerColors = new List<SolidColorBrush>();

        const int rowCount = 6, columnCount = 7, ellipseSize = 100;

        startMenu thisMenu = new startMenu();
        NegaTrans negamaxTest = new NegaTrans();
        
        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();
        IterativDeepening negaTest = new IterativDeepening();
       
        
        public MainWindow()
        {
            InitializeComponent();
            Grid gameGrid = new Grid();
            this.Title = "Connect Four Motherfucker!";
           
            gameGrid.Height = rowCount * 100 + 100; gameGrid.Width = columnCount * 100;
            gameGrid.Margin = new Thickness(10);
            this.Width = gameGrid.Width + 20; this.Height = gameGrid.Height + 20;

            //Define colours used for the users. 
            playerColors.Add(new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));
            playerColors.Add(new SolidColorBrush(Color.FromArgb(255, 255, 255, 0)));
            emptyColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            blackColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            //Intializes the array of ellipses, that are put into the Grid.
            ellipseGameBoard = new Ellipse[columnCount, rowCount];

            //Creates the rows and columns for the Grid, which are added using the two loops.
            ColumnDefinition[] columns = new ColumnDefinition[columnCount];
            RowDefinition[] rows = new RowDefinition[rowCount];


            foreach (ColumnDefinition col in columns)
            {
                gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            foreach (RowDefinition row in rows)
            {
                gameGrid.RowDefinitions.Add(new RowDefinition());
            }

            /**Defines and specializes the Ellipses, which are put into the Grid. 
             * Here the colours, width, height and the eventhandlers are added to the
             * Ellipses using delegates, since it is required to target af specific
             * column (bit of a workaround). Finally adds the the Grid to the Form.
             * */
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int g = i;
                    Ellipse bufferEllipse = new Ellipse();
                    bufferEllipse.Stroke = blackColor;
                    bufferEllipse.Fill = emptyColor;
                    bufferEllipse.Height = ellipseSize - 5;
                    bufferEllipse.Width = ellipseSize - 5 ;
                    Grid.SetColumn(bufferEllipse, i);
                    Grid.SetRow(bufferEllipse, j);


                    bufferEllipse.MouseEnter += (sender, e) =>
                    {
                        mouseEnterColumn(g);
                    };

                    bufferEllipse.MouseLeave += (sender, e) =>
                    {
                        for (int f = 0; f < rowCount; f++)
                        {
                            ellipseGameBoard[g, f].Stroke = blackColor;
                        }
                    };

                    bufferEllipse.MouseDown += (sender, e) =>
                    {
                        ColumnClick(g, true);
                    };
                    ellipseGameBoard[i, j] = bufferEllipse;
                    gameGrid.Children.Add(bufferEllipse);
                }
            }


            gameGrid.RowDefinitions.Add(new RowDefinition());

            //Setup resetButton
            resetButton = new Button();
            resetButton.Content = "Reset";
            resetButton.Height = 100; resetButton.Width = 100;
            Grid.SetRow(resetButton, rowCount + 1);
            Grid.SetColumn(resetButton, 1);
            resetButton.Click += (sender, e) =>
            {
                ResetGame();
            };
            gameGrid.Children.Add(resetButton);
            

            //Setup undoButton
            undoMove = new Button();
            undoMove.Content = "Undo Last Move";
            undoMove.Height = 100; undoMove.Width = 100;
            Grid.SetRow(undoMove, rowCount + 1);
            Grid.SetColumn(undoMove, 2);
            undoMove.Click += (sender, e) =>
            {
                negamaxTest.UndoMove();
                Tuple<int, int> bufferTuple = moveHistory.Pop();
                ellipseGameBoard[bufferTuple.Item1, bufferTuple.Item2].Fill = emptyColor;
            };
            gameGrid.Children.Add(undoMove);
            

            resetButton = new Button();
            resetButton.Content = "Reset";
            resetButton.Height = 100; resetButton.Width = 100;
            Grid.SetRow(resetButton, rowCount + 1);
            Grid.SetColumn(resetButton, 1);
            gameGrid.Children.Add(resetButton);
            resetButton.Click += (sender, e) =>
            {
                ResetGame();
            };

            this.Content = gameGrid;
            thisMenu.ShowDialog();
            
            //negamaxTest.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true); // normal negaMax
            //ColumnClick(negamaxTest.bestMove, false);
            //negamaxTest.ResetBestMove();      // Iterative deepening
            //ColumnClick(negaTest.Deepening(), false);

        }

        private void mouseEnterColumn(int targetColumn)
        {
            for (int ellipseRowCounter = 0; ellipseRowCounter < rowCount; ellipseRowCounter++)
            {
                ellipseGameBoard[targetColumn, ellipseRowCounter].Stroke = playerColors[negamaxTest.GetCurrentPlayer()];
            }
        }

        /*Loops from the bottom of the column, and if it finds an empty cell, it fills it with 
         * the current players color. Also makes a move on the bitboard.
         */
        private void ColumnClick(int targetColumn, bool playerMove)
        {    
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (ellipseGameBoard[targetColumn, i].Fill == emptyColor)
                {
                    ellipseGameBoard[targetColumn, i].Fill = playerColors[negamaxTest.GetCurrentPlayer()];
                    moveHistory.Push(new Tuple<int, int> ( targetColumn, i ));
                    
                    if (negamaxTest.MakeMoveAndCheckIfWin(targetColumn))
                    {

                        MessageBox.Show((negamaxTest.GetCurrentPlayer() == 1 ? "Player one" : "Player two") + " won!");
                        ResetGame();
                    }
                    if (playerMove)
                    {
                        //ColumnClick(negamaxTest.GetBestMove(10), false);
                    } 
                    break;
                }
            }
            mouseEnterColumn(targetColumn);
        }

        private void ResetGame()
        {
            foreach (Ellipse ellipse in ellipseGameBoard)
            {
                ellipse.Fill = emptyColor;
            }
            negamaxTest.ResetGame();
            //negaTest.ResetBitBoard();
        }
    }

}
