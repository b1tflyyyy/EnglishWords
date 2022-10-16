using System;

namespace EnglishWords.BL.Model
{
    /// <summary>
    /// Word.
    /// </summary>
    public class Word
    {
        /// <summary>
        /// Id word.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Word in Ukrainian.
        /// </summary>
        public string? UaWord { get; set; }

        /// <summary>
        /// Word in English.
        /// </summary>
        public string? EnWord { get; set; }

        /// <summary>
        /// Set word.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enWord"></param>
        /// <param name="uaWord"></param>
        public Word(int id, string? enWord, string? uaWord)
        {
            // TODO: Chek input data

            Id = id;
            UaWord = uaWord;
            EnWord = enWord;
        }



        /// <summary>
        /// Return string.
        /// </summary>
        /// <returns>Word.</returns>
        public override string ToString() => $"{Id} {EnWord} {UaWord}";
    }
}
