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
        int fontSize = 16;

        private static double pageWidth = 8.5;
        private static string textBoxHeight = ActualHeightProperty.ToString();
        private static string textBoxWidth = ActualWidthProperty.ToString();
        private static double inch = Convert.ToDouble(textBoxWidth) / pageWidth;

        double margin = 1 * inch;

        double[] marginValues;
    }
}
