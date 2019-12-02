using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    /// <summary>
    /// 
    /// </summary>
    public class TestJob
    {
        private readonly XElement _ksRelationshipsXml;
        private readonly string   _outPathAndFilename;
        private readonly string   _wbPathAndFilename;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="wbPathAndFilename"></param>
        /// <param name="relPathAndFilename"></param>
        /// <param name="outPathAndFilename"></param>
        public TestJob(string wbPathAndFilename, string relPathAndFilename, string outPathAndFilename)
        {
            _wbPathAndFilename  = wbPathAndFilename;
            _outPathAndFilename = outPathAndFilename;
            _ksRelationshipsXml = XElement.Load(relPathAndFilename, LoadOptions.PreserveWhitespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int DoWork()
        {

            SingletonArrayKnightParser.LoadWbXml(_wbPathAndFilename);

            IEnumerable<XElement> allElements =
                from el in _ksRelationshipsXml.Elements()
                select el;

            var knightList = new ArrayKnightList(); 
            
            foreach (var element in allElements)
            {
                if (element.Name != "knight") continue;
                var arrayKnight = SingletonArrayKnightParser.Parse(element);
                knightList.Add(arrayKnight);
            }
            
            //write to xmlfile
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(ArrayKnightList));  
            System.IO.FileStream file = System.IO.File.Create(_outPathAndFilename);  
            writer.Serialize(file, knightList);  
            file.Close();
            
            //write to debug window
            foreach (var knight in knightList)
            {
                Debug.WriteLine("------------------");
                Debug.WriteLine(knight.Name);
                Debug.WriteLine(knight.Squires.Length);
                Debug.WriteLine("------------------");
            }
            
            Debug.WriteLine("****************** [[[ FINISHED ]]] ******************");
            return 0;
        }
    }
}