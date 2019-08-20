using Lith.FlatFile.DummyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lith.FlatFile.Tests
{
    [TestClass]
    public class LineBuildingTests
    {
        private readonly LineBuilder builder;
        private IFlatObject _flatObj;

        public LineBuildingTests()
        {
            _flatObj = new ExampleObject
            {
                Amount = 123.456M,
                ChosenOption = DumbEnum.OptionA,
                FewOptions = 'X',
                FileDate = new DateTime(2011, 05, 03),
                HasSomething = false,
                Name = "TEST",
                FlatObj = new ExampleNested
                {
                    Name = "JANNI",
                },
                ShortMessage = "HAPPI"
            };

            builder = new LineBuilder(_flatObj);
        }

        [TestMethod]
        public void LineLength_TooLongProperty_AutomaticallyShortened()
        {
            _flatObj = new ExampleObject
            {
                Amount = 123.456M,
                ChosenOption = DumbEnum.OptionA,
                FewOptions = 'X',
                FileDate = new DateTime(2011, 05, 03),
                HasSomething = false,
                Name = "TEST",
                FlatObj = new ExampleNested
                {
                    Name = "JANNIE", //1char more
                }
            };

            var line = new LineBuilder(_flatObj).Line;
            Assert.AreNotEqual(string.Empty, line);
        }

        [TestMethod]
        public void FinalLineLength_IsCorrect()
        {
            var expected = _flatObj.TotalLineLength;
            var actual = builder.Line.Length;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FinalLineContent_IsCorrect()
        {
            var expected = "EN0120110503XTEST                     000000000123456JANNIHAPPI     ";
            var actual = builder.Line;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeserializedLine_HasAllProperties()
        {
            var breaker = new LineBreaker<ExampleObject>("EN0120110503XTEST                     000000000123456JANNIHAPPI     ");
            var actual = breaker.Object;

            Assert.AreEqual(DumbEnum.OptionA, actual.ChosenOption);
        }

        [TestMethod]
        public void DeserializedLine_ShortString_HasAllProperties()
        {
            var breaker = new LineBreaker<ExampleObject>("EN0120110503XTEST                     000000000123456JANNIHAPPI");
            var actual = breaker.Object;

            Assert.AreEqual(DumbEnum.OptionA, actual.ChosenOption);
        }
    }
}
