using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConnectfourCode;

namespace PositionTest //TODO: MAKE THIS TEST GREAT AGAIN!!!!!!!!
{
    [TestClass]
    public class BitboardTest
    {
        [TestMethod]
        public void TestCanPlay7SameColumn()
        {
            BitBoard test = new BitBoard(0x55, 0x15);
            bool expectedBool = false;
            bool calcValue = test.CanPlay(0);

            Assert.AreEqual(expectedBool, calcValue);

        }
        [TestMethod]
        public void CanPlay6SamnCoulmn()
        {
            BitBoard test = new BitBoard(63, 0);
            bool expectedBool = true;
            bool calcValue = test.CanPlay(0);

            Assert.AreEqual(expectedBool, calcValue);

        }
        [TestMethod]
        public void CanPlay7SamnColumn7()
        {
            BitBoard test = new BitBoard(0x1FC0000000000, 0); //Hexa decimal for 7 in the 7th column
            bool expectedBool = false;
            bool calcValue = test.CanPlay(6);

            Assert.AreEqual(expectedBool, calcValue);

        }
        [TestMethod]
        public void CanPlay6SamnColumn7()
        {
            BitBoard test = new BitBoard(0xFC0000000000, 0);//Hexa decimal for 7 in the 7th column
            bool expectedBool = true;
            bool calcValue = test.CanPlay(6);

            Assert.AreEqual(expectedBool, calcValue);

        }
    }
}
