using EnglishWords.BL.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EnglishWords.BL.Controller.Tests
{
    [TestClass()]
    public class StatisticsControllerTests
    {
        [TestMethod()]
        public void GetStatisticsTest()
        {
            // Arrange
            var rnd = new Random();

            var countCorrectAnswers = rnd.Next(0, 1000000000);
            var countInCorrectAnswers = rnd.Next(0, 1000000000);
            var countAllAnswers = countCorrectAnswers + countInCorrectAnswers;

            int percentageCorrectAnswer = countCorrectAnswers * 100 / countAllAnswers;
            int percentageInCorrectAnswer = 100 - percentageCorrectAnswer;


            var statistics = new Statistics();
            var statisticsController = new StatisticsController();

            // Act
            for (var i = 0; i < countCorrectAnswers; i++)
                statistics.AddStat(true);

            for (var i = 0; i < countInCorrectAnswers; i++)
                statistics.AddStat(false);

            // Assert
            var stat = statisticsController.GetStatistics(statistics);
            
            statistics.ClearStat();

            Assert.AreEqual(stat.CountCorrectAnswer, countCorrectAnswers);
            Assert.AreEqual(stat.CountInCorrectAnswer, countInCorrectAnswers);
            Assert.AreEqual(stat.PercentageCorrectAnswer, percentageCorrectAnswer);
            Assert.AreEqual(stat.PercentageInCorrectAnswer, percentageInCorrectAnswer);
        }
    }
}