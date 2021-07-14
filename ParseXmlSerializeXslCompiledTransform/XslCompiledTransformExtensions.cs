using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ParseXmlSerializeXslCompiledTransform
{

    public class MyExtensions
    {
        public static XPathNavigator ParseXmlFragment(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                using (XmlReader xr = XmlReader.Create(sr, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment }))
                {
                    try
                    {
                        return new XPathDocument(xr).CreateNavigator();
                    }
                    catch (XmlException e)
                    {
                        throw new XPathException("String passed to ParseXmlFragment is not a well-formed external entity: " + e.Message);
                    }
                }
            }
        }

        public static string Serialize(XPathNavigator node)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(sw, new XmlWriterSettings() { OmitXmlDeclaration = true, Indent = false }))
                {
                    node.WriteSubtree(xw);
               
                }
                return sw.ToString();
            }
        }
    }

}
