// (c) 2017 Roland Boon

using System.Collections.Generic;

namespace BoonOrg.Storage.Domain
{
    internal sealed class DocumentContainer : List<IDocument>, IDocumentContainer
    {
    }
}
