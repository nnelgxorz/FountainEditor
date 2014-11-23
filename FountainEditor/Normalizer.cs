using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FountainEditor {
    public static class Normalizer {
        public static string Normalize(string text) {
            // TODO: store this data for later.

            bool crlf;
            bool cr;
            bool lf;

            if (text.Contains("\r\n")) {
                crlf = true;
            }

            if (text.Contains('\r')) {
                cr = true;
            }

            if (text.Contains('\n')) {
                lf = true;
            }

            // Convert line endings to Unix, since it's the easiest to detect,
            // and most standard.

            text = text.Replace("\r\n", "\n");
            text = text.Replace("\r", "\n");

            return text;
        }
    }
}
