// (c) 2017 Roland Boon

namespace BoonOrg.Geometry
{
    public interface IVector : IPoint
    {
        void Normalize();

        void Set(IVector vector);

        /// <summary>
        /// p2 = p1 + f v. v = this
        /// </summary>
        /// <param name="coordinate">p1</param>
        /// <param name="factor">f</param>
        /// <returns>p2</returns>
        IPoint Translate(IPoint coordinate, double factor);

        /// <summary>
        /// v2 = v2 + f v1. v1 = this
        /// </summary>
        /// <param name="translation">v1</param>
        /// <param name="factor">f</param>
        /// <returns>v2</returns>
        IVector Translate(IVector translation, double factor);

        IPoint ToPoint();

        new IVector Clone();
    }
}
