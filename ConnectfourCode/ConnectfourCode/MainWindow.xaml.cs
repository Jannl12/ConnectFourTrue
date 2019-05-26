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
        public int playerOneIsComputerControlled = 1, playerTwoIsComputerControlled = 0;

        public int GetPlyDepthValue
        {
            get { return Convert.ToInt32(plyDepth.Value); }
        }

        public startMenu(MainWindow mainWindow)
        {
            this.DataContext = mainWindow;

            windowContent.Orientation = Orientation.Vertical;
            //Adding Title
            {
                title.Content = "Welcome to ConnectFour!";
                title.FontSize = 30;
                title.Margin = new Thickness(0, 20, 0, 20);
                title.HorizontalAlignment = HorizontalAlignment.Center;
                windowContent.Children.Add(title);
            }
            //Ading PlyDepth Slider Label
            {
                plyDepthTitle.Content = "Select Plydepth: " + 10;
                plyDepthTitle.FontSize = 16;
                plyDepthTitle.Margin = new Thickness(0, 20, 0, 20);
                plyDepthTitle.HorizontalAlignment = HorizontalAlignment.Center;
                windowContent.Children.Add(plyDepthTitle);
            }
            //Adding Plydepth Slider
            {
                plyDepth.Minimum = 3;
                plyDepth.Maximum = 13;
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
            }


            //Adding Two buttons in Stackpanel for Choosing AI players vs Human players
            {
                playerTypes.Orientation = Orientation.Horizontal;
                playerTypes.HorizontalAlignment = HorizontalAlignment.Center;
                //Player One
                {
                    playerOneModeButton = new Button();
                    playerOneModeButton.HorizontalAlignment = HorizontalAlignment.Center;
                    playerOneModeButton.Content = "AI(ArrayBoard)";
                    playerOneModeButton.Margin = buttonThickness;

                    playerOneModeButton.Width = 150;
                    playerOneModeButton.Height = 100;
                    playerOneModeButton.Click += (sender, e) =>
                    {
                        playerOneIsComputerControlled = (playerOneIsComputerControlled + 1) % 3;
                        switch (playerOneIsComputerControlled)
                        {
                            case 0:
                                playerOneModeButton.Content = "Human Player";
                                break;
                            case 1:
                                playerOneModeButton.Content = "AI(ArrayBoard)";
                                break;
                            case 2:
                                playerOneModeButton.Content = "AI(BitBoard)";
                                break;
                            default:
                                playerOneModeButton.Content = "Error";
                                playerOneModeButton.Background = new SolidColorBrush(Colors.Red);
                                break;
                        }
                        if(playerOneIsComputerControlled != 0 && playerTwoIsComputerControlled != 0)
                        {
                            startButton.IsEnabled = false;
                        }
                        else
                        {
                            startButton.IsEnabled = true;
                        }

                    };
                    playerTypes.Children.Add(playerOneModeButton);
                }
                //Player Two
                {
                    playerTwoModeButton = new Button();
                    playerTwoModeButton.HorizontalAlignment = HorizontalAlignment.Center;
                    playerTwoModeButton.Content = playerTwoModeButton.Content = "Human Player";
                    playerTwoModeButton.Margin = buttonThickness;
                    playerTwoModeButton.Width = 150;
                    playerTwoModeButton.Height = 100;
                    playerTwoModeButton.Click += (sender, e) =>
                    {
                        playerTwoIsComputerControlled = (playerTwoIsComputerControlled + 1) % 3;
                        switch (playerTwoIsComputerControlled)
                        {
                            case 0:
                                playerTwoModeButton.Content = "Human Player";
                                break;
                            case 1:
                                playerTwoModeButton.Content = "AI(ArrayBoard)";
                                break;
                            case 2:
                                playerTwoModeButton.Content = "AI(BitBoard)";
                                break;
                            default:
                                playerTwoModeButton.Content = "Error";
                                break;
                        }
                        if (playerOneIsComputerControlled != 0 && playerTwoIsComputerControlled != 0)
                        {
                            startButton.IsEnabled = false;
                        }
                        else
                        {
                            startButton.IsEnabled = true;
                        }

                    };
                    playerTypes.Children.Add(playerTwoModeButton);
                }
            }

            windowContent.Children.Add(playerTypes);
            //Adding two buttons for exit and start game.
            {
                exitAndStartButton.Orientation = Orientation.Horizontal;
                exitAndStartButton.HorizontalAlignment = HorizontalAlignment.Center;
                //Start Button
                {
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
                }
                //Exit Button
                {
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
                }

                windowContent.Children.Add(exitAndStartButton);

            }

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

        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();

        int[] playerModes = new int[2];

        dynamic AIGameBoard;



        public MainWindow()
        {
            InitializeComponent();


            void newGame()
            {
                newGameMenu = new startMenu(this);
                newGameMenu.ShowDialog();
                this.playerModes[0] = newGameMenu.playerOneIsComputerControlled;
                this.playerModes[1] = newGameMenu.playerTwoIsComputerControlled;

                if(playerModes[0] == 1 || playerModes[1] == 1)
                {
                    AIGameBoard = new NegaMaxAGB(newGameMenu.GetPlyDepthValue);
                } 
                else if (playerModes[0] == 2 || playerModes[1] == 2)
                {
                    AIGameBoard = new NegaTrans(newGameMenu.GetPlyDepthValue);
                }
                else
                {
                    AIGameBoard = new NegaTrans(newGameMenu.GetPlyDepthValue);
                }
            }

            newGame();


            Grid gameGrid = new Grid();
            this.Title = "Connect Four Motherfucker!";
            this.Width = 740; this.Height = 760;

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
                        ColumnClick(g, playerModes[AIGameBoard.GetPreviousPlayer()] != 0);
                    };
                    ellipseGameBoard[i, j] = bufferEllipse;
                    gameGrid.Children.Add(bufferEllipse);
                }
            }



            gameGrid.RowDefinitions.Add(new RowDefinition());


            //Setup resetButton
            resetButton = new gameBoardInteraktionButton(gridElementSize, 1, rowCount + 1, "Reset");
            gameGrid.Children.Add(resetButton);
            resetButton.Click += (sender, e) =>
            {
                ResetGame();
            };



            //Setup undoButton
            undoMove = new gameBoardInteraktionButton(gridElementSize, 3, rowCount + 1, "Undo Move");
            gameGrid.Children.Add(undoMove);
            undoMove.Click += (sender, e) =>
            {
                if (playerModes[0] != 0 || playerModes[1] != 0)
                {
                    if (moveHistory.Count() >= playerModes.Count())
                    {
                        for (int i = 0; i < playerModes.Count(); i++)
                        {
                            AIGameBoard.UndoMove();
                            Tuple<int, int> bufferTuple = moveHistory.Pop();
                            ellipseGameBoard[bufferTuple.Item1, bufferTuple.Item2].Fill = emptyColor;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Not enough moves to Undo!");
                    }

                }
                else
                {
                    if (moveHistory.Count() > 0)
                    {
                        AIGameBoard.UndoMove();
                        Tuple<int, int> bufferTuple = moveHistory.Pop();
                        ellipseGameBoard[bufferTuple.Item1, bufferTuple.Item2].Fill = emptyColor;
                    }
                    else
                    {
                        MessageBox.Show("Not enough moves to Undo!");
                    }
                }
                
            };


            //New Game
            newGameButton = new gameBoardInteraktionButton(gridElementSize, 5, rowCount + 1, "New Game");
            gameGrid.Children.Add(newGameButton);
            newGameButton.Click += (sender, e) =>
            {
                
                newGame();
                ResetGame();
            };

            this.Closed += (sender, e) =>
            {
                System.Windows.Application.Current.Shutdown();
            };
            this.Content = gameGrid;

            if (playerModes[AIGameBoard.GetCurrentPlayer()] != 0)
            {
                ColumnClick(AIGameBoard.GetBestMove(), playerModes[AIGameBoard.GetPreviousPlayer()] != 0);
            }

        }


        private void mouseEnterColumn(int targetColumn)
        {
            for (int ellipseRowCounter = 0; ellipseRowCounter < rowCount; ellipseRowCounter++)
            {
                ellipseGameBoard[ellipseRowCounter, targetColumn].Stroke = playerColors[AIGameBoard.GetCurrentPlayer()];
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

                    ellipseGameBoard[i, targetColumn].Fill = playerColors[AIGameBoard.GetCurrentPlayer()];
                    moveHistory.Push(new Tuple<int, int>(i, targetColumn));
                    AIGameBoard.MakeMove(targetColumn);

                    if (AIGameBoard.IsWin())
                    {
                        MessageBox.Show("Player " + (AIGameBoard.GetPreviousPlayer() + 1).ToString() + " won!");
                        ResetGame();
                    }
                    if (AIGameBoard.IsDraw())
                    {
                        MessageBox.Show("The Game Is A Draw!");
                        ResetGame();
                    }
                    else if (nextMoveAI)
                    {
                        ColumnClick(AIGameBoard.GetBestMove(), playerModes[AIGameBoard.GetPreviousPlayer()] != 0);
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
            moveHistory = new Stack<Tuple<int, int>>();
            AIGameBoard.ResetGame();
            if (playerModes[AIGameBoard.GetCurrentPlayer()] != 0)
            {
                ColumnClick(AIGameBoard.GetBestMove(), (playerModes[AIGameBoard.GetPreviousPlayer()] != 0));
            }
        }
    }

    public class gameBoardInteraktionButton : Button
    {
        public gameBoardInteraktionButton(int diameter, int columnPlacement, int rowPlacement, string content)
        {
            this.Height = diameter * 0.8;
            this.Width = diameter * 0.8;
            this.Margin = new Thickness(diameter * 0.1);
            Grid.SetColumn(this, columnPlacement);
            Grid.SetRow(this, rowPlacement);
            this.Content = content;
        }

    }
}

