using EnglishWords.BL.Model;
using System;
using System.Xml.Serialization;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Base controller.
    /// </summary>
    internal abstract class ControllerBase
    {
        /// <summary>
        /// Saver manager.
        /// </summary>
        private ISerializableDataSaver saverManager = new SerializableDataSaver();

        /// <summary>
        /// Load xml-file.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <returns>List.</returns>
        internal List<T> LoadXml<T>(string fileName) where T : class => saverManager.Load<T>(fileName);

        /// <summary>
        /// Save xml-file.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <param name="data">Data.</param>
        internal void SaveXml<T>(string fileName, T data) where T : class => saverManager.Save(fileName, data);
    }
}
