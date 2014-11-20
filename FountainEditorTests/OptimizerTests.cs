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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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
                new NullTextElement("Test"),
                new SingleSpaceElement(" "),
                new NullTextElement("test."),
                new LineEnding(""),
                new NullTextElement(".Close"),
                new SingleSpaceElement(" "),
                new NullTextElement("on"),
                new SingleSpaceElement(" "),
                new NullTextElement("test"),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(SceneHeadingTextElement), "int. test - Day");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test test.");
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
                new NullTextElement("SUPER"),
                new SingleSpaceElement(" "),
                new NullTextElement("SMASH"),
                new SingleSpaceElement(" "),
                new NullTextElement("CUT"),
                new SingleSpaceElement(" "),
                new TransitionTextElement("TO:"),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
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

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Action. ");
            TestElementTypeAndValue(elements[1], typeof(NoteTextElement), "[[Note]]");

        }

        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
