using System;

namespace Lib
{
    
        public enum InstructionType
        {
            Left,
            Right,



            Tag,
            Clear,
            Condition,
            Stop
        }
      
      
        [Serializable]
        public class Strip
        {
            private bool[] lenta;

            public bool[] Lenta
            {
                get { return lenta; }
                set { lenta = value; }
            }
            private int pos;
            public int Pos
            {
                get
                {
                    return this.pos;
                }
                set
                {
                    this.pos = value;
                }
            }
            public int Length
            {
                get
                {
                    return this.lenta.Length;
                }
            }
            public int Min
            {
                get
                {
                    return -this.lenta.Length / 2;
                }
            }
            public int Max
            {
                get
                {
                    return this.lenta.Length / 2;
                }
            }
            public bool this[int i]
            {
                get
                {
                    if (i > -1000 && i < 1000)
                    {
                        return this.lenta[i + 999];
                    }
                    throw new ArgumentOutOfRangeException();
                }
                set
                {
                    if (i > -1000 && i < 1000)
                    {
                        this.lenta[i + 999] = value;
                        return;
                    }
                    throw new ArgumentOutOfRangeException();
                }
            }
            public Strip()
            {
                this.lenta = new bool[1999];
                this.pos = 0;
            }
            public int GetLastLeftMark()
            {
                int result;
                for (int i = 0; i < this.lenta.Length; i++)
                {
                    if (this.lenta[i])
                    {
                        result = i - 999;
                        return result;
                    }
                }
                result = 0;
                return result;
            }
            public int GetLastRightMark()
            {
                int result;
                for (int i = this.lenta.Length - 1; i >= 0; i--)
                {
                    if (this.lenta[i])
                    {
                        result = i - 999;
                        return result;
                    }
                }
                result = 0;
                return result;
            }
        }
}
    

