using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
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

        private string pathFilenameOutputXml;
        private string pathFilenameOutputXmlNode;
        private XmlDocument outDoc;
        private readonly XElement _ksRelationshipsXml;
        private XmlWriter _writer;
        
        public Job2()
        {
            
            pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\seesharp-output.xml";
            pathFilenameOutputXmlNode = @"C:\projects\SCA-Knight-Squire-Project\data\see-sharp-Node.xml";
            
            _ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
        }


        public void DoWork()
        {
            //First File
            XmlWriterSettings settings = new XmlWriterSettings();  
            settings.Indent = true;
            settings.IndentChars = "    ";
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            FileStream fs = new FileStream(pathFilenameOutputXml, FileMode.Create);
            
            //Node
            XmlWriterSettings settingsNode = new XmlWriterSettings();  
            settingsNode.Indent      = true;
            settingsNode.IndentChars = "    ";
            settingsNode.ConformanceLevel = ConformanceLevel.Fragment;
            FileStream fsNode = new FileStream(pathFilenameOutputXmlNode, FileMode.Create);
            
            using (XmlWriter writer = XmlWriter.Create(fs, settings)) {  
                writer.WriteStartElement(string.Empty, "knights", string.Empty);

                IEnumerable<XElement> relationshipKnights =
                    from knight in _ksRelationshipsXml.Elements("knight")
                    select knight;
                
                foreach (XElement rKnight in relationshipKnights)
                {
                    Dictionary<XName, XElement> rKnightChildren = rKnight.Descendants().ToDictionary(child => child.Name);

                    Debug.WriteLine("----- In: ");
                    Debug.WriteLine(rKnight);

                    XElement newKnight = SingletonKnightParser.Parse(rKnight);
                
                    Debug.WriteLine("newKnight.OuterXml --------------------------------------");
                    Debug.WriteLine(newKnight);
                    
                    writer.WriteValue(newKnight);
                    writer.Flush();
                }
                writer.WriteEndElement();  //knights
                writer.Flush();
                writer.Close();
            }  
            
            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");
        }
 
    }
}

