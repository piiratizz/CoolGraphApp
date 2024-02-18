using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace CoolGraphicsApp.ProgramLogic.Save
{
    internal class BinaryDataFormatter : IDataFormatter
    {
        private const string FileName = "config.bin";
        public T Load<T>(string path)
        {
            T data;

            if (!File.Exists(path + FileName)) return default;

            using (var readStream = new FileStream(path + FileName, FileMode.Open))
            {
                var formatter = new BinaryFormatter();

#pragma warning disable SYSLIB0011
                data = (T)formatter.Deserialize(readStream);
#pragma warning restore SYSLIB0011
            }
            return data;
        }

        public void Save<T>(string path, T data)
        {
            if(!Directory.Exists(path)) 
            {
                Directory.CreateDirectory(path);
            }

            using (var readStream = new FileStream(path + FileName, FileMode.Create))
            {
                var formatter = new BinaryFormatter();

#pragma warning disable SYSLIB0011
                formatter.Serialize(readStream, data);
#pragma warning restore SYSLIB0011
            }
        }
    }
}
