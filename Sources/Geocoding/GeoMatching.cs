using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geocoding
{
    public class GeoMatching
    {
        public Coordinates GetCoordinates(string Address)
        {
            Coordinates coords = getCoordinates(Address);

            if (null != coords)
            {
                string[] addrParts = Address.Split(' ');
                if (1 < addrParts.Count())
                {
                    string prevAddr = Address.Replace(addrParts.Last(), string.Empty);
                    Coordinates prevCoords = getCoordinates(prevAddr);

                    if ((null != prevCoords) &&
                        (prevCoords.Lat == coords.Lat) &&
                        (prevCoords.Lng == coords.Lng))
                    {
                        coords = null;
                    }
                }
            }
            
            return coords;
        }
        
        
        private Coordinates getCoordinates(string Address)
        {
            Coordinates coords = null;

            Models.ymaps YMapsML = null;

            XmlSerializer Serializer = new XmlSerializer();
            string url = string.Format("http://geocode-maps.yandex.ru/1.x/?geocode={0}&key=AHdwuUsBAAAAf6v8bgIAetpaFJGfvBv552_AtpcynnLYZX0AAAAAAAAAAAD4x0xIOtq3pbMPZ6uDJdCqCYDDQQ==", Address);
            YMapsML = (Models.ymaps)Serializer.DeserializeObject(url, typeof(Models.ymaps));

            if ((null != YMapsML) &&
                (null != YMapsML.GeoObjectCollection) &&
                (null != YMapsML.GeoObjectCollection.featureMember) &&
                (0 < YMapsML.GeoObjectCollection.featureMember.Count))

            {
                coords = new Coordinates();

                var member = YMapsML.GeoObjectCollection.featureMember.First();
                string[] poses = member.GeoObject.Point.pos.Split(' ');

                coords.Lat = poses[0];
                coords.Lng = poses[1];
            }

            return coords;
        }
    }
}
