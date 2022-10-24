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
        private const string path = "Words.bin";


        /// <summary>
        /// Add a word.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <exception cref="ArgumentNullException">Null</exception>
        public void Add(Word word)
        {
            if(word == null) throw new ArgumentNullException(nameof(word));
            else
            {
                _words.Add(word);
                SaveBin(path, _words);
            }
        }


        /// <summary>
        /// Get the word by id.
        /// </summary>
        /// <param name="id">Id.</param>
        /// <returns>Word.</returns>
        public Word GetWord(int id)
        {
            if (id < count) return _words[id];
            else throw new ArgumentOutOfRangeException(nameof(id));
        }


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
                || dataWords.countTranslateLet == 0) return PlusInCorrectAnswer(word);

            else
            {
                var sameLet = 0;

                for (int i = 0; i < dataWords.countCorrectLet; i++)
                    if (dataWords.simpleUkWord[i] == dataWords.simpleTranslate[i])
                        sameLet++;

                int interests = 100 * sameLet / dataWords.countCorrectLet;

                if (interests >= 60) return PlusCorrectAnswer();
                else return PlusInCorrectAnswer(word);
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
        /// Add the correct answer.
        /// </summary>
        /// <returns>Bool.</returns>
        private bool PlusCorrectAnswer() => true;

        
        /// <summary>
        /// Add the incorrect answer.
        /// </summary>
        /// <param name="word">Word.</param>
        /// <returns>Bool.</returns>
        private bool PlusInCorrectAnswer(Word word) 
        { 
            _listErrorWords.Add(word);
            return false;
        }
        

        /// <summary>
        /// Get the incorrect words.
        /// </summary>
        /// <returns></returns>
        public List<Word> GetInCorrectWords() => _listErrorWords;


        /// <summary>
        /// Is serializable data available? 
        /// </summary>
        /// <returns>Bool.</returns>
        public bool CheckDataAvailable()
        {
            List<Word> data = LoadBin<Word>(path);
            
            if (data != null) return true;
            else return false;
        }


        /// <summary>
        /// Load words.
        /// </summary>
        /// <returns></returns>
        public int LoadWords() { _words = LoadBin<Word>(path); return count; }
    }
}
