using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace CreateKnightSquireXml
{
    public class MergeJob
    {
        private readonly XmlDocument   _ksRelationshipsXml;
        private readonly StringBuilder _fileStringBuilder;
        private readonly string        _outPathAndFilename;
        private readonly string        _relPathAndFilename;
        private readonly string        _wbPathAndFilename;
        private          XmlWriter     _writer;
        private          XmlDocument   outDoc;

        private string pathFilenameOutputXml;
        private string pathFilenameOutputXmlNode;

        public MergeJob(string wbPathAndFilename, string relPathAndFilename, string outPathAndFilename)
        {
            _wbPathAndFilename  = wbPathAndFilename;
            _relPathAndFilename = relPathAndFilename;
            _outPathAndFilename = outPathAndFilename;

            _fileStringBuilder = new StringBuilder();

            _ksRelationshipsXml = new XmlDocument();
            _ksRelationshipsXml.Load(_relPathAndFilename);
        }


        public int DoWork()
        {
            Debug.WriteLine("MergeJob.DoWork() ...");

            const string xmlheader1 = @"<?xml version=""1.0"" encoding=""utf-8""?>";
            //var xmlheader2 = @"<?xml-stylesheet type=""text/xsl"" href=""./styles/ks.xsl""?>";

            _fileStringBuilder.Append(xmlheader1);
            //_fileStringBuilder.Append(xmlheader2);

            _fileStringBuilder.Append("<knights>");

            var root = _ksRelationshipsXml.DocumentElement;

            SingletonKnightParser.LoadWbXml(_wbPathAndFilename);

            foreach (XmlNode rootChild in root.ChildNodes)
            {
                if (rootChild.LocalName != "knight") continue;
                var xD = new XmlDocument();
                xD.LoadXml(rootChild.OuterXml);
                var rKnightNode = xD.FirstChild;

                //Dictionary<string, XmlNode> rKnightChildren = rKnightNode.ChildNodes.Cast<XmlNode>().ToDictionary(child => child.Name);

                var newKnightStringBuilder = SingletonKnightParser.Parse(rKnightNode);

                _fileStringBuilder.Append(newKnightStringBuilder);
            }

            _fileStringBuilder.Append("</knights>");

            using (var swriter = new StreamWriter(_outPathAndFilename))
            {
                swriter.Write(_fileStringBuilder.ToString());
            }

            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");

            return 0;
        }
    }
}