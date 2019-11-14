using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public sealed class SingletonKnightParser
    {
        private static int _instanceCounter;

        private static XElement _whiteBeltXml;

        private static readonly Lazy<SingletonKnightParser> Lazy = new Lazy<SingletonKnightParser>(() => new SingletonKnightParser());

        public SingletonKnightParser()
        {
            _instanceCounter++;
            Debug.WriteLine("Instances created: " + _instanceCounter);
        }

        public static SingletonKnightParser Instance => Lazy.Value;

        public static int LoadWbXml(string wbxml)
        {
            _whiteBeltXml = XElement.Load(wbxml);
            return 0;
        }

        public static XElement Parse(XElement knightNode)
        {
            Debug.WriteLine("Inbound knightNode: " + knightNode);

            XElement retKnight = new XElement("knight");

            if (knightNode is null) return retKnight;

            if (knightNode.HasElements)
            {
                Dictionary<XName, XElement> childrenList      = knightNode.Descendants().ToDictionary(knightChildNode => knightChildNode.Name);
                string                      searchName = childrenList["name"].Value;

                var wbElementKnights = _whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName);

                if (wbElementKnights is null)
                {

                    IEnumerable<XElement> squiresDescendants = knightNode.Descendants("squires");
                    XElement              newSquiresNode     = new XElement("squires");
                    IEnumerable<XElement> squireNodes        = squiresDescendants as XElement[] ?? squiresDescendants.ToArray();
                    if (squireNodes.Any())
                        foreach (XElement squireNode in squireNodes)
                        {
                            XElement parsedKnight = Parse(squireNode);
                            newSquiresNode.Descendants().Append(parsedKnight);
                        }

                    retKnight.Elements().Append(new XElement("society_precedence", childrenList["society_precedence"].Value));
                    retKnight.Elements().Append(new XElement("name", childrenList["name"].Value));
                    retKnight.Elements().Append(new XElement("name", childrenList["type"].Value));
                    retKnight.Elements().Append(newSquiresNode);

                    Debug.WriteLine(retKnight);
                }
                else
                {
                    //Found him in whiteBelt file
                    Dictionary<XName, XElement> wbChildrenList = wbElementKnights.Descendants().ToDictionary(wbElementKnightChild => wbElementKnightChild.Name);


                    IEnumerable<XElement> retKnightDescendants = retKnight.Descendants();
                    IEnumerable<XElement> knightNodes = retKnightDescendants as XElement[] ?? retKnightDescendants.ToArray();
                    
                   
                    
                    knightNodes.Append(new XElement("society_precedence", wbChildrenList["society_precedence"].Value));
                    knightNodes.Append(new XElement("name", wbChildrenList["name"].Value));
                    knightNodes.Append(new XElement("type", wbChildrenList["type"].Value));
                    knightNodes.Append(new XElement("society_knight_number", wbChildrenList["society_knight_number"].Value));
                    knightNodes.Append(new XElement("society_master_number", wbChildrenList["society_master_number"].Value));
                    knightNodes.Append(new XElement("date_elevated", wbChildrenList["date_elevated"].Value));
                    knightNodes.Append(new XElement("anno_societatous", wbChildrenList["anno_societatous"].Value));
                    knightNodes.Append(new XElement("kingdom_of_elevation", wbChildrenList["kingdom_of_elevation"].Value));
                    knightNodes.Append(new XElement("kingdom_precedence", wbChildrenList["kingdom_precedence"].Value));
                    knightNodes.Append(new XElement("resigned_or_removed", wbChildrenList["resigned_or_removed"].Value));
                    knightNodes.Append(new XElement("passed_away", wbChildrenList["passed_away"].Value));

                    foreach (var node in knightNodes)
                    {
                        Debug.WriteLine(node);
                    }
                    
                    /*IEnumerable<XElement> squiresDescendants = wbElementKnights.Descendants("squires");
                                                                                    var                   newSquiresNode     = new XElement("squires");
                                                                                    IEnumerable<XElement> squireNodes        = squiresDescendants as XElement[] ?? squiresDescendants.ToArray();
                                                                                    if (squireNodes.Any())
                                                                                        foreach (var squireNode in squireNodes)
                                                                                        {
                                                                                            var parsedKnight = Parse(squireNode);
                                                                                            newSquiresNode.Elements().Append(parsedKnight);
                                                                                        }
                                                                
                                                                                    retKnight.Elements().Append(newSquiresNode);*/
                }
            }

            Debug.WriteLine(" -- out: ");
            return retKnight;
        }

        public static StringBuilder Parse(XmlNode knightNode)
        {
            var retKnightBuilder = new StringBuilder("<knight>");

            if (knightNode.HasChildNodes)
            {
                Dictionary<string, XmlNode> childrenList = knightNode.ChildNodes.Cast<XmlNode>().ToDictionary(knightChildNode => knightChildNode.Name);

                string searchNameValue                                = string.Empty;
                if (childrenList.ContainsKey("name")) searchNameValue = childrenList["name"]?.InnerText;

                string searchName = searchNameValue;

                var wbElementKnight = _whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName);

                if (wbElementKnight is null)
                {
                    string notFoundSocietyPrecedenceValue                                              = string.Empty;
                    if (childrenList.ContainsKey("society_precedence")) notFoundSocietyPrecedenceValue = childrenList["society_precedence"]?.InnerText;

                    string notFoundSocietyPrecedenceNode = "<society_precedence>" + notFoundSocietyPrecedenceValue + "</society_precedence>";
                    retKnightBuilder.Append(notFoundSocietyPrecedenceNode);

                    string notFoundNameValue                                = string.Empty;
                    if (childrenList.ContainsKey("name")) notFoundNameValue = childrenList["name"]?.InnerText;

                    string notFoundNameNode = "<name>" + notFoundNameValue + "</name>";
                    retKnightBuilder.Append(notFoundNameNode);
                    Debug.WriteLine(notFoundNameNode);

                    string notFoundTypeValue                                = string.Empty;
                    if (childrenList.ContainsKey("type")) notFoundTypeValue = childrenList["type"]?.InnerText;

                    string notFoundTypeNode = "<type>" + notFoundTypeValue + "</type>";
                    retKnightBuilder.Append(notFoundTypeNode);

                    string notFoundNotes                                 = string.Empty;
                    if (childrenList.ContainsKey("notes")) notFoundNotes = childrenList["notes"]?.InnerText;

                    string notFoundNotesNode;
                    if (notFoundNotes != null && !notFoundNotes.Contains("|not found wbxml"))
                        notFoundNotesNode = "<notes>" + notFoundNotes + "|not found wbxml" + "</notes>";
                    else
                        notFoundNotesNode = "<notes>|not found wbxml</notes>";

                    retKnightBuilder.Append(notFoundNotesNode);

                    var notFoundSquires = new StringBuilder("<squires>");

                    foreach (XmlNode knightChildNode in knightNode.ChildNodes)
                        if (knightChildNode.HasChildNodes)
                        {
                            if (knightChildNode.Name != "squires") continue;
                            foreach (XmlNode squire in knightChildNode.ChildNodes)
                            {
                                var parsedKnight = Parse(squire);
                                notFoundSquires.Append(parsedKnight);
                            }
                        }

                    notFoundSquires.Append("</squires>");
                    retKnightBuilder.Append(notFoundSquires);
                }
                else
                {
                    var xD = new XmlDocument();
                    xD.LoadXml(wbElementKnight.ToString());
                    var wbKnightNode = xD.FirstChild;

                    Dictionary<string, XmlNode> wbChildrenList = wbKnightNode.ChildNodes.Cast<XmlNode>().ToDictionary(wbChildNode => wbChildNode.Name);

                    //Found him in whiteBelt file
                    string foundspValue                                                = string.Empty;
                    if (wbChildrenList.ContainsKey("society_precedence")) foundspValue = wbChildrenList["society_precedence"]?.InnerText;

                    string foundspNode = "<society_precedence>" + foundspValue + "</society_precedence>";
                    retKnightBuilder.Append(foundspNode);

                    string nameValue                                  = string.Empty;
                    if (wbChildrenList.ContainsKey("name")) nameValue = wbChildrenList["name"]?.InnerText;

                    string nameNode = "<name>" + nameValue + "</name>";
                    retKnightBuilder.Append(nameNode);
                    Debug.WriteLine(nameNode);

                    string typeValue                                  = string.Empty;
                    if (wbChildrenList.ContainsKey("type")) typeValue = wbChildrenList["type"]?.InnerText;

                    string typeNode = "<type>" + typeValue + "</type>";
                    retKnightBuilder.Append(typeNode);

                    string sknValue                                                   = string.Empty;
                    if (wbChildrenList.ContainsKey("society_knight_number")) sknValue = wbChildrenList["society_knight_number"]?.InnerText;

                    string sknNode = "<society_knight_number>" + sknValue + "</society_knight_number>";
                    retKnightBuilder.Append(sknNode);

                    string smnValue                                                   = string.Empty;
                    if (wbChildrenList.ContainsKey("society_master_number")) smnValue = wbChildrenList["society_master_number"]?.InnerText;

                    string smnNode = "<society_master_number>" + smnValue + "</society_master_number>";
                    retKnightBuilder.Append(smnNode);

                    string dteValue                                           = string.Empty;
                    if (wbChildrenList.ContainsKey("date_elevated")) dteValue = wbChildrenList["date_elevated"]?.InnerText;

                    string dteNode = "<date_elevated>" + dteValue + "</date_elevated>";
                    retKnightBuilder.Append(dteNode);

                    string annoValue                                              = string.Empty;
                    if (wbChildrenList.ContainsKey("anno_societatous")) annoValue = wbChildrenList["anno_societatous"]?.InnerText;

                    string annoNode = "<anno_societatous>" + annoValue + "</anno_societatous>";
                    retKnightBuilder.Append(annoNode);

                    string keValue                                                  = string.Empty;
                    if (wbChildrenList.ContainsKey("kingdom_of_elevation")) keValue = wbChildrenList["kingdom_of_elevation"]?.InnerText;

                    string keNode = "<kingdom_of_elevation>" + keValue + "</kingdom_of_elevation>";
                    retKnightBuilder.Append(keNode);

                    string kpValue                                                = string.Empty;
                    if (wbChildrenList.ContainsKey("kingdom_precedence")) kpValue = wbChildrenList["kingdom_precedence"]?.InnerText;

                    string kpNode = "<kingdom_precedence>" + kpValue + "</kingdom_precedence>";
                    retKnightBuilder.Append(kpNode);

                    string rrValue                                                 = string.Empty;
                    if (wbChildrenList.ContainsKey("resigned_or_removed")) rrValue = wbChildrenList["resigned_or_removed"]?.InnerText;

                    rrValue = rrValue.Replace("&", " and ");
                    string rrNode = "<resigned_or_removed>" + rrValue + "</resigned_or_removed>";
                    retKnightBuilder.Append(rrNode);

                    string paValue                                         = string.Empty;
                    if (wbChildrenList.ContainsKey("passed_away")) paValue = wbChildrenList["passed_away"]?.InnerText;

                    string paNode = "<passed_away>" + paValue + "</passed_away>";
                    retKnightBuilder.Append(paNode);

                    string notesValue                                   = string.Empty;
                    if (wbChildrenList.ContainsKey("notes")) notesValue = wbChildrenList["notes"]?.InnerText;

                    string notesNode = "<notes>" + notesValue + "</notes>";
                    retKnightBuilder.Append(notesNode);

                    var newSquiresNode = new StringBuilder();
                    newSquiresNode.Append("<squires>");

                    foreach (XmlNode knightNodeChildren in knightNode.ChildNodes)
                        if (knightNodeChildren.Name == "squires")
                            if (knightNodeChildren.HasChildNodes)
                                foreach (XmlNode squireNode in knightNodeChildren.ChildNodes)
                                {
                                    var parsedKnight = Parse(squireNode);
                                    newSquiresNode.Append(parsedKnight);
                                }

                    newSquiresNode.Append("</squires>");
                    retKnightBuilder.Append(newSquiresNode);
                }
            }

            retKnightBuilder.Append("</knight>");
            return retKnightBuilder;
        }
    }
}