using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Ded.TextProcessing;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Ded.Tests
{
    [TestClass]
    public class TestRopeExtensions
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow("\n1\n2\n")]
        [DataRow("\n")]
        public void SplitByShouldSplit(string text)
        {
            var rope = RopeBuilder.BUILD(text);
            var expected = text.Split('\n').ToQueue<string>();
            foreach (var l in rope.SplitBy('\n'))
            {
                Assert.AreEqual(expected.Dequeue(), l.ToString());
            }
            Assert.AreEqual(0, expected.Count, "Queue should be empty after tests");
        }

        [DataRow("abc\n123", 0, 0, 0)]
        [DataRow("abc\n123", 4, 1, 0)]
        [DataRow("abc\n123", 1, 0, 1)]
        [DataTestMethod]
        public void IndexToLineColumn(string text, int idx, int row, int col)
        {
            var r = RopeBuilder.BUILD(text);
            var coord = r.IndexToLineColumn(idx, '\n');
            Assert.AreEqual(coord.Row, row, "Rows should be the same");
            Assert.AreEqual(coord.Col, col, "Column should be the same");
        }
    }

    static class QueueExtension
    {
        public static void Add<T>(this Queue<T> q, T value)
        {
            q.Enqueue(value);
        }

        public static Queue<T> ToQueue<T>(this T[] v)
        {
            return new Queue<T>(v);
        }
    }
}
