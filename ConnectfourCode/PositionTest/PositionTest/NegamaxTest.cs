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

            int[] moveArray = { 3, 3, 3, 3, 3, 0, 2, 5, 5, 5, 1, 4, 2, 2, 1, 5, 5, 2, 2, 0, 3, 5, 2, 4, 0, 0, 0, 6};
            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| | |x|x| |o| |
            int wrongMove1 = 4;             //|x| |x|x| |x| |
            int wrongMove2 = 1;             //|o| |o|o| |o| |
                                            //|x| |o|x| |o| |
                                            //|o|x|x|o|o|x| |
                                            //|o|x|x|x|o|o|o|    

            //Act
            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, 1, true);
            int calcValue = test.bestMove;

            //Assert
            Assert.AreNotEqual(wrongMove1, calcValue);
            Assert.AreNotEqual(wrongMove2, calcValue);

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
        public void NegaMaxEndMinPlayer()
        {
            NegaTrans test = new NegaTrans(9);
            int[] moveArray = {3, 3, 3, 3, 3, 6, 4, 5, 1, 2, 5, 5, 5, 1, 2, 6, 2, 1, 1, 1, 5, 1, 4, 6, 6, 6, 6, 0, 0, 0, 0, 5, 4, 4, 4, 4};
            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| |o| | |o|o|x|
            int expectedInt = 2;            //| |o| |x|x|x|o|
                                            //|x|x| |o|o|x|x|
                                            //|o|o|x|x|x|o|o|
                                            //|x|o|x|o|x|x|o|
                                            //|o|x|o|x|x|o|o|

            test.NegaMax(int.MinValue + 1, int.MaxValue, 9, -1, true);
            int calcValue = test.bestMove;

            Assert.AreNotEqual(expectedInt, calcValue);
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
