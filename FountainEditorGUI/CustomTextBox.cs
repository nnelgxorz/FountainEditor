using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FountainEditorGUI
{
    class CustomTextBox : RichTextBox
    {
        int fontSize;
        int pageNumber;
        static double pageWidth = 8.5;
        static string textBoxHeight = ActualHeightProperty.ToString();
        static string textBoxWidth = ActualWidthProperty.ToString();
        double inch = Convert.ToDouble(textBoxWidth) / pageWidth;

        public void GetMargins()
        {
        }
    }
}
