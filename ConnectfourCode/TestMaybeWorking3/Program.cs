using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConnectfourCode;
using Microsoft.Office.Interop.Excel;

namespace TestMaybeWorking3
{
    class Program
    {
        static void Main(string[] args)
        {

            // Application excelApplication = new Application();
            // excelApplication.Visible = true;
            // var excelWorkBook = excelApplication.Workbooks.Add();
            // var xlsxSheet = excelWorkBook.Sheets as Sheets;
            // var excelWorkSheet = (Worksheet)xlsxSheet.Add(xlsxSheet[1]);
            //int row = 0;
            //excelWorkSheet.Cells[++row, 1] = "Values";
            /*Negamax againstOnlineSolver = new Negamax();
            int count = 2 * 15 * 20;
            int[] plyDeths = { 9 };// 5,6,7,8,9,10,11,12 };

            foreach (int ply in plyDeths)
            {*/

            /* for (int threeRow = 95; threeRow < 105; threeRow += 5)
             {
                 //againstOnlineSolver.Three1 =  threeRow;//threeRow

                 for (int twoRow = 30; twoRow < 76; twoRow += 3)
                 {
                     //againstOnlineSolver.Two1 =  twoRow;// twoRow;
                     for (int oneRow = 10; oneRow < 30; oneRow++)
                     {*/
            //  againstOnlineSolver.ResetBitBoard();
            //  againstOnlineSolver.One1 = oneRow;// oneRow;


            /*int color = 1;
            var driver = new FirefoxDriver();
            string url = "https://connect4.gamesolver.org/?pos=";
            while (!againstOnlineSolver.IsDraw() && !againstOnlineSolver.IsWin())
            {
                if (color == 1)
                {
                    againstOnlineSolver.NegaMax(int.MinValue + 1, int.MaxValue, ply, 1, true);
                    againstOnlineSolver.MakeMove(againstOnlineSolver.bestMove);
                    //if (againstOnlineSolver.IsWin() || againstOnlineSolver.MoveCount > 7)
                    //{
                        //excelWorkSheet.Cells[++row, 1] = threeRow.ToString() + "," + twoRow.ToString() + "," + oneRow.ToString();
                       // excelWorkSheet.Cells[row, 2] = againstOnlineSolver.MoveCount;
                   // }
                    url += (againstOnlineSolver.moveHistory[againstOnlineSolver.moveHistory.Count - 1] + 1).ToString();
                    color *= -1;
                }
                else
                {
                    driver.Url = url;
                    driver.Navigate();

                    Thread.Sleep(1000);
                    var source = driver.PageSource;
                    //fully navigate the dom
                    int[] solutionValues = new int[7];
                    for (int i = 0; i < 7; i++)
                    {
                        var classElement = driver.FindElementByClassName("col" + i.ToString());
                        if (classElement.Text != "")
                            solutionValues[i] = Convert.ToInt32(classElement.Text);
                        else
                            solutionValues[i] = -100;
                    }
                    //var pathElement = driver.FindElementById("solution");


                    //string[] solutionStringValues = Regex.Split(pathElement.Text, Environment.NewLine);
                    //int[] solutionValues = Array.ConvertAll(solutionStringValues, new Converter<string, int>(ConvertStringArray.StringToInt));
                    int maxValue = solutionValues.Max();
                    if (maxValue >= 0)
                        break;

                    int maxIndex = solutionValues.ToList().IndexOf(maxValue);
                    againstOnlineSolver.MakeMove(maxIndex);
                    url += (againstOnlineSolver.moveHistory[againstOnlineSolver.moveHistory.Count - 1] + 1).ToString();
                    color *= -1;
                }
            }
            driver.Close();
            Console.WriteLine($"Number of simulations missing: {--count}");
    //    }
   // }
//  }
}
//excelWorkBook.SaveAs("AgainstOnlineSolverPly7_2");*/

            //}
            /*Negamax test = new Negamax();
            Stopwatch watch = new Stopwatch();
            string[] headers = { "MoveCount", "BestMove", "Time (ms)", "Color" };
            Dictionary<string, string> keyMoves = new Dictionary<string, string>();
            Application excelApplication = new Application();
            //excelApplication.Visible = true;


            var excelWorkBook = excelApplication.Workbooks.Add();
            var xlsxSheet = excelWorkBook.Sheets as Sheets;

            for (int threeRow = 5; threeRow < 100; threeRow += 5)
            {
                test.Three1 = threeRow;//threeRow

                for (int twoRow = 3; twoRow < 60; twoRow += 3)
                {
                    test.Two1 = twoRow;// twoRow;
                    for (int oneRow = 1; oneRow < 30; oneRow++)
                    {
                        long[,] dataArray = new long[44, 4];
                        test.ResetBitBoard();
                        string Moves = "";
                        test.One1 = oneRow;// oneRow;


                        int color = -1;
                        while (!test.IsWin() && !test.IsDraw())
                        {
                            color = -color;
                            //watch.Start();
                            test.NegaMax(int.MinValue + 1, int.MaxValue, 5, color, true);
                            //watch.Stop();
                            Moves += test.bestMove.ToString();
                            test.MakeMove(test.bestMove);
                            //dataArray[test.MoveCount - 1, 0] = test.MoveCount;
                            //dataArray[test.MoveCount - 1, 1] = test.bestMove;
                            //dataArray[test.MoveCount - 1, 2] = watch.ElapsedMilliseconds;
                            //dataArray[test.MoveCount - 1, 3] = color;
                            //watch.Reset();
                        }

                        if (test.IsWin() && color == 1)
                        {
                            keyMoves.Add(test.Three1.ToString() + "," + test.Two1.ToString() + "," + test.One1.ToString(), Moves);

                        }
                        else
                            continue;



                    }
                }
            }
            var excelWorkSheet1 = (Worksheet)xlsxSheet.Add(xlsxSheet[1]);
            excelWorkSheet1.Name = "Overview";
            int row = 1;
            foreach (string key in keyMoves.Keys)
            {
                excelWorkSheet1.Cells[row, 1] = key;
                excelWorkSheet1.Cells[row++, 2] = keyMoves[key];
            }
            excelWorkBook.SaveAs("testParameterNew.xlsx");
        }

            /*TestPlyEffect test1 = new DebugEval();

            test1.NegaMax(int.MinValue + 1, int.MaxValue, 5, 1, true);

            Console.ReadKey();*/

            /*TestPlyEffect<Negamax> test = new TestPlyEffect<Negamax>(7, 7);
            test.PlayConnectFour();
            test.CreateSheet("p1-5_P2-7");
            test.WriteToExcel("ThisIsTest");*/
            //TestPlyEffect<NegamaxArray/*NegaNoAlphaBeta*//*NegaTransNegamax*/> testPlyEffect = new TestPlyEffect<NegamaxArray/*Negamax/*NegaTrans/*NegaNoAlphaBeta*/>(9, 9);
            /*for (int i = 0; i < 1; i++)
            {
                testPlyEffect.PlayConnectFour();
                testPlyEffect.CreateSheet("Run" + (i + 1).ToString());
            }
            testPlyEffect.WriteToExcel("ArrayTest");*/
            NegaTrans test = new NegaTrans(9);
            int[] moveArray = { 3, 3, 3, 3, 3, 3, 1, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 5, 6, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 1 };

            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //|o| |x|o| |x|o|
            int expectedInt = 1;            //|x| |o|x| |o|x|
                                            //|o| |x|o| |x|o|
                                            //|x|x|o|x| |o|x|
                                            //|o|o|x|o| |x|o|
                                            //|x|x|o|x| |x|o|

            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, -1, true);

