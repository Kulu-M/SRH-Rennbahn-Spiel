using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace SRH_Rennbahn
{
    class SaveLoad
    {
        //SPEICHERN
        public static void writeBinary<T>(string datei, T zuSpeichern)
        {
            FileStream fs = null;
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                fs = new FileStream(datei, FileMode.Create);
                bf.Serialize(fs, zuSpeichern);
                fs.Close();
            }
            catch (Exception)
            {
                if (fs != null)
                    fs.Close();
            }
        }

        //LADEN
        internal static T readBinary<T>(string datei)
        {

            T output;
            BinaryFormatter bf = new BinaryFormatter();

            try
            {
                FileStream fs = new FileStream(datei, FileMode.OpenOrCreate);
                output = (T)bf.Deserialize(fs);
                fs.Close();
            }
            catch (Exception)
            {

                output = default(T);
            }
            return output;
        }
    }
}
