using Microsoft.Web.XmlTransform;
using System;
using NLog;
using System.Text;
using System.Xml;

namespace XdtTransform.Impl
{
	public class XdtTransformer : IXdtTransformer
	{
		private static IXdtTransformer current;
		private static Logger logger = LogManager.GetCurrentClassLogger();

		public static IXdtTransformer Current
		{
			get { return current ?? (current = new XdtTransformer()); }
		}

		private XdtTransformer()
		{
			this.PreserveWhitespace = true;
		}

		#region IXdtTransformer

		public Boolean PreserveWhitespace { get; set; }

		public String Transform(String sourceXml, String transformXml)
		{
			if (String.IsNullOrEmpty(sourceXml))
			{
				throw new ArgumentNullException("sourceXml");
			}
			if (String.IsNullOrEmpty(transformXml))
			{
				throw new ArgumentNullException("transformXml");
			}

			try
			{
				using (var doc = new XmlTransformableDocument())
				{
					logger.Debug($"Beginning transformation");

					doc.PreserveWhitespace = this.PreserveWhitespace;
					doc.LoadXml(sourceXml);

					using (var xDoc = new XmlTransformation(transformXml, false, null))
					{
						if (xDoc.Apply(doc))
						{
							var builder = new StringBuilder();
							var xmlWriterSettings = new XmlWriterSettings
							{
								Indent = true,
								IndentChars = "  "
							};
							using (var xmlWriter = XmlTextWriter.Create(builder, xmlWriterSettings))
							{
								doc.WriteTo(xmlWriter);
							}

							logger.Debug("Successfully transformed document");

							return builder.ToString();
						}
						else
						{
							logger.Warn("Unable to transform - xDoc.Apply failed");
							throw new XdtTransformException("Unable to transform");
						}
					}
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex, $"Unable to transform, exception: {ex.Message}");
				throw new XdtTransformException($"Unable to transform, exception: {ex.Message}", ex);
			}
		}

		#endregion IXdtTransformer
	}
}