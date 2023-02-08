using EnglishWords.BL.Model;
using System;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Logic of words.
    /// </summary>
    [Serializable] public class WordController : ControllerBase
    {
        /// <summary>
        /// List of words in which there was an error.
        /// </summary>
        private List<Word> _listErrorWords = new List<Word>();

        /// <summary>
        /// List of all words.
        /// </summary>
        private List<Word> _words = new List<Word>();

        /// <summary>
        /// Amount of all words.
        /// </summary>
        private int count => _words.Count;

        /// <summary>
        /// Path for save/load data.
        /// </summary>
        private const string WORD_PATH = "Words.bin";


        /// <summary>
        /// Add a word.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <exception cref="ArgumentNullException">Null</exception>
        public void AddWord(Word word) => AddData(word, _words, WORD_PATH);


        /// <summary>
        /// Get the word by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Word.</returns>
        public Word GetWord(int id) => GetData(id, count, _words);


        /// <summary>
        /// Compare words.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="translate">Translate.</param>
        /// <returns>Bool.</returns>
        public bool CompareWords(Word word, string inputTranslate)
        {
            var wordSpecificData = GetSpecificInfoAboutData(word.UaWord);
            var inputTranslateSpecificData = GetSpecificInfoAboutData(inputTranslate);

            var result = CompareData(wordSpecificData, inputTranslateSpecificData);

            if (result)
            {
                return PlusCorrectAnswer();
            }
            else
            {
                return PlusIncorrectAnswer(word, _listErrorWords);
            }
        }


        /// <summary>
        /// Get the incorrect words.
        /// </summary>
        /// <returns></returns>
        public List<Word> GetIncorrectWords() => GetIncorrectAnswers(_listErrorWords);


        /// <summary>
        /// Is serializable data available? 
        /// </summary>
        /// <returns>Bool.</returns>
        public bool CheckWordAvailable() => CheckDataAvailable<Word>(WORD_PATH);


        /// <summary>
        /// Load words.
        /// </summary>
        /// <returns></returns>
        public int LoadWords() { _words = LoadData<Word>(WORD_PATH); return count; }

        /// <summary>
        /// Compare words.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="translate">Translate.</param>
        /// <returns>Bool.</returns>
        private bool CompareData(SpecificInfoAboutData data, SpecificInfoAboutData inputData)
        {
            if (data.CountDataLet > inputData.CountDataLet || inputData.CountDataLet == 0)
                return false;
            else
            {
                var sameLet = 0;

                for (int i = 0; i < data.CountDataLet; i++)
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
        private SpecificInfoAboutData GetSpecificInfoAboutData(string data)
        {
            var lowDataWithoutSpaces = data.Replace(" ", "").ToLower();
            var countLet = data.Count();

            var specificInfoAboutData = new SpecificInfoAboutData(lowDataWithoutSpaces, countLet);

            return specificInfoAboutData;
        }
    }
}
