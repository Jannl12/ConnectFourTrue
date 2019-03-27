﻿using System;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Ellipse[,] gameBoard;
        SolidColorBrush yellowColor, redColor, emptyColor, blackColor;
        const int rowCount = 6, columnCount = 7;
        bool Playerturn = true;
        public MainWindow()
        {
            InitializeComponent();
            Grid gameGrid = new Grid();
            this.Width = 100 * columnCount; this.Height = 100 * rowCount;
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
                        mouseEnterHandler(sender, e, g);
                    };
                    bufferEllipse.MouseLeave += delegate (object sender, MouseEventArgs e)
                    {
                        mouseLeaveHandler(sender, e, g);
                    };
                    bufferEllipse.MouseDown += delegate (object sender, MouseButtonEventArgs e)
                    {
                        columnClick(sender, e, g);
                    };
                    gameBoard[i, j] = bufferEllipse;
                    gameGrid.Children.Add(bufferEllipse);
                }
            }
            this.Content = gameGrid;
        }
        private void mouseEnterHandler(object sender, EventArgs e, int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                gameBoard[i, targetColumn].Stroke = Playerturn ? redColor : yellowColor;
            }
        }
        private void mouseLeaveHandler(object sender, EventArgs e, int targetColumn)
        {
            for (int i = 0; i < rowCount; i++)
            {
                gameBoard[i, targetColumn].Stroke = blackColor;
            }
        }
        private void columnClick(object sender, EventArgs e, int targetColumn)
        {
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (gameBoard[i, targetColumn].Fill == emptyColor)
                {
                    gameBoard[i, targetColumn].Fill = Playerturn ? redColor : yellowColor;
                    Playerturn = !Playerturn;
                    mouseEnterHandler(sender, e, targetColumn);
                    break;
                }
            }
        }
        private void resetBoard()
        {
            foreach (Ellipse ellipse in gameBoard)
            {
                ellipse.Fill = emptyColor;
            }
            Playerturn = true;
        }
    }

}