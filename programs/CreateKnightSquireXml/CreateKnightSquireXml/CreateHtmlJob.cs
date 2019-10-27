using System;
using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using System.IO;
using System.Xml.Xsl;


namespace CreateKnightSquireXml
{
    public class CreateHtmlJob
    {
        private readonly string _pathFilenameXsl;
        private readonly string _pathFilenameXml;
        private readonly string _pathFilenameOutputHtml;

        public CreateHtmlJob(string xslPathAndFilename, string xmlPathAndFilename, string htmlPathAndFilename)
        {
            _pathFilenameXsl        = xslPathAndFilename;
            _pathFilenameXml        = xmlPathAndFilename;
            _pathFilenameOutputHtml = htmlPathAndFilename;
        }

        public int DoWork()
        {
            XElement  xmlDoc  = XElement.Load(_pathFilenameXml);
            XDocument xmlTree = new XDocument(xmlDoc);

            string xslText = File.ReadAllText(_pathFilenameXsl);

            XDocument newTree = new XDocument();
            using (XmlWriter writer = newTree.CreateWriter())
            {
                // Load the style sheet.  
                XslCompiledTransform xslt = new XslCompiledTransform();

                xslt.Load(XmlReader.Create(new StringReader(xslText)));

                // Execute the transform and output the results to a writer.  
                xslt.Transform(xmlTree.CreateReader(), writer);
            }

            XmlWriterSettings settings = new XmlWriterSettings {Indent = true, IndentChars = "    "};
            using (XmlTextWriter xtw = new XmlTextWriter(_pathFilenameOutputHtml, Encoding.UTF8))
            {
                newTree.Save(xtw);
            }

            Debug.WriteLine(newTree);


            return 0;
        }
    }
}