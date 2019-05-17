using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;
using NegamaxTest;



namespace PositionTest
{
    [TestClass]
    public class NegamaxTest
    {
        [TestMethod]
        public void BlockFourInARow()
        {
            //Arrange
            NegaTrans test = new NegaTrans();
            test.bitGameBoard[1] = 0x1055400080; //blå (lige)
            test.bitGameBoard[0] = 0x8A2A04000; //rød (ulige)
            //test.MoveCount = 13;
            test.columnHeight[0] += 0;
            test.columnHeight[1] += 1;
            test.columnHeight[2] += 1;
            test.columnHeight[3] += 6;
            test.columnHeight[4] += 4;
            test.columnHeight[5] += 2;
            test.columnHeight[6] += 0;

            //Act
            int expectedMove = 6;
            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);

            //Assert
            Assert.AreEqual(expectedMove, test.bestMove);
        }



        [TestMethod]
        public void NegaMaxFinishTest()
        {
            Negamax test = new Negamax();
 
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

            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);

            int calcValue = test.bestMove;

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
