using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class MissingJob
    {
        private readonly string _pathFilenameOutputfile;
        private readonly string _pathFilenameRelationships;
        private readonly string _pathFilenameWbXml;

        public MissingJob(string wbPathAndFilename, string relPAthAndFilename, string outPathAndFilename)
        {
            _pathFilenameRelationships = relPAthAndFilename;
            _pathFilenameWbXml         = wbPathAndFilename;
            _pathFilenameOutputfile    = outPathAndFilename;
        }

        public int DoWork()
        {
            List<string> found         = new List<string>();
            List<string> missing       = new List<string>();
            var          relationships = XElement.Load(_pathFilenameRelationships);
            var          wbXml         = XElement.Load(_pathFilenameWbXml);

            foreach (var knight in wbXml.Elements())
            {
                string searchName = knight.Descendants("name").FirstOrDefault()?.Value;

                var foundKnight = relationships.Elements("knight").FirstOrDefault(r => (string) r.Element("name") == searchName);

                if (foundKnight == null)
                    missing.Add(searchName);
                else
                    found.Add(searchName);

                using (var file = new StreamWriter(_pathFilenameOutputfile, false))
                {
                    foreach (string item in missing) file.WriteLine(item);
                }

                Debug.WriteLine("Total Found: "   + found.Count);
                Debug.WriteLine("Total Missing: " + missing.Count);
            }
            return 0;
        }
    }
}