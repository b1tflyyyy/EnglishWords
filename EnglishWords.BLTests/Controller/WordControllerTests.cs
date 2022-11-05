using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using EnglishWords.BL.Model;

namespace EnglishWords.BL.Controller.Tests
{
    /// <summary>
    /// WordController Tests
    /// </summary>
    [TestClass()]
    public class WordControllerTests
    {
        [TestMethod()]
        #region Get word Test
        public void GetWordTest()
        {
            // Arrange
            var rnd = new Random();
            var uaWord = rnd.Next().ToString();
            var enWord = rnd.Next().ToString();
            
            var word = new Word(0, enWord, uaWord);
            var wordController = new WordController();

            // Act
            wordController.AddWord(word);

            // Assert
            Assert.AreEqual(word, wordController.GetWord(0));
        }
        #endregion


        [TestMethod()]
        #region Compare words Test
        public void CompareWordsTest()
        {
            // Arrange
            var rnd = new Random();
            var uaWord = rnd.Next(0, 10000).ToString();
            var enWord = rnd.Next(0, 10000).ToString();

            var word = new Word(0, enWord, uaWord);
            var wordController = new WordController();

            // Act
            wordController.AddWord(word);

            // Assert
            var result = wordController.CompareWords(word, uaWord);
            Assert.AreEqual(true, result);
        }
        #endregion
        
        [TestMethod()]
        #region Get incorrect words Test
        public void GetInCorrectWordsTest()
        {
            // Arrange
            var rnd = new Random();
            
            var uaWord = rnd.Next(0, 10000).ToString();
            var enWord = rnd.Next(0, 10000).ToString();
            var translate = rnd.Next(10001, 1000000000).ToString();

            var word = new Word(0, enWord, uaWord);
            var wordController = new WordController();

            // Act 
            wordController.AddWord(word);
            wordController.CompareWords(word, translate);

            // Assert
            var incorrectWord = wordController.GetIncorrectWords();
            
            Assert.AreEqual(uaWord, incorrectWord[0].UaWord);
        }
        #endregion
    }
}