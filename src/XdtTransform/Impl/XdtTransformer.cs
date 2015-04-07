using Microsoft.Web.XmlTransform;
using System;
using System.Text;
using System.Xml;

namespace XdtTransform.Impl
{
    public class XdtTransformer : IXdtTransformer
    {
        private static IXdtTransformer current;
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
                            return builder.ToString();
                        }
                        else
                        {
                            throw new XdtTransformException("Unable to transform");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new XdtTransformException("Unable to transform", ex);
            }
        }

        #endregion
    }
}
