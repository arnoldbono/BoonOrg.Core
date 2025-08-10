// (c) 2017, 2018, 2023 Roland Boon

using System;
using System.Linq;
using System.Collections.Generic;

using BoonOrg.Identification;

using BoonOrg.Geometry.Attributes;
using BoonOrg.Geometry.Visualization;
using BoonOrg.Storage;

namespace BoonOrg.Geometry.Domain.Attributes
{
    internal sealed class GeometryAttributeContainer : GeometryItem, IGeometryAttributeContainer
    {
        private readonly Dictionary<Guid, List<IGeometryAttribute>> m_attributes = new ();
        private readonly Func<IColor> m_colorFunc;
        private readonly IDocumentServer m_documentServer;

        public IEnumerable<Guid> ItemIds => m_attributes.Keys;

        public GeometryAttributeContainer(IIdentity identity, Func<IColor> colorFunc, IDocumentServer documentServer) : base(identity)
        {
            m_colorFunc = colorFunc;
            m_documentServer = documentServer;
        }

        public bool HasAttributeOfType(Guid itemId, Type type)
        {
            return GetAttributes(itemId).Any(x => type.IsInstanceOfType(x));
        }

        public bool HasAttributeOfType(IIdentifiable item, Type type)
        {
            return GetAttributes(item).Any(x => type.IsInstanceOfType(x));
        }

        public bool HasAttributes(IIdentifiable item)
        {
            return (item != null) && HasAttributes(item.Identification.Id);
        }

        public bool HasAttributes(Guid itemId)
        {
            return m_attributes.ContainsKey(itemId);
        }

        public bool HasAttribute<T>(Guid itemId) where T : IGeometryAttribute
        {
            return HasAttributes(itemId) && m_attributes[itemId].Any(a => a is T);
        }

        public void Clear()
        {
            m_attributes.Clear();
        }

        public IEnumerable<IGeometryAttribute> GetAttributes(IIdentifiable item)
        {
            return (item != null) ? GetAttributes(item.Identification.Id) : Enumerable.Empty<IGeometryAttribute>();
        }

        public IEnumerable<IGeometryAttribute> GetAttributes(Guid itemId)
        {
            return HasAttributes(itemId) ? m_attributes[itemId] : Enumerable.Empty<IGeometryAttribute>();
        }

        public T GetAttribute<T>(IIdentifiable item) where T : IGeometryAttribute
        {
            return (item != null) ? GetAttribute<T>(item.Identification.Id) : default(T);
        }

        public T GetAttribute<T>(Guid itemId) where T : IGeometryAttribute
        {
            return (T)GetAttributes(itemId).FirstOrDefault(a => a is T);
        }

        public bool AddAttribute<T>(IIdentifiable item, T attribute) where T : IGeometryAttribute
        {
            return (item != null) && AddAttribute(item.Identification.Id, attribute);
        }

        public bool AddAttributeOfType(IIdentifiable item, IGeometryAttribute attribute, Type type) 
        {
            return (item != null) && !HasAttributeOfType(item, type) && AddAttributeToList(item.Identification.Id, attribute);
        }

        public bool AddAttribute<T>(Guid itemId, T attribute) where T : IGeometryAttribute
        {
            return !HasAttribute<T>(itemId) && AddAttributeToList(itemId, attribute);
        }

        private bool AddAttributeToList(Guid itemId, IGeometryAttribute attribute)
        {
            if (attribute == null)
            {
                return false;
            }

            if (!m_attributes.ContainsKey(itemId))
            {
                m_attributes.Add(itemId, new List<IGeometryAttribute> { attribute });
                return true;
            }

            m_attributes[itemId].Add(attribute);
            return true;
        }

        public bool AddAttribute(Guid itemId, IGeometryAttribute attribute, Type type)
        {
            if (attribute == null || HasAttributeOfType(itemId, type))
            {
                return false;
            }

            if (!m_attributes.ContainsKey(itemId))
            {
                m_attributes.Add(itemId, new List<IGeometryAttribute> { attribute });
                return true;
            }

            m_attributes[itemId].Add(attribute);
            return true;
        }

