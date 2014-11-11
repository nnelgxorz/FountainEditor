using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FountainEditor.Elements;
using FountainEditor;

namespace FountainEditorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tokens = new Tokenizer().Parse("");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
        }
    }
}
