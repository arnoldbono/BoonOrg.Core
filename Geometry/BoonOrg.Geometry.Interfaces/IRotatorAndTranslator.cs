// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IRotatorAndTranslator
    {
        double DegreesToRadians(double angle);

        double RadiansToDegrees(double angle);

        // transform point; rotate with "angleY", "angleZ" and translate with "translation"
        void TransformYZ(IPoint point, IPoint translation, double angleY, double angleZ);

        // transform model; rotate with "angleY", "angleZ" and translate with "translation"
        void TransformYZ(ITriangleContainer model, IPoint translation, double angleY, double angleZ);

        // transform model; rotate with "angleX", "angleY" and translate with "translation"
        void TransformXY(ITriangleContainer model, IPoint translation, double angleX, double angleY);

        void TransformZX(ITriangleContainer model, IPoint translation, double angleX, double angleZ);

        void RotateX(IPoint point, double angle);

        void RotateY(IPoint point, double angle);

        void RotateZ(IPoint point, double angle);

        void RotateX(IPoint point, double cosx, double sinx);

        void RotateY(IPoint point, double cosy, double siny);

        void RotateZ(IPoint point, double cosz, double sinz);

        void Translate(IPoint point, IPoint translation);

        void InverseTranslate(IPoint point, IPoint translation);

        void RotateX(ITriangleContainer model, double cosx, double sinx);

        void RotateY(ITriangleContainer model, double cosy, double siny);

        void RotateZ(ITriangleContainer model, double cosz, double sinz);

        void Translate(ITriangleContainer model, IPoint translation);

        void InverseTranslate(ITriangleContainer model, IPoint translation);
    }
}
