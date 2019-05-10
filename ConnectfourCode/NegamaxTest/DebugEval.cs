using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectfourCode;
using Microsoft.Office.Interop.Excel;


namespace NegamaxTest
{
    class DebugEval : BitBoard
    {
        public int bestMove { get; set; }
        int[] turnArray = { 0, 1, 2, 3, 4, 5, 6 };// { 3, 2, 4, 1, 5, 0, 6 };
        int count = 0;
        Dictionary<int, int> TranspositionTable = new Dictionary<int, int>();
        Application excelApplication;
        private Workbook excelWorkbook;
        Sheets xlsxSheet;
        Worksheet excelWorkSheet;

        public DebugEval()
        {
            excelApplication = new Application();
            excelApplication.Visible = true;
            excelWorkbook = excelApplication.Workbooks.Add();
            xlsxSheet = excelWorkbook.Sheets as Sheets;
            excelWorkSheet = (Worksheet)xlsxSheet.Add(xlsxSheet[1]);
        }



        public void ResetBestMove()
        {
            bestMove = 3;
            //TranspositionTable.Clear();
        }

        private const int height = 6, width = 7;
        public int NegaMax(int alpha, int beta, int maxDepth, int color, bool firstCall)

        //TODO: Skal med i implementeringen
        {

            if (IsWin() || maxDepth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                count++;
                string test = "m: ";
                for (int i = 0; i < moveHistory.Count; i++)
                    test += moveHistory[i].ToString();
                excelWorkSheet.Cells[count, 1] = test;
                excelWorkSheet.Cells[count, 2] = evalBuffer * color;
                return evalBuffer * color;

            }

            foreach (int i in turnArray)
            {
                MakeMove(i);
                if (CanPlay(i))
                {
                    int value = -NegaMax(-beta, -alpha, maxDepth - 1, -color, false);

                    if (value > beta)
                    {
                        UndoMove();
                        /*count++;
                        string test = "b: ";
                        for (int j = 0; j < moveHistory.Count; j++)
                            test += moveHistory[j].ToString();
                        excelWorkSheet.Cells[count, 1] = test;
                        excelWorkSheet.Cells[count, 2] = value;*/
                        return value;
                    }
                    if (value > alpha)
                    {
                        alpha = value;
                        /*string test = "a: ";
                        count++;
                        for (int j = 0; j < moveHistory.Count; j++)
                            test += moveHistory[j].ToString();
                        excelWorkSheet.Cells[count, 1] = test;
                        excelWorkSheet.Cells[count, 2] = value;
                        //excelWorkSheet.Cells[count, 3] = beta;*/
                        if (firstCall)
                            bestMove = i;

                    }
                }
                UndoMove();
            }
            return alpha;
        }
    }
}
