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
            var tokens = new Tokenizer().Parse("Int. SceneHeading - Day");

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

        [TestMethod]
        public void ReturnNullTextElement()
        {
            var tokens = new Tokenizer().Parse("Blah");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());

        }

        [TestMethod]
        public void ReturnLineEnding()
        {
            var tokens = new Tokenizer().Parse("T\r\n");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[1].GetType());

        }
    }
}
