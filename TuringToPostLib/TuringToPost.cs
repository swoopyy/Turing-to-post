using System;
using Lib;
using System.Collections.Generic;

namespace TuringToPostLib
{
    public enum HeadPosition {Start, End}
    public class TuringToPost
    {
        int length;

        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        List<string[]> prog;

        public List<string[]> Prog
        {
            get { return prog; }
            set { prog = value; }
        }

        public static Dictionary<string,string> Encode(string[] states)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] codes = new string[states.Length];

            int length = (int)Math.Ceiling(Math.Log(states.Length, 2));

            for (int i = 0; i < states.Length; i++ )
            {
                codes[i] = TuringToPost.getCode(i, length, states[i]);
            }

            TuringToPost.improveSpaceCode(codes, states);

            for (int i = 0; i < states.Length; i++)
            {
                dic.Add(states[i], codes[i]);
            }

            return dic;   
        }

        private static string getCode(int number, int length, string state)
        {
            //if (state == "_") return "   ";
            string s="";

            for(;number>0;number/=2)
            {
                s += (number % 2) == 1 ? "V" : " ";
            }

            int len = s.Length;
            for(int i=0; i<length-len; i++)
            {
                s += " ";
            }

            return s;
        }

        private static void improveSpaceCode(string[] codes, string[] values)
        {
            int spccode = 0;
            int spcvalue = 0;

            for(int i = 0; i <codes.Length; i++)
            {
                int k = 0;

                for (int j = 0; j < codes[i].Length; j++)
                    if (codes[i][j] == 'V') k++;

                if (k == 0) spccode = i;
            }

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine(values[i]);
                if (values[i] == "_")
                    spcvalue = i;
            }

           // Console.WriteLine(values[spcvalue]); Console.ReadKey();
            string t = codes[spccode];
            codes[spccode] = codes[spcvalue];
            codes[spcvalue] = t;

        }
            
    
        /// <param name="length">длина кода состояния</param>
        public TuringToPost(int length)
        {
            this.length = length;
            this.prog = new List<string[]>();
        }
        /// <summary>
        /// остановка программы
        /// длина команды - 1
        /// </summary>
        public void Stop(string com = null)
        {
            string[] s = PostCommands.CreateStopCommand(com);
            prog.Add(s);
        }
      
        /// <summary>
        /// позиция головки находится в самой первой ячейке кода символа
        /// сдвигаемся в начало следующей ячейки
        /// длина команды - length
        /// </summary>
        public void MoveRight(string com = null)
        {
            int index = prog.Count+1;
            for(int i=0; i<length; i++)
            {
                string[] s = PostCommands.CreateRightCommand(++index, com);
                prog.Add(s);
            }
        }
        /// <summary>
        /// Позиция головки находится в начале правого символа(т.е в первой ячейке его кода)
        /// длина команды - length
        /// </summary>
        public void MoveLeft(string com = null)
        {
            int index = prog.Count + 1;
            for (int i = 0; i < length; i++)
            {
                string[] s = PostCommands.CreateLeftCommand(++index, com);
                prog.Add(s);
            }
        }
        /// <summary>
        /// замена символа
        /// длина команды - динамическая
        /// </summary>
        /// <param name="one">заменяемый символ</param>
        /// <param name="two">заменяющий символ</param>
        /// <param name="pos">позиция головки</param>
        /// <returns>длину команды</returns>
        public void Replace(string one, string two, string com = null, HeadPosition pos = HeadPosition.End)
        {
           int index = prog.Count + 1;

           if(pos == HeadPosition.End)
           {
               // остановка в начале замененного символа
               for(int i=two.Length-1; i>=0; i--)
               {
                   //Console.WriteLine(one + " " + two);
                   if(one[i]==two[i])
                   {
                       string[] s = PostCommands.CreateLeftCommand(++index, com);
                       prog.Add(s);
                   } else
                       if(one[i]!=two[i] && one[i]=='V')
                   {
                       string[] s = PostCommands.CreateClearCommand(++index, com);
                       string[] s2 = PostCommands.CreateLeftCommand(++index, com);
                       prog.Add(s); prog.Add(s2);
                   } else
                       {
                           string[] s = PostCommands.CreateTagCommand(++index, com);
                           string[] s2 = PostCommands.CreateLeftCommand(++index, com);
                           prog.Add(s); prog.Add(s2);
                       }
               }
           } else if(pos == HeadPosition.Start)
           {
               // остановка в начале следующего символа
               for (int i = 0; i < two.Length; i++)
               {
                   if (one[i] == two[i])
                   {
                       string[] s = PostCommands.CreateRightCommand(++index, com);
                       prog.Add(s);
                   }
                   else
                       if (one[i] != two[i] && one[i] == 'V')
                       {
                           string[] s = PostCommands.CreateClearCommand(++index, com);
                           string[] s2 = PostCommands.CreateRightCommand(++index, com);
                           prog.Add(s); prog.Add(s2);
                       }
                       else
                       {
                           string[] s = PostCommands.CreateTagCommand(++index,com);
                           string[] s2 = PostCommands.CreateRightCommand(++index,com);
                           prog.Add(s); prog.Add(s2);
                       }
               }
           }

           string[] s1 = PostCommands.CreateRightCommand(++index); // здесь можно поправить, но не обязательно
           prog.Add(s1);

        }

        /// <summary>
        /// проверяет символ на соответствие заданному
        /// возвращает головку в начало проверяемого символа, если символы на совпали
        /// иначе оставляет головку в конце проверяемого симовола 
        /// выполняет отсылку на команду, обозначенную в index
        /// длина команды - 3*length
        /// </summary>
        /// <param name="symbol">проверямый символ</param>
        /// <param name="reference1">индекс отсылки в случае верной проверки</param>
        /// <param name="reference2">индекс отсылки в случае неуспешной проверки</param>
        /// <returns>количетво созданных комманд</returns>
        public void Check(string checker, int reference1, int reference2, string com = null)
        {
           
            int leftmover = prog.Count + length*2 + 2, //переменная, указывающая на первую комманду сдвига влево
                index = prog.Count+1;

            for (int i = 0; i < length; i++)
            {
                if (checker[i] == 'V')
                {
                    string[] s = PostCommands.CreateConditionCommand(i != 0 ? leftmover + length - i - 1 : reference2, ++index, com);
                    string[] s2 = PostCommands.CreateRightCommand(++index, com);
                    prog.Add(s); prog.Add(s2);
                }

                if (checker[i] == ' ')
                {
                    string[] s = PostCommands.CreateConditionCommand(++index, i != 0 ? leftmover + length - i - 1 : reference2, com);
                    string[] s2 = PostCommands.CreateRightCommand(++index, com);
                    prog.Add(s); prog.Add(s2);
                }
            }

                // успешная проверка
                string[] s1 = PostCommands.CreateLeftCommand(reference1, com);
                prog.Add(s1);
                index = prog.Count + 1;
                // создаем комманды для сдвига
                for (int i = 0; i < length - 1; i++)
                {
                    string[] s = PostCommands.CreateLeftCommand(++index, com);
                    prog.Add(s);
                }
                prog[prog.Count - 1] = PostCommands.CreateLeftCommand(reference2, com);
            
        }
           
    }
}
