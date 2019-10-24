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
        private readonly XmlDocument _ksRelationshipsXml;
        private XmlWriter _writer;
        
        public Job2()
        {
            
            pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\seesharp-output.xml";
            pathFilenameOutputXmlNode = @"C:\projects\SCA-Knight-Squire-Project\data\see-sharp-Node.xml";
            
            _ksRelationshipsXml = new XmlDocument();
            _ksRelationshipsXml.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
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

                var relationshipKnights = _ksRelationshipsXml.GetElementsByTagName("knight");
                foreach (XmlNode rKnight in relationshipKnights)
                {
                    Dictionary<string, XmlNode> rKnightChildren = rKnight.ChildNodes.Cast<XmlNode>().ToDictionary(child => child.Name);

                    Debug.WriteLine("----- In: ");
                    Debug.WriteLine(rKnight.OuterXml);

                    XmlNode newKnight = SingletonKnightParser.Parse(rKnight, _ksRelationshipsXml);
                
                    Debug.WriteLine("newKnight.OuterXml --------------------------------------");
                    Debug.WriteLine(newKnight.OuterXml);
                    
                    if (rKnightChildren["society_precedence"].InnerText == "42")
                    {
                        using (XmlWriter writerNode = XmlWriter.Create(fsNode, settingsNode))
                        {
                            //writerNode.Write(newKnight);
                            writerNode.Flush();
                            writerNode.Close();
                        }
                    }
                    
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

