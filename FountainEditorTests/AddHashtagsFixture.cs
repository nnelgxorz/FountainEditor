using FountainEditorGUI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorTests
{
    [TestClass]
    public class AddHashtagsFixture
    {
        private AddHashtags service;

        [TestInitialize]
        public void Initialize()
        {
            service = new AddHashtags();
        }

        [TestMethod]
        public void Add_WhenCalledWith2ForTheAmount_ReturnsDoubleHashPrependedString()
        {
            // Arrange
            int amount = 2;
            string text = "Outline Element";
            
            // Act
            var result = service.Add(text, amount);

            // Assert
            Assert.AreEqual("##Outline Element", result);
        }
    }
}
