using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace Ded.TextProcessing
{
    public class TextBuffer
    {
        public static readonly TextBuffer Empty = new TextBuffer()
        {
            Text = RopeBuilder.BUILD(string.Empty),
            Cursor = 0,
        };
        public Rope Text { get; private set; }
        public int Cursor { get; private set; }

        private TextBuffer() { }

        public TextBuffer Insert(Rope text)
        {
            var t = Text.SubSequence(0, Cursor).Append(text).Append(Text.SubSequence(Cursor)).Rebalance();
            return new TextBuffer()
            {
                Text = t,
                Cursor = Cursor + text.Length(),
            };
        }
    }

    public static class StringExtensions
    {
        public static Rope AsRope(this string v) => RopeBuilder.BUILD(v);
    }
}
