using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.IO;
using System.Threading;
using System.Reflection;
//using System.IO.Packaging;
using System.Net.Sockets;
using System.Security.Permissions;


namespace CreateKnightSquireXml
{
    public class Job2
    {

        private XElement ksRelationshipsXml;
        private XElement whitebeltXml;
        private string pathFilenameOutputXml;

        public Job2()
        {

            ksRelationshipsXml = XElement.Load(@".\ks_relationships.xml");
            whitebeltXml = XElement.Load(@".\SCA_ChivList-Latest.xml");
            pathFilenameOutputXml = @".\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + "-seesharp-output.xml";
            

        }

        public void DoWork()
        {

            //Console.WriteLine(ksRelationshipsXml);
            //Console.WriteLine(whitebeltXml);

            var relationshipKnights = ksRelationshipsXml.Descendants();

            var relationships = new List<Knight>();

            foreach (var node in relationshipKnights)
            {
                if (node.Name == "knight")
                {
                    
                    var sp = node.Descendants("society_precedence").FirstOrDefault()?.Value;

                    var wbElementKnight = whitebeltXml.Elements("knight").FirstOrDefault(wb => (string)wb.Element("society_precedence") == sp);

                    XElement skn = new XElement("society_knight_number")
                    {
                        Value = wbElementKnight.Descendants("society_knight_number").FirstOrDefault()?.Value
                    };
                    node.Add(skn);

                    XElement smn = new XElement("society_master_number")
                    {
                        Value = wbElementKnight.Descendants("society_master_number").FirstOrDefault()?.Value
                    };
                    node.Add(smn);

                    XElement dte = new XElement("date_elevated")
                    {
                        Value = wbElementKnight.Descendants("date_elevated").FirstOrDefault()?.Value
                    };
                    node.Add(dte);

                    XElement asXElement = new XElement("anno_societatous")
                    {
                        Value = wbElementKnight.Descendants("anno_societatous").FirstOrDefault()?.Value
                    };
                    node.Add(asXElement);

                    XElement keXElement = new XElement("kingdom_of_elevation")
                    {
                        Value = wbElementKnight.Descendants("kingdom_of_elevation").FirstOrDefault()?.Value
                    };
                    node.Add(keXElement);

                    XElement kpXElement = new XElement("kingdom_precedence")
                    {
                        Value = wbElementKnight.Descendants("kingdom_precedence").FirstOrDefault()?.Value
                    };
                    node.Add(kpXElement);

                    XElement rrXElement = new XElement("resigned_or_removed")
                    {
                        Value = wbElementKnight.Descendants("resigned_or_removed").FirstOrDefault()?.Value
                    };
                    node.Add(rrXElement);

                    XElement paXElement = new XElement("passed_away")
                    {
                        Value = wbElementKnight.Descendants("passed_away").FirstOrDefault()?.Value
                    };
                    node.Add(paXElement);

                    XElement sqXElement = new XElement("squires")
                    {
                        Value = wbElementKnight.Descendants("squires").FirstOrDefault()?.Value
                    };
                    node.Add(sqXElement);
                }
            }

            XElement outputXml = new XElement("knights", relationshipKnights);  
            outputXml.Save(pathFilenameOutputXml);  
            string str = File.ReadAllText(pathFilenameOutputXml);  
            Console.WriteLine(str);  

            Console.WriteLine("complete ...");

        }
    }
}

