using System;
using System.Net.Http.Headers;

namespace EnglishWords.BL.Model
{
    /// <summary>
    /// Statistics.
    /// </summary>
    public class Statistics
    {
        #region Properties

        /// <summary>
        /// Amount the correct answers.
        /// </summary>
        public int CountCorrectAnswer { get; private set; } = 0;

        /// <summary>
        /// Amount the incorrect answers.
        /// </summary>
        public int CountIncorrectAnswer { get; private set; } = 0;

        /// <summary>
        /// Percentage of correct answers.
        /// </summary>
        public int PercentageCorrectAnswer { get; private set; } = 0;

        /// <summary>
        /// Percentage of incorrect answers.
        /// </summary>
        public int PercentageIncorrectAnswer { get; private set; } = 0;

        /// <summary>
        /// Amount all answers.
        /// </summary>
        internal int CountAllAnswers { get; private set; } = 0;

        #endregion

        /// <summary>
        /// Blank constructor
        /// </summary>
        public Statistics() { }

        /// <summary>
        /// Set statistics.
        /// </summary>
        /// <param name="countCorrectAnswer"></param>
        /// <param name="countInCorrectAnswer"></param>
        /// <param name="persentageCorrectAnswer"></param>
        /// <param name="persentageInCorrectAnswer"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Statistics(int countCorrectAnswer, 
                          int countInCorrectAnswer, 
                          int persentageCorrectAnswer,
                          int persentageInCorrectAnswer)
        {
            #region Check data
            
            if (countCorrectAnswer < 0) throw new ArgumentNullException(nameof(countCorrectAnswer));
            if (countInCorrectAnswer < 0) throw new ArgumentNullException(nameof(countInCorrectAnswer));
            if (persentageCorrectAnswer < 0) throw new ArgumentNullException(nameof(persentageCorrectAnswer));
            if (persentageInCorrectAnswer < 0) throw new ArgumentNullException(nameof(persentageInCorrectAnswer));
            
            #endregion

            CountCorrectAnswer = countCorrectAnswer;
            CountIncorrectAnswer = countInCorrectAnswer;
            PercentageCorrectAnswer = persentageCorrectAnswer;
            PercentageIncorrectAnswer = persentageInCorrectAnswer;
        }

        /// <summary>
        /// Record data for statistics.
        /// </summary>
        /// <param name="result"></param>
        public void AddStat(bool result)
        {
            if (result) CountCorrectAnswer++;
            else CountIncorrectAnswer++;
            
            CountAllAnswers = CountCorrectAnswer + CountIncorrectAnswer;
        }

        /// <summary>
        /// Clear statistics.
        /// </summary>
        public void ClearStat()
        {
            CountCorrectAnswer = 0;
            CountIncorrectAnswer = 0;
            CountAllAnswers = 0;
        }
    }
}
