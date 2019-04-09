using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest
{
    [TestClass]
    public class EvaluationTest
    {
        [TestMethod]
        public void TestFourScore()
        {
            //intialize
            BitBoard testPoint = new BitBoard();
            testPoint.bitGameBoard[0] = 0x380;
            testPoint.bitGameBoard[1] = 0xF;
            int expectedValue = int.MaxValue;
            //act
            int calcValue = testPoint.EvaluateBoard();
            //assert
            Assert.AreEqual(expectedValue, calcValue);
        }
        [TestMethod]
        public void TestThreeScore()
        {
            //intialize
            BitBoard testPoint = new BitBoard();
            testPoint.MoveCount = 1;
            testPoint.bitGameBoard[0] = 0x7;
            testPoint.bitGameBoard[1] = 0x380;
            int expectedValue = 15;
            //act
            int calcValue = testPoint.EvaluateBoard();
            //assert
            Assert.AreEqual(expectedValue, calcValue);
        }
        [TestMethod]
        public void TestTwoScore()
        {
            //intialize
            BitBoard testPoint = new BitBoard();
            testPoint.bitGameBoard[0] = 0x380;
            testPoint.bitGameBoard[1] = 0x3;
            int expectedValue = int.MaxValue;
            //act
            int calcValue = testPoint.EvaluateBoard();
            //assert
            Assert.AreEqual(expectedValue, calcValue);
        }
        [TestMethod]
        public void TestOneScore()
        {
            //intialize
            BitBoard testPoint = new BitBoard();
            testPoint.bitGameBoard[0] = 0x380;
            testPoint.bitGameBoard[1] = 0x1;
            int expectedValue = int.MaxValue;
            //act
            int calcValue = testPoint.EvaluateBoard();
            //assert
            Assert.AreEqual(expectedValue, calcValue);
        }
    }
}
