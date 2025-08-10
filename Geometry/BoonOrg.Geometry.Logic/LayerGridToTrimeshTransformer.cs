// (c) 2017 Roland Boon

using System.Linq;
using System.Collections.Generic;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Logic
{
    internal sealed class LayerGridToTrimeshTransformer : ILayerGridToTrimeshTransformer
    {
        private readonly ITrimeshCreator m_trimeshCreator;
        private readonly ITriangleCreator m_triangleCreator;

        public LayerGridToTrimeshTransformer(ITrimeshCreator trimeshCreator,
            ITriangleCreator triangleCreator)
        {
            m_trimeshCreator = trimeshCreator;
            m_triangleCreator = triangleCreator;
        }

        public ITrimesh Transform(ILayerGrid grid, string name)
        {
            //var triangles = new List<ITriangle>();

            var triangleVertexIndices = new List<int>();
            var map = new Dictionary<IPoint, int>();

            int count = 0;
            var gridNodes = grid.Nodes;
            var columns = grid.Columns;
            var rows = grid.Rows;
            for (int c = 0; c < columns; c++)
            {
                for (int r = 0; r < rows; r++)
                {
                    map.Add(gridNodes[r, c], count++);
                }
            }

            foreach (IPoint[] cell in grid.Cells)
            {
                List<IPoint> nodes = GetPeakOnFirstNode(cell, 4);

                System.Diagnostics.Debug.Assert(!nodes.Any(x => !map.ContainsKey(x)));

                IPoint node0 = nodes[0];
                IPoint node1 = nodes[1];
                IPoint node2 = nodes[2];
                IPoint node3 = nodes[3];

                if (node2.Z > node1.Z && node2.Z > node3.Z)
                {
                    triangleVertexIndices.Add(map[node1]);
                    triangleVertexIndices.Add(map[node0]);
                    triangleVertexIndices.Add(map[node3]);

                    triangleVertexIndices.Add(map[node2]);
                    triangleVertexIndices.Add(map[node1]);
                    triangleVertexIndices.Add(map[node3]);

                    // triangles.Add(m_triangleCreator.Create(node1, node0, node3));
                    // triangles.Add(m_triangleCreator.Create(node2, node1, node3));
                }
                else
                {
                    triangleVertexIndices.Add(map[node0]);
                    triangleVertexIndices.Add(map[node2]);
                    triangleVertexIndices.Add(map[node1]);

                    triangleVertexIndices.Add(map[node0]);
                    triangleVertexIndices.Add(map[node3]);
                    triangleVertexIndices.Add(map[node2]);

                    //triangles.Add(m_triangleCreator.Create(node0, node2, node1));
                    //triangles.Add(m_triangleCreator.Create(node0, node3, node2));
                }
            }

            return m_trimeshCreator.Create(triangleVertexIndices, map.Keys, name);
            // return m_trimeshCreator.Create(triangles, name);
        }

        /// <inheritdoc/>
        public List<IPoint> GetPeakOnFirstNode(IPoint[] cell, int aboveCount)
        {
            var nodes = new List<IPoint>(cell);
            if (aboveCount != 0)
            {
                double peak = nodes.Select(n => n.Z).Max();
                while (nodes[0].Z != peak)
                {
                    RotateNodes(nodes);
                }
            }

            return nodes;
        }

        public void RotateNodes(List<IPoint> nodes)
        {
            int count = (nodes == null) ? 0 : nodes.Count;
            if (count > 1)
            {
                count--;
                IPoint backup = nodes[count];
                nodes.RemoveAt(count);
                nodes.Insert(0, backup);
            }
        }
    }
}
