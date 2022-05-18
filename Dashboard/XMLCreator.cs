using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Dashboard
{
    internal class XMLCreator
    {
        public static void SavaData(object obj, string filename)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType()); 
            TextWriter writer = new StreamWriter(filename);
            xmlSerializer.Serialize(writer, obj);
            writer.Close();
        }

        public static SettingData ReadData(SettingData obj, string filename)
        {
            
            XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
            FileStream read = new FileStream(filename, FileMode.Open, FileAccess.Read);
            obj = (SettingData)xmlSerializer.Deserialize(read);
            read.Close();
            return obj;
            
        }
    }
}
