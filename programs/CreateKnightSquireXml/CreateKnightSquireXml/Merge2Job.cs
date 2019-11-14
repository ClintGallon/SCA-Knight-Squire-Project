using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class Merge2Job
    {
        private readonly XElement      _ksRelationshipsXml;
        private readonly string        _outPathAndFilename;
        private readonly string        _wbPathAndFilename;
        private          Stream     _stream;
        private readonly XElement _out;

        private string pathFilenameOutputXml;


        public Merge2Job(string wbPathAndFilename, string relPathAndFilename, string outPathAndFilename)
        {
            _wbPathAndFilename  = wbPathAndFilename;
            _outPathAndFilename = outPathAndFilename;
            _ksRelationshipsXml = XElement.Load(relPathAndFilename);

            const string doubleQuote = "\"";
            const string ss = @"<?xml version=" + doubleQuote + "1.0" + doubleQuote + " encoding=" + doubleQuote + "utf-8" + doubleQuote + "?><knights></knights>";
            _out = XElement.Parse(ss);
        }


        public int DoWork()
        {
            Debug.WriteLine("Merge2Job.DoWork() ...");
            XElement knights = _out.Element("knights");
            SingletonKnightParser.LoadWbXml(_wbPathAndFilename);
            
            IEnumerable<XElement> allElements =
                from el in _ksRelationshipsXml.Elements()
                select el;
            
            foreach (XElement element in allElements)
            {
                if (element.Name != "knight") continue;
                XElement knight = SingletonKnightParser.Parse(element);
                knights?.Add(knight);
            }

            using (FileStream destinationStream = File.Create(_outPathAndFilename))
            {
                knights?.Save(destinationStream, SaveOptions.None);    
            }
            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");
            return 0;
        }
    }
}