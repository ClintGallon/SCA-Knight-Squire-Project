using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class KnightParser
    {

        XElement whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml");

        public KnightParser()
        {
            
        }
        public XElement Parse(XElement knightXElement)
        {
            
            Console.WriteLine("knightXElement: " + knightXElement);
            var retKnight = new XElement("knight");

            if (knightXElement is null)
            {
                return retKnight;
            }
            
            if (knightXElement.HasElements)
            {

                IEnumerable<XElement> knightNodes = 
                    from knight in knightXElement.Descendants("knight")  
                    select knight;

                foreach (var knightNode in knightNodes)
                {
                    var sp = knightNode.Descendants("society_precedence").FirstOrDefault()?.Value;

                        var wbElementKnight = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("society_precedence") == sp);

                        if (wbElementKnight is null)
                        {
                            //Not found in the whitebelt file
                            //use the data you have sent to you

                            XElement notFoundSocietyPrecedentNode = new XElement("society_precedence")
                                                                    {
                                                                        Value = sp
                                                                    };
                            retKnight.Add(notFoundSocietyPrecedentNode);

                            var notFoundNameValue = knightNode.Descendants("name").FirstOrDefault()?.Value;
                            XElement notFoundNameNode = new XElement("name")
                                              {
                                                  Value = notFoundNameValue
                                              };
                            retKnight.Add(notFoundNameNode);

                            var notFoundTypeValue = knightNode.Descendants("type").FirstOrDefault()?.Value;
                            XElement notFoundTypeNode = new XElement("type")
                                                {
                                                    Value = notFoundTypeValue
                                                };
                            retKnight.Add(notFoundTypeNode);

                            var notFoundSquiresXElement = knightNode.Descendants("squires").FirstOrDefault();

                            var notFoundSquires = 
                                from knight in notFoundSquiresXElement.Descendants("knight") 
                                select knight;

                            var newNotFoundSquiresXElement = new XElement("squires");
                            
                            foreach (var notFoundSquire in notFoundSquires)
                            {
                                var notFoundParser = new KnightParser();
                                var notFoundParsedKnight = notFoundParser.Parse(notFoundSquire);
                                newNotFoundSquiresXElement.Add(notFoundParsedKnight);
                                retKnight.Add(newNotFoundSquiresXElement);
                            }

                            

                        }
                        else
                        {
                            //Found him in whiteBelt file
                            XElement spNode = new XElement("society_precedence")
                                           {
                                               Value = sp
                                           };
                            retKnight.Add(spNode);

                            var nameValue = wbElementKnight.Descendants("name").FirstOrDefault()?.Value;
                            XElement elName = new XElement("name")
                                           {
                                               Value = nameValue
                                           };
                            retKnight.Add(elName);

                            var typeValue = wbElementKnight.Descendants("type").FirstOrDefault()?.Value;
                            XElement typeNode = new XElement("type")
                                              {
                                                  Value = typeValue
                                              };
                            retKnight.Add(typeNode);
                            
                            var sknValue = wbElementKnight.Descendants("society_knight_number").FirstOrDefault()?.Value;
                            XElement skn = new XElement("society_knight_number")
                                           {
                                               Value = sknValue
                                           };
                            retKnight.Add(skn);

                            var smnValue = wbElementKnight.Descendants("society_master_number").FirstOrDefault()?.Value;
                            XElement smn = new XElement("society_master_number")
                                           {
                                               Value = smnValue
                                           };
                            retKnight.Add(smn);

                            var dteValue = wbElementKnight.Descendants("date_elevated").FirstOrDefault()?.Value;
                            XElement dte = new XElement("date_elevated")
                                           {
                                               Value = dteValue
                                           };
                            retKnight.Add(dte);

                            var asXElementValue = wbElementKnight.Descendants("anno_societatous").FirstOrDefault()?.Value;
                            XElement asXElement = new XElement("anno_societatous")
                                                  {
                                                      Value = asXElementValue
                                                  };
                            retKnight.Add(asXElement);

                            var keXElementValue = wbElementKnight.Descendants("kingdom_of_elevation").FirstOrDefault()?.Value;
                            XElement keXElement = new XElement("kingdom_of_elevation")
                                                  {
                                                      Value = keXElementValue
                                                  };
                            retKnight.Add(keXElement);

                            var kpXElementValue = wbElementKnight.Descendants("kingdom_precedence").FirstOrDefault()?.Value;
                            XElement kpXElement = new XElement("kingdom_precedence")
                                                  {
                                                      Value = kpXElementValue
                                                  };
                            retKnight.Add(kpXElement);

                            var rrXElementValue = wbElementKnight.Descendants("resigned_or_removed").FirstOrDefault()?.Value;
                            XElement rrXElement = new XElement("resigned_or_removed")
                                                  {
                                                      Value = rrXElementValue
                                                  };
                            retKnight.Add(rrXElement);

                            var paXElementValue = wbElementKnight.Descendants("passed_away").FirstOrDefault()?.Value;
                            XElement paXElement = new XElement("passed_away")
                                                  {
                                                      Value = paXElementValue
                                                  };
                            retKnight.Add(paXElement);

                            var squiresXElement = knightXElement.Descendants("squires").FirstOrDefault();

                            var squires = 
                                from knight in squiresXElement.Descendants("knight") 
                                select knight;

                            var newSquiresXElement = new XElement("squires");
                            
                            if (squires.Any())
                            {
                                foreach (var squire in squires)
                                {
                                    var parser = new KnightParser();
                                    var parsedKnight = parser.Parse(squire);
                                    newSquiresXElement.Add(parsedKnight);
                                    retKnight.Add(newSquiresXElement);
                                }
                            }
                            else
                            {
                                retKnight.Add(newSquiresXElement);
                            }
                        }

                }
            }
            return retKnight;
        }
    }
}