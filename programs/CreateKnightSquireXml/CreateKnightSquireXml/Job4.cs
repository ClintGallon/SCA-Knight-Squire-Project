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
    class Job4
    {
        private XmlDocument relationshipsDoc;
        private XElement whiteBeltXml;
        private XElement outputXml;
        private string ksRelationshipsXmlFilename;
        private string pathFilenameOutputXml;

        public Job4()
        {
           ksRelationshipsXmlFilename = @"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships-squirenames.xml";
           DateTime currentDateTime = DateTime.Now;
           pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest_squirenames-" + currentDateTime.ToString("yyyyMMdd-MMss") + ".xml";
           whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml"); 
           relationshipsDoc = new XmlDocument();
           relationshipsDoc.Load(ksRelationshipsXmlFilename);
        }

        public void DoWork()
        {
  
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "    ";
            XmlWriter writer = XmlWriter.Create(pathFilenameOutputXml, settings);

            writer.WriteStartDocument();

            writer.WriteStartElement("knights");
            var wbKnights = whiteBeltXml.Descendants("knight");

            foreach (var wbKnight in wbKnights)
            {

                writer.WriteStartElement("knight");
                writer.WriteElementString("society_precedence", wbKnight.Descendants("society_precedence").FirstOrDefault()?.Value);
                writer.WriteElementString("society_knight_number", wbKnight.Descendants("society_knight_number").FirstOrDefault()?.Value);
                writer.WriteElementString("society_master_number", wbKnight.Descendants("society_master_number").FirstOrDefault()?.Value);
                writer.WriteElementString("name", wbKnight.Descendants("name").FirstOrDefault()?.Value);
                writer.WriteElementString("type", wbKnight.Descendants("type").FirstOrDefault()?.Value);
                writer.WriteElementString("date_elevated", wbKnight.Descendants("date_elevated").FirstOrDefault()?.Value);
                writer.WriteElementString("anno_societatous", wbKnight.Descendants("anno_societatous").FirstOrDefault()?.Value);
                writer.WriteElementString("kingdom_of_elevation", wbKnight.Descendants("kingdom_of_elevation").FirstOrDefault()?.Value);
                writer.WriteElementString("kingdom_precedence", wbKnight.Descendants("kingdom_precedence").FirstOrDefault()?.Value);
                writer.WriteElementString("resigned_or_removed", wbKnight.Descendants("resigned_or_removed").FirstOrDefault()?.Value);
                writer.WriteElementString("passed_away", wbKnight.Descendants("passed_away").FirstOrDefault()?.Value);
                writer.WriteElementString("notes", wbKnight.Descendants("notes").FirstOrDefault()?.Value);

                var outputName = wbKnight.Descendants("name").FirstOrDefault()?.Value;
                var squiresNames = FindSquireNames(outputName);
                writer.WriteElementString("squire-names", squiresNames);

                writer.WriteEndElement(); //knight
            }

            writer.WriteEndElement(); //knights
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

        }

        private string FindSquireNames(string knightName)
        {

            string strReturn = string.Empty;

            XmlNodeList rKnightsNodeList = relationshipsDoc.GetElementsByTagName("knight");
            Debug.WriteLine("rKnightsNodeList total: " + rKnightsNodeList.Count);
            foreach (XmlNode node in rKnightsNodeList)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "name")
                        if (childNode.InnerText == knightName)
                        {
                            Debug.WriteLine("Found: " + knightName);

                            //walk all the childnodes of this node to find the node named "squire-names"
                            foreach (XmlNode childNode2 in node)
                            {
                                if (childNode2.Name == "squire-names")
                                {
                                    strReturn = childNode2.InnerText;
                                    Debug.WriteLine("Returning: " + strReturn);
                                    return strReturn;
                                }
                            }
                        }
                }
            }
            Debug.WriteLine("Returning: " + strReturn);
            return strReturn;
        }

    } 
}
