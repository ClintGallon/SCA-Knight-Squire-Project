using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;

namespace CreateKnightSquireXml
{
    class Job3
    {

        private XElement ksRelationshipsXml;
        private XElement whiteBeltXml;
        private XElement outputXml;
        private string pathFilenameOutputXml;

        public Job3()
        {
           ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
           whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml"); 
           pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\KnightsNotInRelationships.xml";
        }

        public void DoWork()
        {
            var namesNotFound = new List<string>();
            var namesFound = new List<string>();
                 
            IEnumerable<XElement> wbKnights = 
                from knight in whiteBeltXml.Descendants("knight")  
                select knight;


            var relKnights = ksRelationshipsXml.XPathSelectElements("//knight");

            foreach (var wbKnight in wbKnights)
            {
                var outputName = wbknight.Descendants("name").FirstOrDefault()?.Value;

                //see if we can find the name in the relationships file ... 
                
                IEnumerable<XElement> relationshipKnights = 
                    from knight in ksRelationshipsXml.XPathSelectElements("//knight")  
                    select knight;


                var firstOrDefault = relationshipKnights.Descendants("name").FirstOrDefault(rk => (string) rk.Element("name") == outputName);

                if (firstOrDefault?.Value == outputName)
                {
                    namesFound.Add(outputName);
                }
                else
                {
                    namesNotFound.Add(outputName);
                    
                }
            }
            Console.WriteLine("Found: "     + namesFound.Count);
            Console.WriteLine("Not Found: " + namesNotFound.Count);
            
        }
    } 
}
