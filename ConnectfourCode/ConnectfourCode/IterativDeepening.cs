﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    class IterativDeepening:BitBoard
    {

        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
        int maxTimeInMiliseconds = 3000;
        private bool overtime = false;
        Stopwatch watch = new Stopwatch();
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();



        public int Deepening()
        {
            int depth = 3;
            int bestMove = 0;
            watch.Start();

            while(watch.ElapsedMilliseconds < maxTimeInMiliseconds)
            {
                int tempMove = (int)NegaMax(double.MinValue + 1, double.MaxValue, ++depth, 1, true, overtime);
                if (!overtime)
                    bestMove = tempMove;
            }
            watch.Reset();
            return bestMove;
        }

        public double NegaMax(double alpha, double beta, int depth, int color, bool firstCall, bool overtime)

        //TODO: Skal med i implementeringen
        {
            ulong lookUpBoardKey = GetBoardKey();
            int evalBuffer = 0;
            if (overtime)
                return 0;
            else if(watch.ElapsedMilliseconds > maxTimeInMiliseconds)
            {
                overtime = true;
                return 0;
            }
                
            if (IsWin() || depth == 0 || IsDraw())
            {
                if (TranspositionTable.TryGetValue(lookUpBoardKey, out evalBuffer))
                    return evalBuffer * color;
                else
                {
                    evalBuffer = EvaluateBoard();
                    TranspositionTable[lookUpBoardKey] = evalBuffer;
                    return evalBuffer * color;
                }
                
               

            }
            double value = int.MinValue;
            int bestMove = 0;

            foreach (int i in turnArray)
            {
                MakeMove(i);
                if (CanPlay(i))
                {
                    value = Math.Max(value, -NegaMax(-beta, -alpha, depth - 1, -color, false, overtime));

                    if (value > alpha)
                    {
                        alpha = value;
                        if (firstCall)
                            bestMove = i;

                    }
                    if (value >= beta)
                    {
                        UndoMove();
                        break;
                    }

                }
                UndoMove();
            }
            if (firstCall)
                return bestMove;
            else
                return value;
        }
    }
}
