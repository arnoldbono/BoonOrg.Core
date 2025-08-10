// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BoonOrg.Geometry.Creators;
using BoonOrg.Geometry.Domain;
using BoonOrg.Geometry.Domain.Creators;
using BoonOrg.Identification.Domain;
using BoonOrg.Registrations.Domain;
using BoonOrg.Registrations;

namespace BoonOrg.Geometry.Logic.Tests
{
    [TestClass]
    public class DeformedCellToTetrahedraHelperTests
    {
        private readonly IResolver m_resolver;

        public DeformedCellToTetrahedraHelperTests()
        {
            m_resolver = Resolver.Build();
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_CubeHasKnownVolume()
        {
            TetrahedraFinder finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane();

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, 1.0),
                new Point(1.0, 0.0, 1.0),
                new Point(1.0, 1.0, 1.0),
                new Point(0.0, 1.0, 1.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            Assert.IsTrue(tetrahedra.Count == 6);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 1.0) < 1.0e-8);
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_Ramp1HasKnownVolume()
        {
            var finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane(); ;

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, 1.0),
                new Point(1.0, 0.0, 1.0),
                new Point(1.0, 1.0, 0.0),
                new Point(0.0, 1.0, 0.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            Assert.IsTrue(tetrahedra.Count == 3);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 0.5) < 1.0e-8);
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_Ramp2HasKnownVolume()
        {
            var finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane();

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, 1.0),
                new Point(1.0, 0.0, 0.0),
                new Point(1.0, 1.0, 0.0),
                new Point(0.0, 1.0, 1.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            Assert.IsTrue(tetrahedra.Count == 3);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 0.5) < 1.0e-8);
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_Wedge1HasKnownVolume()
        {
            var finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane();

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, 0.0),
                new Point(1.0, 0.0, 1.0),
                new Point(1.0, 1.0, 0.0),
                new Point(0.0, 1.0, 1.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            // Collapsed tetrahedra are not reported
            Assert.IsTrue(tetrahedra.Count == 2);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 1.0 / 3.0) < 1.0e-8);
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_Wedge2HasKnownVolume()
        {
            var finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane();

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, -0.5),
                new Point(1.0, 0.0, 1.0),
                new Point(1.0, 1.0, -0.5),
                new Point(0.0, 1.0, 1.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            Assert.IsTrue(tetrahedra.Count == 2);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 0.148148148148148) < 1.0e-8);
        }

        [TestMethod]
        public void DeformedCellToTetrahedraHelperTest_Wedge3HasKnownVolume()
        {
            var finder = GetTetrahedraFinder();
            var volumeCalculator = new TetrahedraVolumeCalculator();

            IPlane plane = NewPlane();

            var nodes = new IPoint[]
            {
                new Point(0.0, 0.0, 1.0),
                new Point(1.0, 0.0, 1.0),
                new Point(1.0, 1.0, 0.0),
                new Point(0.0, 1.0, 1.0),
            };

            List<ITetrahedron> tetrahedra =
                finder.ComputeTetrahedraInDeformedCell(nodes, plane).ToList();

            Assert.IsTrue(tetrahedra.Count == 4);

            double volume = volumeCalculator.Compute(tetrahedra);

            Assert.IsTrue(Math.Abs(volume - 0.6666666666666666) < 1.0e-8);
        }

        private TetrahedraFinder GetTetrahedraFinder()
        {
            var tetrahedronCreator = new TetrahedronCreator(m_resolver.Resolve<Func<ITetrahedron>>());
            var triangleCreator = m_resolver.Resolve<ITriangleCreator>();
            var pointCreator = m_resolver.Resolve<IPointCreator>();
            var trimeshCreator = m_resolver.Resolve<ITrimeshCreator>();
            var intersectionCalculator = new IntersectionCalculator(pointCreator, m_resolver.Resolve<IVectorLogic>());
            var transformer = new LayerGridToTrimeshTransformer(trimeshCreator, triangleCreator);

            // ROBOCOP TODO var cutoffFinder = new TrimeshPlaneCutoffFinder(intersectionCalculator, transformer, triangleCreator, trimeshCreator);
            // ROBOCOP TODO return new TetrahedraFinder(tetrahedronCreator, intersectionCalculator, cutoffFinder, triangleCreator);
            return null;
        }

        private IPlane NewPlane()
        {
            return m_resolver.Resolve<IPlane>();
        }
    }
}
