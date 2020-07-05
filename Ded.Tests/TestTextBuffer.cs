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
    }
}
