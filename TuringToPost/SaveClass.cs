using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lib
{
    [Serializable]
    public class SaveClass
    {
        private Strip lenta;
        private string text;
        private string[,] prog;
        public string[,] Prog
        {
            get
            {
                return this.prog;
            }
            set
            {
                this.prog = value;
            }
        }
        public Strip Lenta
        {
            get
            {
                return this.lenta;
            }
            set
            {
                this.lenta = value;
            }
        }
        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }
        public SaveClass(Strip str, string[,] pro, string tex)
        {
            this.Lenta = str;
            this.Prog = pro;
            this.Text = tex;
        }
        public void Save(string path)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
            binaryFormatter.Serialize(fileStream, this);
            fileStream.Close();
        }
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
