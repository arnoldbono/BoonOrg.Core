// (c) 2023 Roland Boon

using System;
using System.Numerics;

namespace BoonOrg.Geometry
{
    public interface IInstanceMatrices
    {
        Vector3 Translation { get; set; }

        Matrix4x4 Rotation { get; set; }

        Vector3 Scale { get; set; }

        Vector3 CenterPoint { get; set; }

        Matrix4x4 ModelView { get; }

        void SetTranslation(IPoint point);

        void SetRotation(Quaternion quaternion);

    }
}
