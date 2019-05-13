using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest
{
    [TestClass]
    public class EvaluateBoardLoop
    {
        [TestMethod]
        public void TestMethod1()
        {
            ArrayGameBoard test = new ArrayGameBoard();
            test.gameboard[3, 0] = 1;
            test.gameboard[3, 1] = 1;
            test.gameboard[2, 0] = 2;
            
            int expectedValue = test.EvaluataBoardLoop();
            int actual = test.EvaluateBoard();

            Assert.AreEqual(expectedValue, actual);
        }
    }
}
