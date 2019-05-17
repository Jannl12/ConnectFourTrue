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

        public int GetPlyDepthValue
        {
            get { return Convert.ToInt32(plyDepth.Value); }
        }

        public startMenu(MainWindow mainWindow)
        {
            this.DataContext = mainWindow;

            windowContent.Orientation = Orientation.Vertical;
            //Adding Title
                title.Content = "Welcome to ConnectFour!";
                title.FontSize = 30;
                title.Margin = new Thickness(0, 20, 0, 20);
                title.HorizontalAlignment = HorizontalAlignment.Center;
            windowContent.Children.Add(title);
            //Ading PlyDepth Slider Label
                plyDepthTitle.Content = "Select Plydepth: " + 10;
                plyDepthTitle.FontSize = 16;
                plyDepthTitle.Margin = new Thickness(0, 20, 0, 20);
                plyDepthTitle.HorizontalAlignment = HorizontalAlignment.Center;
            windowContent.Children.Add(plyDepthTitle);
            //Adding Plydepth Slider
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

                //Adding Two buttons in Stackpanel for Choosing AI players vs Human players
                playerTypes.Orientation = Orientation.Horizontal;
                playerTypes.HorizontalAlignment = HorizontalAlignment.Center;
                    //Player One
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
                    //Player Two
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
                //Adding two buttons for exit and start game.
                exitAndStartButton.Orientation = Orientation.Horizontal;
                exitAndStartButton.HorizontalAlignment = HorizontalAlignment.Center;
                    //Start Button
                    startButton = new Button();
                    startButton.HorizontalAlignment = HorizontalAlignment.Center;
                    startButton.Content = "Start Game";
                    startButton.Margin = buttonThickness;
                    startButton.Width = 150;
                    startButton.Height = 100;
                    startButton.Click += (sender, e) =>
                    {
                        this.Close();
                    };
                exitAndStartButton.Children.Add(startButton);
                    //Exit Button
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
            this.Content = windowContent;
        }

    }
    public partial class MainWindow : Window
    {
        Ellipse[,] ellipseGameBoard;

        Button resetButton, undoMove, newGameButton;

        SolidColorBrush emptyColor, blackColor;
        List<SolidColorBrush> playerColors = new List<SolidColorBrush>();

        const int rowCount = 6, columnCount = 7;
        const double aspect = 760 / 740;
        int gridElementSize = 100;

        startMenu newGameMenu;
        NegaTrans negaMaxBoard;

        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();

        IterativDeepening negaTest = new IterativDeepening();

        bool[] playerModes = new bool[2];



        public MainWindow()
        {
            InitializeComponent();
            newGameMenu = new startMenu(this);
            newGameMenu.ShowDialog();
            this.playerModes[0] = newGameMenu.PlayerOneIsComputerControlled;
            this.playerModes[1] = newGameMenu.PlayerTwoIsComputerControlled;
            negaMaxBoard = new NegaTrans(newGameMenu.GetPlyDepthValue);


            Grid gameGrid = new Grid();
            this.Title = "Connect Four Motherfucker!";
            this.Width = 740; this.Height = 760;
            newGameMenu = new startMenu(this);

            gameGrid.Height = rowCount * gridElementSize + 100;
            gameGrid.Width = columnCount * gridElementSize;
            gameGrid.Margin = new Thickness(10);

            //Define colours used for the users. 
            playerColors.Add(new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)));
            playerColors.Add(new SolidColorBrush(Color.FromArgb(255, 255, 255, 0)));
            emptyColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            blackColor = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));

            //Intializes the array of ellipses, that are put into the Grid.
            ellipseGameBoard = new Ellipse[rowCount, columnCount];

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
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    int g = j;
                    Ellipse bufferEllipse = new Ellipse();
                    bufferEllipse.Stroke = blackColor;
                    bufferEllipse.Fill = emptyColor;
                    bufferEllipse.Height = gridElementSize - 5;
                    bufferEllipse.Width = gridElementSize - 5;
                    Grid.SetColumn(bufferEllipse, j);
                    Grid.SetRow(bufferEllipse, i);


                    bufferEllipse.MouseEnter += (sender, e) =>
                    {
                        mouseEnterColumn(g);
                    };

                    bufferEllipse.MouseLeave += (sender, e) =>
                    {
                        mouseLeaveColumn(g);
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
            resetButton.Height = gridElementSize - gridElementSize / 5;
            resetButton.Width = gridElementSize - gridElementSize / 5;
            resetButton.Margin = new Thickness(gridElementSize / 5 / 2);
            Grid.SetRow(resetButton, rowCount + 1);
            Grid.SetColumn(resetButton, 1);
            gameGrid.Children.Add(resetButton);
            resetButton.Click += (sender, e) =>
            {
                ResetGame();
            };



            //Setup undoButton
            undoMove = new Button();
            undoMove.Content = "Undo Last \r\n    Move";
            undoMove.Height = gridElementSize - gridElementSize / 5;
            undoMove.Width = gridElementSize - gridElementSize / 5;
            resetButton.Margin = new Thickness(gridElementSize / 5 / 2);
            Grid.SetRow(undoMove, rowCount + 2);
            Grid.SetColumn(undoMove, 3);
            undoMove.Click += (sender, e) =>
            {
                negaMaxBoard.UndoMove();
                Tuple<int, int> bufferTuple = moveHistory.Pop();
                ellipseGameBoard[bufferTuple.Item1, bufferTuple.Item2].Fill = emptyColor;
            };
            gameGrid.Children.Add(undoMove);


            //New Game
            newGameButton = new Button();
            newGameButton.Content = "New Game";
            newGameButton.Height = gridElementSize - gridElementSize / 5;
            newGameButton.Width = gridElementSize - gridElementSize / 5;
            newGameButton.Margin = new Thickness(gridElementSize / 5 / 2);
            Grid.SetRow(newGameButton, rowCount + 1);
            Grid.SetColumn(newGameButton, 5);
            gameGrid.Children.Add(newGameButton);
            newGameButton.Click += (sender, e) =>
            {
                ResetGame();
                newGameMenu.ShowDialog();
                this.playerModes[0] = newGameMenu.PlayerOneIsComputerControlled;
                this.playerModes[1] = newGameMenu.PlayerTwoIsComputerControlled;
                negaMaxBoard = new NegaTrans(newGameMenu.GetPlyDepthValue);
            };

            this.Closed += (sender, e) =>
            {
                System.Windows.Application.Current.Shutdown();
            };
            this.Content = gameGrid;

            if (playerModes[0])
            {
                ColumnClick(negaMaxBoard.GetBestMove(1), false);
            }

        }


        private void mouseEnterColumn(int targetColumn)
        {
            for (int ellipseRowCounter = 0; ellipseRowCounter < rowCount; ellipseRowCounter++)
            {
                ellipseGameBoard[ellipseRowCounter, targetColumn].Stroke = playerColors[negaMaxBoard.GetCurrentPlayer()];
            }
        }

        private void mouseLeaveColumn(int targetColumn)
        {
            for (int f = 0; f < rowCount; f++)
            {
                ellipseGameBoard[f, targetColumn].Stroke = blackColor;
            }
        }

        /*Loops from the bottom of the column, and if it finds an empty cell, it fills it with 
         * the current players color. Also makes a move on the bitboard.
         */
        private void ColumnClick(int targetColumn, bool nextMoveAI)
        {    
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (ellipseGameBoard[i, targetColumn].Fill == emptyColor)
                {

                    ellipseGameBoard[i, targetColumn].Fill = playerColors[negaMaxBoard.GetCurrentPlayer()];
                    moveHistory.Push(new Tuple<int, int> ( i, targetColumn));
                    negaMaxBoard.MakeMove(targetColumn);

                    if (negaMaxBoard.IsWin())
                    {
                        MessageBox.Show("Player " + (negaMaxBoard.GetPreviousPlayer() + 1).ToString() + " won!");
                        ResetGame();
                    }
                    else if (nextMoveAI)
                    {
                        ColumnClick(negaMaxBoard.GetBestMove(negaMaxBoard.GetCurrentPlayer() == 0 ? 1 : -1), playerModes[negaMaxBoard.GetCurrentPlayer()]);
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

            negaMaxBoard.ResetGame();
        }
    }
}
