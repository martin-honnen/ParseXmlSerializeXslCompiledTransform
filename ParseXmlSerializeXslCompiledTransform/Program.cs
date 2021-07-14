using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace ParseXmlSerializeXslCompiledTransform
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml = @"<re>
  <bl><![CDATA[&#60;ul&#62;&#60;li&#62;Linienfl&#252;ge&#60;/li&#62;&#60;/ul&#62;]]></bl>
</re>";

            XslCompiledTransform processor = new XslCompiledTransform();

            processor.Load("XSLTFile1.xslt");

            XsltArgumentList xsltArgumentList = new XsltArgumentList();
            xsltArgumentList.AddExtensionObject("http://example.com/mf", new MyExtensions());

            using (StringReader sr = new StringReader(xml))
            {
                using (XmlReader xr = XmlReader.Create(sr))
                {
                    processor.Transform(xr, xsltArgumentList, Console.Out);
                }
            }
        }
    }

    public class MyExtensions
    {
        public static XPathNavigator ParseXmlFragment(string xml)
        {
            using (StringReader sr = new StringReader(xml))
            {
                using (XmlReader xr = XmlReader.Create(sr, new XmlReaderSettings() { ConformanceLevel = ConformanceLevel.Fragment }))
                return new XPathDocument(xr).CreateNavigator();
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
