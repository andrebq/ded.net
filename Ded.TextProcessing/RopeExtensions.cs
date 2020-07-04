using System;
using System.Collections.Generic;
using System.Text;

namespace Ded.TextProcessing
{
    public static class RopeExtensions
    {

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
                    idx = rope.IndexOf(c, start);
                }
                yield return rope.SubSequence(start);
            }
        }
    }
}