            Console.WriteLine(test.bestMove);
            //Console.WriteLine(test.EvaluateBoard());
            Console.ReadKey();
        }
    }

    public class ConvertStringArray
    {
        public static int StringToInt(string s)
        {
            return Convert.ToInt32(s);
        }
    }
    public class TestPlyEffect<T> where T : NegamaxArray/*NegaNoAlphaBeta*//*NegaTransNegamax*/, new()
    {
        private List<List<long>> data = new List<List<long>>();
        private int firstPlayerPlyDepth;
        private int secondPlayerPlyDepth;
        private int alpha = int.MinValue + 1;
        private int beta = int.MaxValue;
        private int color;
        private int plyDepth;
        private string[] headers = { "MoveCount", "Color", "PlyDepth", "BestMove", "Time (ms)", "Result" };
        private string result = "No result";
        private T plyNega = new T();
        Application excelApplication = new Application();
        private Workbook excelWorkbook;
        Sheets xlsxSheet;
        Worksheet excelWorkSheet;


        public TestPlyEffect(int p1, int p2)
        {
            firstPlayerPlyDepth = p1;
            secondPlayerPlyDepth = p2;
            excelApplication.Visible = true;
            excelWorkbook = excelApplication.Workbooks.Add();
            xlsxSheet = excelWorkbook.Sheets as Sheets;

        }
        public void PlayConnectFour()
        {
            while (!plyNega.IsDraw() && !plyNega.IsWin())
            {
                int player = plyNega.GetCurrentPlayer();
                MakeAndTimeMove(player);
                if (plyNega.IsWin())
                    result = "Win P" + (player + 1).ToString();
                else if (plyNega.IsDraw())
                    result = "Draw Game";
            }
            plyNega.ResetGame();
        }

        private void MakeAndTimeMove(int player)
        {
            Stopwatch Watch = new Stopwatch();
            if (player != 0 && player != 1)
            {
                Console.WriteLine("Not right number. Insert 0 for first player or 1 for second");
            }
            else if (player == 0)
            {
                plyDepth = firstPlayerPlyDepth;
                color = 1;
            }
            else
            {
                plyDepth = secondPlayerPlyDepth;
                color = -1;
            }
            List<long> tempData = new List<long>();

            Watch.Start();
            plyNega.NegaMax(alpha, beta, plyDepth, color, true);
            Watch.Stop();
            //plyNega.ResetTranspositionTable();
            tempData.Add(plyNega.MoveCount());
            tempData.Add(color);
            tempData.Add(plyDepth);
            tempData.Add(plyNega.bestMove);
            tempData.Add(Watch.ElapsedMilliseconds);
            data.Add(tempData);
            plyNega.MakeMove(plyNega.bestMove);
        }

        public void CreateSheet(string sheetName)
        {
            excelWorkSheet = (Worksheet)xlsxSheet.Add(xlsxSheet[1]);
            excelWorkSheet.Name = sheetName;
            for (int i = 1; i <= headers.Length; i++)
                excelWorkSheet.Cells[1, i] = headers[i - 1];

            excelWorkSheet.Cells[2, headers.Length] = result;

            for (int r = 0; r < data.Count; r++)
            {
                for (int c = 0; c < data[r].Count; c++)
                    excelWorkSheet.Cells[r + 2, c + 1] = data[r][c];

            }
            data.Clear();
        }
        public void WriteToExcel(string fileName)
        {
            excelWorkbook.SaveAs(fileName);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}

/*//TODO: Add a loop to this and make it write the moves in console
           Negamax test = new Negamax();
           Stopwatch watch = new Stopwatch();
           string[] headers = { "MoveCount", "BestMove", "Time (ms)", "Color" };
           Dictionary<string, string> keyMoves = new Dictionary<string, string>();
           Application excelApplication = new Application();
           //excelApplication.Visible = true;


           var excelWorkBook = excelApplication.Workbooks.Add();
           var xlsxSheet = excelWorkBook.Sheets as Sheets;

           for (int threeRow = 5; threeRow < 100; threeRow += 5)
           {
               test.Three1 = threeRow;//threeRow

               for (int twoRow = 3; twoRow < 60; twoRow += 3)
               {
                   test.Two1 = twoRow;// twoRow;
                   for (int oneRow = 1; oneRow < 30; oneRow++)
                   {
                       long[,] dataArray = new long[44, 4];
                       test.ResetBitBoard();
                       string Moves = "";
                       test.One1 = oneRow;// oneRow;


                       int color = -1;
                       while (!test.IsWin() && !test.IsDraw())
                       {
                           color = -color;
                           //watch.Start();
                           test.NegaMax(int.MinValue + 1, int.MaxValue, 7, color, true);
                           //watch.Stop();
                           Moves += test.bestMove.ToString();
                           test.MakeMove(test.bestMove);
                           //dataArray[test.MoveCount - 1, 0] = test.MoveCount;
                           //dataArray[test.MoveCount - 1, 1] = test.bestMove;
                           //dataArray[test.MoveCount - 1, 2] = watch.ElapsedMilliseconds;
                           //dataArray[test.MoveCount - 1, 3] = color;
                           //watch.Reset();
                       }

                       if (test.IsWin() && color == 1)
                       {
                           keyMoves.Add(test.Three1.ToString() + "," + test.Two1.ToString() + "," + test.One1.ToString(), Moves);

                       }
                       else
                           continue;



                   }
               }
           }
           var excelWorkSheet1 = (Worksheet)xlsxSheet.Add(xlsxSheet[1]);
           excelWorkSheet1.Name = "Overview";
           int row = 1;
           foreach(string key in keyMoves.Keys)
           {
               excelWorkSheet1.Cells[row, 1] = key;
               excelWorkSheet1.Cells[row++, 2] = keyMoves[key];
           }
           excelWorkBook.SaveAs("testParametersply9.xlsx");
       }*/
