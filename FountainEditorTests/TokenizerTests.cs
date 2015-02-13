using FountainEditor.Language;
using FountainEditor.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FountainEditorTests
{
    [TestClass]
    public class TokenizerTests
    {
        private IFountainService fountainService = new FountainService();

        [TestMethod]
        public void ReturnOutlineCheck()
        {
            var tokens = fountainService.Parse("# Outline Element");

            Assert.AreEqual(typeof(SectionElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSynopsis()
        {
            var tokens = fountainService.Parse("= Synopsis Element");

            Assert.AreEqual(typeof(SynopsisElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingINT()
        {
            var tokens = fountainService.Parse("Int. SceneHeading - Day");

            Assert.AreEqual(typeof(SceneHeadingElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnSceneHeadingEXT()
        {
            var tokens = fountainService.Parse("Ext. Scene Heading - Day");

            Assert.AreEqual(typeof(SceneHeadingElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnTransition()
        {
            var tokens = fountainService.Parse("Cut to:");

            Assert.AreEqual(typeof(TransitionElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnTransitionForced()
        {
            var tokens = fountainService.Parse("> Fade to Black");

            Assert.AreEqual(typeof(TransitionElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnCenteredText()
        {
            var tokens = fountainService.Parse(">The End<");

            Assert.AreEqual(typeof(CenteredElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnNote()
        {
            var tokens = fountainService.Parse("[[This is a note\r\n  \r\n]]");

            Assert.AreEqual(typeof(NoteElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnLyrics()
        {
            var tokens = fountainService.Parse("~ Sing this");

            Assert.AreEqual(typeof(LyricElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnPageBreak()
        {
            var tokens = fountainService.Parse("===\r\n");

            Assert.AreEqual(typeof(PageBreakElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnParenthetical()
        {
            var tokens = fountainService.Parse("(Wryly)");

            Assert.AreEqual(typeof(ParentheticalElement), tokens[0].GetType());
        }

        [TestMethod]
        public void ReturnNullTextElement()
        {
            var tokens = fountainService.Parse("Blah");

            // Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void NoteWithoutPrecedingSpace()
        {
            var tokens = fountainService.Parse("Blah.[[Blah\r\nBlah]]");

            // Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            // Assert.AreEqual(typeof(Note), tokens[1].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void UnfinishedNote()
        {
            var tokens = fountainService.Parse("Blah[[Blah\r\n  \r\nBlah]");

            // Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[1].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void ReturnDoubleSpace()
        {
            var tokens = fountainService.Parse("Blah.\n  \n");

            // Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            // Assert.AreEqual(typeof(LineEnding), tokens[1].GetType());
            // Assert.AreEqual(typeof(DoubleSpaceElement), tokens[2].GetType());
            // Assert.AreEqual(typeof(LineEnding), tokens[3].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void ReturnSingleSpace()
        {
            var tokens = fountainService.Parse("Blah. Blah.");

            // Assert.AreEqual(typeof(NullTextElement), tokens[0].GetType());
            // Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[2].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void ConsolidateTabs()
        {
            var tokens = fountainService.Parse("\t\t\tBlah\r\n\t\tBlah.");

            // Assert.AreEqual(typeof(TabElement), tokens[0].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[1].GetType());
            // Assert.AreEqual(typeof(LineEnding), tokens[2].GetType());
            // Assert.AreEqual(typeof(TabElement), tokens[3].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[4].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }

        [TestMethod]
        public void ReturnTitlePageKey()
        {
            var tokens = fountainService.Parse("Title: Blah\r\nauthor:Blah\r\nDraft:Blah");

            // Assert.AreEqual(typeof(cTitlePageKey), tokens[0].GetType());
            // Assert.AreEqual(typeof(SingleSpaceElement), tokens[1].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[2].GetType());
            // Assert.AreEqual(typeof(LineEnding), tokens[3].GetType());
            // Assert.AreEqual(typeof(cTitlePageKey), tokens[4].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[5].GetType());
            // Assert.AreEqual(typeof(LineEnding), tokens[6].GetType());
            // Assert.AreEqual(typeof(rTitlePageKey), tokens[7].GetType());
            // Assert.AreEqual(typeof(NullTextElement), tokens[8].GetType());

            Assert.Fail("TODO: fix this test or remove it.");
        }
    }
}
