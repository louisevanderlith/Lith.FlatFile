using Lith.FlatFile.DummyModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            foreach(var item in input)
            {
                actual.AppendLine(item.ToFlatLine());
            }
        }

        [TestMethod]
        public void ReadFile_Complete()
        {

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
                Name = "TEST"
            };

            result.Add(item);
            
            var itemb = new ExampleObjectB();

            result.Add(itemb);

            return result;
        }
    }
}
