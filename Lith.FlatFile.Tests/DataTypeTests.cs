using Lith.FlatFile.Core;
using Lith.FlatFile.DummyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lith.FlatFile.Tests
{
    [TestClass]
    public class DataTypeTests
    {
        [TestMethod]
        public void EnumSerialize()
        {
            var input = DumbEnum.OptionA;
            var actual = input.Transform(2);
            var expected = "01";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumDeserialize()
        {
            var input = "01";
            var actual = input.ToEnum<DumbEnum>();
            var expected = DumbEnum.OptionA;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BoolSerialize()
        {
            var input = false;
            var actual = input.Transform("YES", "NO");
            var expected = "NO ";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void BoolDeserialize()
        {
            var input = "NO ";
            var actual = input.ToBool("YES");
            var expected = false;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalSerialize()
        {
            var input = 567.89M;
            var actual = input.Transform(10, 2);
            var expected = "0000056789";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalSerializeAdvanced()
        {
            var input = 5.6789M;
            var actual = input.Transform(10, 4);
            var expected = "0000056789";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalDeserialize()
        {
            var input = "0000056789";
            var actual = input.ToDecimal(2);
            var expected = 567.89M;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DecimalDeserializeAdvanced()
        {
            var input = "0000056789";
            var actual = input.ToDecimal(4);
            var expected = 5.6789M;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeSerialize()
        {
            var input = new DateTime(2001, 09, 08);
            var actual = input.Transform("yyyyMMdd");
            var expected = "20010908";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeSerializeAdvanced()
        {
            var input = new DateTime(2001, 09, 08, 13, 58, 12);
            var actual = input.Transform("yyyyMMddHHmmss");
            var expected = "20010908135812";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeDeserialize()
        {
            var input = "20010908";
            var actual = input.ToDateTime("yyyyMMdd");
            var expected = new DateTime(2001, 09, 08);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DateTimeDeserializeAdvanced()
        {
            var input = "20010908135812";
            var actual = input.ToDateTime("yyyyMMddHHmmss");
            var expected = new DateTime(2001, 09, 08, 13, 58, 12);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FlatObjectDeserialize()
        {
            var input = "JANNI";
            var actual = input.ToFlatObject<ExampleNested>();
            var expected = new ExampleNested
            {
                Name = input
            };

            Assert.AreEqual(expected.ToFlatLine(), actual.ToFlatLine());
        }
    }
}
