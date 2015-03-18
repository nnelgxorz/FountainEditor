using FountainEditor.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor.Language
{
    public interface IMarkdownService
    {
        Element[] Parse(string text);
    }
}
