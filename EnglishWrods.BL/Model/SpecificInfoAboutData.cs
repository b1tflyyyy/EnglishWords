using System;

namespace EnglishWords.BL.Model
{
    /// <summary>
    /// Specific info about data.
    /// </summary>
    public class SpecificInfoAboutData
    {
        #region Properties

        /// <summary>
        /// Data.
        /// </summary>
        internal string Data { get; }

        /// <summary>
        /// Number of letters in a word.
        /// </summary>
        internal int CountDataLet { get; }

        #endregion

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="countDataLet"></param>
        /// <exception cref="ArgumentNullException"></exception>
        internal SpecificInfoAboutData(string data, int countDataLet)
        {
            #region Check input data

            if (data is null) throw new ArgumentNullException(nameof(data));
            if (countDataLet == 0) throw new ArgumentNullException(nameof(countDataLet));

            #endregion

            Data = data;
            CountDataLet = countDataLet;
        }
    }
}
