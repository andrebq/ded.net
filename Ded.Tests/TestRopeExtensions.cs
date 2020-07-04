using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Ded.TextProcessing;
using System.Runtime.CompilerServices;

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
