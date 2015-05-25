using FountainEditorGUI;
using FountainEditorGUI.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorTests
{
    [TestClass]
    public class CollectionItemsNestingServiceFixture
    {
        private CollectionItemsNestingService service;

        [TestInitialize]
        public void Initialize()
        {
            var add = new StubIAddHashtags();
            add.AddStringInt32 = (text, amount) => "##Hello";

            var remove = new StubIRemoveHashtags();
            remove.RemoveStringInt32 = (text, amount) => "Hello";

            service = new CollectionItemsNestingService(add, remove);
        }

        [TestMethod]
        public void DoNesting_WhenAmountIs0AndDropActionIsNotNest_ReturnCollectionUnaltered()
        {
            // Arrange
            ObservableCollection<string> collection = new ObservableCollection<string>();
            ObservableCollection<string> expected = new ObservableCollection<string>();
            int amount = 0;
            string dropAction = "Before";

            for (int i = 0; i < 5; i++)
            {
                collection.Add("");
                expected.Add("");
            }

            // Act
            var result = service.DoNesting(collection, amount, dropAction);

            // Assert
            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void MyTestMethod()
        {
            // Arrange
            ObservableCollection<string> collection = new ObservableCollection<string>();
            ObservableCollection<string> expected = new ObservableCollection<string>();
            int amount = 1;
            string dropAction = "Nest";

            // Act

            // Assert
        }
    }
}
