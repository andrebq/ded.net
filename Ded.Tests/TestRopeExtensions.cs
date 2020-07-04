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
        public void SplitByShouldSplit()
        {
            var rope = RopeBuilder.BUILD("first line\nsecond line");
            var expected = new Queue<string>()
            {
                "first line",
                "second line",
            };
            foreach (var l in rope.SplitBy('\n'))
            {
                Assert.AreEqual(l.ToString(), expected.Dequeue());
            }
        }
    }

    static class QueueExtension
    {
        public static void Add<T>(this Queue<T> q, T value)
        {
            q.Enqueue(value);
        }
    }
}
