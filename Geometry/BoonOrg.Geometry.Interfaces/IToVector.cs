// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry
{
	public interface IToVector
	{
        IVector ToVector(IIdentifiableContainer container);

        IVector ToVector();
    }
}
