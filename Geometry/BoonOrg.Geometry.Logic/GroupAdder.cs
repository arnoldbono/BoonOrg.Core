// (c) 2019 Roland Boon

using BoonOrg.Identification;
using BoonOrg.Identification.Domain;
using BoonOrg.Storage;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class GroupAdder : IGroupAdder
    {
        /// <inheritdoc/>
        public IIdentifiableContainer Execute(IDocument document, string name, string path)
        {
            var root = document.Get<IIdentifiableContainer>();
            var groupId = new Identity(name);
            var group = new IdentifiableContainer(groupId);
            var parentGroup = root.Find(path);
            if (parentGroup != null)
            {
                parentGroup.Identification.Adopt(group);
            }
            return group;
        }
    }
}
