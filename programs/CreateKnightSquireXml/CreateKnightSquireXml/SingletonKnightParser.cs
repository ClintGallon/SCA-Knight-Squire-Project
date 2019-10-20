using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateKnightSquireXml
{
    public sealed class SingletonKnightParser
    {
        private static int instanceCounter = 0;

        private static readonly XElement whiteBeltXml = XElement.Load(@"C:\projects\SCA-Knight-Squire-Project\data\SCA_ChivList-Latest.xml");

        private static readonly Lazy<SingletonKnightParser> lazy = new Lazy<SingletonKnightParser>(() => new SingletonKnightParser());

        public static SingletonKnightParser Instance => lazy.Value;

        private SingletonKnightParser()
        {
            instanceCounter++;
            Debug.WriteLine("Instances created: " + instanceCounter);
            
        }


        public static XElement Parse(XElement knightXElement)
        {
            Debug.WriteLine("Inbound knightXElement: " + knightXElement);
            var retKnight = new XElement("knight");

            if (knightXElement is null)
            {
                return retKnight;
            }
            if (knightXElement.HasElements)
            {
                    var sp = knightXElement.Descendants("society_precedence").FirstOrDefault()?.Value;

                    Debug.WriteLine("-----------sp------------");
                    Debug.WriteLine(sp);

                    var wbElementKnight = whiteBeltXml.Elements("knight").FirstOrDefault(wb => (string) wb.Element("society_precedence") == sp);

                    Debug.WriteLine("-----------wbElementKnight------------");
                    Debug.WriteLine(wbElementKnight);

                    if (wbElementKnight is null)
                    {
                        //Not found in the whitebelt file
                        //use the data you have sent to you

                        XElement notFoundSocietyPrecedentNode = new XElement("society_precedence")
                                                                {
                                                                    Value = sp
                                                                };
                        retKnight.Add(notFoundSocietyPrecedentNode);

                        Debug.WriteLine("-----------notFoundSocietyPrecedentNode------------");
                        Debug.WriteLine(notFoundSocietyPrecedentNode);


                        var notFoundNameValue = knightXElement.Descendants("name").FirstOrDefault()?.Value;
                        XElement notFoundNameNode = new XElement("name")
                                                    {
                                                        Value = notFoundNameValue
                                                    };
                        retKnight.Add(notFoundNameNode);

                        Debug.WriteLine("-----------notFoundNameNode------------");
                        Debug.WriteLine(notFoundNameNode);

                        var notFoundTypeValue = knightXElement.Descendants("type").FirstOrDefault()?.Value;
                        XElement notFoundTypeNode = new XElement("type")
                                                    {
                                                        Value = notFoundTypeValue
                                                    };
                        retKnight.Add(notFoundTypeNode);

                        Debug.WriteLine("-----------notFoundTypeNode------------");
                        Debug.WriteLine(notFoundTypeNode);


                        var notFoundSquiresXElement = knightXElement.Descendants("squires").FirstOrDefault();

                        Debug.WriteLine("-----------notFoundSquiresXElement------------");
                        Debug.WriteLine(notFoundSquiresXElement);

                        var notFoundSquires =
                            from knight in notFoundSquiresXElement.Descendants("knight")
                            select knight;

                        var newNotFoundSquiresXElement = new XElement("squires");

                        foreach (var notFoundSquire in notFoundSquires)
                        {
                            Debug.WriteLine("-----------notFoundSquire------------");
                            Debug.WriteLine(notFoundSquire);

                            var notFoundParsedKnight = Parse(notFoundSquire);
                            newNotFoundSquiresXElement.Add(notFoundParsedKnight);
                            retKnight.Add(newNotFoundSquiresXElement);

                            Debug.WriteLine("-----------newNotFoundSquiresXElement------------");
                            Debug.WriteLine(newNotFoundSquiresXElement);

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

                        Debug.WriteLine("-----------spNode------------");
                        Debug.WriteLine(spNode);

                        var nameValue = wbElementKnight.Descendants("name").FirstOrDefault()?.Value;
                        XElement elName = new XElement("name")
                                          {
                                              Value = nameValue
                                          };
                        retKnight.Add(elName);

                        Debug.WriteLine("-----------elName------------");
                        Debug.WriteLine(elName);
                        
                        var typeValue = wbElementKnight.Descendants("type").FirstOrDefault()?.Value;
                        XElement typeNode = new XElement("type")
                                            {
                                                Value = typeValue
                                            };
                        retKnight.Add(typeNode);

                        Debug.WriteLine("-----------typeNode------------");
                        Debug.WriteLine(typeNode);


                        var sknValue = wbElementKnight.Descendants("society_knight_number").FirstOrDefault()?.Value;
                        XElement skn = new XElement("society_knight_number")
                                       {
                                           Value = sknValue
                                       };
                        retKnight.Add(skn);

                        Debug.WriteLine("-----------skn------------");
                        Debug.WriteLine(skn);

                        var smnValue = wbElementKnight.Descendants("society_master_number").FirstOrDefault()?.Value;
                        XElement smn = new XElement("society_master_number")
                                       {
                                           Value = smnValue
                                       };
                        retKnight.Add(smn);

                        Debug.WriteLine("-----------smn------------");
                        Debug.WriteLine(smn);

                        var dteValue = wbElementKnight.Descendants("date_elevated").FirstOrDefault()?.Value;
                        XElement dte = new XElement("date_elevated")
                                       {
                                           Value = dteValue
                                       };
                        retKnight.Add(dte);

                        Debug.WriteLine("-----------dte------------");
                        Debug.WriteLine(dte);

                        var asXElementValue = wbElementKnight.Descendants("anno_societatous").FirstOrDefault()?.Value;
                        XElement asXElement = new XElement("anno_societatous")
                                              {
                                                  Value = asXElementValue
                                              };
                        retKnight.Add(asXElement);

                        Debug.WriteLine("-----------asXElement------------");
                        Debug.WriteLine(asXElement);

                        var keXElementValue = wbElementKnight.Descendants("kingdom_of_elevation").FirstOrDefault()?.Value;
                        XElement keXElement = new XElement("kingdom_of_elevation")
                                              {
                                                  Value = keXElementValue
                                              };
                        retKnight.Add(keXElement);

                        Debug.WriteLine("-----------keXElement------------");
                        Debug.WriteLine(keXElement);

                        var kpXElementValue = wbElementKnight.Descendants("kingdom_precedence").FirstOrDefault()?.Value;
                        XElement kpXElement = new XElement("kingdom_precedence")
                                              {
                                                  Value = kpXElementValue
                                              };
                        retKnight.Add(kpXElement);

                        Debug.WriteLine("-----------kpXElement------------");
                        Debug.WriteLine(kpXElement);
                        
                        var rrXElementValue = wbElementKnight.Descendants("resigned_or_removed").FirstOrDefault()?.Value;
                        XElement rrXElement = new XElement("resigned_or_removed")
                                              {
                                                  Value = rrXElementValue
                                              };
                        retKnight.Add(rrXElement);

                        Debug.WriteLine("-----------rrXElement------------");
                        Debug.WriteLine(rrXElement);

                        var paXElementValue = wbElementKnight.Descendants("passed_away").FirstOrDefault()?.Value;
                        XElement paXElement = new XElement("passed_away")
                                              {
                                                  Value = paXElementValue
                                              };
                        retKnight.Add(paXElement);

                        Debug.WriteLine("-----------paXElement------------");
                        Debug.WriteLine(paXElement);

                        var squiresXElement = knightXElement.Descendants("squires").FirstOrDefault();

                        var squires =
                            from knight in squiresXElement.Descendants("knight")
                            select knight;

                        var newSquiresXElement = new XElement("squires");

                        if (squires.Any())
                        {
                            foreach (var squire in squires)
                            {
                                Debug.WriteLine("-----------squire------------");
                                Debug.WriteLine(squire);

                                var parsedKnight = Parse(squire);
                                newSquiresXElement.Add(parsedKnight);
                                retKnight.Add(newSquiresXElement);

                                Debug.WriteLine("-----------newSquiresXElement------------");
                                Debug.WriteLine(newSquiresXElement);

                            }
                        }
                        else
                        {
                            retKnight.Add(newSquiresXElement);
                            Debug.WriteLine("-----------newSquiresXElement------------");
                            Debug.WriteLine(newSquiresXElement);
                        }
                    }
            }
            Debug.WriteLine(" -- out: ");
            Debug.WriteLine(retKnight);
            return retKnight;
        }
    }
}