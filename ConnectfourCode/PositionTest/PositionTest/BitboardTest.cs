using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ConnectfourCode;
using System.Collections.Generic;

namespace PositionTest //TODO: MAKE THIS TEST GREAT AGAIN!!!!!!!!
{
    /*[TestClass]
    public class EvaluateBoardTest
    {
        [TestMethod]
        public void EvaluateBoardLeft()
        {
            //Arrange
            BitBoard test = new BitBoard();

            int[] moveArray = { 1, 0, 1, 0, 1, 0 };
            foreach (int move in moveArray)
                test.MakeMove(move);

            //Act                                       //  1 2 3 4 5 6 7
            int expectedValue = 0;                      // | | | | | | | | 6
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 5
                                                        // | | | | | | | | 4
                                                        // |o|x| | | | | | 3
            //Assert                                    // |o|x| | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| | | | | | 1
        }
        
        [TestMethod]
        public void EvaluateBoardMiddle()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 3, 3, 3};
            foreach (int move in moveArray)
                test.MakeMove(move);

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 0;                      // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | |x| | | | 3
            //Assert                                    // | | | |o| | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |x| | | | 1
        }
        
        [TestMethod]
        public void EvaluateBoardRight()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 5, 6, 5, 6, 5, 6 };
            foreach (int move in moveArray)
                test.MakeMove(move);

            //Act                                       //  1 2 3 4 5 6 7
            int expectedValue = 0;                      // | | | | | | | | 6
                                                        // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | |x|o| 3
            //Assert                                    // | | | | | |x|o| 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | | | |x|o| 1
        }

        [TestMethod]
        public void EvaluateBoardAlmostAllCombinations()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 0, 1, 1, 3, 3, 3, 0, 3, 0, 4, 5, 5, 6, 6, 6, 6, 6, 4, 3 };
            foreach (int move in moveArray)
                test.MakeMove(move);

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -1;                     // | | | |x| | |x| 5
            int calcValue = test.EvaluateBoard();       // | | | |o| | |o| 4
                                                        // |x| | |o| | |x| 3
            //Assert                                    // |x|x| |x|o|o|o| 2
            Assert.AreEqual(expectedValue, calcValue);  // |x|o| |o|o|x|x| 1
        }



        //        [TestMethod]
        //        public void EvaluateBoardRight()
        //        {
        //            //Arrange
        //            BitBoard test = new BitBoard();

        //            test.bitGameBoard[0] = 0x1C0000000000; // 0 0001 1100 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000
        //            test.bitGameBoard[1] = 0x3800000000;   // 0 0000 0000 0011 1000 0000 0000 0000 0000 0000 0000 0000 0000

        //            Stopwatch sw = new Stopwatch(), sw2 = new Stopwatch(); ;
        //            //  1 2 3 4 5 6 7
        //            //Act  
        //            sw.Start();// | | | | | | | | 6
        //            int expectedValue = test.EvaluateBoard();
        //            sw.Stop();
        //            sw2.Start();// | | | | | | | | 5
        //            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
        //            sw.Stop();                                           // | | | | | |x|o| 3
        //            //Assert                                    // | | | | | |x|o| 2
        //            Assert.IsTrue(sw.Elapsed <= sw2.Elapsed);  // | | | | | |x|o| 1
        //        }

        [TestMethod]
        public void EvaluateBoardOnlyMiddle()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 3 };
            foreach (int move in moveArray)
                test.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 0;                      // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | | | | 3
            //Assert                                    // | | | | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |o| | | | 1
        }


        [TestMethod]
        public void EvaluateBoardBaseCase()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0};
            foreach (int move in moveArray)
                test.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 1;                      // | | | |x|x| | | 5
            int calcValue = test.EvaluateBoard();       // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o| | |x|o| |o| 1
        }

        [TestMethod]
        public void EvaluateBoardBaseFailCase()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0, 3 };
            foreach (int move in moveArray)
                test.MakeMove(move);

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | |x| | | | 6
            int expectedValue = 2;                      // | | | |x|x| | | 5
            int calcValue = test.EvaluateBoard();       // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o| | |x|o| |o| 1
        }
        [TestMethod]
        public void EvaluateBoardBaseRightMove()
        {
            //Arrange
            BitBoard test = new BitBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0, 1 };
            foreach (int move in moveArray)
                test.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 4;                      // | | | |x|x| | | 5
            int calcValue = test.EvaluateBoard();       // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| |x|o| |o| 1
        }
    }
}


        }
        [TestMethod]
        public void CanPlay6SamnColumn7()
        {
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0xFC0000000000;
            test.bitGameBoard[0] = 0;  //Hexa decimal for 7 in the 7th column
            bool expectedBool = true;

            bool calcValue = test.CanPlay(6);

            Assert.AreEqual(expectedBool, calcValue);
        }
    }
    //[TestClass]
    //public class TestCountSetBits
    //{
    //    [TestMethod]
        //public void TestCSB3Bit()
        //{
        //    BitBoard test = new BitBoard();
        //    test.bitGameBoard[0] = 0x55;
        //    test.bitGameBoard[1] = 0x15;
        //    int expectedValue = 3;

        //    int calcValue = test.CountSetBits(0x7);

        //    Assert.AreEqual(expectedValue, calcValue);

        //}
    //}*/
}

