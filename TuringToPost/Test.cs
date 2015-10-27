using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lib
{
    public class Test
    {
        public static SaveClass Load(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            SaveClass result = (SaveClass)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return result;
        }

    }
}
