using System;

namespace EnglishWords.BL.Model
{
    public class Word
    {
        /// <summary>
        /// Id слова.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Слово на укр языке.
        /// </summary>
        public string UkWord { get; set; }

        /// <summary>
        /// Слово на англ языке.
        /// </summary>
        public string EnWord { get; set; }

        /// <summary>
        /// Возвращаем строку.
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Id} {EnWord} {UkWord}";
    }
}
