using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;

namespace Ded.TextProcessing
{
    public class TextBuffer
    {
        public const CursorOffset Origin = CursorOffset.Origin;

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

        public TextBuffer Remove(int ammount)
        {
            if (ammount < 0)
            {
                return this.MoveCursor(ammount, CursorOffset.Current).Remove(-ammount);
            }
            ammount = Math.Min(ammount, Text.Length() - Cursor);
            var t = Text.SubSequence(0, Cursor).Append(Text.SubSequence(Cursor + ammount));
            return new TextBuffer
            {
                Text = t,
                Cursor = Cursor,
            };
        }

        public TextBuffer MoveCursor(int n, CursorOffset offset)
        {
            switch(offset)
            {
                case CursorOffset.Origin:
                    n = Math.Min(n, Text.Length() - 1);
                    return new TextBuffer()
                    {
                        Cursor = n,
                        Text = this.Text,
                    };
                case CursorOffset.Current:
                    n = Cursor + n;
                    n = Math.Max(0, Math.Min(n, Text.Length() - 1));
                    return new TextBuffer()
                    {
                        Cursor = n,
                        Text = this.Text,
                    };
                default:
                    throw new ArgumentException($"Offset: {offset} is not supported");
            }
        }
    }

    public enum CursorOffset
    {
        Origin,
        Current
    }

    public static class StringExtensions
    {
        public static Rope AsRope(this string v) => RopeBuilder.BUILD(v);
    }
}
