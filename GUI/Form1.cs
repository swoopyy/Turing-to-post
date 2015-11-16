/*
 * Программа предназначена для конвертации файлов .alg в аналогичные файлы .mpst
 * Кодировка входных символов записана в описании программы,
 * некоторые символы в зависимости от программы, написанной на машине Тьюринга, могут 
 * не использоваться.
 * Для логических функций, головка на выходе указывает на первый символ кодировки исходного
 * символа машины Тьюринга
 */


using System;
using System.IO;
using Lib;
using ЕМТ;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button_convert.Enabled = false;
            this.button_saveto.Enabled = false;
        }

        TuringInfo ti;
        SaveClass cl;
        public  TuringInfo GetFile()
        {
            TuringInfo ti = new TuringInfo();
            System.Windows.Forms.OpenFileDialog openfile = new System.Windows.Forms.OpenFileDialog();
            openfile.Filter = "EMT files|*.alg";
            var result = openfile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ti = Контейнер.GetTuringInfo(openfile.FileName);
            }
            else throw new FileLoadException("file weren't chosen");
            return ti;
        }


        public  void SaveTo(SaveClass cl)
        {
            System.Windows.Forms.SaveFileDialog openfile = new System.Windows.Forms.SaveFileDialog();
            openfile.Filter = "Post files|*.mpst";
            var result = openfile.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                cl.Save(openfile.FileName);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button_open_Click(object sender, EventArgs e)
        {
            try
            {
                ti = this.GetFile();
                this.button_convert.Enabled = true;
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_saveto_Click(object sender, EventArgs e)
        {
            try
            {
                this.SaveTo(cl);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_convert_Click(object sender, EventArgs e)
        {
            try
            {
                string[,] prog = ti.GeneratePostProgram();
                string text = "В программе используется следующая кодировка:\r\nнижнее подчеркивание = пробел\r\n";

                foreach (var item in ti.Codes)
                {
                    text +=  item.Key + " - " + ReplaceSpaces(item.Value) + "; " ;
                }

                cl = new SaveClass(new Strip(), prog, text);
                this.button_saveto.Enabled = true;
                MessageBox.Show("Converted!");
            } catch(ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        static string ReplaceSpaces(string s)
        {
            string st = "";
            for (int i = 0; i < s.Length; i++)
                if (s[i] == ' ') st += '_';
                else
                    st += s[i];
            return st;
        }
    }
}
