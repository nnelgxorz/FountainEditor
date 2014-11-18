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
            TestElementTypeAndValue(elements[0], typeof(CharacterTextElement), "TESTY TEST (O.S.)");
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
            TestElementTypeAndValue(elements[2], typeof(ActionTextElement), "Test Test Test.");
            TestElementTypeAndValue(elements[3], typeof(LineEnding), "");
        }

        [TestMethod]
        public void ConvertLooseParentheticals()
        {
            var elements = new List<Element>
            {
                new NullTextElement("Test"),
                new NullTextElement("Test"),
                new ParentheticalTextElement("(Test)."),
            };

            new Optimizer().Optimize(elements);
            TestElementTypeAndValue(elements[0], typeof(ActionTextElement), "Test Test (Test).");
        }

        private static void TestElementTypeAndValue(Element element, Type type, string value)
        {
            Assert.AreEqual(type, element.GetType());
            Assert.AreEqual(value, element.Text);
        }
    }
}
