﻿using System.Diagnostics;
using System.Text;
using System.Xml;
using System.IO;

namespace CreateKnightSquireXml
{
    public class MergeJob
    {
        private string _wbPathAndFilename;
        private string _relPathAndFilename;
        private string _outPathAndFilename;

        private          string        pathFilenameOutputXml;
        private          string        pathFilenameOutputXmlNode;
        private          XmlDocument   outDoc;
        private readonly XmlDocument   _ksRelationshipsXml;
        private          XmlWriter     _writer;
        private          StringBuilder _fileStringBuilder;

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

            XmlElement root = _ksRelationshipsXml.DocumentElement;

            SingletonKnightParser.LoadWbXml(_wbPathAndFilename);

            foreach (XmlNode rootChild in root.ChildNodes)
            {
                if (rootChild.LocalName != "knight") continue;
                XmlDocument xD = new XmlDocument();
                xD.LoadXml(rootChild.OuterXml);
                XmlNode rKnightNode = xD.FirstChild;

                //Dictionary<string, XmlNode> rKnightChildren = rKnightNode.ChildNodes.Cast<XmlNode>().ToDictionary(child => child.Name);

                StringBuilder newKnightStringBuilder = SingletonKnightParser.Parse(rKnightNode);

                _fileStringBuilder.Append(newKnightStringBuilder);
            }

            _fileStringBuilder.Append("</knights>");

            using (StreamWriter swriter = new StreamWriter(_outPathAndFilename))
            {
                swriter.Write(_fileStringBuilder.ToString());
            }

            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");

            return 0;
        }
    }
}