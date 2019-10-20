﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CreateKnightSquireXml
{
    [XmlRoot("KnightList")]
    public class ArrayKnightList : IEnumerable
    {
        private List<ArrayKnight> _knights;

        public ArrayKnightList()
        {
            if (_knights is null)
            {
                _knights = new List<ArrayKnight>();
            }
        }

        public bool KnightList(ArrayKnight[] kArray)
        {
            _knights = new List<ArrayKnight>();
            foreach (ArrayKnight k in kArray)
            {
                _knights.Add(k);
            }

            return true;
        }

        public bool Add(ArrayKnight addKnight)
        {
            _knights.Add(addKnight);

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public ArrayKnightListEnum GetEnumerator()
        {
            return new ArrayKnightListEnum(_knights.ToArray());
        }
    }
    
    // When you implement IEnumerable, you must also implement IEnumerator.
    public class ArrayKnightListEnum : IEnumerator
    {
        private readonly List<ArrayKnight> _knights;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public ArrayKnightListEnum(ArrayKnight[] list)
        {
            _knights = new List<ArrayKnight>();

            foreach (var l in list)
            {
                _knights.Add(l);
            }

        }

        public bool Add(ArrayKnight addKnight)
        {
            _knights.Add(addKnight);

            return true;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _knights.ToArray().Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public ArrayKnight Current
        {
            get
            {
                try
                {
                    return _knights[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    [XmlType("ArrayKnight")] // define Type
    public class  ArrayKnight 
    {
        private ArrayKnight[] _squires;

        [XmlElement(ElementName = "society_precedence")]
        public int SocietyPrecedence
        {
            get;
            set;
        }

        [XmlElement(ElementName = "society_knight_number")]
        public int KnightNumber
        {
            get;
            set;
        }

        [XmlElement(ElementName = "society_master_number")]
        public int MasterNumber
        {
            get;
            set;
        }

        [XmlElement(ElementName = "name")]
        public string Name
        {
            get;
            set;
        }

        [XmlElement(ElementName = "type")]
        public string Type
        {
            get;
            set;
        }

        [XmlElement(ElementName = "date_elevated")]
        public DateTime DateElevated
        {
            get;
            set;
        }

        [XmlElement(ElementName = "anno_societatous")]
        public int AnnoSocietatous
        {
            get;
            set;
        }

        [XmlElement(ElementName = "kingdom_of_elevation")]
        public string Kingdom
        {
            get;
            set;
        }

        [XmlElement(ElementName = "kingdom_precedence")]
        public int KingdomPrecedence
        {
            get;
            set;
        }

        [XmlElement(ElementName = "resigned_or_removed")]
        public string ResignedOrRemoved
        {
            get;
            set;
        }

        [XmlElement(ElementName = "passed_away")]
        public string PassedAway
        {
            get;
            set;
        }
        
        [XmlElement(ElementName = "notes")]
        public string Notes
        {
            get;
            set;
        }
        
        [XmlArray(ElementName = "squires")]
        [XmlArrayItem(ElementName = "knight")]
        public ArrayKnight[] Squires
        {
            get
            {
                if (_squires != null) return _squires;
                return new ArrayKnight[] { };
            }
            set => _squires = value;
        }

        public ArrayKnight[] ParseSquires(XElement squiresNode)
        {
            var retSquires = new List<ArrayKnight>();

            if (squiresNode.HasElements)
            {
                var descElements = squiresNode.Descendants();

                foreach (var sqXElement in descElements)
                {
                    if (sqXElement.Name == "knight")
                    {
                        var newKnight = new ArrayKnight();
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
            return retSquires.ToArray();
        }

    }
}
