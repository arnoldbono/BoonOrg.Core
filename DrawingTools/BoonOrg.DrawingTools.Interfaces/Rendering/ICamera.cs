// (c) 2023 Roland Boon

using System;
using System.Numerics;

using BoonOrg.Geometry;

namespace BoonOrg.DrawingTools.Rendering
{
    public interface ICamera
    {
        bool Orthographic { get; set; }

        Vector3 Translation { get; set; }

        Vector3 Rotation { get; set; }

        Vector3 CameraPosition { get; set; } // Eye

        Vector3 CameraTarget { get; set; } // Target

        Matrix4x4 Model { get; }

        // Note, translate the scene in the reverse direction of where we want to move.
        Matrix4x4 View { get; }

        Matrix4x4 GetProjection();

        Matrix4x4 GetMVP(float verticalOffset);

        Matrix4x4 GetMVP();

        Matrix4x4 GetMV();

        Matrix4x4 GetViewportMatrix();

        void Update(IBoundingBox boundingBox);

        void Reset();

        void PanX(float pan);

        void PanY(float pan);

        void PanZ(float pan);

        void Zoom(float zoom);

        void RotateX(float angle);

        void RotateY(float angle);

        void RotateZ(float angle);
    }
}
