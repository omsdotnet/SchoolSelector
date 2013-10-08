using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Geocoding.Models
{
    
    public class BoundedBy
    {
        public BoundedBy()
        {
            this.Envelope = new Envelope();
        }

        public Envelope Envelope { get; set; }
    }
}
