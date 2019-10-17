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
    public class Job
    {

        private XElement ksRelationshipsXml;
        private XElement whitebeltXml;
        private XElement outputXml;
        // private string pathFilenameOutputXml;
       
        public Job()
        {
           
            ksRelationshipsXml = XElement.Load(@".\ks_relationships.xml");  
            whitebeltXml = XElement.Load(@".\SCA_ChivList-Latest.xml");
           // pathFilenameOutputXml = @".\seesharp-output.xml";
            outputXml = new XElement("knights");
            
        }
        
        public void DoWork()
        {

            //Console.WriteLine(ksRelationshipsXml);
            Console.WriteLine(whitebeltXml);

            //IEnumerable<XElement> knights =
            //    from el in whitebeltXml.Elements("knight")
            //    where (string) el.Element("society_precedence") == "10"
            //    select el;
            //foreach (XElement el in knights)
            //{
            //    Console.WriteLine((string) el.Value);
            //}
            //XElement root = XElement.Parse(@"<Root> <Child> Text </Child> </Root>");  
            //using (StringWriter sw = new StringWriter()) {  
            //    root.Save(sw);  
            //    Console.WriteLine(sw.ToString());  
            //}  

            //// LINQ to XML query  
            //IEnumerable<XElement> list1 = data.Elements();
  
            //// XPath expression  
            //IEnumerable<XElement> list2 = whitebeltXml.Root.XPathSelectElements("./*");  

            var relationshipKnights = ksRelationshipsXml.Descendants();

            var relationships = new List<Knight>();

            foreach (var node in relationshipKnights)
            {
                if (node.Name == "knight")
                {
                    var newKnight = new Knight();
                    var nodeElements = node.Descendants();
                    foreach (var el in nodeElements)
                    {
                        switch (el.Name.ToString())
                        {
                            case "name":
                                newKnight.Name = el.Value;
                                break;

                            case "society_precedence":
                                newKnight.SocietyPrecedence = int.Parse(el.Value);
                                break;

                            case "type":
                                newKnight.Type = el.Value;
                                break;

                            case "squires":
                                var returningSquires = newKnight.ParseSquires(el);
                                foreach (var rs in returningSquires)
                                {
                                    newKnight.Squires.Add(rs);
                                }
                                break;

                            default:
                                break;
                        }
                    }
                    relationships.Add(newKnight);
                }

            }
            
            var fortyOne = relationships.First(Knight => Knight.SocietyPrecedence == 1);

            
            foreach (var k in relationships)
            {
                var knights =
                    from el in whitebeltXml.Elements("knight")
                    where (string) el.Element("society_precedence") == k.SocietyPrecedence.ToString()
                    select el;

                foreach (var el in knights)
                {
                    int.TryParse(el.Element("society_knight_number")?.Value, out int kn);
                    k.KnightNumber = kn;
                    int.TryParse(el.Element("society_master_number")?.Value, out int mn);
                    k.MasterNumber = mn;
                    k.Name = el.Element("name")?.Value;
                    k.Type = el.Element("type")?.Value;
                    k.DateElevated = DateTime.Parse(el.Element("date_elevated")?.Value);
                    int.TryParse(el.Element("anno_societatous")?.Value, out int ass);
                    k.AnnoSocietatus = ass;
                    k.Kingdom = el.Element("kingdom_of_elevation")?.Value;
                    int.TryParse(el.Element("kingdom_precedence")?.Value, out int kp);
                    k.KingdomPrecedence = kp;
                    k.ResignedOrRemoved = el.Element("kingdom_precedence")?.Value;
                    k.PassedAway = el.Element("passed_away")?.Value;
                    k.Squires = k.ParseSquires(el.Element("squires"));
                }

                Console.WriteLine(k);
            }


            Console.WriteLine("Ending ...");

        }
    }
}

