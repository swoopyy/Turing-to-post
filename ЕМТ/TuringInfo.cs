using System;
using System.Collections;
using TuringToPostLib;
using System.Collections.Generic;
using Lib;
namespace ЕМТ
{
    public struct TuringInfo
    {
        Dictionary<string, string> codes;

        public Dictionary<string, string> Codes
        {
            get { return codes; }
            set { codes = value; }
        }
        string[][][] _table;

        public string[][][] Table
        {
            get { return _table; }
            set { _table = value; }
        }

        string[] _alphabet;

        public string[] Alphabet
        {
            get { return _alphabet; }
            set { _alphabet = value; }
        }

        string[] _states;

        public string[] States
        {
            get { return _states; }
            set { _states = value; }
        }

        Dictionary<string, int> _dicStates;

        public Dictionary<string, int> DicStates
        {
            get { return _dicStates; }
            set { _dicStates = value; }
        }

        string[] _comments;

        public string[] Comments
        {
            get { return _comments; }
            set { _comments = value; }
        }
        public TuringInfo(string[][][] tab, string[] alph, string[] stat, string[] comm)
        {
            this._table = tab;
            this._alphabet = alph;
            this._comments = comm;
            this._states = stat;
            codes = TuringToPost.Encode(alph); // кодировка
            this._dicStates = new Dictionary<string, int>();

            this.ImproveTable(); // заменяем все символы замены стоп клеток на символ алфавита соответствующего столбца

            for(int i=0; i<_states.Length; i++)
            {
               
                try
                {
                    _dicStates.Add(_states[i], i);
                } catch
                {
                    throw new ArgumentException("Команды с одинаковыми именами недопустимы");
                }
            }
        }
        /// <summary>
        /// Добавляет состояния перехода и замены в клетки остановки, чтобы ее мог съесть replace и check
        /// </summary>
        private void ImproveTable()
        {
            for (int i = 0; i < Table.Length; i++)
                for (int j = 0; j < Table[0][0].Length; j++)
                    if (Table[i][1][j] == "halt")
                    {
                        Table[i][0][j] = Alphabet[j];
                        Table[i][2][j] = this.States[0];
                    }
        }
        public static TuringInfo Create(ArrayList list)
        {
            return new TuringInfo((string[][][])list[0], (string[])list[1], (string[])list[2], (string[])list[3]);
        }

        public string[,] GeneratePostProgram()
        {
            
            TuringToPost t = new TuringToPost(codes[this.Alphabet[0]].Length);
            List<int> st = new List<int>(); // индексы состояний
            List<int> refern = new List<int>(); //индексы, в которые будем пихать отсылки

            int fir = this.Table.Length; // колв-о комманд (строк в таблице)
            int sec = this.Table[0].Length; // 3 по идее
            int thi = this.Table[0][0].Length; // кол-во столбцов
            for(int i=0; i<fir; i++) // генерим программу без отсылок
            {
                st.Add(t.Prog.Count + 1); // добавляем индекс состояния

                for(int j=0; j<thi; j++)
                {
                    if (Table[i][0][j] == "" && Table[i][1][j] == "") continue; // если состояние не описано

                    string alph = codes[Alphabet[j]],
                           stt = codes[Table[i][0][j]],
                           mover = Table[i][1][j];

                    int count = this.calcStLen(alph, stt, mover); // длина для неудачной отсылки

                    // неверные отсылки, длину надо прибавлять к текущему состоянию
                    t.Check(alph, t.Prog.Count + 3 * t.Length + 1, t.Prog.Count + count + 1);

                    t.Replace(alph, stt);

                    if (mover == "r") 
                        t.MoveRight();
                    else
                        if (mover == "l")
                            t.MoveLeft();
                        else
                            if (mover == "halt")
                            t.Stop();

                   
                     refern.Add(t.Prog.Count - 1);
                }
            }

           t.Stop(); // остановка в конце по сут не нужна, но последняя отсылка указывает на несуществующую команду
            

            for (int i = 0, k = 0; i < fir; i++) // генерим отсылки
            {

                for (int j = 0; j < thi; j++)
                {
                    if (this.Table[i][2][j] == "") continue;

                    int statind = this.DicStates[this.Table[i][2][j]];
                    int what = refern[k++];
                    int where = st[statind];
                    if (t.Prog[what][0] != PostCommands.Stop)
                    t.Prog[what][1] = where.ToString();
                }
            }

            string[,] outst = new string[t.Prog.Count, t.Prog[0].Length];
            for (int i = 0; i < outst.GetLength(0); i++)
                for (int j = 0; j < outst.GetLength(1); j++)
                    outst[i, j] = t.Prog[i][j];

       
            return outst;
        }

        /// <summary>
        /// Считает длину команды
        /// </summary>
        private int calcStLen(string one, string two, string mover)
        {
            int count = 3 * one.Length;

            for (int i = 0; i < one.Length; i++)
                if (one[i] == two[i])
                    count++;
                else
                    count += 2;
            count++; // остаточный сдвиг вправо в реплейсе

            if (mover == "r" || mover == "l")
                count += one.Length;
            else 
                if (mover == "halt") count++;

            return count;
        }

       
            
    }
}
