﻿using Lith.FlatFile.Core;
using System;
using System.Text;

namespace Lith.FlatFile
{
    public class LineBuilder
    {
        private readonly IFlatObject _flatObject;
        private string _line;

        public LineBuilder(IFlatObject flatObject)
        {
            _flatObject = flatObject;
        }

        public string Line
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_line))
                {
                    _line = GetLine();
                }

                return _line;
            }
        }

        private string GetLine()
        {
            var result = new StringBuilder();
            var properties = _flatObject.GetFlatProperties();
            var transformer = new Transformer();

            foreach (var prop in properties)
            {
                try
                {
                    var item = transformer.Transform(prop);

                    result.Append(item);
                }
                catch (Exception exc)
                {
                    var message = string.Format("{0} couln't be transformed. See inner exception for details.", prop.Name);
                    throw new Exception(message, exc);
                }
            }

            var difference = _flatObject.TotalLineLength - result.Length;

            if (difference >= 0)
            {
                result.Append(FlatteningHelper.Filler(difference));
            }
            else
            {
                var msg = string.Format("The total line length {0} is more than specified {1}", result.Length, _flatObject.TotalLineLength);
                throw new Exception(msg);
            }

            return result.ToString();
        }
    }
}
