using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    [XmlRoot("KnightList")]
    public class ArrayKnightList : IEnumerable
    {
        private List<ArrayKnight> _knights;

        /// <summary>
        /// 
        /// </summary>
        public ArrayKnightList()
        {
            if (_knights is null) _knights = new List<ArrayKnight>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kArray"></param>
        /// <returns></returns>
        public bool KnightList(ArrayKnight[] kArray)
        {
            _knights = new List<ArrayKnight>();
            foreach (var k in kArray) _knights.Add(k);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addKnight"></param>
        /// <returns></returns>
        public bool Add(ArrayKnight addKnight)
        {
            _knights.Add(addKnight);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ArrayKnightListEnum GetEnumerator()
        {
            return new ArrayKnightListEnum(_knights.ToArray());
        }
    }

    // When you implement IEnumerable, you must also implement IEnumerator.
    /// <summary>
    /// 
    /// </summary>
    public class ArrayKnightListEnum : IEnumerator
    {
        private readonly List<ArrayKnight> _knights;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        private int _position = -1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        public ArrayKnightListEnum(IEnumerable<ArrayKnight> list)
        {
            _knights = new List<ArrayKnight>();

            foreach (var l in list) _knights.Add(l);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public ArrayKnight Current
        {
            get
            {
                try
                {
                    return _knights[_position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            _position++;
            return _position < _knights.ToArray().Length;
        }

        /// <summary>
        /// Resets the pointer position
        /// </summary>
        public void Reset()
        {
            _position = -1;
        }

        object IEnumerator.Current => Current;

        /// <summary>
        /// Adds a knight to the list
        /// </summary>
        /// <param name="addKnight"></param>
        /// <returns></returns>
        public bool Add(ArrayKnight addKnight)
        {
            _knights.Add(addKnight);
            return true;
        }
    }

    /// <summary>
    /// Knight class stored in an array
    /// </summary>
    [XmlType("ArrayKnight")] // define Type
    public class ArrayKnight
    {
        private ArrayKnight[] _squires;

        /// <summary>
        /// Precedence in the Society
        /// </summary>
        [XmlElement(ElementName = "society_precedence")]
        public int SocietyPrecedence { get; set; }

        /// <summary>
        /// Ordered Number the person who was knighted was made.
        /// </summary>
        [XmlElement(ElementName = "society_knight_number")]
        public int KnightNumber { get; set; }

        /// <summary>
        /// Ordered number of master made
        /// </summary>
        [XmlElement(ElementName = "society_master_number")]
        public int MasterNumber { get; set; }

        /// <summary>
        /// Persona Name
        /// </summary>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Possibilities: Knight, Master, Squire
        /// </summary>
        [XmlElement(ElementName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Date Elevated to the chivalry
        /// </summary>
        [XmlElement(ElementName = "date_elevated", DataType = "date")]
        public DateTime DateElevated { get; set; }

        /// <summary>
        /// SCA year
        /// </summary>
        [XmlElement(ElementName = "anno_societatous")]
        public int AnnoSocietatous { get; set; }

        /// <summary>
        /// Kingdom of elevation
        /// </summary>
        [XmlElement(ElementName = "kingdom_of_elevation")]
        public string Kingdom { get; set; }

        /// <summary>
        /// Order Number made chivalry for that kingdom
        /// </summary>
        [XmlElement(ElementName = "kingdom_precedence")]
        public int KingdomPrecedence { get; set; }

        /// <summary>
        /// Date and Explanation of removal from the chivalry
        /// </summary>
        [XmlElement(ElementName = "resigned_or_removed")]
        public string ResignedOrRemoved { get; set; }

        /// <summary>
        /// Date of the persons passing
        /// </summary>
        [XmlElement(ElementName = "passed_away")]
        public string PassedAway { get; set; }

        /// <summary>
        /// Notes
        /// </summary>
        [XmlElement(ElementName = "notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Those Knights, Masters, or squires who were trained by the person
        /// </summary>
        [XmlArray(ElementName     = "squires")]
        [XmlArrayItem(ElementName = "ArrayKnight")]
        public ArrayKnight[] Squires
        {
            get { return _squires ?? new ArrayKnight[] { }; }
            set => _squires = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="squiresNode"></param>
        /// <returns></returns>
        public ArrayKnight[] ParseSquires(XElement squiresNode)
        {
            List<ArrayKnight> retSquires = new List<ArrayKnight>();

            if (!squiresNode.HasElements) return retSquires.ToArray();
            IEnumerable<XElement> descElements = squiresNode.Descendants();

            foreach (var sqXElement in descElements)
            {
                var newKnight = new ArrayKnight();
                if (sqXElement.Name != "knight") continue;
                IEnumerable<XElement> knightDescendants = sqXElement.Descendants();

                foreach (var knightDescXElement in knightDescendants)
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
                    }

                retSquires.Add(newKnight);
            }

            return retSquires.ToArray();
        }
    }
}