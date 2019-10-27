using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CreateKnightSquireXml
{
    public sealed class SingletonKnightParser
    {
        private static int instanceCounter = 0;

        private static XElement whiteBeltXml; 
        
        private static readonly Lazy<SingletonKnightParser> lazy = new Lazy<SingletonKnightParser>(() => new SingletonKnightParser());

        public static SingletonKnightParser Instance => lazy.Value;

        public static int LoadWbXml(string wbxml)
        {
            whiteBeltXml = XElement.Load(wbxml);

            return 0;
        }
        
        static SingletonKnightParser()
        {
            instanceCounter++;
            Debug.WriteLine("Instances created: " + instanceCounter);
        }

        public static XElement Parse(XElement knightNode)
        {

            Debug.WriteLine("Inbound knightNode: " + knightNode);
            XElement retKnight = new XElement("knight");

            if (knightNode is null)
            {
                return retKnight;
            }
            if (knightNode.HasElements)
            {
                Dictionary<XName, XElement> childrenList = knightNode.Descendants().ToDictionary(knightChildNode => knightChildNode.Name);

                string sp = childrenList["society_precedence"].Value;

                Debug.WriteLine("-----------sp------------");
                Debug.WriteLine(sp);

                XElement wbElementKnight = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("society_precedence") == sp);

                Debug.WriteLine("-----------wbElementKnight------------");
                Debug.WriteLine(wbElementKnight);
                
                if (wbElementKnight is null)
                {

                    XElement notFoundSocietyPrecedentNode = new XElement("society_precedence") {Value = sp};
                    retKnight.Descendants().Append(notFoundSocietyPrecedentNode);

                    Debug.WriteLine("-----------notFoundSocietyPrecedentNode------------");
                    Debug.WriteLine(notFoundSocietyPrecedentNode);

                    string notfoundNameValue = childrenList["name"].Value;
                    XElement notFoundNameNode = new XElement("name") {Value = notfoundNameValue};
                    retKnight.Descendants().Append(notFoundNameNode);

                    Debug.WriteLine("-----------notFoundNameNode------------");
                    Debug.WriteLine(notFoundNameNode);

                    string notFoundTypeValue = childrenList["type"].Value;
                    XElement notFoundTypeNode = new XElement("type") {Value = notFoundTypeValue};
                    retKnight.Descendants().Append(notFoundTypeNode);

                    Debug.WriteLine("-----------notFoundTypeNode------------");
                    Debug.WriteLine(notFoundTypeNode);

                    XElement notFoundSquiresNode = childrenList["squires"];
                    Debug.WriteLine("-----------notFoundSquiresNode------------");
                    Debug.WriteLine(notFoundSquiresNode);

                    XElement newNotFoundSquiresNode = new XElement("squires"); 

                    foreach (XElement notFoundSquireNode in notFoundSquiresNode.Descendants())
                    {
                        Debug.WriteLine("-----------notFoundSquireNode------------");
                        Debug.WriteLine(notFoundSquireNode);

                        XElement notFoundParsedKnight = SingletonKnightParser.Parse(notFoundSquireNode);
                        newNotFoundSquiresNode.Descendants().Append(notFoundParsedKnight);
                    }

                    Debug.WriteLine("-----------newNotFoundSquiresNode------------");
                    Debug.WriteLine(newNotFoundSquiresNode);

                    retKnight.Descendants().Append(notFoundSquiresNode);
                }
                else
                {
                    
                    //Found him in whiteBelt file
                    XElement spNode = new XElement("society_precedence") {Value = sp};
                    retKnight.Descendants().Append(spNode);

                    Debug.WriteLine("-----------spNode------------");
                    Debug.WriteLine(spNode);

                    string nameValue = childrenList["name"].Value;
                    XElement nameNode = new XElement("name") {Value = nameValue ?? throw new NullReferenceException()};
                    retKnight.Descendants().Append(nameNode);

                    Debug.WriteLine("-----------nameNode------------");
                    Debug.WriteLine(nameNode);

                    string typeValue = childrenList["type"].Value;
                    XElement typeNode = new XElement("type") {Value = typeValue ?? throw new NullReferenceException()};
                    retKnight.Descendants().Append(typeNode);

                    Debug.WriteLine("-----------typeNode------------");
                    Debug.WriteLine(typeNode);

                    string sknValue = childrenList["society_knight_number"].Value;
                    XElement skn = new XElement("society_knight_number") {Value = sknValue};
                    retKnight.Descendants().Append(skn);

                    Debug.WriteLine("-----------skn------------");
                    Debug.WriteLine(skn);

                    string smnValue = childrenList["society_master_number"].Value;
                    XElement smn = new XElement("society_master_number") {Value = smnValue};
                    retKnight.Descendants().Append(smn);

                    Debug.WriteLine("-----------smn------------");
                    Debug.WriteLine(smn);

                    string dteValue = childrenList["date_elevated"].Value;
                    XElement dte = new XElement("date_elevated") {Value = dteValue};
                    retKnight.Descendants().Append(dte);

                    Debug.WriteLine("-----------dte------------");
                    Debug.WriteLine(dte);

                    string annoValue = childrenList["anno_societatous"].Value;
                    XElement annoNode = new XElement("anno_societatous") {Value = annoValue};
                    retKnight.Descendants().Append(annoNode);

                    Debug.WriteLine("-----------annoNode------------");
                    Debug.WriteLine(annoNode);

                    string keValue = childrenList["kingdom_of_elevation"].Value;
                    XElement keNode  = new XElement("kingdom_of_elevation") {Value = keValue};
                    retKnight.Descendants().Append(keNode);

                    Debug.WriteLine("-----------keNode------------");
                    Debug.WriteLine(keNode);

                    string   kpValue = childrenList["kingdom_precedence"].Value;
                    XElement kpNode  = new XElement("kingdom_precedence") {Value = kpValue};
                    retKnight.Descendants().Append(kpNode);

                    Debug.WriteLine("-----------kpNode------------");
                    Debug.WriteLine(kpNode);
                    
                    string   rrValue = childrenList["resigned_or_removed"].Value;
                    XElement rrNode  = new XElement("resigned_or_removed") {Value = rrValue};
                    retKnight.Descendants().Append(rrNode);

                    Debug.WriteLine("-----------rrNode------------");
                    Debug.WriteLine(rrNode);

                    string   paValue = childrenList["resigned_or_removed"].Value;
                    XElement paNode  = new XElement("resigned_or_removed") {Value = paValue};
                    retKnight.Descendants().Append(paNode);

                    Debug.WriteLine("-----------paNode------------");
                    Debug.WriteLine(paNode);

                    IEnumerable<XElement> squiresDescendants = knightNode.Descendants("squires"); 

                    XElement newSquiresNode = new XElement("squires");

                    IEnumerable<XElement> squireNodes = squiresDescendants as XElement[] ?? squiresDescendants.ToArray();
                    if (squireNodes.Any())
                    {
                        foreach (XElement squireNode in squireNodes)
                        {
                            Debug.WriteLine("-----------squire------------");
                            Debug.WriteLine(squireNode);

                            XElement parsedKnight = SingletonKnightParser.Parse(squireNode);
                            newSquiresNode.Descendants().Append(parsedKnight);

                        }
                        retKnight.Descendants().Append(newSquiresNode);
                        Debug.WriteLine("-----------newSquiresNode------------");
                        Debug.WriteLine(newSquiresNode);
                    }
                    else
                    {
                        retKnight.Descendants().Append(newSquiresNode);
                        Debug.WriteLine("-----------newSquiresNode------------");
                        Debug.WriteLine(newSquiresNode);
                    }
                }
            }
            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retKnight);
            return retKnight;
        }

        public static StringBuilder Parse(XmlNode knightNode)
        {
            
            StringBuilder retKnightBuilder = new StringBuilder("<knight>");

            if (knightNode.HasChildNodes)
            {
                Dictionary<string, XmlNode> childrenList = knightNode.ChildNodes.Cast<XmlNode>().ToDictionary(knightChildNode => knightChildNode.Name);

                string searchNameValue = string.Empty;
                if (childrenList.ContainsKey("name"))
                {
                    searchNameValue = childrenList["name"]?.InnerText;
                }
                string searchName = searchNameValue;

                XElement wbElementKnight = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName);
                
                if (wbElementKnight is null)
                {
                    string notFoundSocietyPrecedenceValue = string.Empty;
                    if (childrenList.ContainsKey("society_precedence"))
                    {
                        notFoundSocietyPrecedenceValue = childrenList["society_precedence"]?.InnerText;
                    }
                    string notFoundSocietyPrecedenceNode = "<society_precedence>" + notFoundSocietyPrecedenceValue + "</society_precedence>";
                    retKnightBuilder.Append(notFoundSocietyPrecedenceNode);

                    string notFoundNameValue = string.Empty;
                    if (childrenList.ContainsKey("name"))
                    {
                        notFoundNameValue = childrenList["name"]?.InnerText;
                    }
                    string notFoundNameNode = "<name>" + notFoundNameValue + "</name>";
                    retKnightBuilder.Append(notFoundNameNode);
                    Debug.WriteLine(notFoundNameNode);

                    string notFoundTypeValue = string.Empty;
                    if (childrenList.ContainsKey("type"))
                    {
                        notFoundTypeValue = childrenList["type"]?.InnerText;
                    }
                    string notFoundTypeNode = "<type>" + notFoundTypeValue + "</type>";
                    retKnightBuilder.Append(notFoundTypeNode);

                    string notesNode = "<notes>Not found whitebelt spreadsheet</notes>";
                    retKnightBuilder.Append(notesNode);

                    StringBuilder notFoundSquires = new StringBuilder("<squires>");

                    foreach (XmlNode knightChildNode in knightNode.ChildNodes)
                    {
                        if (knightChildNode.HasChildNodes)
                        {
                            if (knightChildNode.Name != "squires") continue;
                            foreach (XmlNode squire in knightChildNode.ChildNodes)
                            {
                                StringBuilder parsedKnight = SingletonKnightParser.Parse(squire);
                                notFoundSquires.Append(parsedKnight);
                            }
                            notFoundSquires.Append("</squires>");
                            retKnightBuilder.Append(notFoundSquires);
                        }
                        else
                        {
                            notFoundSquires.Append("</squires>");
                            retKnightBuilder.Append(notFoundSquires);
                        }
                    }
                }
                else
                {
                    
                    XmlDocument xD = new XmlDocument();
                    xD.LoadXml(wbElementKnight.ToString());
                    XmlNode wbKnightNode = xD.FirstChild;

                    Dictionary<string, XmlNode> wbChildrenList = wbKnightNode.ChildNodes.Cast<XmlNode>().ToDictionary(wbChildNode => wbChildNode.Name);

                    //Found him in whiteBelt file
                    string foundspValue = string.Empty;
                    if (wbChildrenList.ContainsKey("society_precedence"))
                    {
                        foundspValue = wbChildrenList["society_precedence"]?.InnerText;
                    }
                    string foundspNode = "<society_precedence>" + foundspValue + "</society_precedence>";
                    retKnightBuilder.Append(foundspNode);

                    string nameValue = string.Empty;
                    if (wbChildrenList.ContainsKey("name"))
                    {
                        nameValue = wbChildrenList["name"]?.InnerText;
                    }
                    string nameNode = "<name>" + nameValue + "</name>";
                    retKnightBuilder.Append(nameNode);
                    Debug.WriteLine(nameNode);

                    string typeValue = string.Empty;
                    if (wbChildrenList.ContainsKey("type"))
                    {
                        typeValue = wbChildrenList["type"]?.InnerText;
                    }
                    string typeNode = "<type>" + typeValue + "</type>";
                    retKnightBuilder.Append(typeNode);

                    string sknValue = string.Empty;
                    if (wbChildrenList.ContainsKey("society_knight_number"))
                    {
                        sknValue = wbChildrenList["society_knight_number"]?.InnerText;
                    }
                    string sknNode = "<society_knight_number>" + sknValue + "</society_knight_number>";
                    retKnightBuilder.Append(sknNode);

                    string smnValue = string.Empty;
                    if (wbChildrenList.ContainsKey("society_master_number"))
                    {
                        smnValue = wbChildrenList["society_master_number"]?.InnerText;
                    }
                    string smnNode = "<society_master_number>" + smnValue + "</society_master_number>";
                    retKnightBuilder.Append(smnNode);

                    string dteValue = string.Empty;
                    if (wbChildrenList.ContainsKey("date_elevated"))
                    {
                        dteValue = wbChildrenList["date_elevated"]?.InnerText;
                    }
                    string dteNode = "<date_elevated>" + dteValue + "</date_elevated>";
                    retKnightBuilder.Append(dteNode);

                    string annoValue = string.Empty;
                    if (wbChildrenList.ContainsKey("anno_societatous"))
                    {
                        annoValue = wbChildrenList["anno_societatous"]?.InnerText;
                    }
                    string annoNode = "<anno_societatous>" + annoValue + "</anno_societatous>";
                    retKnightBuilder.Append(annoNode);

                    string keValue = string.Empty;
                    if (wbChildrenList.ContainsKey("kingdom_of_elevation"))
                    {
                        keValue = wbChildrenList["kingdom_of_elevation"]?.InnerText;
                    }
                    string keNode = "<kingdom_of_elevation>" + keValue + "</kingdom_of_elevation>";
                    retKnightBuilder.Append(keNode);

                    string kpValue = string.Empty;
                    if (wbChildrenList.ContainsKey("kingdom_precedence"))
                    {
                        kpValue = wbChildrenList["kingdom_precedence"]?.InnerText;
                    }
                    string kpNode = "<kingdom_precedence>" + kpValue + "</kingdom_precedence>";
                    retKnightBuilder.Append(kpNode);

                    string rrValue = string.Empty;
                    if (wbChildrenList.ContainsKey("resigned_or_removed"))
                    {
                        rrValue = wbChildrenList["resigned_or_removed"]?.InnerText;
                    }
                    rrValue = rrValue.Replace("&", " and ");
                    string rrNode = "<resigned_or_removed>" + rrValue + "</resigned_or_removed>";
                    retKnightBuilder.Append(rrNode);

                    string paValue = string.Empty;
                    if (wbChildrenList.ContainsKey("passed_away"))
                    {
                        paValue = wbChildrenList["passed_away"]?.InnerText;
                    }
                    string paNode = "<passed_away>" + paValue + "</passed_away>";
                    retKnightBuilder.Append(paNode);

                    string notesValue = string.Empty;
                    if (wbChildrenList.ContainsKey("notes"))
                    {
                        notesValue = wbChildrenList["notes"]?.InnerText;
                    }
                    string notesNode = "<notes>" + notesValue + "</notes>";
                    retKnightBuilder.Append(notesNode);

                    StringBuilder newSquiresNode = new StringBuilder();
                    newSquiresNode.Append("<squires>");
                    
                    foreach (XmlNode knightNodeChildren in knightNode.ChildNodes)
                    {
                        if (knightNodeChildren.Name == "squires")
                        {
                            if (knightNodeChildren.HasChildNodes)
                            {
                                foreach (XmlNode squireNode in knightNodeChildren.ChildNodes)
                                {
                                    StringBuilder parsedKnight = SingletonKnightParser.Parse(squireNode);
                                    newSquiresNode.Append(parsedKnight);
                                }
                                newSquiresNode.Append("</squires>");
                                retKnightBuilder.Append(newSquiresNode);
                            }
                            else
                            {
                                newSquiresNode.Append("</squires>");
                                retKnightBuilder.Append(newSquiresNode);
                            }
                        }
                    }
                }
            }
            retKnightBuilder.Append("</knight>");
            return retKnightBuilder;
            
        }
    }
}