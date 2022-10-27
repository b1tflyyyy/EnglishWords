using System;

namespace EnglishWords.BL.Model
{
    /// <summary>
    /// Word.
    /// </summary>
    [Serializable] public class Word
    {
        #region Properties

        /// <summary>
        /// Id word.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Word in Ukrainian.
        /// </summary>
        public string UaWord { get; }

        /// <summary>
        /// Word in English.
        /// </summary>
        public string EnWord { get; }

        #endregion

        #pragma warning disable CS8618
        public Word() { }

        /// <summary>
        /// Set word.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enWord"></param>
        /// <param name="uaWord"></param>
        public Word(int id, string enWord, string uaWord)
        {
            #region Check input data
            
            if(id < 0) throw new ArgumentNullException(nameof(id), "id < 0");
            if (string.IsNullOrEmpty(enWord)) throw new ArgumentNullException(nameof(enWord));
            if (string.IsNullOrEmpty(uaWord)) throw new ArgumentNullException(nameof(uaWord));

            #endregion

            Id = id;
            UaWord = uaWord;
            EnWord = enWord;
        }



        /// <summary>
        /// Return string.
        /// </summary>
        /// <returns>Word.</returns>
        public override string ToString() => $"Id: {Id} \nword in english: {EnWord} \nword in ukrainian: {UaWord}.";
    }
}
