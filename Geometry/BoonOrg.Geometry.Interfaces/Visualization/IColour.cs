// (c) 2023 Roland Boon

namespace BoonOrg.Geometry.Visualization
{
    public interface IColour
    {
        float Red { get; set; } // [0..1]

        float Green { get; set; } // [0..1]

        float Blue { get; set; } // [0..1]

        float Alpha { get; set; } // [0..1]

        void Set(float red, float green, float blue, float alpha);

        void Set(float red, float green, float blue);

    }
}
