// (c) 2023 Roland Boon

using System;
using System.Numerics;

using BoonOrg.Geometry;

namespace BoonOrg.DrawingTools.Rendering
{
    public sealed class Camera : ICamera
    {
        private readonly IViewport m_viewport;

        private float m_scale;
        private float m_fov;
        private float m_length;

        public bool Orthographic { get; set; }

        public Vector3 Translation { get; set; }

        public Vector3 Rotation { get; set; }

        public Vector3 CameraPosition { get; set; } // Eye

        public Vector3 CameraTarget { get; set; } // Target

        public Matrix4x4 Model
        {
            get
            {
                return Matrix4x4.CreateTranslation(-CameraTarget) * Matrix4x4.CreateScale(m_scale / m_length) * Matrix4x4.CreateTranslation(Translation);
            }
        }

        // Note, translate the scene in the reverse direction of where we want to move.
        public Matrix4x4 View
        {
            get
            {
                return Matrix4x4.CreateRotationZ(DegreesToRadians(Rotation.Z)) *
                    Matrix4x4.CreateRotationX(DegreesToRadians(Rotation.X)) *
                    Matrix4x4.CreateRotationY(DegreesToRadians(Rotation.Y)) *
                    Matrix4x4.CreateTranslation(-CameraPosition);
            }
        }

        public Camera(IViewport viewport)
        {
            m_viewport = viewport;

            Reset();
        }

        public Matrix4x4 GetProjection()
        {
            var ratio = m_viewport.Width / m_viewport.Height;
            if (Orthographic)
            {
                return Matrix4x4.CreateOrthographicOffCenter(-ratio, ratio, -1.0f, 1.0f, 0.01f, 1000.0f);
            }

            var fov = DegreesToRadians(m_fov);
            return Matrix4x4.CreatePerspectiveFieldOfView(fov, ratio, 0.01f, 10.0f);
        }

        public Matrix4x4 GetMVP(float verticalOffset)
        {
            var model = Model * Matrix4x4.CreateTranslation(0.0f, 0.0f, verticalOffset / m_length);
            return Matrix4x4.Multiply(model, Matrix4x4.Multiply(View, GetProjection()));
        }

        public Matrix4x4 GetMVP()
        {
            return Matrix4x4.Multiply(Model, Matrix4x4.Multiply(View, GetProjection()));
        }

        public Matrix4x4 GetMV()
        {
            return Matrix4x4.Multiply(Model, View);
        }

        public Matrix4x4 GetViewportMatrix()
        {
            var width = m_viewport.Width;
            var height = m_viewport.Height;

            return new Matrix4x4(width / 2.0f, 0.0f, 0.0f, 0.0f,
                0.0f, height / 2.0f, 0.0f, 0.0f,
                0.0f, 0.0f, 1.0f, 0.0f,
                0.0f, 0.0f, 0.0f, 1.0f);
        }

        public void Update(IBoundingBox boundingBox)
        {
            if (boundingBox == null)
            {
                return;
            }

            m_length = (float)boundingBox.Scale;

            CameraPosition = new Vector3(0.0f, 0.0f, 2.0f); // Vertices are scaled using Length to fit in front of camera.

            var center = boundingBox.Center;
            CameraTarget = new Vector3((float)center.X, (float)center.Y, (float)center.Z);
        }

        public void Reset()
        {
            Rotation = Vector3.Zero;
            m_scale = 1.0f;
            Translation = Vector3.Zero;
            CameraPosition = new Vector3(0.0f, 0.0f, 2.0f);
            CameraTarget = new Vector3(0.0f, 0.0f, 0.0f);
            m_fov = 45.0f;
            m_length = 1000.0f;
        }

        public void PanX(float pan) => Translation = new Vector3(Translation.X + pan, Translation.Y, Translation.Z);

        public void PanY(float pan) => Translation = new Vector3(Translation.X, Translation.Y + pan, Translation.Z);

        public void PanZ(float pan) => Translation = new Vector3(Translation.X, Translation.Y, Translation.Z + pan);

        public void Zoom(float zoom) => m_scale *= zoom;

        public void RotateX(float angle) =>
            Rotation = new Vector3(CorrectRotationAngle(Rotation.X + angle), Rotation.Y, Rotation.Z);

        public void RotateY(float angle) =>
            Rotation = new Vector3(Rotation.X, CorrectRotationAngle(Rotation.Y + angle), Rotation.Z);

        public void RotateZ(float angle) =>
            Rotation = new Vector3(Rotation.X, Rotation.Y, CorrectRotationAngle(Rotation.Z + angle));

        private static float DegreesToRadians(float degrees) => (float)((double)degrees * Math.PI / 180.0);

        private static float CorrectRotationAngle(float angle)
        {
            while (angle < 0.0f)
            {
                angle += 360.0f;
            }

            while (angle >= 360.0f)
            {
                angle -= 360.0f;
            }

            return angle;
        }
    }
}
