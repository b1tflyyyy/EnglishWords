using EnglishWords.BL.Model;
using EnglishWords.BL.Services.DataHandler;
using System;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Base controller.
    /// </summary>
    public abstract class ControllerBase
    {
        #region Data handler

        private readonly IDataHandler dataHandler = new DataHandler();

        /// <summary>
        /// Add data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="_list"></param>
        /// <param name="path"></param>
        internal void AddData<T>(T data, List<T> _list, string path) where T : class =>
             dataHandler.AddData(data, _list, path);

        /// <summary>
        /// Check the availability of data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        internal bool CheckDataAvailable<T>(string path) where T : class =>
            dataHandler.CheckDataAvailable<T>(path);

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="_list"></param>
        /// <returns></returns>
        internal T GetData<T>(int id, int count, List<T> _list) where T : class =>
            dataHandler.GetData(id, count, _list);

        /// <summary>
        /// Get the incorrect answers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_listIncorrectAnswers"></param>
        /// <returns></returns>
        internal List<T> GetIncorrectAnswers<T>(List<T> _listIncorrectAnswers) where T : class =>
            dataHandler.GetIncorrectAnswers(_listIncorrectAnswers);
        
        /// <summary>
        /// Add the correct answer.
        /// </summary>
        /// <returns>Bool.</returns>
        internal bool PlusCorrectAnswer() => dataHandler.PlusCorrectAnswer();

        /// <summary>
        /// Add the incorrect answer.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <returns>Bool.</returns>
        internal bool PlusIncorrectAnswer<T>(T errorAnswer, List<T> _errorAnswers) where T : class =>
            dataHandler.PlusIncorrectAnswer(errorAnswer, _errorAnswers);

        /// <summary>
        /// Load data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        internal List<T> LoadData<T>(string path) where T : class =>
            dataHandler.LoadData<T>(path);

        /// <summary>
        /// Compare words.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="translate">Translate.</param>
        /// <returns>Bool.</returns>
        // internal bool CompareData(SpecificInfoAboutData data, SpecificInfoAboutData inputData) =>
            // dataHandler.CompareData(data, inputData);

        /// <summary>
        /// Get more information about words.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="translate">Translate.</param>
        /// <returns></returns>
        //internal SpecificInfoAboutData GetSpecificInfoAboutData(string data) =>
            //dataHandler.GetSpecificInfoAboutData(data);

        #endregion
    }
}
