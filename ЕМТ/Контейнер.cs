using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
namespace ЕМТ
{

    [Serializable]
    public class Контейнер
    {
        private static object Data;
        private static void Serialize(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(fileStream, Контейнер.Data);
            fileStream.Flush();
            fileStream.Close();
        }
        private static void Deserialize(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            Контейнер.Data = binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
        }

        public static TuringInfo GetTuringInfo(string path)
        {
            Контейнер.Deserialize(path);
		    ArrayList arrayList = (ArrayList)Контейнер.Data;
            return TuringInfo.Create(arrayList);
        }
    }
} 
