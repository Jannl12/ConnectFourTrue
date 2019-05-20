using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using ConnectfourCode;
using System.Collections.Generic;

namespace ArrayGameBoardEvaluateTest //TODO: MAKE THIS  AGBtest GREAT AGAIN!!!!!!!!
{
    [TestClass]
    public class ArrayGameBoardEvaluateBoard
    {
        [TestMethod]
        public void ArrayEvaluateBoardLeft()
        {
            //Arrange
            ArrayGameBoard AGBtest = new ArrayGameBoard();

            int[] moveArray = { 1, 0, 1, 0, 1, 0 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

            //Act                                       //  1 2 3 4 5 6 7
            int expectedValue = 0;                      // | | | | | | | | 6
            int calcValue = AGBtest.EvaluateBoard();    // | | | | | | | | 5
                                                        // | | | | | | | | 4
                                                        // |o|x| | | | | | 3
            //Assert                                    // |o|x| | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| | | | | | 1
        }

        [TestMethod]
        public void EvaluateBoardMiddle()
        {
            //Arrange
            ArrayGameBoard AGBtest = new ArrayGameBoard();
            int[] moveArray = { 3, 3, 3 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 0;                      // | | | | | | | | 5
            int calcValue = AGBtest.EvaluateBoard();    // | | | | | | | | 4
                                                        // | | | |x| | | | 3
            //Assert                                    // | | | |o| | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |x| | | | 1
        }

        [TestMethod]
        public void EvaluateBoardRight()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 5, 6, 5, 6, 5, 6 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

            //Act                                       //  1 2 3 4 5 6 7
            int expectedValue = 0;                      // | | | | | | | | 6
                                                        // | | | | | | | | 5
            int calcValue = AGBtest.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | |x|o| 3
                                                        //Assert                                    // | | | | | |x|o| 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | | | |x|o| 1
        }

        [TestMethod]
        public void EvaluateBoardAlmostAllCombinations()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 0, 1, 1, 3, 3, 3, 0, 3, 0, 4, 5, 5, 6, 6, 6, 6, 6, 4, 3 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -1;                     // | | | |x| | |x| 5
            int calcValue = AGBtest.EvaluateBoard();    // | | | |o| | |o| 4
                                                        // |x| | |o| | |x| 3
            //Assert                                    // |x|x| |x|o|o|o| 2
            Assert.AreEqual(expectedValue, calcValue);  // |x|o| |o|o|x|x| 1
        }





        [TestMethod]
        public void EvaluateBoardOnlyMiddle()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 3 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 0;                      // | | | | | | | | 5
            int calcValue = AGBtest.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | | | | 3
                                                        //Assert                                    // | | | | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |o| | | | 1
        }


        [TestMethod]
        public void EvaluateBoardBaseCase()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);

            //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 1;                      // | | | |x|x| | | 5
            int calcValue = AGBtest.EvaluateBoard();    // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o| | |x|o| |o| 1
        }

        [TestMethod]
        public void EvaluateBoardBaseFailCase()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0, 3 };
            foreach (int move in moveArray)
                AGBtest.MakeMove(move);
                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | |x| | | | 6
            int expectedValue = 2;                      // | | | |x|x| | | 5
            int calcValue = AGBtest.EvaluateBoard();    // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o| | |x|o| |o| 1
        }
        [TestMethod]
        public void EvaluateBoardBaseRightMove()
        {
            //Arrange
            ArrayGameBoard  AGBtest = new ArrayGameBoard();
            int[] moveArray = { 3, 3, 3, 3, 3, 4, 4, 4, 4, 6, 4, 0, 1 };
            foreach (int move in moveArray)
            AGBtest.MakeMove(move);

                                                        //  1 2 3 4 5 6 7   
            //Act                                       // | | | | | | | | 6
            int expectedValue = 4;                      // | | | |x|x| | | 5
            int calcValue = AGBtest.EvaluateBoard();    // | | | |o|x| | | 4
                                                        // | | | |x|o| | | 3
            //Assert                                    // | | | |o|x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| |x|o| |o| 1
        }
    }  
}