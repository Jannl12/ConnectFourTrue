using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace ArrayGameBoardEvaluateTest
{
    [TestClass]
    public class NegamaxArrayTest
    {
        [TestMethod]
        public void NegaMaxFinishTest()
        {

            //Arrange
            NegaMaxAGB test = new NegaMaxAGB(9);

            int[] moveArray = { 3, 3, 3, 3, 3, 0, 2, 5, 5, 5, 1, 4, 2, 2, 1, 5, 5, 2, 2, 0, 3, 5, 2, 4, 0, 0, 0, 6 };
            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| | |x|x| |o| |
            int wrongMove1 = 4;             //|x| |x|x| |x| |
            int wrongMove2 = 1;             //|o| |o|o| |o| |
                                            //|x| |o|x| |o| |
                                            //|o|x|x|o|o|x| |
                                            //|o|x|x|x|o|o|o|    

            //Act
            int calcValue= test.GetBestMove();


            //Assert
            Assert.AreNotEqual(wrongMove1, calcValue);
            Assert.AreNotEqual(wrongMove2, calcValue);

        }

        [TestMethod]
        public void NegaMaxStartTest()
        {
            NegaMaxAGB test = new NegaMaxAGB(9);

            int expectedInt = 3;

            int calcValue = test.GetBestMove();

            Assert.AreEqual(expectedInt, calcValue);
        }

        [TestMethod]
        public void NegaMaxEndMinPlayer()
        {
            NegaMaxAGB test = new NegaMaxAGB(9);
            int[] moveArray = { 3, 3, 3, 3, 3, 6, 4, 5, 1, 2, 5, 5, 5, 1, 2, 6, 2, 1, 1, 1, 5, 1, 4, 6, 6, 6, 6, 0, 0, 0, 0, 5, 4, 4, 4, 4 };
            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| |o| | |o|o|x|
            int expectedInt = 2;            //| |o| |x|x|x|o|
                                            //|x|x| |o|o|x|x|
                                            //|o|o|x|x|x|o|o|
                                            //|x|o|x|o|x|x|o|
                                            //|o|x|o|x|x|o|o|

            int calcValue = test.GetBestMove();

            Assert.AreNotEqual(expectedInt, calcValue);
        }

        [TestMethod]
        public void NegaMaxDrawMinPlayer()
        {
            NegaMaxAGB test = new NegaMaxAGB(9);
            int[] moveArray = { 3, 3, 3, 3, 3, 3, 1, 2, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 5, 6, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 1 };

            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //|o| |x|o| |x|o|
            int expectedInt = 1;            //|x| |o|x| |o|x|
                                            //|o| |x|o| |x|o|
                                            //|x|x|o|x| |o|x|
                                            //|o|o|x|o| |x|o|
                                            //|x|x|o|x| |x|o|

            int calcValue = test.GetBestMove();

            Assert.AreEqual(expectedInt, calcValue);
        }


        [TestMethod]
        public void NegaMaxStartTestMaxEnd()
        {
            NegaMaxAGB test = new NegaMaxAGB(9);
            int[] moveArray = { 2, 3, 2, 1, 1, 0, 0, 5, 1, 6 };

            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| | | | | | | |
            int expectedInt = 3;            //| | | | | | | |
                                            //| | | | | | | |
                                            //| | | | | | | |
                                            //|x|x|x| | | | |
                                            //|o|o|x|o| |o|o|
  
            int calcValue = test.GetBestMove();

            Assert.AreEqual(expectedInt, calcValue);
        }

        [TestMethod]
        public void NegaMaxStartTestMinEnd()
        {
            NegaMaxAGB test = new NegaMaxAGB(9);
            int[] moveArray = { 2, 3, 2, 1, 1, 0, 0, 5, 1, 6, 1 };

            foreach (int move in moveArray) // 0 1 2 3 4 5 6
                test.MakeMove(move);        //| | | | | | | |
            int expectedInt = 4;            //| | | | | | | |
                                            //| | | | | | | |
                                            //| |x| | | | | |
                                            //|x|x|x| | | | |
                                            //|o|o|x|o| |o|o|

            int calcValue = test.GetBestMove();

            Assert.AreEqual(expectedInt, calcValue);
        }
    }
}
