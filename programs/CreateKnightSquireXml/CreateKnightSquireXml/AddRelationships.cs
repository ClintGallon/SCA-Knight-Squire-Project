using System.Linq;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class AddRelationshipsJob
    {
        private readonly string _pathFilenameRelationships;
        private readonly string _pathFilenameNewRelationships;
        private readonly string _pathFilenameOutput;

        public AddRelationshipsJob(string relationshipsPathAndFilename, string newRelationshipsPathAndFilename, string outPathAndFilename)
        {    
            _pathFilenameRelationships = relationshipsPathAndFilename;
            _pathFilenameNewRelationships = newRelationshipsPathAndFilename;
            _pathFilenameOutput = outPathAndFilename;
        }

        public int DoWork()
        {
            XElement relationships = XElement.Load(_pathFilenameRelationships);
            XElement newRelationships = XElement.Load(_pathFilenameNewRelationships);

            string searchNameSquire = string.Empty;
            string searchType = string.Empty;
            
            foreach (XElement el in newRelationships.Elements())
            {
                if (el.Name != "knight") continue;
                string searchNameKnight = el.Descendants("name").FirstOrDefault()?.Value;

               foreach (XElement sqrNode in el.Descendants("squires"))
               {
                   searchNameSquire = sqrNode.Descendants("name").FirstOrDefault()?.Value;
                   searchType       = sqrNode.Descendants("type").FirstOrDefault()?.Value;
               }

               XElement foundKnight = relationships.Elements("knight").FirstOrDefault(r => (string) r.Element("name") == searchNameKnight);

               XElement squiresNode = foundKnight?.Descendants("squires").FirstOrDefault();

               if (squiresNode == null) continue;
               foreach (XElement squireNode in squiresNode.Descendants())
               {
                   bool found = false;
                   foreach (XElement squireNodeDescendant in squireNode.Descendants())
                   {
                       if (squireNodeDescendant.Name == searchNameSquire)
                       {
                           found = true;
                       }
                   }

                   if (found != false) continue;
                   XElement newSquireElement = XElement.Load("<squire><society_precedence>-1</society_precedence><name>" + searchNameSquire + "</name><type>" + searchType + "</type><squires/></squire>");
                   squiresNode.Descendants().Append(newSquireElement);
               }
            }

            return 0;
        }
    }
}