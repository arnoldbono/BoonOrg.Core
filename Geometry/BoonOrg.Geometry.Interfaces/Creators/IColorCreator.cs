// (c) 2018 Roland Boon

using BoonOrg.Registrations;
using BoonOrg.Geometry.Visualization;

namespace BoonOrg.Geometry.Creators
{
    public interface IColorCreator : ICreator<IColor>
    {
        IColor Create(byte red, byte green, byte blue);

        IColor Create(double red, double green, double blue);

        IColor Create(byte red, byte green, byte blue, byte alpha);

        IColor Create(double red, double green, double blue, double alpha);

        IColor Create(byte brightness);

        IColor Create(double brightness);
    }
}
