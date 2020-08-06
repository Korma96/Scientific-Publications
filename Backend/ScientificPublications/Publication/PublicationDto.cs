using System.Collections.Generic;

namespace ScientificPublications.Publication
{
    public class PublicationDto
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public List<string> Authors { get; set; }
    }
}
