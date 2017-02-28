using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    [XmlRoot(Namespace = "http://maps.yandex.ru/ymaps/1.x")]
    public class ymaps
    {
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces xmlns = new XmlSerializerNamespaces();

        public ymaps()
        {
            GeoObjectCollection = new GeoObjectCollection();

            xmlns.Add("gml", "http://www.opengis.net/gml");
            xmlns.Add("ymaps", "http://maps.yandex.ru/ymaps/1.x");
            xmlns.Add("repr", "http://maps.yandex.ru/representation/1.x");
        }

        [XmlAttributeAttribute("schemaLocation")]
        public string xsiSchemaLocation = "http://maps.yandex.ru/schemas/ymaps/1.x/ymaps.xsd";

        public GeoObjectCollection GeoObjectCollection { get; set; }

        /// <summary>
        /// Добавить гео-Объект
        /// </summary>
        /// <param name="name">наименование</param>
        /// <param name="description">описание</param>
        /// <param name="pos">позиция</param>
        public void AddGeoObject(string name, string description, string pos)
        {
            GeoObject geoObj = new GeoObject();
            geoObj.name = name;
            geoObj.description = description;
            geoObj.Point.pos = pos.Replace(',','.');

            this.GeoObjectCollection.featureMembers.GeoObject.Add(geoObj);
        }
    }
}
