using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Geocoding
{
    public class XmlSerializer
    {
        public Stream GetFileWriteStream(string path)
        {
            return new FileStream(path,
                         FileMode.Create,
                         FileAccess.Write, FileShare.None);
        }

        public XmlTextReader GetFileStreamOpen(string path)
        {
            //return new FileStream(path,
            // FileMode.Open,
            // FileAccess.Read, FileShare.None);

            return new XmlTextReader(path);
        }

        public bool SerializeObject(object objectToSerialize, string fileName)
        {
            bool ret = false;

            Stream stream = this.GetFileWriteStream(fileName);

            try
            {
                var xs = new System.Xml.Serialization.XmlSerializer(objectToSerialize.GetType());
                xs.Serialize(stream, objectToSerialize);
                ret = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                stream.Close();
            }

            return ret;
        }

        public object DeserializeObject(string fileName, Type type)
        {
            object ret = null;

            XmlTextReader stream = this.GetFileStreamOpen(fileName);

            try
            {
                var xs = new System.Xml.Serialization.XmlSerializer(type);
                ret = xs.Deserialize(stream);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                stream.Close();
            }

            return ret;
        }
    }
}
