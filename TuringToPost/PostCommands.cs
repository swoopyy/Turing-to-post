using System;

namespace Lib
{
    public static class PostCommands
    {
        public static string Stop
        {
            get { return " S  Стоп"; }
        }

        public static string Tag
        {
            get { return " V  Метка"; }
        }

        public static string Left
        {
            get { return "<- Влево"; }
        }

        public static string Right
        {
            get { return "-> Вправо"; }
        }

        public static string Clear
        {
            get { return " X  Удаление"; }
        }

        public static string Condition
        {
            get { return " ?  Переход"; }
        }

        public static string[] CreateLeftCommand(int reference, string comment=null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Left;
            s[1] = (reference).ToString();
            s[2] = comment == null ? "" : comment;
            return s;
        }

        public static string[] CreateRightCommand(int reference, string comment = null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Right;
            s[1] = (reference).ToString();
            s[2] = comment == null ? "" : comment;
            return s;
        }
        public static string[] CreateTagCommand(int reference, string comment = null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Tag;
            s[1] = (reference).ToString();
            s[2] = comment == null ? "" : comment;
            return s;
        }
        public static string[] CreateClearCommand(int reference, string comment = null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Clear;
            s[1] = (reference).ToString();
            s[2] = comment == null ? "" : comment;
            return s;
        }
        public static string[] CreateConditionCommand(int reference1, int reference2, string comment = null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Condition;
            s[1] = PostCommands.MakeCondition(reference1, reference2);
            s[2] = comment == null ? "" : comment;
            return s;
        }
        public static string[] CreateStopCommand(string comment = null)
        {
            string[] s = new string[3];
            s[0] = PostCommands.Stop;
            s[1] = "";
            s[2] = comment == null ? "" : comment;
            return s;
        }

        public static string MakeCondition(int first, int second)
        {
            if (first < 1 || second < 1) throw new ArgumentException("cond");
            return first + "," + second;
        }
    }
}
