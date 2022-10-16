using EnglishWords.BL.Model;
using System;
using System.Xml.Serialization;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// XML data saver.
    /// </summary>
    public class SerializableDataSaver : ISerializableDataSaver
    {
        /// <summary>
        /// Load any data.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <returns>List.</returns>
        public List<T> Load<T>(string fileName) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                List<T>? data = xmlSerializer.Deserialize(fs) as List<T>;

                if (data == null) return new List<T>();
                else return data;
            }
        }


        /// <summary>
        /// Save any data.
        /// </summary>
        /// <typeparam name="T">Name</typeparam>
        /// <param name="fileName">Path.</param>
        /// <param name="data">Data.</param>
        public void Save<T>(string fileName, T data) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, data);
            }
        }
    }
}
