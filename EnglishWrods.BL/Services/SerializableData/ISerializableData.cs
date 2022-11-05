using System;

namespace EnglishWords.BL.Services.SerializableData
{
    /// <summary>
    /// Save/load data.
    /// </summary>
    internal interface ISerializableData
    {
        /// <summary>
        /// Save data.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="fileName">Path.</param>
        /// <param name="data">Data.</param>
        internal void Save<T>(string fileName, T data) where T : class;

        /// <summary>
        /// Load data.
        /// </summary>
        /// <typeparam name="T">Name.</typeparam>
        /// <param name="data">Data.</param>
        /// <returns>List.</returns>
        internal List<T> Load<T>(string fileName) where T : class;
    }
}
