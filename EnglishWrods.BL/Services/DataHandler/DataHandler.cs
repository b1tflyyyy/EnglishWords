using EnglishWords.BL.Model;
using EnglishWords.BL.Services.SerializableData;
using System;


namespace EnglishWords.BL.Services.DataHandler
{
    public class DataHandler : IDataHandler
    {
        private readonly ISerializableData dataBin = new SerializableBin();

        /// <summary>
        /// Add data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="_list"></param>
        /// <param name="path"></param>
        public void AddData<T>(T data, List<T> _list, string path) where T : class
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            else
            {
                _list.Add(data);
                dataBin.Save(path, _list);
            }
        }

        /// <summary>
        /// Check the availability of data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool CheckDataAvailable<T>(string path) where T : class
        {
            List<T> data = dataBin.Load<T>(path);
            return data != null;
        }

        /// <summary>
        /// Get the data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="count"></param>
        /// <param name="_list"></param>
        /// <returns></returns>
        public T GetData<T>(int id, int count, List<T> _list) where T : class
        {
            if (id < count) return _list[id];
            else throw new ArgumentOutOfRangeException(nameof(id));
        }

        /// <summary>
        /// Get the incorrect answers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_listIncorrectAnswers"></param>
        /// <returns></returns>
        public List<T> GetIncorrectAnswers<T>(List<T> _listIncorrectAnswers) where T : class => 
            _listIncorrectAnswers;

        /// <summary>
        /// Add the correct answer.
        /// </summary>
        /// <returns>Bool.</returns>
        public bool PlusCorrectAnswer() => true;

        /// <summary>
        /// Add the incorrect answer.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <returns>Bool.</returns>
        public bool PlusIncorrectAnswer<T>(T errorAnswer, List<T> _errorAnswers) where T : class
        {
            _errorAnswers.Add(errorAnswer);
            return false;
        }

        /// <summary>
        /// Load data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<T> LoadData<T>(string path) where T : class =>
            dataBin.Load<T>(path);

        /// <summary>
        /// Compare data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public bool CompareData(SpecificInfoAboutData data, SpecificInfoAboutData inputData)
        {
            if (data.CountDataLet > inputData.CountDataLet || inputData.CountDataLet == 0)
                return false;
            else
            {
                var sameLet = 0;

                for(int i = 0; i < data.CountDataLet; i++)
                    if (data.Data[i] == inputData.Data[i])
                        sameLet++;

                int percentage = 100 * sameLet / data.CountDataLet;
                
                return percentage >= 60;
            }
        }

        /// <summary>
        /// Get the specific info about words.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public SpecificInfoAboutData GetSpecificInfoAboutData(string data)
        {
            var lowDataWithoutSpaces = data.Replace(" ", "").ToLower();
            var countLet = data.Count();

            var specificInfoAboutData = new SpecificInfoAboutData(lowDataWithoutSpaces, countLet);

            return specificInfoAboutData;
        }
    }
}
