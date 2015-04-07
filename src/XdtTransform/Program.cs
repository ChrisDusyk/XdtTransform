using CommandLine;
using CommandLine.Text;
using System;

namespace XdtTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {

            }
            Environment.Exit(1);
        }

        private void Process(Options options)
        {

        }
    }

    internal class Options
    {
        [Option('s', "source", Required = true, HelpText = "Source XML file (e.g. web.config)")]
        public String Source { get; set; }

        [Option('t', "transform", Required = true, HelpText = "Transform XML file (e.g. web.DEBUG.config)")]
        public String Transform { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Destination XML file (e.g. output\\web.config)")]
        public String Destination { get; set; }

        [HelpOption]
        public String GetUsage()
        {
            return HelpText.AutoBuild(this, ctx => HelpText.DefaultParsingErrorsHandler(this, ctx));
        }
    }
}
