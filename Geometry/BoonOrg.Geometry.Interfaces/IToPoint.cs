// (c) 2017 Roland Boon

using BoonOrg.Identification;

namespace BoonOrg.Geometry
{
	public interface IToPoint
	{
        IPoint ToPoint(IIdentifiableContainer container);

        IPoint ToPoint();
    }
}
