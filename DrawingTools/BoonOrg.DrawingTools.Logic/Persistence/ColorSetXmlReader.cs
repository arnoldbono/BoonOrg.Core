// (c) 2022, 2023 Roland Boon

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

using BoonOrg.Geometry.Creators;
using BoonOrg.DrawingTools.ColorMapping;

namespace BoonOrg.DrawingTools.Persistence
{
    public sealed class ColorSetXmlReader : IColorSetXmlReader
    {
        private readonly Func<IColorSet> m_colorSetFunc;
        private readonly Func<IColorPin> m_colorPinFunc;
        private readonly IColorSetService m_colorSetService;
        private readonly IColorCreator m_colorCreator;

        public ColorSetXmlReader(Func<IColorSet> colorSetFunc, Func<IColorPin> colorPinFunc, IColorSetService colorSetService, IColorCreator colorCreator)
        {
            m_colorSetFunc = colorSetFunc;
            m_colorPinFunc = colorPinFunc;
            m_colorSetService = colorSetService;
            m_colorCreator = colorCreator;
        }

        public IEnumerable<IColorSet> Read(Stream stream)
        {
            var colorSets = new List<IColorSet>();

            var doc = new XmlDocument();
            try
            {
                doc.Load(stream);

                var root = doc.FirstChild;
                if (root.Name != @"joaColorSetDictionary" || !root.HasChildNodes)
                {
                    return colorSets;
                }

                foreach (XmlNode node in root.ChildNodes)
                {
                    if (node.Name != @"joaColorSet" || node.Attributes == null)
                    {
                        continue;
                    }

                    var colorSet = m_colorSetFunc();
                    var colorPins = new List<IColorPin>();

                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        switch (attribute.Name)
                        {
                            case @"Classes":
                                colorSet.Fractions = int.Parse(attribute.Value);
                                break;
                            case @"Name":
                                colorSet.Name = attribute.Value;
                                break;
                        }
                    }

                    foreach (XmlNode child in node.ChildNodes)
                    {
                        if (child.Name != @"joaColorDef" || child.Attributes == null)
                        {
                            continue;
                        }

                        var fraction = 0.0;
                        byte colorRed = 0;
                        byte colorGreen = 0;
                        byte colorBlue = 0;
                        byte colorAlpha = 0;
                        var colorName = string.Empty;

                        foreach (XmlAttribute attribute in child.Attributes)
                        {
                            switch (attribute.Name)
                            {
                                case @"Fraction":
                                    fraction = double.Parse(attribute.Value);
                                    break;
                                case @"Red":
                                    colorRed = byte.Parse(attribute.Value);
                                    break;
                                case @"Blue":
                                    colorBlue = byte.Parse(attribute.Value);
                                    break;
                                case @"Green":
                                    colorGreen = byte.Parse(attribute.Value);
                                    break;
                                case @"Alpha":
                                    colorAlpha = byte.Parse(attribute.Value);
                                    break;
                            }
                        }

                        var colorPin = m_colorPinFunc();
                        colorPin.Fraction = fraction;
                        colorPin.Color = m_colorCreator.Create(colorRed, colorGreen, colorBlue, colorAlpha);

                        colorPins.Add(colorPin);
                    }

                    colorSet.Pins = colorPins.ToArray();

                    m_colorSetService.FilterEntries(colorSet);

                    colorSets.Add(colorSet);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return colorSets;
        }
    }
}
