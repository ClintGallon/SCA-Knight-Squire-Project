using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
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
    public class MissingJob
    {
        private readonly string _pathFilenameRelationships;
        private readonly string _pathFilenameWbXml;
        private readonly string _pathFilenameOutputfile;

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
            XElement     relationships = XElement.Load(_pathFilenameRelationships);
            XElement     wbXml         = XElement.Load(_pathFilenameWbXml);

            foreach (XElement knight in wbXml.Elements())
            {
                string searchName = knight.Descendants("name").FirstOrDefault()?.Value;

                XElement foundKnight = relationships.Elements("knight").FirstOrDefault(r => (string) r.Element("name") == searchName);

                if (foundKnight == null)
                {
                    missing.Add(searchName);
                }
                else
                {
                    found.Add(searchName);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(_pathFilenameOutputfile, false))
                {
                    foreach (string item in missing)
                    {
                        file.WriteLine(item);
                    }
                }

                Debug.WriteLine("Total Found: "   + found.Count);
                Debug.WriteLine("Total Missing: " + missing.Count);
            }


            return 0;
        }
    }
}