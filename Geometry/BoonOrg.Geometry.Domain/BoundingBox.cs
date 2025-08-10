// (c) 2017 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Domain
{
    internal sealed class BoundingBox : IBoundingBox
    {
        public double MinX { get; set; }
        public double MaxX { get; set; }
        public double MinY { get; set; }
        public double MaxY { get; set; }
        public double MinZ { get; set; }
        public double MaxZ { get; set; }

        public double ScaleX => MaxX - MinX;
        public double ScaleY => MaxY - MinY;
        public double ScaleZ => MaxZ - MinZ;

        public BoundingBox()
        {
            MinX = double.MaxValue;
            MaxX = double.MinValue;
            MinY = double.MaxValue;
            MaxY = double.MinValue;
            MinZ = double.MaxValue;
            MaxZ = double.MinValue;
        }

        public void Copy(IBoundingBox box)
        {
            MinX = box.MinX;
            MinY = box.MinY;
            MinZ = box.MinZ;
            MaxX = box.MaxX;
            MaxY = box.MaxY;
            MaxZ = box.MaxZ;
        }

        public double Scale
        {
            get
            {
                return Math.Max(Math.Max(ScaleX, ScaleY), ScaleZ);
            }
        }

        public IPoint Center
        {
            get
            {
                return new Point(MinX + ScaleX / 2.0, MinY + ScaleY / 2.0, MinZ + ScaleZ / 2.0);
            }
        }

        public void Expand(IBoundingBox box)
        {
            if (MinX > box.MinX)
            {
                MinX = box.MinX;
            }
            if (MinY > box.MinY)
            {
                MinY = box.MinY;
            }
            if (MinZ > box.MinZ)
            {
                MinZ = box.MinZ;
            }
            if (MaxX < box.MaxX)
            {
                MaxX = box.MaxX;
            }
            if (MaxY < box.MaxY)
            {
                MaxY = box.MaxY;
            }
            if (MaxZ < box.MaxZ)
            {
                MaxZ = box.MaxZ;
            }
        }

        public void Expand(IPoint p)
        {
            if (MinX > p.X)
            {
                MinX = p.X;
            }
            if (MinY > p.Y)
            {
                MinY = p.Y;
            }
            if (MinZ > p.Z)
            {
                MinZ = p.Z;
            }
            if (MaxX < p.X)
            {
                MaxX = p.X;
            }
            if (MaxY < p.Y)
            {
                MaxY = p.Y;
            }
            if (MaxZ < p.Z)
            {
                MaxZ = p.Z;
            }
        }

        public void Expand(IEnumerable<IPoint> vertices)
        {
            foreach (IPoint c in vertices)
            {
                Expand(c);
            }
        }

        public double GetVolume()
        {
            double height = ScaleZ;
            double width = ScaleY;
            double length = ScaleX;
            return height * width * length;
        }

        public IArea GetArea()
        {
            return new Area(MinX, MaxX, MinY, MaxY);
        }
    }
}
