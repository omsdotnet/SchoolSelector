using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    public class Envelope
    {
        public string lowerCorner { get; set; }
        public string upperCorner { get; set; }
    }
}
