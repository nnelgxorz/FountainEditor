using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FountainEditor.Elements;
using FountainEditor;
using System.Collections.Generic;

namespace FountainEditorTests
{
    [TestClass]
    public class OptimizerTests
    {
        [TestMethod]
        public void OptimizeCharacterAndDialogue()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("TESTY"),
                new SingleSpaceElement(" "),
                new NullTextElement("TEST"),
                new SingleSpaceElement(" "),
                new ParentheticalTextElement("(O.S.)"),
                new LineEnding(""),
                new ParentheticalTextElement("(Hmm)"),
                new LineEnding(""),
                new NullTextElement("I"),
                new SingleSpaceElement(" "),
                new NullTextElement("love"),
                new SingleSpaceElement(" "),
                new NullTextElement("tests"),
                new LineEnding(""),
                new LineEnding(""),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "TESTY TEST (O.S.)");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(ParentheticalTextElement), "(Hmm)");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[5], typeof(DialogueTextElement), "I love tests");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[7], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeDualDialogueCharacterAndDialogue()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("^OTHER"),
                new SingleSpaceElement(" "),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("I"),
                new SingleSpaceElement(" "),
                new NullTextElement("love"),
                new SingleSpaceElement(" "),
                new NullTextElement("tests"),
                new SingleSpaceElement(" "),
                new NullTextElement("too."),
                new LineEnding(""),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(DualDialogueTextElement), "^OTHER TEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(DialogueTextElement), "I love tests too.");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeSceneHeadings()
        {
            var elements = new List<Element>
            {
                new SceneHeadingTextElement("int."),
                new SingleSpaceElement(" "),
                new NullTextElement("test"),
                new SingleSpaceElement(" "),
                new NullTextElement("- Day"),
                new LineEnding(""),
                new NullTextElement("Test."),

            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(SceneHeadingTextElement), "int. test - Day");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test.");
        }

        [TestMethod]
        public void OptimizeForcedSceneHeadings()
        {
            var elements = new List<Element>
            {
                new NullTextElement(".Close"),
                new SingleSpaceElement(" "),
                new NullTextElement("on"),
                new SingleSpaceElement(" "),
                new NullTextElement("test"),
                new LineEnding(""),
                new NullTextElement("Test."),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(SceneHeadingTextElement), ".Close on test");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test.");
        }

        [TestMethod]
        public void OptimizeTransitions()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("SUPER"),
                new SingleSpaceElement(" "),
                new NullTextElement("SMASH"),
                new SingleSpaceElement(" "),
                new NullTextElement("CUT"),
                new SingleSpaceElement(" "),
                new TransitionTextElement("TO:"),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(TransitionTextElement), "SUPER SMASH CUT TO:");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
        }

        [TestMethod]
        public void OptimizeAction()
        {
            var elements = new List<Element>
            {
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
                new LineEnding(""),

            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Test Test.");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test Test.");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
        }

        [TestMethod]
        public void ConvertLooseParentheticals()
        {
            var elements = new List<Element>
            {
                new CharacterTextElement("Mr. Test"),
                new LineEnding(""),
                new ParentheticalTextElement("Wryly"),
                new LineEnding(""),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new ParentheticalTextElement("(Test)."),
                new LineEnding(""),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "Mr. Test");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ParentheticalTextElement), "Wryly");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[5], typeof(ActionTextElement), "Test Test (Test).");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "");
        }

        [TestMethod]
        public void CharacterErrorHandling()
        {
            var elements = new List<Element>
            {
                new NullTextElement("What"),
                new SingleSpaceElement(" "),
                new NullTextElement("a"),
                new SingleSpaceElement(" "),
                new NullTextElement("CRAZY"),
                new SingleSpaceElement(" "),
                new NullTextElement("TEST,"),
                new SingleSpaceElement(" "),
                new NullTextElement("man!"),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "What a CRAZY TEST, man!");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
        }

        [TestMethod]
        public void DialogueWithLineBreak()
        {
            var elements = new List<Element>
            {
                new CharacterTextElement("TESTO"),
                new LineEnding("\r\n"),
                new NullTextElement("This"),
                new SingleSpaceElement(" "),
                new NullTextElement("is"),
                new LineEnding("\r\n"),
                new NullTextElement("Dialogue."),
                new LineEnding("\r\n"),
                new LineEnding("\r\n"),
                new NullTextElement("This"),
                new SingleSpaceElement(" "),
                new NullTextElement("is"),
                new SingleSpaceElement(" "),
                new NullTextElement("not.")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTO");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[2], typeof(DialogueTextElement), "This is");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), ("Dialogue."));
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[7], typeof(ActionTextElement), "This is not.");
        }

        [TestMethod]
        public void DialogueWithEmptyLine()
        {
            var elements = new List<Element>
            {
                new CharacterTextElement("TESTO"),
                new LineEnding("\r\n"),
                new NullTextElement("This"),
                new SingleSpaceElement(" "),
                new NullTextElement("is"),
                new LineEnding("\r\n"),
                new DoubleSpaceElement("  "),
                new LineEnding("\r\n"),
                new NullTextElement("Dialogue."),
                new LineEnding("\r\n"),
                new LineEnding("\r\n"),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTO");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[2], typeof(DialogueTextElement), "This is");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), "  ");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[6], typeof(DialogueTextElement), "Dialogue.");
            TestElementTypeAndValue(elements[7], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[8], typeof(LineEnding), "\r\n");
        }

        [TestMethod]
        public void DialogueAndParentheticals()
        {
            var elements = new List<Element>
            {
                new CharacterTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
                new LineEnding(""),
                new ParentheticalTextElement("(Test)"),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
                new LineEnding(""),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TEST");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(DialogueTextElement), "Test Test.");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[4], typeof(ParentheticalTextElement), "(Test)");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[6], typeof(DialogueTextElement), "Test Test.");
        }

        [TestMethod]
        public void ForcedCharacterName()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("@McTEST"),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "@McTEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(DialogueTextElement), "Test Test.");
        }

        [TestMethod]
        public void ForcedActionElement()
        {
            var elements = new List<Element>
            {
                new NullTextElement("!TEST"),
                new SingleSpaceElement(" "),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Test."),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "!TEST TEST");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test Test.");
        }

        [TestMethod]
        public void PreserveCenteredText()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new CenteredTextElement(">The End<"),
                new LineEnding(""),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(CenteredTextElement), ">The End<");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
        }

        [TestMethod]
        public void RetainSpacesBetweenElements()
        {
            var elements = new List<Element>
            {
                new NullTextElement("Action."),
                new SingleSpaceElement(" "),
                new NoteTextElement("[[Note]]"),
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Action. ");
            TestElementTypeAndValue(elements[1], typeof(NoteTextElement), "[[Note]]");
        }

        [TestMethod]
        public void TabbedCharacterAndDialogue()
        {
            var elements = new List<Element>
            {
                new TabElement(""),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new TabElement(""),
                new NullTextElement("Test."),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TabElement), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "TEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(TabElement), "");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), "Test.");
            //TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TabbedDualDialogueCharacterAndDialogue()
        {
            var elements = new List<Element>
            {
                new TabElement(""),
                new NullTextElement("^TEST"),
                new LineEnding(""),
                new TabElement(""),
                new NullTextElement("Test."),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TabElement), "");
            TestElementTypeAndValue(elements[1], typeof(DualDialogueTextElement), "^TEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(TabElement), "");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), "Test.");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TabbedForcedCharacterAndDialogue()
        {
            var elements = new List<Element>
            {
                new TabElement(""),
                new NullTextElement("@McTEST"),
                new LineEnding(""),
                new TabElement(""),
                new NullTextElement("Test."),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TabElement), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "@McTEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(TabElement), "");
            TestElementTypeAndValue(elements[4], typeof(DialogueTextElement), "Test.");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TabbedParentheticals()
        {
            var elements = new List<Element>
            {
                new TabElement(""),
                new CharacterTextElement("TEST"),
                new LineEnding(""),
                new TabElement(""),
                new ParentheticalTextElement("(Test)"),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TabElement), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "TEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(TabElement), "");
            TestElementTypeAndValue(elements[4], typeof(ParentheticalTextElement), "(Test)");
            TestElementTypeAndValue(elements[5], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TabbedTransitions()
        {
            var elements = new List<Element>
            {
                new TabElement(""),
                new NullTextElement("Cut"),
                new SingleSpaceElement(" "),
                new TransitionTextElement("to:"),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TabElement), "");
            TestElementTypeAndValue(elements[1], typeof(TransitionTextElement), "Cut to:");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TitlePageKeysAndValues()
        {
            var elements = new List<Element>
            {
                new TitlePageKey("Title:"),
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("Script"),
                new LineEnding(""),
                new TitlePageKey("Author:"),
                new SingleSpaceElement(" "),
                new NullTextElement("Glenn"),
                new SingleSpaceElement(" "),
                new NullTextElement("Becker"),
                new LineEnding(""),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TitlePageKey), "Title:");
            TestElementTypeAndValue(elements[1], typeof(TitlePageValue), "Test Script");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(TitlePageKey), "Author:");
            TestElementTypeAndValue(elements[4], typeof(SingleSpaceElement), " ");
            TestElementTypeAndValue(elements[5], typeof(TitlePageValue), "Glenn Becker");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[7], typeof(LineEnding), "");
        }

        [TestMethod]
        public void ElipsisDialogueAfterParenthetical()
        {
            var elements = new List<Element>
            {
                new LineEnding(""),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("Test."),
                new LineEnding(""),
                new ParentheticalTextElement("(Test)"),
                new LineEnding(""),
                new NullTextElement("..."),
                new SingleSpaceElement(" "),
                new NullTextElement("Test"),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "TEST");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(DialogueTextElement), "Test.");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[5], typeof(ParentheticalTextElement), "(Test)");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[7], typeof(DialogueTextElement), "... Test");
            TestElementTypeAndValue(elements[8], typeof(LineEnding), "");
        }

        [TestMethod]
        public void TabbedMultiLineTitlePageValues()
        {
            var elements = new List<Element>
            {
                new TitlePageKey("Title:"),
                new LineEnding(""),
                new TabElement(""),
                new NullTextElement("Title"),
                new LineEnding(""),
                new TabElement(""),
                new SingleSpaceElement(" "),
                new NullTextElement("Based"),
                new SingleSpaceElement(" "),
                new NullTextElement("on"),
                new LineEnding(""),
                new TitlePageKey("Author"),
                new LineEnding(""),
                new LineEnding("")
            };

            Optimizer.Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(TitlePageKey), "Title:");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(TabElement), "");
            TestElementTypeAndValue(elements[3], typeof(TitlePageValue), "Title");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[5], typeof(TabElement), "");
            TestElementTypeAndValue(elements[6], typeof(SingleSpaceElement), " ");
            TestElementTypeAndValue(elements[7], typeof(TitlePageValue), "Based on");
            TestElementTypeAndValue(elements[8], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[9], typeof(TitlePageKey), "Author");
            TestElementTypeAndValue(elements[10], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[11], typeof(LineEnding), "");
        }
        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
