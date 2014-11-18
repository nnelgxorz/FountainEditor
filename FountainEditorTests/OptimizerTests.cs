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
                new NullTextElement("TEST"),
                new ParentheticalTextElement("(O.S.)"),
                new LineEnding(""),
                new ParentheticalTextElement("Hmm"),
                new LineEnding(""),
                new NullTextElement("I"),
                new NullTextElement("love"),
                new NullTextElement("tests"),
                new LineEnding(""),
                new LineEnding(""),
                new NullTextElement("^OTHER"),
                new NullTextElement("TEST"),
                new LineEnding(""),
                new NullTextElement("I"),
                new NullTextElement("love"),
                new NullTextElement("tests"),
                new NullTextElement("too."),
                new LineEnding(""),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[1], typeof(CharacterTextElement), "TESTY TEST (O.S.)");
            TestElementTypeAndValue(elements[2], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[3], typeof(ParentheticalTextElement), "Hmm");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[5], typeof(DialogueTextElement), "I love tests");
            TestElementTypeAndValue(elements[6], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[7], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[8], typeof(DualDialogueTextElement), "^OTHER TEST");
            TestElementTypeAndValue(elements[9], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[10], typeof(DialogueTextElement), "I love tests too.");
            TestElementTypeAndValue(elements[11], typeof(LineEnding), "");
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
                new NullTextElement("SUPER"),
                new NullTextElement("SMASH"),
                new NullTextElement("CUT"),
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
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test Test Test.");
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
                new NullTextElement("Test"),
                new NullTextElement("Test"),
                new ParentheticalTextElement("(Test)."),
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "Mr. Test");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[2], typeof(ParentheticalTextElement), "Wryly");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
            TestElementTypeAndValue(elements[4], typeof(ActionTextElement), "Test Test (Test).");
        }

        [TestMethod]
        public void CharacterErrorHandling()
        {
            var elements = new List<Element>
            {
                new NullTextElement("What"),
                new NullTextElement("a"),
                new NullTextElement("CRAZY"),
                new NullTextElement("TEST"),
                new NullTextElement("man!"),
                new LineEnding("")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "What a CRAZY TEST man!");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "");
            
        }

        [TestMethod]
        public void DialogueErrorHandling()
        {
            var elements = new List<Element>
            {
                new NullTextElement("TESTO"),
                new LineEnding("\r\n"),
                new NullTextElement("This"),
                new NullTextElement("is"),
                new LineEnding("\r\n"),
                new NullTextElement("  "),
                new NullTextElement("Dialogue."),
                new LineEnding("\r\n"),
                new LineEnding("\r\n"),
                new NullTextElement("This"),
                new NullTextElement("is"),
                new NullTextElement("not.")
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTO");
            TestElementTypeAndValue(elements[1], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[2], typeof(DialogueTextElement), "This is \r\nDialogue.");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[4], typeof(LineEnding), "\r\n");
            TestElementTypeAndValue(elements[5], typeof(ActionTextElement), "This is not.");
        }

        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
