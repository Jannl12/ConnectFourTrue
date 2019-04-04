using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest
{
    [TestClass]
    public class NegamaxTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Negamax test = new Negamax();
            test.bitGameBoard[0] = 0x4BB4A01615; //blå (lige)
            test.bitGameBoard[1] = 0x140bB74009A4; //rød (ulige)
            test.MoveCount = 31;
            test.columnHeight[0] += 6;
            test.columnHeight[1] += 6;
            test.columnHeight[2] += 0;
            test.columnHeight[3] += 6;
            test.columnHeight[4] += 6;
            test.columnHeight[5] += 6;
            test.columnHeight[6] += 1;
            int expectedInt = 2;

            test.NegaMax(test, int.MinValue, int.MaxValue, 9, 1);
            int calcValue = test.bestMove;

            Assert.AreEqual(expectedInt, calcValue);

        }
    }
}
