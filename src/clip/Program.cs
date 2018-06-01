using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clip
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new StringBuilder();

            string line = null;
            while ((line = Console.ReadLine()) != null)
                builder.AppendLine(line);

            // remove extra lines

            while (builder[builder.Length - 1] == '\r' || builder[builder.Length - 1] == '\n')
                builder.Remove(builder.Length - 1, 1);

            // copy to the clipboard

            var text = builder.ToString();

            Clipboard.SetText(text, TextDataFormat.UnicodeText);
        }
    }
}
