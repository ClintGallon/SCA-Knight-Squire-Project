using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class SingletonArrayKnightParser
    {
        private static int _instanceCounter;

        private static XElement _whiteBeltXml;

        private static readonly Lazy<SingletonArrayKnightParser> Lazy = new Lazy<SingletonArrayKnightParser>(() => new SingletonArrayKnightParser());

        /// <summary>
        /// 
        /// </summary>
        public SingletonArrayKnightParser()
        {
            _instanceCounter++;
            Debug.WriteLine("Instances created: " + _instanceCounter);
        }

        /// <summary>
        /// 
        /// </summary>
        public static SingletonArrayKnightParser Instance => Lazy.Value;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wbxml"></param>
        /// <returns></returns>
        public static int LoadWbXml(string wbxml)
        {
            _whiteBeltXml = XElement.Load(wbxml);
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="knightXElement"></param>
        /// <returns></returns>
        public static ArrayKnight Parse(XElement knightXElement)
        {
            Debug.WriteLine("Inbound knightXElement: " + knightXElement);

            var retKnight = new ArrayKnight();

            if (knightXElement is null) return retKnight;

            if (knightXElement.HasElements)
            {
                string searchName = knightXElement.Elements("name").FirstOrDefault()?.Value;
                string searchDateElevated = knightXElement.Elements("date_elevated").FirstOrDefault()?.Value;
                string searchType = knightXElement.Elements("type").FirstOrDefault()?.Value;
                var wbElementKnights = _whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName && (string) wb.Element("date_elevated") == searchDateElevated && (string) wb.Element("type") == searchType);
                if (wbElementKnights is null)
                {
                    retKnight.Name = knightXElement.Elements("name").FirstOrDefault()?.Value;
                    retKnight.DateElevated = DateTime.Parse(knightXElement.Elements("date_elevated").FirstOrDefault()?.Value);
                    retKnight.Type = knightXElement.Elements("type").FirstOrDefault()?.Value;
                    if (knightXElement.Elements("type").FirstOrDefault()?.Value == "knight")
                    {
                        if (knightXElement.Elements("notes").FirstOrDefault()?.Value != "")
                        {
                            string strValue = knightXElement.Elements("notes").FirstOrDefault()?.Value + "|not in wbXml";
                            retKnight.Notes = strValue;
                        }
                        else
                        {
                            retKnight.Notes = "not in wbXml";
                        }
                    }
                }
                else
                {
                    //Found him in whiteBelt file
                    retKnight.SocietyPrecedence = !string.IsNullOrEmpty(wbElementKnights.Elements("society_precedence").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_precedence").FirstOrDefault()?.Value) : -1;
                    retKnight.Name = wbElementKnights.Elements("name").FirstOrDefault()?.Value;
                    retKnight.Type = wbElementKnights.Elements("type").FirstOrDefault()?.Value;
                    retKnight.KnightNumber = !string.IsNullOrEmpty(wbElementKnights.Elements("society_knight_number").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_knight_number").FirstOrDefault()?.Value) : 0;
                    retKnight.MasterNumber = !string.IsNullOrEmpty(wbElementKnights.Elements("society_master_number").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_master_number").FirstOrDefault()?.Value) : 0;
                    retKnight.DateElevated = DateTime.Parse(wbElementKnights.Elements("date_elevated").FirstOrDefault()?.Value ?? "1/1/1966");
                    retKnight.AnnoSocietatous = !string.IsNullOrEmpty(wbElementKnights.Elements("anno_societatous").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("anno_societatous").FirstOrDefault()?.Value) : 0;
                    retKnight.Kingdom = wbElementKnights.Elements("kingdom_of_elevation").FirstOrDefault()?.Value;
                    retKnight.KingdomPrecedence = !string.IsNullOrEmpty(wbElementKnights.Elements("kingdom_precedence").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("kingdom_precedence").FirstOrDefault()?.Value) : 0;
                    retKnight.ResignedOrRemoved = wbElementKnights.Elements("resigned_or_removed").FirstOrDefault()?.Value;
                    retKnight.PassedAway = wbElementKnights.Elements("passed_away").FirstOrDefault()?.Value;
                    retKnight.Notes = wbElementKnights.Elements("notes").FirstOrDefault()?.Value;
                }
                
                List<ArrayKnight> squiresList = new List<ArrayKnight>();
                
                if (knightXElement.Elements("squires").Elements("knight").Any()) squiresList.AddRange(knightXElement.Elements("squires").Elements("knight").Select(ParseSquire));

                retKnight.Squires = squiresList.ToArray();

            }

            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retKnight);
            return retKnight;
        }

        private static ArrayKnight ParseSquire(XElement squireXElement)
        {

            Debug.WriteLine("Inbound squireXElement: " + squireXElement);

            var retSquire = new ArrayKnight();

            if (squireXElement is null) return retSquire;

            if (squireXElement.HasElements)
            {
                string searchName       = squireXElement.Elements("name").FirstOrDefault()?.Value;
                var    wbElementKnights = _whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("name") == searchName);
                if (wbElementKnights is null)
                {
                    retSquire.Name = squireXElement.Elements("name").FirstOrDefault()?.Value;
                    retSquire.Type = squireXElement.Elements("type").FirstOrDefault()?.Value;
                    if (squireXElement.Elements("type").FirstOrDefault()?.Value == "knight")
                    {
                        if (squireXElement.Elements("notes").FirstOrDefault()?.Value != "")
                        {
                            string strValue = squireXElement.Elements("notes").FirstOrDefault()?.Value + "|not in wbXml";
                            retSquire.Notes = strValue;
                        }
                        else
                        {
                            retSquire.Notes = "not in wbXml";
                        }
                    }
                }
                else
                {
                    //Found him in whiteBelt file
                    retSquire.SocietyPrecedence = !string.IsNullOrEmpty(wbElementKnights.Elements("society_precedence").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_precedence").FirstOrDefault()?.Value) : -1;
                    retSquire.Name              = wbElementKnights.Elements("name").FirstOrDefault()?.Value;
                    retSquire.Type              = wbElementKnights.Elements("type").FirstOrDefault()?.Value;
                    retSquire.KnightNumber      = !string.IsNullOrEmpty(wbElementKnights.Elements("society_knight_number").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_knight_number").FirstOrDefault()?.Value) : 0;
                    retSquire.MasterNumber      = !string.IsNullOrEmpty(wbElementKnights.Elements("society_master_number").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("society_master_number").FirstOrDefault()?.Value) : 0;
                    retSquire.DateElevated      = DateTime.Parse(wbElementKnights.Elements("date_elevated").FirstOrDefault()?.Value    ?? "1/1/1966");
                    retSquire.AnnoSocietatous   = !string.IsNullOrEmpty(wbElementKnights.Elements("anno_societatous").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("anno_societatous").FirstOrDefault()?.Value) : 0;
                    retSquire.Kingdom           = wbElementKnights.Elements("kingdom_of_elevation").FirstOrDefault()?.Value;
                    retSquire.KingdomPrecedence = !string.IsNullOrEmpty(wbElementKnights.Elements("kingdom_precedence").FirstOrDefault()?.Value) ? int.Parse(wbElementKnights.Elements("kingdom_precedence").FirstOrDefault()?.Value) : 0;
                    retSquire.ResignedOrRemoved = wbElementKnights.Elements("resigned_or_removed").FirstOrDefault()?.Value;
                    retSquire.PassedAway        = wbElementKnights.Elements("passed_away").FirstOrDefault()?.Value;
                    retSquire.Notes             = wbElementKnights.Elements("notes").FirstOrDefault()?.Value;
                }

                List<ArrayKnight> squiresList = new List<ArrayKnight>();

                if (squireXElement.Elements("squires").Elements("knight").Any()) squiresList.AddRange(squireXElement.Elements("squires").Elements("knight").Select(ParseSquire));

                retSquire.Squires = squiresList.ToArray();

            }
            
            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retSquire);
            return retSquire;
        }
    }
}