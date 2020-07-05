using Ded.TextProcessing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ded.Tests
{
    [TestClass]
    public class TestTextBuffer
    {

        [TestMethod]
        public void TestInsert()
        {
            var b = TextBuffer.Empty;
            Assert.AreEqual("ab", b.Insert("a".AsRope()).Insert("b".AsRope()).Text.ToString());
        }

        public void TestRemove(string input, int offset, int remove, string expected)
        {
        }

        [TestMethod]
        public void TestMoveCursor()
        {
            var b = TextBuffer.Empty.Insert("abc123".AsRope()).MoveCursor(-1, CursorOffset.Current);
            Assert.AreEqual(5, b.Cursor);

            Assert.AreEqual("abc12", TextBuffer.Empty.Insert("abc123".AsRope()).MoveCursor(-1, CursorOffset.Current).Remove(1).Text.ToString());
            Assert.AreEqual("abc1", TextBuffer.Empty.Insert("abc123".AsRope()).Remove(-2).Text.ToString());
        }
    }
}
