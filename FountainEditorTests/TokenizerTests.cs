using FountainEditor;
using FountainEditor.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FountainEditorTests
{
    [TestClass]
    public class TokenizerTests
    {
        [TestMethod]
        public void ReturnOutlineCheck()
        {
            var tokens = Tokenizer.Parse("# Outline Element");

            Assert.AreEqual(typeof(OutlineTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSynopsis()
        {
            var tokens = Tokenizer.Parse("= Synopsis Element");

            Assert.AreEqual(typeof(SynopsisTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingINT()
        {
            var tokens = Tokenizer.Parse("Int. SceneHeading - Day");

            Assert.AreEqual(typeof(SceneHeadingTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingEXT()
        {
            var tokens = Tokenizer.Parse("Ext. Scene Heading - Day");

            Assert.AreEqual(typeof(SceneHeadingTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnTransition()
        {
            var tokens = Tokenizer.Parse("Cut to:");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            Assert.AreEqual(typeof(TransitionTextElement), tokens[2].GetType());
        }

        [TestMethod]
        public void ReturnTransitionForced()
        {
            var tokens = Tokenizer.Parse("> Fade to Black");

            Assert.AreEqual(typeof(TransitionTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnCenteredText()
        {
            var tokens = Tokenizer.Parse(">The End<");

            Assert.AreEqual(typeof(CenteredTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnNote()
        {
            var tokens = Tokenizer.Parse("[[This is a note\r\n  \r\n]]");

            Assert.AreEqual(typeof(NoteTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnLyrics()
        {
            var tokens = Tokenizer.Parse("~ Sing this");

            Assert.AreEqual(typeof(LyricsTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnPageBreak()
        {
            var tokens = Tokenizer.Parse("===\r\n");

            Assert.AreEqual(typeof(PageBreakTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnParenthetical()
        {
            var tokens = Tokenizer.Parse("(Wryly)");

            Assert.AreEqual(typeof(ParentheticalTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnNullTextElement()
        {
            var tokens = Tokenizer.Parse("Blah");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnLineEnding()
        {
            var tokens = Tokenizer.Parse("T \nT \n");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[2].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[3].GetType());
            Assert.AreEqual(typeof(SingleSpaceElement), tokens[4].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[5].GetType());
        }

        [TestMethod]
        public void NoteWithoutPrecedingSpace()
        {
            var tokens = Tokenizer.Parse("Blah.[[Blah\r\nBlah]]");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(NoteTextElement), tokens[1].GetType());
        }

        [TestMethod]
        public void UnfinishedNote()
        {
            var tokens = Tokenizer.Parse("Blah[[Blah\r\n  \r\nBlah]");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[1].GetType());
        }

        [TestMethod]
        public void ReturnDoubleSpace()
        {
            var tokens = Tokenizer.Parse("Blah.\n  \n");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[1].GetType());
            Assert.AreEqual(typeof(DoubleSpaceElement), tokens[2].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[3].GetType());
        }

        [TestMethod]
        public void ReturnSingleSpace()
        {
            var tokens = Tokenizer.Parse("Blah. Blah.");

            Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[2].GetType());
        }

        [TestMethod]
        public void ConsolidateTabs()
        {
            var tokens = Tokenizer.Parse("\t\t\tBlah\r\n\t\tBlah.");

            Assert.AreEqual(typeof(TabElement), tokens[0].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[1].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[2].GetType());
            Assert.AreEqual(typeof(TabElement), tokens[3].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[4].GetType());
        }

        [TestMethod]
        public void ReturnTitlePageKey()
        {
            var tokens = Tokenizer.Parse("Title: Blah\r\nauthor:Blah");

            Assert.AreEqual(typeof(TitlePageKey), tokens[0].GetType());
            Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[2].GetType());
            Assert.AreEqual(typeof(LineEnding), tokens[3].GetType());
            Assert.AreEqual(typeof(TitlePageKey), tokens[4].GetType());
            Assert.AreEqual(typeof(NullTextElement), tokens[5].GetType());
        }
    }
}
