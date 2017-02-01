using CommandLine;
using CommandLine.Text;
using NLog;
using System;
using System.IO;
using XdtTransform.Impl;

namespace XdtTransform
{
	internal class Program
	{
		private static Logger logger = LogManager.GetLogger("Main");

		private static void Main(string[] args)
		{
			var options = new Options();
			if (CommandLine.Parser.Default.ParseArguments(args, options))
			{
				try
				{
					var program = new Program(options);
					program.Process();
				}
				catch (XdtTransformException tex)
				{
					Console.WriteLine("Error transforming file: {0}", tex.Message);
					logger.Error(tex, $"Error transforming file: {tex.Message}");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Unexpected exception: {0}", ex.Message);
					logger.Error(ex, $"Unexpected exception: {ex.Message}");
				}
			}
			Environment.Exit(0);
		}

		private readonly Options options;

		public Program(Options options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (!File.Exists(options.Source))
			{
				throw new ApplicationException("Source file does not exist.");
			}
			if (!File.Exists(options.Transform))
			{
				throw new ApplicationException("Transform file does not exist.");
			}

			this.options = options;
		}

		private void Process()
		{
			var sourceXml = File.ReadAllText(this.options.Source);
			var transformXml = File.ReadAllText(this.options.Transform);

			using (var destination = File.Open(this.options.Destination, FileMode.Create, FileAccess.Write))
			{
				using (var writer = new StreamWriter(destination))
				{
					var resultXml = XdtTransformer.Current.Transform(sourceXml, transformXml);
					writer.Write(resultXml);
					writer.Flush();
				}
			}
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