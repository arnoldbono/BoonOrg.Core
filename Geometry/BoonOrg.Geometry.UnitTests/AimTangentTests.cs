// (c) 2017 Roland Boon

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BoonOrg.Registrations;
using BoonOrg.Registrations.Domain;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Logic;

namespace BoonOrg.Geometry.UnitTests
{
    [TestClass]
    public class AimTangentTests
    {
        private IResolver m_resolver;

        [TestInitialize]
        public void TestInitialize()
        {
            m_resolver = Resolver.Instance();
        }

        [TestMethod]
        public void AimZ_Test()
        {
            // Arrange
            double[][] testVectors = new double[][]
            {
                new double[] { 2.0, 1.0, 0.0 },
                new double[] { -2.0, 1.0, 0.0 },
                new double[] { 0.0, 0.0, 1.0 },
                new double[] { 1.0, 0.0, 0.0 },
                new double[] { 0.0, 1.0, 0.0 },
                new double[] { 1.0, 0.0, 1.0 },
                new double[] { 2.0, -1.0, 8.0 },
                new double[] { -1.0, 0.0, 0.0 },
                new double[] { 0.0, -1.0, 0.0 },
                new double[] { 0.0, 0.0, -1.0 }
            };

            var rotatorAndTranslator = m_resolver.Resolve<IRotatorAndTranslator>();
            var vectorLogic = m_resolver.Resolve<IVectorLogic>();
            var vectorCreator = m_resolver.Resolve<IVectorCreator>();

            foreach (double[] testVector in testVectors)
            {
                var vector1 = vectorCreator.Create(testVector[0], testVector[1], testVector[2]);

                // Act
                bool aimed = vectorLogic.AimZ(vector1, out double rotateX, out double rotateY);
                double length1 = vector1.Length;

                var vector2 = vectorCreator.Create(0.0, 0.0, length1);
                rotatorAndTranslator.RotateX(vector2, -rotateX);
                rotatorAndTranslator.RotateY(vector2, -rotateY);

                vectorLogic.Translate(vector2, vector1, -1.0);

                // Assert
                Assert.IsTrue(vector2.Length2.IsAlmostZero());
            }
        }
    }
}
