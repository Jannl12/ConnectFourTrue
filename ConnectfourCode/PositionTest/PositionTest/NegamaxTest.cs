using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;
using NegamaxTest;
using System.Collections.Generic;

namespace PositionTest
{
    class CreateNegaMaxTestArrange : Negamax
    {
        public CreateNegaMaxTestArrange(List<int> inputMoves)
        {
            foreach(int move in inputMoves)
            {
                this.MakeMove(move);
            }
        }
    }


    [TestClass]
    public class NegamaxTest
    {
        [TestMethod]
        public void NegaMaxFinishTest()
        {
<<<<<<< HEAD
            //Arrange
            NegaTrans test = new NegaTrans();
=======
            Negamax test = new Negamax(9);

            test.bitGameBoard[1] = 0x4BB40A01615; //blå (lige)
            test.bitGameBoard[0] = 0x140B74009AA; //rød (ulige)
            //test.MoveCount = 31;
            test.columnHeight[0] += 6;
            test.columnHeight[1] += 6;
            test.columnHeight[2] += 0;
            test.columnHeight[3] += 6;
            test.columnHeight[4] += 6;
            test.columnHeight[5] += 6;
            test.columnHeight[6] += 1;

            int expectedInt = 2;
>>>>>>> master

            int[] moveArray = {3,3,3,3,3,0,2,5,5,5,1,4,2,2,1,5,5,2,2,0,3,5,2,4,0,0,0,6 };
            foreach (int move in moveArray)
                test.MakeMove(move);
            int expectedInt = 6;

            //Act
            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);
            int calcValue = test.bestMove;

            //Assert
            Assert.AreEqual(expectedInt, calcValue);

        }

        [TestMethod]
        public void NegaMaxStartTest()
        {
            Negamax test = new Negamax();
            test.bitGameBoard[0] = 0x0; //blå (lige)
            test.bitGameBoard[1] = 0x0; //rød (ulige)
          
            int expectedInt = 3;


            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);
            int calcValue = test.bestMove;

            Assert.AreEqual(expectedInt, calcValue);

        }

        [TestMethod]
        public void NegaMaxStartTestNoAlphaBeta()
        {
            NegaNoAlphaBeta test = new NegaNoAlphaBeta();
            int expectedInt = 3;

            test.NegaMax(9, 1, true);

            int calcValue = test.bestMove;

            Assert.AreEqual(expectedInt, calcValue);

        }
    }
}
