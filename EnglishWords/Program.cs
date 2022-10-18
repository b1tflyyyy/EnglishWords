﻿using EnglishWords.BL.Model;
using EnglishWords.BL.Controller;
using System;
using System.Text;
using System.Globalization;
using System.Resources;

namespace View.CMD
{ 
    internal class Program
    {
        private static CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-ua");
        private static ResourceManager? resourceManager = new ResourceManager("View.CMD.Languages.Messages", typeof(Program).Assembly);
        
        private static void Main(string[] args)
        {
            LoadConsoleConfig();

            Console.WriteLine($"{RM("Hello")} \n{RM("ChooseLearn")} \n\t{RM("Words")} \n\t{RM("IrregularVerbs")}");

            var mode = ChooseMode();

            
            var wordController = new WordController();
            var statisticsController = new StatisticsController();

            var statistics = new Statistics();

            #region If mode learn words

            if (mode == "words")
            {
                #region Write all words 
                
                Console.WriteLine($"{RM("EnterAmountWords")}");
                
                var countWords = TryInt();

                Console.Clear();

                
                for(int i = 0; i < countWords; i++)
                {
                    Console.Write($"{RM("EnterEnglishWord")} ");
                    var enWord = TryString();
                    
                    Console.Write($"{RM("EnterTranslate")} ");
                    var uaWord = TryString();

                    var word = new Word(i, uaWord, enWord);

                    wordController.Add(word);

                    Console.Clear();
                }

                #endregion

                
                Console.WriteLine($"{RM("EndEnterWord")} \n{RM("GoLearn")}");

                Console.WriteLine();

                while (true)
                {
                    for (int i = 0; i < countWords; i++)
                    {
                        var word = wordController.GetWord(i);

                        Console.Write($"{RM("EnterTranslateWordLearn")} {word.EnWord}: ");

                        var translate = TryString();

                        
                        var result = wordController.CompareWords(word, translate);

                        statistics.AddStat(result);

                        if (result)
                        {
                            Console.WriteLine($"{RM("CorrectAnswer")}");
                            Delay(1500);
                        }
                        else
                        {
                            Console.WriteLine($"{RM("IncorrectAnswer")} - {word.UaWord}.");
                            Delay(5500);
                        }

                        Console.Clear();
                    }


                    var stat = statisticsController.GetStatistics(statistics);
                    statistics.ClearStat();

                    Console.WriteLine($"{RM("Greating")} \n{RM("EndLearnWords")} \n\n{RM("AmountCorrectTranslateWord")} {stat?.CountCorrectAnswer} - {stat?.PercentageCorrectAnswer}%, \n{RM("AmountIncorrectTranslateWord")} {stat?.CountInCorrectAnswer} - {stat?.PercentageInCorrectAnswer}%");
                    
                    Console.WriteLine($"\n{RM("ToPayAttentionWords")} ");
                    
                    
                    foreach(Word word in wordController.GetInCorrectWords())
                    {
                        Console.WriteLine($"\n{RM("WordInEnglishLanguage")} {word.EnWord} \n{RM("TranslateWord")} {word.UaWord}");
                    }

                    Console.WriteLine($"\n{RM("RepeatOrExit")}");

                    var input = Console.ReadLine();

                    if (input == "repeat") Console.Clear();
                    else break;
                }
            }

            #endregion
            
            else
            {

            }
        }

        #region Console IO helper 

        /// <summary>
        /// Console config.
        /// </summary>
        private static void LoadConsoleConfig()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
        }

        /// <summary>
        /// Choose mode.
        /// </summary>
        /// <returns></returns>
        private static string ChooseMode()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "words") { Console.Clear();  return input; }
                else if (input == "verbs") { Console.Clear(); return input; }
                else Console.WriteLine($"{RM("IncorrectMode")} \n{RM("TryAgain")}");
            }
        }

        /// <summary>
        /// Try parse num.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int TryInt()
        {
            while (true)
            {
                var num = Console.ReadLine();

                if (int.TryParse(num, out int result) && result <= 500)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"{RM("ErrorOrCountWordsOverflow")}");
                }
            }
        }

        /// <summary>
        /// Try parse only complete string.
        /// </summary>
        /// <returns></returns>
        private static string TryString()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input)) Console.WriteLine($"{RM("ErrorNullInput")} \n{RM("TryAgain")}");
                else return input; 
            }
        }

        /// <summary>
        /// Delay.
        /// </summary>
        /// <param name="delay"></param>
        private static void Delay(int delay) => Thread.Sleep(delay);

        /// <summary>
        /// Resource manager.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string? RM(string str) => resourceManager?.GetString(str, culture);

        #endregion
    }
}
