using Lith.FlatFile.Core;
using Lith.FlatFile.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lith.FlatFile.Tests
{
    [TestClass]
    public class FlatteningHelperTests
    {
        [TestMethod]
        public void GetDatePoint_CorrectYear()
        {
            var actual = FlatteningHelper.GetDatePoint("yyyyMMdd", 'y');
            var expected = new Point(0, 4);

            Assert.AreEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void GetPointValue_CorrectYear()
        {
            var input = FlatteningHelper.GetDatePoint("yyyyMMdd", 'y');
            var actual = FlatteningHelper.GetPointValue(input, "20010908");
            var expected = 2001;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDatePoint_CorrectMonth()
        {
            var actual = FlatteningHelper.GetDatePoint("yyyyMMdd", 'M');
            var expected = new Point(4, 2);

            Assert.AreEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void GetPointValue_CorrectMonth()
        {
            var input = FlatteningHelper.GetDatePoint("yyyyMMdd", 'M');
            var actual = FlatteningHelper.GetPointValue(input, "20010908");
            var expected = 9;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDatePoint_NOTCorrectDay()
        {
            var actual = FlatteningHelper.GetDatePoint("yyyyMMdd", 'D');
            var expected = new Point(6, 2);

            Assert.AreNotEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void GetPointValue_NOTCorrectDay()
        {
            var input = FlatteningHelper.GetDatePoint("yyyyMMdd", 'D');
            var actual = FlatteningHelper.GetPointValue(input, "20010908");
            var expected = 8;

            Assert.AreNotEqual(expected, actual);
        }
    }
}
