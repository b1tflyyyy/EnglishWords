using EnglishWords.BL.Model;
using System;

namespace EnglishWords.BL.Controller
{
    /// <summary>
    /// Logic of statistics.
    /// </summary>
    public class StatisticsController
    {
        /// <summary>
        /// Get word statistics.
        /// </summary>
        /// <param name="statistics"></param>
        /// <returns></returns>
        public Statistics GetStatistics(Statistics statistics)
        {
            int perecentageCorrectAnswer = statistics.CountCorrectAnswer * 100 / statistics.CountAllAnswers;
            int perecentageInCorrectAnswer = 100 - perecentageCorrectAnswer;

            var readyStatistics = new Statistics(statistics.CountCorrectAnswer,
                                                 statistics.CountInCorrectAnswer,
                                                 perecentageCorrectAnswer,
                                                 perecentageInCorrectAnswer);

            return readyStatistics;
        }
    }
}
