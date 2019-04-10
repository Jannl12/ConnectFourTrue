using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest //TODO: MAKE THIS TEST GREAT AGAIN!!!!!!!!
{
    [TestClass]
    public class EvaluateBoardTest
    {
        [TestMethod]
        public void EvaluateBoardLeft()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x7;
            test.bitGameBoard[1] = 0x380;

            //Act                                       //  1 2 3 4 5 6 7
            int expectedValue = -6;                     // | | | | | | | | 6
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
            test.bitGameBoard[0] = 0xA00000;
            test.bitGameBoard[1] = 0x400000;
                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 9;                      // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | |o| | | | 3
            //Assert                                    // | | | |x| | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |o| | | | 1
        }
        [TestMethod]
        public void EvaluateBoardRight()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x1C0000000000;
            test.bitGameBoard[1] = 0x3800000000;
                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -6;                     // | | | | | | | | 5
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
            test.bitGameBoard[0] = 0x1C1002400095;
            test.bitGameBoard[1] = 0x801A0C10A;
                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -18;                    // | | | |o| | |o| 5
            int calcValue = test.EvaluateBoard();       // | | | |x| | |x| 4
                                                        // |o| | |x| | |o| 3
            //Assert                                    // |o|o| |o|x|x|x| 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| |x|x|o|o| 1
        }
    }
    [TestClass]
    public class CanPlayTest
    {
        [TestMethod]
        public void TestCanPlay7SameColumn()
        {
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x55;
            test.bitGameBoard[1] = 0x15;

            bool expectedBool = false;
            bool calcValue = test.CanPlay(0);

            Assert.AreEqual(expectedBool, calcValue);

        }
        [TestMethod]
        public void CanPlay6SamnCoulmn()
        {
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0;
            test.bitGameBoard[1] = 63;
            bool expectedBool = true;
            bool calcValue = test.CanPlay(0);

            Assert.AreEqual(expectedBool, calcValue);

        }
        [TestMethod]
        public void CanPlay7SamnColumn7()
        {

            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x1FC0000000000;  //Hexa decimal for 7 in the 7th column
            test.bitGameBoard[1] = 0;
            bool expectedBool = false;

            bool calcValue = test.CanPlay(6);

            Assert.AreEqual(expectedBool, calcValue);

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
    [TestClass]
    public class TestCountSetBits
    {
        [TestMethod]
        public void TestCSB3Bit()
        {
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x55;
            test.bitGameBoard[1] = 0x15;
            int expectedValue = 3;

            int calcValue = test.CountSetBits(0x7);

            Assert.AreEqual(expectedValue, calcValue);

        }
    }
}
