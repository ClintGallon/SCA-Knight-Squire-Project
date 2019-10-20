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
using System.Diagnostics;
using System.IO;


namespace CreateKnightSquireXml
{
    public class Job
    {

        private XElement ksRelationshipsXml;
        private XElement whiteBeltXml;
        private XElement outputXml; 
        private string pathFilenameSerializedOutput;
       
        public Job()
        {
            ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
            whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml");
            pathFilenameSerializedOutput = @"C:\projects\SCA-Knight-Squire-Project\data\serialized-output.xml";
            outputXml = new XElement("knights");
            
        }
        
        public void DoWork()
        {

            //Console.WriteLine(ksRelationshipsXml);
           // Console.WriteLine(whitebeltXml);

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

     
            IEnumerable<XElement> relationshipKnights = 
                from knight in ksRelationshipsXml.Descendants("knight")  
                select knight;  

            var relationships = new DictKnightList();

            foreach (var node in relationshipKnights)
            {
                var newKnight = new DictKnight();
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
                            newKnight.Squires = newKnight.ParseSquires(el);
                            break;

                        default:
                            break;
                    }
                }

                if (newKnight.SocietyPrecedence == -1)
                {
                    Debug.WriteLine("Name: " + newKnight.Name + " Type: " + newKnight.Type);
                }
                relationships.Add(newKnight);
                if (newKnight.SocietyPrecedence == 83)
                {
                    break;
                }
            }
            
            foreach (var k in relationships)
            {
                var wbk = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("society_precedence") == k.SocietyPrecedence.ToString());

                if (wbk is null)
                {
                    //Nothing to do
                }
                else
                {
                    int.TryParse(wbk.Element("society_knight_number")?.Value, out int kn);
                    k.KnightNumber = kn;
                    int.TryParse(wbk.Element("society_master_number")?.Value, out int mn);
                    k.MasterNumber = mn;
                    k.Name = wbk.Element("name")?.Value;
                    k.Type = wbk.Element("type")?.Value;
                    k.DateElevated = DateTime.Parse(wbk.Element("date_elevated")?.Value);
                    int.TryParse(wbk.Element("anno_societatous")?.Value, out int ass);
                    k.AnnoSocietatous = ass;
                    k.Kingdom = wbk.Element("kingdom_of_elevation")?.Value;
                    int.TryParse(wbk.Element("kingdom_precedence")?.Value, out int kp);
                    k.KingdomPrecedence = kp;
                    k.ResignedOrRemoved = wbk.Element("kingdom_precedence")?.Value;
                    k.PassedAway = wbk.Element("passed_away")?.Value;
                    k.Squires = k.ParseSquires(wbk.Element("squires"));
                }
               // Debug.WriteLine(k);
            }

            Debug.WriteLine("Serializing ...");

            XmlSerializer serializer = new XmlSerializer(typeof(ArrayKnightList));
            TextWriter writer = new StreamWriter(pathFilenameSerializedOutput);
            serializer.Serialize(writer, relationships);
            writer.Close();

            Debug.WriteLine("Ending ...");
        }
    }
}

