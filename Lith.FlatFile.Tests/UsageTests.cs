using Lith.FlatFile.DummyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lith.FlatFile.Tests
{
    [TestClass]
    public class UsageTests
    {
        [TestMethod]
        public void WriteFile_ByLine_Complete()
        {
            var input = GetInputData();
            var actual = new StringBuilder();

            foreach (var item in input)
            {
                actual.AppendLine(item.ToFlatLine());
            }
        }

        [TestMethod]
        public void WriteFile_NullValues_NoErrors()
        {
            var input = new ExampleObjectB();
            var actual = input.ToFlatLine();
            Console.WriteLine(actual);
            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void ReadFile_Complete()
        {
            var input = new ExampleObject
            {
                Amount = 543.221M,
                ChosenOption = DumbEnum.OptionB,
                FewOptions = 'Y',
                FileDate = DateTime.Now,
                HasSomething = false,
                Name = "TEST",
                FlatObj = new ExampleNested
                {
                    Name = "JANNI",
                },
            };

            var line = input.ToFlatLine();
            Console.Write(line);
            var xampleObj = new LineBreaker<ExampleObject>(line).Object;

            Assert.AreEqual(input.FlatObj.Name, xampleObj.FlatObj.Name);
        }

        private List<IFlatObject> GetInputData()
        {
            var result = new List<IFlatObject>();

            var item = new ExampleObject
            {
                Amount = 543.221M,
                ChosenOption = DumbEnum.OptionB,
                FewOptions = 'Y',
                FileDate = DateTime.Now,
                HasSomething = false,
                Name = "TEST",
                FlatObj = new ExampleNested
                {
                    Name = "JANNI",
                },
            };

            result.Add(item);

            var itemb = new ExampleObjectB();

            result.Add(itemb);

            return result;
        }


    }
}
