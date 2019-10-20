using System;
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
    public class ListKnightList : IEnumerable
    {
        private List<ListKnight> _knights;

        public bool KnightList(List<ListKnight> listArray)
        {
            _knights = new List<ListKnight>();
            foreach (ListKnight k in listArray)
            {
                _knights.Add(k);
            }

            return true;
        }

        public bool Add(ListKnight addKnight)
        {
            _knights.Add(addKnight);

            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public ListKnightListEnum GetEnumerator()
        {
            return new ListKnightListEnum(_knights.ToArray());
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    public class ListKnightListEnum : IEnumerator
    {
        private readonly List<ListKnight> _knights;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public ListKnightListEnum(ListKnight[] list)
        {
            _knights = new List<ListKnight>();

            foreach (var l in list)
            {
                _knights.Add(l);
            }

        }

        public bool Add(ListKnight addKnight)
        {
            _knights.Add(addKnight);

            return true;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _knights.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current => Current;

        public ListKnight Current
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

    [XmlType("ListKnight")] // define Type
    public class ListKnight
    {

        [XmlElement(ElementName = "society_precedence")]
        public int SocietyPrecedence { get; set; }

        [XmlElement(ElementName = "society_knight_number")]
        public int KnightNumber { get; set; }

        [XmlElement(ElementName = "society_master_number")]
        public int MasterNumber { get; set; }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        [XmlElement(ElementName = "date_elevated")]
        public DateTime DateElevated { get; set; }

        [XmlElement(ElementName = "anno_societatous")]
        public int AnnoSocietatous { get; set; }

        [XmlElement(ElementName = "kingdom_of_elevation")]
        public string Kingdom { get; set; }

        [XmlElement(ElementName = "kingdom_precedence")]
        public int KingdomPrecedence { get; set; }

        [XmlElement(ElementName = "resigned_or_removed")]
        public string ResignedOrRemoved { get; set; }

        [XmlElement(ElementName = "passed_away")]
        public string PassedAway { get; set; }

        [XmlElement(ElementName = "notes")]
        public string Notes { get; set; }

        [XmlArray(ElementName = "squires")]
        [XmlArrayItem(ElementName = "knight")]
        public List<ListKnight> Squires { get; set; } = new List<ListKnight>();

        public List<ListKnight> ParseSquires(XElement squiresNode)
        {
            var retSquires = new List<ListKnight>();

            if (squiresNode.HasElements)
            {
                Debug.WriteLine(squiresNode);
                var descElements = squiresNode.Descendants();

                foreach (var sqXElement in descElements)
                {
                    if (sqXElement.Name == "knight")
                    {
                        var newKnight = new ListKnight();
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
                       //Debug.WriteLine("retSquires.Count: " + retSquires.Count.ToString());
                    }
                }
            }
            //Debug.WriteLine("retSquires :");
            //foreach (var s in retSquires)
            //{
            //    Debug.WriteLine(s.Name);
            //}
            return retSquires;
        }
    }
}