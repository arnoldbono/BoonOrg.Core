// (c) 2017, 2018 Roland Boon

using System;
using System.Collections.Generic;

namespace BoonOrg.Geometry.Domain
{
    /// <summary>
    /// A polyline which is closed to form a polygon if IsPolygon is set.
    /// </summary>
    internal sealed class Polyline : IPolyline
    {
        private readonly List<IPoint> m_points = new();
        private readonly Func<IBoundingBox> m_boundingBoxFunc;

        public IEnumerable<IPoint> Vertices => m_points;

        public bool Closed { get; set; }

        public Polyline(Func<IBoundingBox> boundingBoxFunc)
        {
            m_boundingBoxFunc = boundingBoxFunc;
        }

        public void Set(IEnumerable<IPoint> points, bool closed)
        {
            m_points.Clear();
            m_points.AddRange(points);
            Closed = closed;
        }

        public override string ToString()
        {
            return $@"Polyline (points: {m_points.Count}, closed: {Closed})";
        }

        public void Clear()
        {
            m_points.Clear();
        }

        public void Append(IPoint point)
        {
            m_points.Add(point);
        }

        public void Insert(int index, IPoint point)
        {
            m_points.Insert(index, point);
        }

        public void Remove(IPoint point)
        {
            m_points.RemoveAll(p => p.Equals(point));
        }

        public IBoundingBox GetBoundingBox()
        {
            var boudingBox = m_boundingBoxFunc();
            boudingBox.Expand(Vertices);
            return boudingBox;
        }

        public void ExpandBoundingBox(IBoundingBox box)
        {
            box.Expand(Vertices);
        }
    }
}
