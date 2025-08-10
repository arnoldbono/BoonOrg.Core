// (c) 2017 Roland Boon
//------------------------------------------------------------------
// (c) 2009 Jianzhong Zhang
// This code is under The Code Project Open License
// Please read the attached license document before using this class
//------------------------------------------------------------------
// https://www.codeproject.com/Articles/42174/High-performance-WPF-D-Chart
//------------------------------------------------------------------
// Axes is a reworked version of Chart3D.
// It can no longer contain Mesh3D instances.
//------------------------------------------------------------------

using BoonOrg.Identification;
using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class Axes : CompoundShape<ITriangleContainer>, IAxes
    {
        private const double AxisLengthWidthRatio = 200; // axis length / width ratio

        private readonly ICylinderCreator m_cylinderCreator;
        private readonly IBarCreator m_barCreator;
        private readonly IConeCreator m_coneCreator;
        private readonly IPointCreator m_pointCreator;
        private readonly IVectorCreator m_vectorCreator;
        private readonly IRotatorAndTranslator m_rotatorAndTranslator;

        public IVector Lengths { get; private set; }
        public IPoint Center { get; private set; }

        public Axes(IIdentity identity,
            ICylinderCreator cylinderCreator,
            IBarCreator barCreator,
            IConeCreator coneCreator,
            IPointCreator pointCreator,
            IVectorCreator vectorCreator,
            IRotatorAndTranslator rotatorAndTranslator) : base(identity)
        {
            m_cylinderCreator = cylinderCreator;
            m_barCreator = barCreator;
            m_coneCreator = coneCreator;
            m_pointCreator = pointCreator;
            m_vectorCreator = vectorCreator;
            m_rotatorAndTranslator = rotatorAndTranslator;
        }

        public void SetAxes(IPoint center, IVector lengths)
        {
            Center = center;
            Lengths = lengths;
            AddAxes();
        }

        public void SetAxes(IBoundingBox box)
        {
            SetAxes(0.05, box);
        }

        public void SetAxes(double margin, IBoundingBox box)
        {
            double centerX = box.MinX - margin * box.ScaleX;
            double centerY = box.MinY - margin * box.ScaleY;
            double centerZ = box.MinZ - margin * box.ScaleZ;
            IPoint center = m_pointCreator.Create(centerX, centerY, centerZ);

            double lengthX = (1.0 + 2.0 * margin) * box.ScaleX;
            double lengthY = (1.0 + 2.0 * margin) * box.ScaleY;
            double lengthZ = (1.0 + 2.0 * margin) * box.ScaleZ;
            IVector lengths = m_vectorCreator.Create(lengthX, lengthY, lengthZ);

            SetAxes(center, lengths);
        }

        private void AddAxes()
        {
            if (Center == null || Lengths == null)
            {
                return;
            }

            double radius = (Lengths.X + Lengths.Y + Lengths.Z) / (3 * AxisLengthWidthRatio);

            ICylinder cylinderX = m_cylinderCreator.Create(radius, radius, Lengths.X, 6);
            ITriangleContainer container = cylinderX.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X + Lengths.X / 2, Center.Y, Center.Z), 0, 90);
            m_containers.Add(container);

            ICone coneX = m_coneCreator.Create(2 * radius, 2 * radius, radius * 5, 6);
            container = coneX.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X + Lengths.X, Center.Y, Center.Z), 0, 90);
            m_containers.Add(container);

            ICylinder cylinderY = m_cylinderCreator.Create(radius, radius, Lengths.Y, 6);
            container = cylinderY.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X, Center.Y + Lengths.Y / 2, Center.Z), 90, 90);
            m_containers.Add(container);

            ICone coneY = m_coneCreator.Create(2 * radius, 2 * radius, radius * 5, 6);
            container = coneY.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X, Center.Y + Lengths.Y, Center.Z), 90, 90);
            m_containers.Add(container);

            ICylinder cylinderZ = m_cylinderCreator.Create(radius, radius, Lengths.Z, 6);
            container = cylinderZ.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X, Center.Y, Center.Z + Lengths.Z / 2), 0, 0);
            m_containers.Add(container);

            ICone coneZ = m_coneCreator.Create(2 * radius, 2 * radius, radius * 5, 6);
            container = coneZ.Create();
            m_rotatorAndTranslator.TransformYZ(container,
                m_pointCreator.Create(Center.X, Center.Y, Center.Z + Lengths.Z), 0, 0);
            m_containers.Add(container);
        }
    }
}
