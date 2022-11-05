using System;
using System.Collections.Generic;

namespace EnglishWords.BL.Services.DataHandler
{
    internal interface IDataHandler
    {
        /// <summary>
        /// Add data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="_list"></param>
        /// <param name="path"></param>
        internal void AddData<T>(T data, List<T> _list, string path) where T : class;

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="_list"></param>
        /// <returns></returns>
        internal T GetData<T>(int id, int count, List<T> _list) where T : class;

        /// <summary>
        /// Check the availability of data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        internal bool CheckDataAvailable<T>(string path) where T : class;

        /// <summary>
        /// Add the correct answer.
        /// </summary>
        /// <returns>Bool.</returns>
        internal bool PlusCorrectAnswer();

        /// <summary>
        /// Add the incorrect answer.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <returns>Bool.</returns>
        internal bool PlusIncorrectAnswer<T>(T answer, List<T> _errorAnswers) where T : class;

        /// <summary>
        /// Get the incorrect answers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_listIncorrectAnswers"></param>
        /// <returns></returns>
        internal List<T> GetIncorrectAnswers<T>(List<T> _listIncorrectAnswers) where T : class;

        /// <summary>
        /// Load data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        internal List<T> LoadData<T>(string path) where T : class;
    }
}
