using System;
using System.Collections.Generic;
using System.IO;
using NDesk.Options;

namespace clip
{
    public sealed class CommandLine
    {
        private CommandLine()
        {
        }

        public bool ShowUsage { get; private set; }
        public PeekFrom PeekFrom { get; private set; }
        public string InputFile { get; private set; }

        public static CommandLine Parse(string[] args)
        {
            var cmdLine = new CommandLine();

            var options = new OptionSet
            {
                {"h|help", (v) => cmdLine.ShowUsage = v!= null},
                {"?", (v) => cmdLine.ShowUsage = v!= null},
            };

            try
            {
                var remaining = options.Parse(args);
                cmdLine.ValidateArguments(remaining);
                return cmdLine;
            }
            catch (OptionException)
            {
                Environment.Exit(1);
                System.Diagnostics.Debug.Assert(false);
                return null;
            }
        }

        private void ValidateArguments(List<string> remaining)
        {
            if (ShowUsage)
            {
                ShowHelp();
                Environment.Exit(0);
            }

            if (remaining.Count > 0)
            {
                var path = remaining[0];
                if (!File.Exists(path))
                {
                    Console.Error.WriteLine("The specified file does not exist.");
                    Environment.Exit(5);
                }

                PeekFrom = PeekFrom.File;
                InputFile = remaining[0];
            }
        }

        public void ShowHelp(bool summary = false)
        {
            var usage = summary
                    ? "\r\nPlease, type CLIP.EXE --help for usage."
                    : "\r\n" +
                      "CLIP.EXE [options] [<input-file-name>]\r\n" +
                      "\r\n" +
                      "Description:\r\n" +
                      "\tCopies the output of command-line applications to the Windows clipboard.\r\n" +
                      "\tThe copied text can then be pasted into other programs.\r\n" +
                      "\r\n" +
                      "Options:\r\n" +
                      "\t/?|-h|--help           Display the following help notice.\r\n" +
                      "\r\n" +
                      "Examples:\r\n" +
                      "\tDIR | CLIP.EXE         Copies the contents of the current folder\r\n" +
                      "\t                       in the Windows clipboard.\r\n" +
                      "\r\n" +
                      "\tCLIP.EXE < README.TXT  Copies the contents of the README.TXT file\r\n" +
                      "\t                       in the Windows clipboard.\r\n" +
                      "\r\n" +
                      "\tCLIP.EXE README.TXT    Copies the contents of the README.TXT file\r\n" +
                      "\t                       in the Windows clipboard.\r\n"
                ;

            Console.WriteLine(usage);
        }
    }
}
