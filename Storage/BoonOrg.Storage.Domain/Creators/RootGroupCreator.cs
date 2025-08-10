// (c) 2019 Roland Boon

using System;

using BoonOrg.Identification;

namespace BoonOrg.Storage.Domain.Creators
{
    internal sealed class RootGroupCreator : IInitialDocumentContentCreator
    {
        private readonly Guid Root = Guid.Parse(@"32b23a24-60a8-4398-8b0c-bed6480bb900");
        private readonly Func<IIdentifiableContainer> m_containerCreator;

        public RootGroupCreator(Func<IIdentifiableContainer> containerCreator)
        {
            m_containerCreator = containerCreator;
        }

        public IIdentifiable Create()
        {
            var group = m_containerCreator();
            group.Identification.Rename(@"Root");
            group.Identification.ResetId(Root);
            return group;
        }
    }
}
