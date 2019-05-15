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
        public void NegaMaxFinishTest()
        {
            //Arrange
            Negamax test = new Negamax();
            test.bitGameBoard[0] = 0x140B74009AA; //gul (ulige) // 000 0101000 0001011 0111010 0000000 0010011 0101010
            test.bitGameBoard[1] = 0x4BB40A01615; //rød (lige)  // ‭001 0010111 0110100 0000101 0000000 0101100 0010101

            test.MoveCount = 31;
            test.columnHeight[0] += 6;                                  //  1 2 3 4 5 6 7
            test.columnHeight[1] += 6;                                  // |o|x| |o|x|o| | 6
            test.columnHeight[2] += 0;                                  // |x|o| |o|x|x| | 5
            test.columnHeight[3] += 6;                                  // |o|x| |o|o|o| | 4
            test.columnHeight[4] += 6;                                  // |x|x| |x|x|x| | 3
            test.columnHeight[5] += 6;                                  // |o|o| |o|o|x| | 2
            test.columnHeight[6] += 1;                                  // |x|o| |x|o|x|x| 1

            //Act
            int expectedInt = 2;
            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);
            int calcValue = test.bestMove;
            
            //Act
            Assert.AreEqual(expectedInt, calcValue);

        }

        [TestMethod]
        public void NegaMaxStartTest()
        {
            Negamax test = new Negamax();
            test.bitGameBoard[1] = 0x0; //rød (ulige)
            test.bitGameBoard[0] = 0x0; //blå (lige)
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
