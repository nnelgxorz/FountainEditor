using FountainEditorGUI;
using FountainEditorGUI.Messages;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditorTests
{
    [TestClass]
    public class AddRemoveItemsToListFixture
    {
      private AddRemoveItemsToList service;

      [TestInitialize]
      public void Initialize()
      {
          service = new AddRemoveItemsToList();
      }

      [TestMethod]
      public void Remove_WhenCalledWith2StartAnd4End_ReturnsListCount6()
      {
          // Arrange
          List<SectionIndexClass> list = new List<SectionIndexClass>();
          int start = 2;
          int end = 4;

          for (int i = 0; i < 8; i++)
          {
              list.Add(null);
          }

          // Act
          var result = service.Remove(list, start, end);
          
          // Assert
          Assert.AreEqual(6, result.Count);
      }

      [TestMethod]
      public void Add_WhenCalledWithListLength4AndSubListLength4_ReturnsListCount8()
      {
          // Arrange
          List<SectionIndexClass> list = new List<SectionIndexClass>();
          List<SectionIndexClass> subList = new List<SectionIndexClass>();
          int start = 2;
          DragDropMessage message = new DragDropMessage(1, 2, "OutlineElement1", "OutlineElement2", 1, 2, "Before");

          for (int i = 0; i < 4; i++)
          {
              list.Add(null);
              subList.Add(null);
          }
          // Act
          var result = service.Add(list, subList, start, message);

          // Assert
          Assert.AreEqual(8, result.Count());
      }

      [TestMethod]
      public void Remove_WhenStartIsOneAndEndIsThreeAndCountIsFive_ReturnsListWithOnlyItems0And4()
      {
          // Arrange
          List<SectionIndexClass> list = new List<SectionIndexClass>();
          List<SectionIndexClass> expected = new List<SectionIndexClass>();
          int start = 1;
          int end = 4;

          expected.Add(new SectionIndexClass(1, "0", 1, 1));
          expected.Add(new SectionIndexClass(1, "1", 1, 1));

          list.Add(expected[0]);
          for (int i = 1; i <= 3; i++)
          {
              list.Add(new SectionIndexClass(1, i.ToString(), 1, 1));
          }
          list.Add(expected[1]);

          // Act
          var result = service.Remove(list, start, end);
          
          // Assert
          Assert.IsTrue(result.SequenceEqual(expected));
      }
      [TestMethod]
      public void Add_WhenListHas0And1SublistHas2And3_ReturnListWith0231()
      {
          // Arrange
          List<SectionIndexClass> list = new List<SectionIndexClass>();
          List<SectionIndexClass> subList = new List<SectionIndexClass>();
          List<SectionIndexClass> expected = new List<SectionIndexClass>();
          DragDropMessage message = new DragDropMessage(1, 1, "2", "0", 1, 1, "After");
          int start = 0;

          for (int i = 0; i < 2; i++)
          {
              list.Add(new SectionIndexClass(1, i.ToString(), 1, 1));
              subList.Add(new SectionIndexClass(1, (i+2).ToString(), 1, 1));
          }
          expected.Add(new SectionIndexClass(1, "0", 1, 1));
          expected.Add(new SectionIndexClass(1, "2", 1, 1));
          expected.Add(new SectionIndexClass(1, "3", 1, 1));
          expected.Add(new SectionIndexClass(1, "1", 1, 1));
          // Act
          var result = service.Add(list, subList, start, message);

          // Assert
          Assert.IsTrue(result.SequenceEqual(expected));
      }
    }
}
