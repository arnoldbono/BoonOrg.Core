// (c) 2015, 2017 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using BoonOrg.Units;
using BoonOrg.Storage;

using BoonOrg.Geometry.Creators;

namespace BoonOrg.Geometry.Storage
{
    internal sealed class LayerGridImporter : ILayerGridImporter
    {
        // The text file contains a grid of values (integer in PAT example but assumed double).
        // Each line contains the same number of numbers with a varying number of digits and a variable number of spaces in between.

        private readonly IPointCreator m_pointCreator;
        private readonly ILayerGridCreator m_gridCreator;
        private readonly IFilePathCleanup m_filePathCleanup;

        public LayerGridImporter(IPointCreator pointCreator, ILayerGridCreator gridCreator, IFilePathCleanup filePathCleanup)
        {
            m_pointCreator = pointCreator;
            m_gridCreator = gridCreator;
            m_filePathCleanup = filePathCleanup;
        }

        /// <inheritdoc/>
        public ILayerGrid Import(IDocument document, string path, IUnit unit, string name)
        {
            path = m_filePathCleanup.Cleanup(document, path);
            if (!m_filePathCleanup.FileExists(path))
            {
                return null;
            }
            // So, path exists. Exception thrown to caller.
            using (TextReader reader = File.OpenText(path))
            {
                return Import(reader, unit, name);
            }
        }

        public ILayerGrid Import(TextReader reader, IUnit unit, string name)
        {
            double conversionFactorSI = (unit == null) ? 1.0 : unit.Conversion;

            int columnCount = 0;
            var rows = new List<IPoint[]>();

            while (true)
            {
                string line = reader.ReadLine();

                // Did we read all the lines from the file?
                if (null == line || line.Length == 0)
                {
                    break;
                }

                // NB. This above command can cause a memory exception if the file is big and there are no
                // new line characters in the file. We have a small file in the PAT example.

                string[] numbers = SplitLineIntoNumbers(line);

                // Set or test the number of columns
                if (0 == columnCount)
                {
                    columnCount = numbers.Length;
                }
                else if (numbers.Length != columnCount)
                {
                    throw new Exception(string.Format("Variable number of columns per row. Expected {0}, Found {1}", columnCount, numbers.Length));
                }

                var values = new IPoint[numbers.Length];
                for (int i = 0; i < numbers.Length; i++)
                {
                    double depth = double.Parse(numbers[i]) * conversionFactorSI; // Compute internally in SI

                    // X and Y will be set later.
                    values[i] = m_pointCreator.Create(0.0, 0.0, -depth);
                }

                rows.Add(values);
            }

            return m_gridCreator.Create(rows, name);
        }

        private static string[] SplitLineIntoNumbers(string line)
        {
            // Normally, a string.Split works nicely, but we don't want to repair later for all the zero length entries
            // due to two or more space characters separating the numbers.
            // We don't mind the two new's per line, since PAT example is such a small file.
            var numbers = new List<string>();
            var number = new StringBuilder();
            int index = 0;
            while (index < line.Length)
            {
                char ch = line[index];
                if (char.IsWhiteSpace(ch))
                {
                    if (number.Length != 0)
                    {
                        numbers.Add(number.ToString());
                        number.Clear();
                    }
                }
                else // assume anything else is part of the number
                {
                    number.Append(ch);
                }
                index++;
            }
            if (number.Length != 0)
            {
                numbers.Add(number.ToString());
                number.Clear();
            }
            return numbers.ToArray();
        }

    }
}
