using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace clip
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var builder = new StringBuilder();

            var cmdLine = CommandLine.Parse(args);

            if (cmdLine.PeekFrom == PeekFrom.StdIn)
            {
                if (!Console.IsInputRedirected || Console.In.Peek() == -1)
                {
                    cmdLine.ShowHelp(summary: true);
                    Environment.Exit(0);
                }

                builder = CaptureStdIn();
            }

            else
            {
                System.Diagnostics.Debug.Assert(!String.IsNullOrEmpty(cmdLine.InputFile));
                builder.Append(File.ReadAllText(cmdLine.InputFile));
            }

            // copy to the clipboard

            var text = builder.RemoveTrailingLines().ToString();

            Clipboard.SetText(text, TextDataFormat.UnicodeText);
        }

        private static StringBuilder CaptureStdIn()
        {
            var builder = new StringBuilder();

            string line = null;
            while ((line = Console.ReadLine()) != null)
                builder.AppendLine(line);

            return builder;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Help");
        }
    }

    internal static class StringBuilderExtensions
    {
        public static StringBuilder RemoveTrailingLines(this StringBuilder builder)
        {
            // remove extra lines

            while (builder[builder.Length - 1] == '\r' || builder[builder.Length - 1] == '\n')
                builder.Remove(builder.Length - 1, 1);

            return builder;
        }
    }
}