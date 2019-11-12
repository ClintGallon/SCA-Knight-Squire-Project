using System.Linq;
using System.Xml.Linq;

namespace CreateKnightSquireXml
{
    public class AddRelationshipsJob
    {
        private readonly string _pathFilenameNewRelationships;
        private readonly string _pathFilenameOutput;
        private readonly string _pathFilenameRelationships;

        public AddRelationshipsJob(string relationshipsPathAndFilename, string newRelationshipsPathAndFilename, string outPathAndFilename)
        {
            _pathFilenameRelationships    = relationshipsPathAndFilename;
            _pathFilenameNewRelationships = newRelationshipsPathAndFilename;
            _pathFilenameOutput           = outPathAndFilename;
        }

        public int DoWork()
        {
            var relationships    = XElement.Load(_pathFilenameRelationships);
            var newRelationships = XElement.Load(_pathFilenameNewRelationships);

            string searchNameSquire = string.Empty;
            string searchType       = string.Empty;

            foreach (var el in newRelationships.Elements())
            {
                if (el.Name != "knight") continue;
                string searchNameKnight = el.Descendants("name").FirstOrDefault()?.Value;

                foreach (var sqrNode in el.Descendants("squires"))
                {
                    searchNameSquire = sqrNode.Descendants("name").FirstOrDefault()?.Value;
                    searchType       = sqrNode.Descendants("type").FirstOrDefault()?.Value;
                }

                var foundKnight = relationships.Elements("knight").FirstOrDefault(r => (string) r.Element("name") == searchNameKnight);

                var squiresNode = foundKnight?.Descendants("squires").FirstOrDefault();

                if (squiresNode == null) continue;
                foreach (var squireNode in squiresNode.Descendants())
                {
                    bool found = false;
                    foreach (var squireNodeDescendant in squireNode.Descendants())
                        if (squireNodeDescendant.Name == searchNameSquire)
                            found = true;

                    if (found) continue;
                    var newSquireElement = XElement.Load("<squire><society_precedence>-1</society_precedence><name>" + searchNameSquire + "</name><type>" + searchType + "</type><squires/></squire>");
                    squiresNode.Descendants().Append(newSquireElement);
                }
            }
            return 0;
        }
    }
}