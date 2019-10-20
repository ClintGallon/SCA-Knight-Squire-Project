using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;

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
            
            XElement ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
            XElement whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml");
            pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-seesharp-output.xml";
            outputXml = new XElement("knights");

        }

        public void DoWork()
        {

/*
            XElement relationships = ksRelationshipsXml.Element("knights");  
            XElement newKnights = new XElement("knights",  
                                               from r in this.ksRelationshipsXml.Element("knight")  
                                               join w in this.whiteBeltXml.Element("knight").Elements("knight")  
                                                   on (string) r.Element("society_precedence") equals  
                                                   (string) w.Element("society_precedence")  
                                                    select new XElement("knight",  
                                                                   new XElement("society_precedence", (string) r.Element("society_precedence")),  
                                                                   new XElement("name", (string) r.Element("name")),  
                                                                   new XElement("type", (string) r.Element("type")),
                                                                   new XElement("squires", (string) w.Element("society_knight_number")),
                                                                   new XElement("squires", (string) w.Element("society_master_number")),
                                                                   new XElement("squires", (string) w.Element("date_elevated")),
                                                                   new XElement("squires", (string) w.Element("anno_societatous")),
                                                                   new XElement("squires", (string) w.Element("kingdom_of_elevation")),
                                                                   new XElement("squires", (string) w.Element("kingdom_precedence")),
                                                                   new XElement("squires", (string) w.Element("resigned_or_removed")),
                                                                   new XElement("squires", (string) w.Element("passed_away")),
                                                                   new XElement("squires", (string) w.Element("notes")),
                                                                   new XElement("squires", (XElement) r.Element("squires"))
                                                                  )  );
                                                                   Console.WriteLine(newKnights);
*/

           
        }
    } 
}
