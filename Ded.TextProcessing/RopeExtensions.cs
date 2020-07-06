using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ded.TextProcessing
{
    public static class RopeExtensions
    {

        public static (int Row, int Col) IndexToLineColumn(this Rope rope, int idx, char nlChar)
        {
            (int Row, int Col) pos = (0, 0);
            foreach (char c in rope)
            {
                if (idx == 0) { break; }
                if (c == nlChar)
                {
                    pos.Row++;
                    pos.Col = 0;
                } else
                {
                    pos.Col++;
                }
                idx--;
            }
            return pos;
        }

        public static IEnumerable<Rope> SplitBy(this Rope rope, char c)
        {
            var start = 0;
            var idx = rope.IndexOf(c);
            if (idx < 0)
            {
                yield return rope;
            } else
            {
                while(idx >= start)
                {
                    yield return rope.SubSequence(start, idx);
                    start = idx + 1;
                    if (start == rope.Length())
                    {
                        break;
                    }
                    idx = rope.IndexOf(c, start);
                }
                yield return rope.SubSequence(start);
            }
        }
    }
}
