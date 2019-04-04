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
            test.columnHeight[0] = 4;
            test.columnHeight[1] = 13;
            test.columnHeight[2] = 14;
            test.columnHeight[3] = 27;
            test.columnHeight[4] = 34;
            test.columnHeight[5] = 41;
            test.columnHeight[6] = 43;
            int expectedInt = 4;
            int calcValue = test.NegaMax(test, -1000, 1000, 8);

            Assert.AreEqual(expectedInt, calcValue);

        }
    }
}
