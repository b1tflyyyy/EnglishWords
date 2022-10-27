using EnglishWords.BL.Model;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Bin data serializer.
    /// </summary>
    public class SerializableBin : ISerializableDataSaver
    {
        /// <summary>
        /// Load any data.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <returns>List.</returns>
        public List<T> Load<T>(string fileName) where T : class
        {
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                #pragma warning disable SYSLIB0011
                if (fs.Length > 0 && formatter.Deserialize(fs) is List<T> data)
                    return data;
                else
                #pragma warning disable CS8603 // Possible null reference return.
                    return null;
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
            var formatter = new BinaryFormatter();

            using (var fs = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                #pragma warning disable SYSLIB0011
                formatter.Serialize(fs, data);
            }
        }
    }
}
