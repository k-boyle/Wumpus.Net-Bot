using System.Collections.Generic;
using Umbreon.Interactive.Paginator;
using Wumpus.Entities;

namespace Espeon.Interactive.Paginator
{
    public class PaginatedMessage
    {
        public IEnumerable<object> Pages { get; set; }

        public string Content { get; set; } = "";

        public EmbedAuthor Author { get; set; } = null;
        public string Title { get; set; } = "";

        public PaginatedAppearanceOptions Options { get; set; } = PaginatedAppearanceOptions.Default;
    }
}
