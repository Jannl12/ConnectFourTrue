using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest //TODO: MAKE THIS TEST GREAT AGAIN!!!!!!!!
{
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
