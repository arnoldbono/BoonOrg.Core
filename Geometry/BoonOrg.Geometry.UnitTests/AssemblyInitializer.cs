// (c) 2017 Roland Boon

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BoonOrg.Registrations;
using BoonOrg.Registrations.Domain;

namespace BFCS.Geometry.UnitTests
{
    [TestClass]
    public class AssemblyInitializer
    {
        private static IResolver m_resolver;

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            m_resolver = Resolver.Build();
        }

    }
}
