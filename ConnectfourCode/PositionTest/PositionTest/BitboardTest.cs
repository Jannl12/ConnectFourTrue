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
            test.bitGameBoard[0] = 0x7;            // 0 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0111
            test.bitGameBoard[1] = 0x380;          // 0 0000 0000 0000 0000 0000 0000 0000 0000 0000 0011 1000 0000

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
            test.bitGameBoard[0] = 0xA00000;       // 0 0000 0000 0000 0000 0000 0000 1010 0000 0000 0000 0000 0000
            test.bitGameBoard[1] = 0x400000;       // 0 0000 0000 0000 0000 0000 0000 0100 0000 0000 0000 0000 0000

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
            test.bitGameBoard[0] = 0x1C0000000000; // 0 0001 1100 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000
            test.bitGameBoard[1] = 0x3800000000;   // 0 0000 0000 0011 1000 0000 0000 0000 0000 0000 0000 0000 0000

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
            test.bitGameBoard[0] = 0x540802400107; // 0 0101 0100 0000 1000 0000 0010 0100 0000 0000 0001 0000 0111
            test.bitGameBoard[1] = 0x281031A00080; // 0 0010 1000 0001 0000 0011 0001 1010 0000 0000 0000 1000 0000
                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -6;                     // | | | |o| | |o| 5
            int calcValue = test.EvaluateBoard();       // | | | |x| | |x| 4
                                                        // |o| | |x| | |o| 3
            //Assert                                    // |o|o| |o|x|x|x| 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|x| |x|x|o|o| 1
        }

        [TestMethod]
        public void EvaluateBoardOnlyMiddle()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x200000;      // 0 0000 0000 0000 0000 0000 0000 0010 0000 0000 0000 0000 0000
            test.bitGameBoard[1] = 0x0;           // 0 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 7;                      // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | | | | 3
            //Assert                                    // | | | | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // | | | |o| | | | 1
        }

        [TestMethod]
        public void EvaluateBoardOnlyLeft()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x1;           // 0 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0001
            test.bitGameBoard[1] = 0x0;           // 0 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000 0000

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = 3;                      // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | | | | | 3
            //Assert                                    // | | | | | | | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o| | | | | | | 1
        }

        [TestMethod]
        public void EvaluateBoard111x ()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x4081;         // 0|0000|00 00|0000|0 000|0000| 0000|000 0|0000|01 00|0000|1 000|0001
            test.bitGameBoard[1] = 0x70008102;     // 0|0000|00 00|0000|0 000|0111| 0000|000 0|0000|10 00|0001|0 000|0010

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | | | | | 6
            int expectedValue = -37;                    // | | | | | | | | 5
            int calcValue = test.EvaluateBoard();       // | | | | | | | | 4
                                                        // | | | | |x| | | 3
            //Assert                                    // |x|x|x| |x| | | 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|o|o| |x| | | 1
        }
        
        [TestMethod]
        public void EvaluateBoard111x_x111()
        {
            //Arrange
            BitBoard test = new BitBoard();
            test.bitGameBoard[0] = 0x102040004081; // 0|0001|00 00|0010|0 000|0100| 0000|000 0|0000|01 00|0000|1 000|0001
            test.bitGameBoard[1] = 0xEDDBB003870E; // 0|1110|11 01|1101|1 011|1011| 0000|000 0|0011|10 00|0111|0 000|1110

                                                        //  1 2 3 4 5 6 7
            //Act                                       // | | | | |x|x|x| 6
            int expectedValue = -37;                    // | | | | |x|x|x| 5
            int calcValue = test.EvaluateBoard();       // |x|x|x| |x|x|x| 4
                                                        // |x|x|x| |o|o|o| 3
            //Assert                                    // |x|x|x| |x|x|x| 2
            Assert.AreEqual(expectedValue, calcValue);  // |o|o|o| |x|x|x| 1
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
