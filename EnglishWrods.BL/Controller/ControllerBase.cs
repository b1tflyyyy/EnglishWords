using EnglishWords.BL.Model;
using System;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Base controller.
    /// </summary>
    public abstract class ControllerBase
    {
        /// <summary>
        /// Saver manager.
        /// </summary>
        private readonly ISerializableDataSaver binManager = new SerializableBin();

        /// <summary>
        /// Load xml-file.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <returns>List.</returns>
        internal List<T> LoadBin<T>(string fileName) where T : class => binManager.Load<T>(fileName);

        /// <summary>
        /// Save xml-file.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <param name="data">Data.</param>
        internal void SaveBin<T>(string fileName, T data) where T : class => binManager.Save(fileName, data);
    }
}
