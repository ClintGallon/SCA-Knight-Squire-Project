using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class Knight 
    {
        public int SocietyPrecedence
        {
            get;
            set;
        }

        public int KnightNumber
        {
            get;
            set;
        }

        public int MasterNumber
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Type
        {
            get;
            set;
        }

        public DateTime DateElevated
        {
            get;
            set;
        }

        public int AnnoSocietatus
        {
            get;
            set;
        }

        public string Kingdom
        {
            get;
            set;
        }

        public int KingdomPrecedence
        {
            get;
            set;
        }

        public string ResignedOrRemoved
        {
            get;
            set;
        }

        public string PassedAway
        {
            get;
            set;
        }
        
        public string Notes
        {
            get;
            set;
        }

        public List<Knight> Squires { get; set; } = new List<Knight>();

        public List<Knight> ParseSquires(XElement squiresNode)
        {
            var retSquires = new List<Knight>();

            if (squiresNode.HasElements)
            {
                var descElements = squiresNode.Descendants();

                foreach (var sqXElement in descElements)
                {
                    if (sqXElement.Name == "knight")
                    {
                        var newKnight = new Knight();
                        var knightDescendants = sqXElement.Descendants();

                        foreach (var knightDescXElement in knightDescendants)
                        {
                            switch (knightDescXElement.Name.ToString())
                            {
                                case "name":
                                    newKnight.Name = knightDescXElement.Value;
                                    break;
                                case "society_precedence":
                                    newKnight.SocietyPrecedence = int.Parse(knightDescXElement.Value);
                                    break;
                                case "type":
                                    newKnight.Type = knightDescXElement.Value;
                                    break;
                                case "squires":
                                    newKnight.Squires = newKnight.ParseSquires(knightDescXElement);
                                    break;
                                default:
                                    break;
                            }
                        }
                        retSquires.Add(newKnight);
                    }
                }
            }
            return retSquires;
        }

    }
}
