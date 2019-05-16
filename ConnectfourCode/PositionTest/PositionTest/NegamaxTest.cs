using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;
using NegamaxTest;
using System.Collections.Generic;

namespace PositionTest
{
    /*class CreateNegaMaxTestArrange : Negamax
    {
        public CreateNegaMaxTestArrange(List<int> inputMoves)
        {
            foreach(int move in inputMoves)
            {
                this.MakeMove(move);
            }
        }
    }*/


    [TestClass]
    public class NegamaxTest
    {
        [TestMethod]
        public void NegaMaxFinishTest()
        {

            //Arrange
            NegaTrans test = new NegaTrans(9);

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
            NegaTrans test = new NegaTrans(9);
          
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
