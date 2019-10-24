﻿using System;
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
           ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships-squirenames.xml");
           whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml"); 
           pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest_squirenames.xml";
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
                var rElementKnight = ksRelationshipsXml.Elements("knight").FirstOrDefault(r => (string) r.Element("name") == outputName);

                if (rElementKnight is null)
                {
                    
                }
                else
                {
                    var squiresNames = rElementKnight.Descendants("squire-names").FirstOrDefault()?.Value;
                    writer.WriteElementString("squire-names", squiresNames);
                }
                writer.WriteEndElement(); //knight
            }
            //Debug.WriteLine("Found: " + namesFound.Count);
            //Debug.WriteLine("Not Found: " + namesNotFound.Count);

            writer.WriteEndElement(); //knights
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();
        }
    } 
}
