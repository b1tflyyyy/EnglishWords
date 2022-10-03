using EnglishWords.BL.Model;
using System;
using System.Collections;

namespace EnglishWords.BL.Controller
{
    public class WordController : IEnumerable<Word>
    {
        /// <summary>
        /// Список всех слов.
        /// </summary>
        private List<Word> _words = new List<Word>();

        /// <summary>
        /// Кол-во всех слов.
        /// </summary>
        private int Count => _words.Count; 

        /// <summary>
        /// Добавление слова.
        /// </summary>
        /// <param name="word"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(Word word)
        {
            if(word == null) throw new ArgumentNullException(nameof(word));
            else
            {
                _words.Add(word);
            }
        }

        /// <summary>
        /// Вернуть/установить слово по id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Word this[int id]
        {
            get
            {
                if (id < Count) return _words[id];
                else return null;
            }
            set
            {
                if(id < Count) _words[id] = value;
            }
        }

        #region Интерфейс IEnumerable

        IEnumerator<Word> IEnumerable<Word>.GetEnumerator() => _words.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _words.GetEnumerator();
        
        #endregion
    }
}
