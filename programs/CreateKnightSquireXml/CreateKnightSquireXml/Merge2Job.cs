using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class Merge2Job
    {
        private readonly XElement _ksRelationshipsXml;
        private readonly XElement _out;
        private readonly string   _outPathAndFilename;
        private readonly string   _wbPathAndFilename;
        private          Stream   _stream;

        private string pathFilenameOutputXml;

        public Merge2Job(string wbPathAndFilename, string relPathAndFilename, string outPathAndFilename)
        {
            _wbPathAndFilename  = wbPathAndFilename;
            _outPathAndFilename = outPathAndFilename;
            _ksRelationshipsXml = XElement.Load(relPathAndFilename);

            const string doubleQuote = "\"";
            const string ss          = @"<?xml version=" + doubleQuote + "1.0" + doubleQuote + " encoding=" + doubleQuote + "utf-8" + doubleQuote + "?><knights></knights>";
            _out = XElement.Parse(ss);
        }

        public int DoWork()
        {
            //debug.WriteLine("Merge2Job.DoWork() ...");
            var knights = _out.Element("knights");
            SingletonKnightParser.LoadWbXml(_wbPathAndFilename);

            IEnumerable<XElement> allElements =
                from el in _ksRelationshipsXml.Elements()
                select el;

            List<XElement> allTheKnights = new List<XElement>();

            foreach (var element in allElements)
            {
                if (element.Name != "knight") continue;
                var knight = SingletonKnightParser.Parse(element);
                allTheKnights.Add(knight);
                knights?.Add(knight);
            }

            using (var file = new StreamWriter(_outPathAndFilename))
            {
                const string doubleQuote = "\"";
                const string xmlHeader   = @"<?xml version=" + doubleQuote + "1.0" + doubleQuote + " encoding=" + doubleQuote + "utf-8" + doubleQuote + "?>";
                file.WriteLine(xmlHeader);
                const string knightsStart = "<knights>";
                file.WriteLine(knightsStart);

                foreach (string line in allTheKnights.Select(knightElement => knightElement.ToString()))
                {
                    file.WriteLine(line);
                }

                const string knightsEnd = "</knights>";
                file.WriteLine(knightsEnd);
            }

            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");
            return 0;
        }
    }
}