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
            var tokens = new Tokenizer().Parse("# Outline Element \r\n");

            Assert.AreEqual(typeof(OutlineTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[1].GetType());
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

        [TestMethod]
        public void ReturnNullTextElement()
        {
            var tokens = new Tokenizer().Parse("  ");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());

        }

        [TestMethod]
        public void ReturnLineEnding()
        {
            var tokens = new Tokenizer().Parse("\r\n");

            Assert.AreEqual(typeof(LineEnding), tokens[0].GetType());
        }
    }

    [TestClass]
    public class OptimizerTests
    {
        [TestMethod]
        public void OptimizeCharacterAndDialogue()
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
                new NullTextElement("tests"),
                new LineEnding(""),
                new NullTextElement("^OTHER"),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("I"),
                new NullTextElement("love"),
                new NullTextElement("tests"),
                new NullTextElement("too."),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTY TEST");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ParentheticalTextElement), "Hmm");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), "I love tests");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[6], typeof(DualDialogueTextElement), "^OTHER TEST");
            TestElementTypeAndValue(elements[7], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[8], typeof(DialogueTextElement), "I love tests too.");
            TestElementTypeAndValue(elements[9], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeSceneHeadings()
        {
            var elements = new List<Element>
            {
                new SceneHeadingTextElement("int."),
                new NullTextElement("test"),
                new NullTextElement("- test"),
                new LineEnding(""),
                new NullTextElement("Test"),
                new NullTextElement("test"),
                new NullTextElement("test."),
                new LineEnding(""),
                new NullTextElement(".Close"),
                new NullTextElement("on"),
                new NullTextElement("test"),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(SceneHeadingTextElement), "int. test - test");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test test test.");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[4], typeof(SceneHeadingTextElement), ".Close on test");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeTransitions()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("SMASH"),
                new NullTextElement("CUT"),
                new TransitionTextElement("TO:"),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(TransitionTextElement), "SMASH CUT TO:");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeAction()
        {
            var elements = new List<Element>
            {
                new NullTextElement("Test"),
                new NullTextElement("Test"),
                new NullTextElement("Test."),
                new LineEnding(""),
                new NullTextElement("Test"),
                new NullTextElement("Test"),
                new NullTextElement("Test."),
                new LineEnding(""),

            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Test Test Test.");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Test Test Test.");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
        }

        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
