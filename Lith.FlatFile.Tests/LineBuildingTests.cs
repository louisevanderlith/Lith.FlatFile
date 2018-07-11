using Lith.FlatFile.DummyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Lith.FlatFile.Tests
{
    [TestClass]
    public class LineBuildingTests
    {
        private readonly LineBuilder builder;
        private readonly IFlatObject _flatObj;

        public LineBuildingTests()
        {
            _flatObj = new ExampleObject
            {
                Amount = 123.456M,
                ChosenOption = DumbEnum.OptionA,
                FewOptions = 'X',
                FileDate = new DateTime(2011, 05, 03),
                HasSomething = false,
                Name = "TEST"
            };

            builder = new LineBuilder(_flatObj);
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
            var expected = "EN0120110503XTEST                     000000000123456                      ";
            var actual = builder.Line;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeserializedLine_HasAllProperties()
        {
            var breaker = new LineBreaker<ExampleObject>("EN0120110503XTEST                     000000000123456                      ");
            var actual = breaker.Object;

            Assert.AreEqual(DumbEnum.OptionA, actual.ChosenOption);
        }
    }
}
