using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialFunction
{
    [Serializable]
    public class SerialClass
    {
        string name;
        List<object> param;

        public SerialClass(string n, List<object> p)
        {
            name = n;
            param = p;
        }

        public string GetName()
        {
            return name;
        }

        public List<object> GetParam()
        {
            return param;
        }
    }
}