        public void UpdateAttribute<T>(IIdentifiable item, T attribute) where T : IGeometryAttribute
        {
            if (item == null)
            {
                return;
            }

            UpdateAttribute(item.Identification.Id, attribute);
        }

        public void UpdateAttribute<T>(Guid itemId, T attribute) where T : IGeometryAttribute
        {
            if (attribute == null)
            {
                return;
            }

            RemoveAttribute<T>(itemId);
            AddAttribute(itemId, attribute);
        }

        public void RemoveAttribute<T>(IIdentifiable item) where T : IGeometryAttribute
        {
            if (item == null)
            {
                return;
            }

            RemoveAttribute<T>(item.Identification.Id);
        }

        public void RemoveAttribute<T>(Guid itemId) where T : IGeometryAttribute
        {
            if (!HasAttribute<T>(itemId))
            {
                return;
            }

            m_attributes[itemId].Remove(GetAttribute<T>(itemId));
        }

        public void RemoveAttributes(IIdentifiable item)
        {
            if (item == null)
            {
                return;
            }

            RemoveAttributes(item.Identification.Id);
        }

        public void RemoveAttributes(Guid itemId)
        {
            m_attributes.Remove(itemId);
        }

        public void AddDefaultAttributes(IIdentifiable item)
        {
            if (item == null)
            {
                return;
            }

            AddDefaultAttributes(item.Identification.Id);
        }

        private void AddDefaultAttributes(Guid itemId)
        {
            if (HasAttribute<IDrawSurfaceAttribute>(itemId))
            {
                // We've been here before
                return;
            }

            var documentAttributes = m_documentServer.Document?.Get<IGeometryAttributeContainer>();
            if (documentAttributes != null)
            {
                if (documentAttributes.HasAttribute<IDrawSurfaceAttribute>(itemId))
                {
                    var drawSurfaceAttribute = documentAttributes.GetAttribute<IDrawSurfaceAttribute>(itemId);
                    AddAttribute<IDrawSurfaceAttribute>(itemId, new DrawSurfaceAttribute()
                    {
                        ShowSurface = drawSurfaceAttribute.ShowSurface,
                        ShowProperty = drawSurfaceAttribute.ShowProperty,
                        ShowWireframe = drawSurfaceAttribute.ShowWireframe,
                        UseLighting = true,
                        FacetedTriangles = false
                    });
                }

                if (documentAttributes.HasAttribute<ISolidMaterialAttribute>(itemId))
                {
                    var solidMaterialAttribute = documentAttributes.GetAttribute<ISolidMaterialAttribute>(itemId);
                    AddAttribute<ISolidMaterialAttribute>(itemId, new SolidMaterialAttribute(m_colorFunc)
                    {
                        Front = solidMaterialAttribute.Front,
                        Back = solidMaterialAttribute.Back
                    });
                }

                if (documentAttributes.HasAttribute<IOpacityAttribute>(itemId))
                {
                    var opacityAttribute = documentAttributes.GetAttribute<IOpacityAttribute>(itemId);
                    AddAttribute<IOpacityAttribute>(itemId, new OpacityAttribute
                    {
                        Opacity = opacityAttribute.Opacity
                    });
                }
            }
            else
            {
                var solidMaterial = new SolidMaterialAttribute(m_colorFunc);
                solidMaterial.Front.Set(0xE1, 0xE1, 0xE1);
                solidMaterial.Back.Set(0x1E, 0x1E, 0x1E);

                AddAttribute<IDrawSurfaceAttribute>(itemId, new DrawSurfaceAttribute() { ShowSurface = true });
                AddAttribute<ISolidMaterialAttribute>(itemId, solidMaterial);
                AddAttribute<IOpacityAttribute>(itemId, new OpacityAttribute { Opacity = 255 });
            }
        }
    }
}
