using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FountainEditor.Elements;
using FountainEditor;
using System.Collections.Generic;

namespace FountainEditorTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void ReturnOutlineCheck()
        {
            var tokens = new Tokenizer().Parse("# Outline Element");

            Assert.AreEqual(typeof(OutlineTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSynopsis()
        {
            var tokens = new Tokenizer().Parse("= Synopsis Element");

            Assert.AreEqual(typeof(SynopsisTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingINT()
        {
            var tokens = new Tokenizer().Parse("Int. Scene Heading - Day");

            Assert.AreEqual(typeof(SceneHeadingTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingEXT()
        {
            var tokens = new Tokenizer().Parse("Ext. Scene Heading - Day");

            Assert.AreEqual(typeof(SceneHeadingTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnTransition()
        {
            var tokens = new Tokenizer().Parse("Cut to:");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(TransitionTextElement), tokens[1].GetType());
        }

        [TestMethod]
        public void ReturnTransitionForced()
        {
            var tokens = new Tokenizer().Parse(">Fade to Black");

            Assert.AreEqual(typeof(TransitionTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnCenteredText()
        {
            var tokens = new Tokenizer().Parse(">The End<");

            Assert.AreEqual(typeof(CenteredTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnNote()
        {
            var tokens = new Tokenizer().Parse("[[This is a note]]");

            Assert.AreEqual(typeof(NoteTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnLyrics()
        {
            var tokens = new Tokenizer().Parse("~ Sing this");

            Assert.AreEqual(typeof(LyricsTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnPageBreak()
        {
            var tokens = new Tokenizer().Parse("===");

            Assert.AreEqual(typeof(PageBreakTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnParenthetical()
        {
            var tokens = new Tokenizer().Parse("(Wryly)");

            Assert.AreEqual(typeof(ParentheticalTextElement), tokens[0].GetType());
        }
    }
    [TestClass]
    public class OptimizerTests
    {
        [TestMethod]
        public void OptimizerTest()
        {
            var elements = new List<Element>
            {
                new NullTextElement("TESTY"),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new ParentheticalTextElement("Hmm"),
                new LineEnding(""),
                new NullTextElement("I"),
                new NullTextElement("love"),
                new NullTextElement("tests")
            };

            new Optimizer().Optimize(elements);

            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTY TEST");
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[0], typeof(ParentheticalTextElement), "Hmm");
            TestElementTypeAndValue(elements[0], typeof(Action), "");
            TestElementTypeAndValue(elements[0], typeof(DialogueTextElement), "I love tests");

        }

        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
