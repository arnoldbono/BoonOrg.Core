// (c) 2023 Roland Boon

using System;
using System.Numerics;

namespace BoonOrg.Geometry.Domain
{
    internal class InstanceMatrices : IInstanceMatrices
    {
        public Vector3 Translation { get; set; }

        public Matrix4x4 Rotation { get; set; }

        public Vector3 Scale { get; set; }

        public Vector3 CenterPoint { get; set; }

        public Matrix4x4 ModelView => Matrix4x4.CreateScale(Scale, CenterPoint) * Rotation * Matrix4x4.CreateTranslation(Translation);

        public void SetTranslation(IPoint point)
        {
            Translation = new Vector3((float)point.X, (float)point.Y, (float)point.Z);
        }

        public void SetRotation(Quaternion quaternion)
        {
            Rotation = Matrix4x4.CreateFromQuaternion(quaternion);
        }

        public InstanceMatrices()
        {
            Translation = Vector3.Zero;
            Rotation = Matrix4x4.Identity;
            Scale = Vector3.One;
            CenterPoint = Vector3.Zero;
        }
    }
}
