using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolGraphicsApp
{
    internal interface IDataFormatter
    {
        void Save<T>(string path, T data);
        T Load<T>(string path);
    }
}
