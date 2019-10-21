using System;
using System.CodeDom;
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
    public class DictKnightList : IDictionary<int, DictKnight>
    {
        private Dictionary<int, DictKnight> _knights;

        public ICollection<int> Keys => _knights.Keys;

        public ICollection<DictKnight> Values => _knights.Values;

        public int Count => _knights.Count;

        public bool IsReadOnly => false;

        public DictKnight this[int key] { get => _knights[key]; set => _knights.Add(key, value); }

        public bool KnightList(Dictionary<int, DictKnight> kArray)
        {
            _knights = new Dictionary<int, DictKnight>();
            for (int i = 0; i <= kArray.Count; i++)
            {
                var current = kArray[i];
                _knights.Add(i, current);
            }
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public DictKnightListEnum GetEnumerator()
        {
            return new DictKnightListEnum(_knights);
        }

        public bool ContainsKey(int key)
        {
            return _knights.ContainsKey(key);
        }

        public void Add(int key, DictKnight value)
        {
            _knights.Add(key, value);
        }

        public bool Remove(int key)
        {
            return _knights.Remove(key);
        }

        public bool TryGetValue(int key, out DictKnight value)
        {
            bool blnReturn = false;

            _knights.TryGetValue(key, out DictKnight outboundvalue);

            value = outboundvalue;

            blnReturn = !(outboundvalue is null);

            return blnReturn;
        }

        public void Add(KeyValuePair<int, DictKnight> item)
        {
            _knights.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _knights = new Dictionary<int, DictKnight>();
        }

        public bool Contains(KeyValuePair<int, DictKnight> item)
        {
            return _knights.Contains<KeyValuePair<int, DictKnight>>(item);
        }

        public void CopyTo(KeyValuePair<int, DictKnight>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<int, DictKnight> item)
        {
            return _knights.Remove(item.Key);
        }

        IEnumerator<KeyValuePair<int, DictKnight>> IEnumerable<KeyValuePair<int, DictKnight>>.GetEnumerator()
        {
           throw new NotImplementedException();
        }
    }
    
    // When you implement IEnumerable, you must also implement IEnumerator.
    public class DictKnightListEnum : IEnumerator
    {
        private readonly Dictionary<int, DictKnight> _knights;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public DictKnightListEnum(Dictionary<int, DictKnight> dict)
        {
            _knights = new Dictionary<int, DictKnight>();

            foreach (var l in dict)
            {
                _knights.Add(_knights.Count + 1, l.Value);
            }

        }

        public bool Add(DictKnight addKnight)
        {
            _knights.Add(_knights.Count + 1, addKnight);
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

        public Dictionary<int, DictKnight> Current
        {
            get
            {
                try
                {
                    Dictionary<int, DictKnight> retDict = new Dictionary<int, DictKnight>();
                    retDict.Add(retDict.Count + 1, _knights[position]);
                    return retDict;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }

    [XmlType("DictKnight")] // define Type
    public class DictKnight 
    {
        private Dictionary<int, DictKnight> _squires;

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
        public Dictionary<int, DictKnight> Squires
        {
            get
            {
                if (_squires != null) return _squires;
                return new Dictionary<int, DictKnight>();
            }
            set => _squires = value;
        }

        public Dictionary<int, DictKnight> ParseSquires(XElement squiresNode)
        {
            var retSquires = new Dictionary<int, DictKnight>();

            if (squiresNode.HasElements)
            {
                var descElements = squiresNode.Descendants();

                foreach (var sqXElement in descElements)
                {
                    if (sqXElement.Name == "knight")
                    {
                        var newKnight = new DictKnight();
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
                        retSquires.Add(retSquires.Count() + 1, newKnight);
                    }
                }
            }
            return retSquires;
        }

    }
}
