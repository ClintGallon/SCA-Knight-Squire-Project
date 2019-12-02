using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SingletonKnightParser
    {
        private static int _instanceCounter;

        private static XElement _whiteBeltXml;

        private static readonly Lazy<SingletonKnightParser> Lazy = new Lazy<SingletonKnightParser>(() => new SingletonKnightParser());

        /// <summary>
        /// constructor
        /// </summary>
        public SingletonKnightParser()
        {
            _instanceCounter++;
            Debug.WriteLine("Instances created: " + _instanceCounter);
        }

        /// <summary>
        /// 
        /// </summary>
        public static SingletonKnightParser Instance => Lazy.Value;

        /// <summary>
        /// Loads the whitebelt xml file into memory
        /// </summary>
        /// <param name="wbxml"></param>
        /// <returns></returns>
        public static int LoadWbXml(string wbxml)
        {
            _whiteBeltXml = XElement.Load(wbxml);
            return 0;
        }

        /// <summary>
        /// Given an XElement of a knight ... parse it all and pass back an XElement
        /// </summary>
        /// <param name="knightNode"></param>
        /// <returns></returns>
        public static XElement Parse(XElement knightNode)
        {
            Debug.WriteLine("Inbound knightNode: " + knightNode);

            var retKnight = new XElement("knight");

            if (knightNode is null) return retKnight;

            if (knightNode.HasElements)
            {
                //Dictionary<XName, XElement> childrenList      = knightNode.Descendants().ToDictionary(knightChildNode => knightChildNode.Name);
                string searchName = knightNode.Elements("name").FirstOrDefault()?.Value;

                var wbElementKnights = _whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName);

                if (wbElementKnights is null)
                {
                    retKnight.Add(new XElement("society_precedence", "-1"));
                    retKnight.Add(knightNode.Elements("name"));
                    retKnight.Add(knightNode.Elements("type"));
                    if (knightNode.Elements("type").FirstOrDefault()?.Value == "knight")
                    {
                        if (knightNode.Elements("notes").FirstOrDefault()?.Value != "")
                        {
                            string strValue = knightNode.Elements("notes").FirstOrDefault()?.Value + "|not in wbXml";
                            retKnight.Add(new XElement("notes", strValue));
                        }
                        else
                        {
                            retKnight.Add(new XElement("notes", "not in wbXml"));
                        }
                    }
                }
                else
                {
                    //Found him in whiteBelt file
                    retKnight.Add(wbElementKnights.Elements("society_precedence"));
                    retKnight.Add(wbElementKnights.Elements("name"));
                    retKnight.Add(wbElementKnights.Elements("type"));
                    retKnight.Add(wbElementKnights.Elements("society_knight_number"));
                    retKnight.Add(wbElementKnights.Elements("society_master_number"));
                    retKnight.Add(wbElementKnights.Elements("date_elevated"));
                    retKnight.Add(wbElementKnights.Elements("anno_societatous"));
                    retKnight.Add(wbElementKnights.Elements("kingdom_of_elevation"));
                    retKnight.Add(wbElementKnights.Elements("kingdom_precedence"));
                    retKnight.Add(wbElementKnights.Elements("resigned_or_removed"));
                    retKnight.Add(wbElementKnights.Elements("passed_away"));
                    retKnight.Add(wbElementKnights.Elements("notes"));
                }

                retKnight.Add(knightNode.Elements("squire-names"));
                var newSquiresNode = new XElement("squires");
                if (knightNode.Elements("squires").Elements("knight").Any())
                    foreach (var squireNode in knightNode.Elements("squires").Elements("knight"))
                    {
                        var parsedKnight = Parse(squireNode);
                        newSquiresNode.Add(parsedKnight);
                    }

                retKnight.Add(newSquiresNode);
            }

            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retKnight);
            return retKnight;
        }

        /// <summary>
        /// Pass in an XML Node return back a string.
        /// </summary>
        /// <param name="knightNode"></param>
        /// <returns></returns>
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
                    retKnightBuilder.Append("<society_precedence>-1</society_precedence>");
                    if (childrenList.ContainsKey("name")) retKnightBuilder.Append(childrenList["name"].OuterXml);

                    if (childrenList.ContainsKey("type")) retKnightBuilder.Append(childrenList["type"].OuterXml);

                    retKnightBuilder.Append("<notes>not found wbxml</notes>");
                }
                else
                {
                    //Found him in whiteBelt file
                    var xD = new XmlDocument();
                    xD.LoadXml(wbElementKnight.ToString());
                    var wbKnightNode = xD.FirstChild;

                    Dictionary<string, XmlNode> wbChildrenList = wbKnightNode.ChildNodes.Cast<XmlNode>().ToDictionary(wbChildNode => wbChildNode.Name);

                    if (wbChildrenList.ContainsKey("society_precedence")) retKnightBuilder.Append(wbChildrenList["society_precedence"].OuterXml);

                    if (wbChildrenList.ContainsKey("name")) retKnightBuilder.Append(wbChildrenList["name"].OuterXml);

                    if (wbChildrenList.ContainsKey("type")) retKnightBuilder.Append(wbChildrenList["type"].OuterXml);

                    if (wbChildrenList.ContainsKey("society_knight_number")) retKnightBuilder.Append(wbChildrenList["society_knight_number"].OuterXml);

                    if (wbChildrenList.ContainsKey("society_master_number")) retKnightBuilder.Append(wbChildrenList["society_master_number"].OuterXml);

                    if (wbChildrenList.ContainsKey("date_elevated")) retKnightBuilder.Append(wbChildrenList["date_elevated"].OuterXml);

                    if (wbChildrenList.ContainsKey("anno_societatous")) retKnightBuilder.Append(wbChildrenList["anno_societatous"].OuterXml);

                    if (wbChildrenList.ContainsKey("kingdom_of_elevation")) retKnightBuilder.Append(wbChildrenList["kingdom_of_elevation"].OuterXml);

                    if (wbChildrenList.ContainsKey("kingdom_precedence")) retKnightBuilder.Append(wbChildrenList["kingdom_precedence"].OuterXml);

                    if (wbChildrenList.ContainsKey("resigned_or_removed")) retKnightBuilder.Append(wbChildrenList["resigned_or_removed"].OuterXml);

                    if (wbChildrenList.ContainsKey("passed_away")) retKnightBuilder.Append(wbChildrenList["passed_away"].OuterXml);

                    if (wbChildrenList.ContainsKey("notes")) retKnightBuilder.Append(wbChildrenList["notes"].OuterXml);
                }

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

            retKnightBuilder.Append("</knight>");
            Debug.WriteLine(retKnightBuilder);
            return retKnightBuilder;
        }
    }
}