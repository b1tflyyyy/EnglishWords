using EnglishWords.BL.Model;
using System;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Logic of words.
    /// </summary>
    [Serializable] public class WordController : ControllerBase
    // TODO: think about IEnumerable
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
        private const string path = "Words.bin";


        /// <summary>
        /// Add a word.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <exception cref="ArgumentNullException">Null</exception>
        public void AddWord(Word word) => AddData(word, _words, path);


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
        public bool CompareWords(Word word, string translate)
        {
            var dataWords = GetInfoAboutWords(word, translate);

            if (dataWords.countCorrectLet > dataWords.countTranslateLet 
                || dataWords.countTranslateLet == 0) 
                return PlusIncorrectAnswer(word, _listErrorWords);

            else
            {
                var sameLet = 0;

                for (int i = 0; i < dataWords.countCorrectLet; i++)
                    if (dataWords.simpleUkWord[i] == dataWords.simpleTranslate[i])
                        sameLet++;

                int interests = 100 * sameLet / dataWords.countCorrectLet;

                if (interests >= 60) return PlusCorrectAnswer();
                else return PlusIncorrectAnswer(word, _listErrorWords);
            }
        }


        /// <summary>
        /// Get more information about words.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <param name="translate">Translate.</param>
        /// <returns></returns>
        private (string simpleUkWord, 
                 string simpleTranslate,
                 int countTranslateLet,
                 int countCorrectLet) 
                 GetInfoAboutWords(Word word, string translate)
        {
            var simpleUkWord = word.UaWord.Replace(" ", "").ToLower();
            var simpleTranslate = translate.Replace(" ", "").ToLower();
            var countCorrectLet = simpleUkWord.Count();
            var countTranslateLet = simpleTranslate.Count();

            return (simpleUkWord, simpleTranslate, countTranslateLet, countCorrectLet);
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
        public bool CheckDataAvailable() => CheckDataAvailable<Word>(path);


        /// <summary>
        /// Load words.
        /// </summary>
        /// <returns></returns>
        public int LoadWords() { _words = LoadData<Word>(path); return count; }
    }
}
