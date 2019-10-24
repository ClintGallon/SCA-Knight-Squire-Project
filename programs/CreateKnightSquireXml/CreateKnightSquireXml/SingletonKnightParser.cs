using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using System.Linq;
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

        private static readonly XElement whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml");

        private static readonly Lazy<SingletonKnightParser> lazy = new Lazy<SingletonKnightParser>(() => new SingletonKnightParser());

        public static SingletonKnightParser Instance => lazy.Value;

        public SingletonKnightParser()
        {
            instanceCounter++;
            Debug.WriteLine("Instances created: " + instanceCounter);
        }

        public static XmlNode Parse(XmlNode knightNode, XmlDocument ksRelationshipsXml)
        {

            Debug.WriteLine("Inbound knightNode: " + knightNode.OuterXml);
            XmlNode retKnight = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "knight", String.Empty);

            if (knightNode is null)
            {
                return retKnight;
            }
            if (knightNode.HasChildNodes)
            {
                Dictionary<string, XmlNode> childrenList = knightNode.ChildNodes.Cast<XmlNode>().ToDictionary(knightChildNode => knightChildNode.Name);

                string sp = childrenList["society_precedence"].InnerText;

                Debug.WriteLine("-----------sp------------");
                Debug.WriteLine(sp);

                XElement wbElementKnight = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("society_precedence") == sp);

                Debug.WriteLine("-----------wbElementKnight------------");
                Debug.WriteLine(wbElementKnight);
                
                if (wbElementKnight is null)
                {
                //Not found in the whitebelt file
                //use the data you have sent to you

                    XmlNode notFoundSocietyPrecedentNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "society_precedence", string.Empty);
                    notFoundSocietyPrecedentNode.InnerText = sp;
                    retKnight.AppendChild(notFoundSocietyPrecedentNode);

                    Debug.WriteLine("-----------notFoundSocietyPrecedentNode------------");
                    Debug.WriteLine(notFoundSocietyPrecedentNode.OuterXml);

                    string notFoundNameValue = childrenList["name"].InnerText;
                    XmlNode notFoundNameNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "name", string.Empty);
                    notFoundNameNode.InnerText = notFoundNameValue;
                    retKnight.AppendChild(notFoundNameNode);

                    Debug.WriteLine("-----------notFoundNameNode------------");
                    Debug.WriteLine(notFoundNameNode.OuterXml);

                    string notFoundTypeValue = childrenList["type"].InnerText;
                    XmlNode notFoundTypeNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "type", string.Empty);
                    notFoundTypeNode.InnerText = notFoundTypeValue;
                    retKnight.AppendChild(notFoundTypeNode);

                    Debug.WriteLine("-----------notFoundTypeNode------------");
                    Debug.WriteLine(notFoundTypeNode.OuterXml);

                    XmlNode notFoundSquiresNode = childrenList["squires"];
                    Debug.WriteLine("-----------notFoundSquiresNode------------");
                    Debug.WriteLine(notFoundSquiresNode.OuterXml);

                    XmlNode newNotFoundSquiresNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "squires", string.Empty);

                    foreach (XmlNode notFoundSquireNode in notFoundSquiresNode)
                    {
                        Debug.WriteLine("-----------notFoundSquireNode------------");
                        Debug.WriteLine(notFoundSquireNode.OuterXml);

                        XmlNode notFoundParsedKnight = SingletonKnightParser.Parse(notFoundSquireNode, ksRelationshipsXml);
                        newNotFoundSquiresNode.AppendChild(notFoundParsedKnight);
                    }

                    Debug.WriteLine("-----------newNotFoundSquiresNode------------");
                    Debug.WriteLine(newNotFoundSquiresNode.OuterXml);

                    retKnight.AppendChild(newNotFoundSquiresNode);
                }
                else
                {

                    
                    //Found him in whiteBelt file
                    XmlNode spNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "society_precedence", string.Empty);
                    spNode.InnerText = sp;
                    retKnight.AppendChild(spNode);

                    Debug.WriteLine("-----------spNode------------");
                    Debug.WriteLine(spNode.OuterXml);

                    string nameValue = wbElementKnight.Descendants("name").FirstOrDefault()?.Value;
                    XmlNode nameNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "name", string.Empty);
                    nameNode.InnerText = nameValue;
                    retKnight.AppendChild(nameNode);

                    Debug.WriteLine("-----------nameNode------------");
                    Debug.WriteLine(nameNode.OuterXml);
                    
                    string typeValue = wbElementKnight.Descendants("type").FirstOrDefault()?.Value;
                    XmlNode typeNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "type", string.Empty);
                    typeNode.InnerText = typeValue;
                    retKnight.AppendChild(typeNode);

                    Debug.WriteLine("-----------typeNode------------");
                    Debug.WriteLine(typeNode.OuterXml);

                    string sknValue = wbElementKnight.Descendants("society_knight_number").FirstOrDefault()?.Value;
                    XmlNode skn = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"society_knight_number", string.Empty);
                    skn.InnerText = sknValue;
                    retKnight.AppendChild(skn);

                    Debug.WriteLine("-----------skn------------");
                    Debug.WriteLine(skn.OuterXml);

                    string smnValue = wbElementKnight.Descendants("society_master_number").FirstOrDefault()?.Value;
                    XmlNode smn = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"society_master_number", string.Empty);
                    smn.InnerText = smnValue;
                    retKnight.AppendChild(smn);

                    Debug.WriteLine("-----------smn------------");
                    Debug.WriteLine(smn.OuterXml);

                    string dteValue = wbElementKnight.Descendants("date_elevated").FirstOrDefault()?.Value;
                    XmlNode dte = ksRelationshipsXml.CreateNode(XmlNodeType.Element, "date_elevated", string.Empty);
                    dte.InnerText = dteValue;
                    retKnight.AppendChild(dte);

                    Debug.WriteLine("-----------dte------------");
                    Debug.WriteLine(dte.OuterXml);

                    string annoValue = wbElementKnight.Descendants("anno_societatous").FirstOrDefault()?.Value;
                    XmlNode annoNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"anno_societatous", string.Empty);
                    annoNode.InnerText = annoValue;
                    retKnight.AppendChild(annoNode);

                    Debug.WriteLine("-----------annoNode------------");
                    Debug.WriteLine(annoNode.OuterXml);

                    string keValue = wbElementKnight.Descendants("kingdom_of_elevation").FirstOrDefault()?.Value;
                    var keNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"kingdom_of_elevation", string.Empty);
                    keNode.InnerText = keValue;
                    retKnight.AppendChild(keNode);

                    Debug.WriteLine("-----------keNode------------");
                    Debug.WriteLine(keNode.OuterXml);

                    string kpValue = wbElementKnight.Descendants("kingdom_precedence").FirstOrDefault()?.Value;
                    var kpNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"kingdom_precedence", string.Empty);
                    kpNode.InnerText = kpValue;
                    retKnight.AppendChild(kpNode);

                    Debug.WriteLine("-----------kpNode------------");
                    Debug.WriteLine(kpNode.OuterXml);
                    
                    string rrValue = wbElementKnight.Descendants("resigned_or_removed").FirstOrDefault()?.Value;
                    var rrNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"resigned_or_removed", string.Empty);
                    rrNode.InnerText = rrValue;
                    retKnight.AppendChild(rrNode);

                    Debug.WriteLine("-----------rrNode------------");
                    Debug.WriteLine(rrNode.OuterXml);

                    string paValue = wbElementKnight.Descendants("passed_away").FirstOrDefault()?.Value;
                    var paNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"passed_away", string.Empty);
                    paNode.InnerText = paValue;
                    retKnight.AppendChild(paNode);

                    Debug.WriteLine("-----------paNode------------");
                    Debug.WriteLine(paNode.OuterXml);

                    XmlNode squiresNode = childrenList["squires"];

                    var newSquiresNode = ksRelationshipsXml.CreateNode(XmlNodeType.Element,"squires", string.Empty);

                    if (squiresNode.HasChildNodes)
                    {
                        foreach (XmlElement squireNode in squiresNode)
                        {
                            Debug.WriteLine("-----------squire------------");
                            Debug.WriteLine(squireNode.OuterXml);

                            XmlNode parsedKnight = SingletonKnightParser.Parse(squireNode, ksRelationshipsXml);
                            newSquiresNode.AppendChild(parsedKnight);

                        }
                        retKnight.AppendChild(newSquiresNode);
                        Debug.WriteLine("-----------newSquiresNode------------");
                        Debug.WriteLine(newSquiresNode);
                    }
                    else
                    {
                        retKnight.AppendChild(newSquiresNode);
                        Debug.WriteLine("-----------newSquiresNode------------");
                        Debug.WriteLine(newSquiresNode.OuterXml);
                    }
                }
            }
            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retKnight.OuterXml);
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
                            if (knightChildNode.Name == "squires")
                            {
                                foreach (XmlNode squire in knightChildNode.ChildNodes)
                                {
                                    StringBuilder parsedKnight = SingletonKnightParser.Parse(squire);
                                    notFoundSquires.Append(parsedKnight);
                                }
                                notFoundSquires.Append("</squires>");
                                retKnightBuilder.Append(notFoundSquires);
                            }                          
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