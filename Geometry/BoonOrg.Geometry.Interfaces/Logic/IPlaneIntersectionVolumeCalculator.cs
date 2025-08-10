// (c) 2017 Roland Boon

namespace BoonOrg.Geometry.Logic
{
    public interface IPlaneIntersectionVolumeCalculator
    {
        /// <summary>
        /// Compute the volume of the space enclosed between the surface and an intersection plane.
        /// </summary>
        /// <param name="surface">A surface.</param>
        /// <param name="plane">The intersection plane, a Fluid Contact, say.</param>
        /// <returns>The volume in m^3</returns>
        double Compute(ITrimesh trimesh, IPlane plane);
    }
}
