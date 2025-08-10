// (c) 2017, 2023 Roland Boon

namespace BoonOrg.Geometry.Logic
{
    public interface IVectorLogic
    {
        /// <summary>
        /// p2 = p1 + f1 v1 + f2 v2.
        /// </summary>
        /// <param name="point">p1</param>
        /// <param name="scale1">f1</param>
        /// <param name="scale2">f2</param>
        /// <param name="vector1">v1</param>
        /// <param name="vector2">v2</param>
        /// <returns>p2</returns>
        IPoint Span(IPoint point, double scale1, double scale2, IVector vector1, IVector vector2);

        IPoint Span(double scale1, double scale2, IPoint point1, IPoint point2);

        /// <summary>
        /// Translate the <paramref name="point"/> with the vector<paramref name="translation"/> times <paramref name="factor"/>.
        /// </summary>
        /// <param name="point">The vector to translate.</param>
        /// <param name="translation">The translate vector.</param>
        /// <param name="factor">The translation weight.</param>
        void Translate(IPoint point, IVector translation, double factor);

        /// <summary>
        /// Translate the <paramref name="vector"/> with the vector<paramref name="translation"/> times <paramref name="factor"/>.
        /// </summary>
        /// <param name="vector">The vector to translate.</param>
        /// <param name="translation">The translate vector.</param>
        /// <param name="factor">The translation weight.</param>
        void Translate(IVector vector, IVector translation, double factor);

        /// <summary>
        /// Get <paramref name="v1"/> x <paramref name="v2"/>
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>The cross product</returns>
        IVector GetCrossProduct(IVector v1, IVector v2);

        /// <summary>
        /// Get <paramref name="v1"/> . <paramref name="v2"/>
        /// </summary>
        /// <param name="v1">Vector 1</param>
        /// <param name="v2">Vector 2</param>
        /// <returns>The inner product</returns>
        double GetInnerProduct(IVector v1, IVector v2);

        IVector Weighed(IVector vectorU, IVector vectorV, double factorU, double factorV);

        double ComputeLength(IVector vector1, IVector vector2);

        double ComputeLength2(IVector vector1, IVector vector2);

        double ComputeLength(IPoint point1, IPoint point2);

        double ComputeLength2(IPoint point1, IPoint point2);
            
        void GenerateOrthogonalBasis(IVector vector, out IVector U, out IVector V);

        // Change the orientation of normal (0,0,1) to aline with the given tangent vector.
        // The initial rotation is about the y axis.
        // The second rotation is about the x axis.
        // See http://www.dotcsw.com/doc/shadows.html (http://www.dotcsw.com/doc/placecam.c)
        bool AimZ(IVector tangent, out double rotateX, out double rotateY);

        /// <summary>
        /// Subtract <paramref name="vector2"/> from <paramref name="vector1"/> and store in <paramref name="vectorOut"/>.
        /// </summary>
        void Subtract(IVector vectorOut, IVector vector1, IVector vector2);

        /// <summary>
        /// Subtract <paramref name="vector2"/> from <paramref name="vector1"/> and return the result.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The substraction.</returns>
        IVector Subtract(IVector vector1, IVector vector2);

        /// <summary>
        /// Returns <paramref name="pointA"/> - <paramref name="pointB"/>
        /// </summary>
        IVector Subtract(IPoint pointA, IPoint pointB);

        /// <summary>
        /// Returns <paramref name="pointA"/> + <paramref name="pointB"/>
        /// </summary>
        IPoint Addition(IPoint pointA, IPoint pointB);

        IVector FindUpVector(IVector normal);

        void ComputeSpanVectors(IVector normal, IVector up, out IVector span1, out IVector span2);

        /// <summary>
        /// Project a normal onto a plane with plane normal.
        /// </summary>
        /// <param name="planeNormal">The (normalized) plane normal.</param>
        /// <param name="normal">The (normalized) normal to project.</param>
        /// <returns>The projected normal.</returns>
        IVector ProjectNormalToPlane(IVector planeNormal, IVector normal);

        bool AlmostEquals(IPoint a, IPoint b);
    }
}
