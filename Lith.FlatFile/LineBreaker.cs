using System;

namespace Lith.FlatFile
{
    public class LineBreaker<T> where T : IFlatObject
    {
        private T _object;
        private readonly string _line;

        public LineBreaker(string line)
        {
            _line = line;
        }

        public T Object
        {
            get
            {
                if (_object == null)
                {
                    _object = GetObject();
                }

                return _object;
            }
        }

        private T GetObject()
        {
            var result = Activator.CreateInstance<T>();
            var objType = result.GetType();
            var properties = result.GetFlatProperties();
            var breaker = new Parser();

            foreach (var prop in properties)
            {
                var resultProperty = objType.GetProperty(prop.Name);
                var propertyType = resultProperty.PropertyType;

                var value = breaker.Parse(propertyType, _line, prop.Attributes);

                if (value != null)
                {
                    if (prop.Name == "ID" && result.ID != value.ToString())
                    {
                        //We don't want to process the wrong line.
                        break;
                    }

                    if (resultProperty.CanWrite)
                    {
                        resultProperty.SetValue(result, value);
                    }
                }
            }

            return result;
        }
    }
}
