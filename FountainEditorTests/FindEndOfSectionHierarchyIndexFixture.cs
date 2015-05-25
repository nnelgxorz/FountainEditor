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
    public class FindEndOfSectionHierarchyIndexFixture
    {
        private FindEndOfSectionHierarchyIndex service;

        [TestInitialize]
        public void Initialize()
        {
            service = new FindEndOfSectionHierarchyIndex();
        }

        [TestMethod]
        public void Find_WhenFirstSectionAndLastIndexHashCountsAreEqual_ReturnLastIndex()
        {
            // Arrange
            List<SectionIndexClass> list = new List<SectionIndexClass>();
            int start = 0;

            list.Add(new SectionIndexClass(1, "", 1, 1));
            list.Add(new SectionIndexClass(1, "", 1, 2));
            list.Add(new SectionIndexClass(1, "", 1, 3));
            list.Add(new SectionIndexClass(1, "", 1, 4));
            list.Add(new SectionIndexClass(1, "", 1, 1));

            // Act
            var result = service.Find(list, start);

            // Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void Find_WhenStartIs4AndListCountIsFive_ReturnStartIndex()
        {
            // Arrange
            List<SectionIndexClass> list = new List<SectionIndexClass>();
            int start = 4;

            list.Add(new SectionIndexClass(1, "", 1, 1));
            list.Add(new SectionIndexClass(1, "", 1, 2));
            list.Add(new SectionIndexClass(1, "", 1, 3));
            list.Add(new SectionIndexClass(1, "", 1, 1));
            list.Add(new SectionIndexClass(1, "", 1, 2));

            // Act
            var result = service.Find(list, start);

            // Assert
            Assert.AreEqual(4, result);
        }
    }
}
