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
        private XElement outputXElement;
        private XElement ksRelationshipsXml;
        private XmlTextWriter txtWriter;
        private XElement outputFile; 
        private static XDocument outDoc;
        
        public Job2()
        {
            ksRelationshipsXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\ks_relationships.xml");
            pathFilenameOutputXml = @"C:\projects\SCA-Knight-Squire-Project\data\seesharp-output.xml";
            outputXElement = XElement.Parse(@"<knights></knights>");
            outputXElement.Save(pathFilenameOutputXml, SaveOptions.None);
            
        }

        public void DoWork()
        {

            //Console.WriteLine(ksRelationshipsXml);
            //Console.WriteLine(whitebeltXml);

            //var relationshipKnights = ksRelationshipsXml.Descendants("knight");

            IEnumerable<XElement> relationshipKnights = 
                from knight in ksRelationshipsXml.Descendants("knight")  
                select knight;  

            
            foreach (var rKnight in relationshipKnights)
            {
                if (rKnight.Name == "knight")
                {
                    Debug.WriteLine("----- In: ");
                    Debug.WriteLine(rKnight);
                    var newKnight = SingletonKnightParser.Parse(rKnight);
                   
                    outDoc = XDocument.Load(pathFilenameOutputXml);
                    if (outDoc.Root != null && outDoc.Root.HasElements)
                    {
                        outDoc.Root.LastNode.AddAfterSelf(newKnight);
                    }
                    else
                    {
                        outDoc.Root.Add(newKnight);
                    }
                    outDoc.Save(pathFilenameOutputXml);
                }
            }
            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");
        }
 
    }
}

