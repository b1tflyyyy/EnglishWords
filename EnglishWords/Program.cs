using EnglishWords.BL.Model;
using EnglishWords.BL.Controller;
using System;
using System.Text;
using System.Globalization;
using System.Resources;
using System.Data;

namespace View.CMD
{ 
    internal class Program
    {
        private static readonly CultureInfo culture = CultureInfo.CreateSpecificCulture("uk-ua");
        private static readonly ResourceManager? resourceManager = new ResourceManager("View.CMD.Languages.Messages", typeof(Program).Assembly);
        
        private static void Main(string[] args)
        {
            LoadConsoleConfig();

            Console.WriteLine($"{RM("Hello")} \n{RM("ChooseLearn")} \n\t{RM("Words")} \n\t{RM("IrregularVerbs")}");
            var mode = ChooseMode();
            
            var wordController = new WordController();
            var verbController = new VerbController();

            var statisticsController = new StatisticsController();
            var statistics = new Statistics();
       

            #region If mode learn words
            if (mode == "words")
            {
                var dataIsAvailable = wordController.CheckWordAvailable();
                var needWriteWords = false;
                var countWords = 0;

                // Load data or enter new words.
                if (dataIsAvailable)
                {
                    Console.WriteLine($"{RM("DoYouWantLoadWords")} \n\t{RM("LoadData")} \n\t{RM("NoLoadData")}");
                    var input = TryInt($"{RM("InputError")} \n{RM("TryAgain")}", 1);

                    if (input == 0)
                        countWords = wordController.LoadWords();
                    else
                        needWriteWords = true;

                    Console.Clear();
                }
                
                // Enter new words.
                if (!dataIsAvailable || needWriteWords)
                {
                    Console.WriteLine($"{RM("EnterAmountWords")}");
                    countWords = TryInt($"{RM("ErrorOrCountWordsOverflow")}", 500);

                    Console.Clear();

                    for (int i = 0; i < countWords; i++)
                    {
                        Console.Write($"{RM("EnterEnglishWord")} ");
                        var enWord = TryString();

                        Console.Write($"{RM("EnterTranslate")} ");
                        var uaWord = TryString();

                        var word = new Word(i, enWord, uaWord);
                        wordController.AddWord(word);

                        Console.Clear();
                    }

                    Console.WriteLine($"{RM("EndEnterWords")} \n{RM("GoLearn")}");
                    Console.WriteLine();
                }

                // Check words.
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

                    Console.WriteLine($"{RM("Greating")} \n{RM("EndLearnWords")} \n\n{RM("AmountCorrectTranslateWord")} {stat?.CountCorrectAnswer} - {stat?.PercentageCorrectAnswer}%, \n{RM("AmountIncorrectTranslateWord")} {stat?.CountIncorrectAnswer} - {stat?.PercentageIncorrectAnswer}%");
                    
                    #pragma warning disable CS8602 // stat may be null here.
                    if(stat.PercentageCorrectAnswer != 100)
                    {
                        Console.WriteLine($"\n{RM("ToPayAttentionWords")} ");
                        foreach (var word in wordController.GetIncorrectWords())
                        {
                            Console.WriteLine($"\n{RM("WordInEnglishLanguage")} {word.EnWord} \n{RM("TranslateWord")} {word.UaWord}");
                        }
                    }

                    Console.WriteLine($"\n{RM("RepeatOrExit")}");
                    var key = Console.ReadKey();

                    if (ConsoleKey.Enter == key.Key) Console.Clear();
                    else break;
                }
            }
            #endregion

            #region If mode learn verbs
            else
            {
                var dataIsAvailable = verbController.CheckVerbAvailable();
                var needWriteVerbs = false;
                var countVerbs = 0;

                if(dataIsAvailable)
                {
                    // Console.WriteLine("Чи бажаєте ви завантажити неправильні дієслова? \n\t0 - так \n\t1 - ні");
                    Console.WriteLine($"{RM("DoYouWantLoadVerbs")} \n\t{RM("LoadData")} \n\t{RM("NoLoadData")}");

                    var input = TryInt($"{RM("InputError")} \n{RM("TryAgain")}", 1);
                    
                    if (input == 0)
                        countVerbs = verbController.LoadVerbs();
                    else
                        needWriteVerbs = true;

                    Console.Clear();
                }

                if (!dataIsAvailable || needWriteVerbs)
                {
                    Console.WriteLine($"{RM("EnterAmountVerbs")}");
                    countVerbs = TryInt($"{RM("ErrorOrCountWordsOverflow")}", 500);

                    Console.Clear();

                    for (int i = 0; i < countVerbs; i++)
                    {
                        Console.WriteLine($"{RM("EnterFirstVerbForm")}");
                        var firstForm = TryString();

                        Console.WriteLine($"{RM("EnterSecondVerbForm")}");
                        var secondForm = TryString();

                        Console.WriteLine($"{RM("EnterThirdVerbForm")}");
                        var thirdForm = TryString();

                        var verb = new Verb(i, firstForm, secondForm, thirdForm);
                        verbController.AddVerb(verb);

                        Console.Clear();
                    }

                    Console.WriteLine($"{RM("EndEnterVerbs")} \n{RM("GoLearn")}");
                    Console.WriteLine();
                }

                #region For check
                // for check ----------->
                //for (int i = 0; i < countVerbs; i++)
                //{
                //    var verb = verbController.GetVerb(i);

                //    Console.WriteLine($"{verb.Id}, {verb.FirstForm}, {verb.SecondForm}, {verb.ThirdForm}");
                //}
                #endregion

                // check verbs
                while(true)
                {
                    for(int i = 0; i < countVerbs; i++)
                    {
                        var verb = verbController.GetVerb(i);

                        Console.WriteLine("enter first verb form: ");
                        var inputVerbFirstForm = TryString();

                        Console.WriteLine("enter second verb form: ");
                        var inputVerbSecondForm = TryString();

                        Console.WriteLine("enter third verb form: ");
                        var inputVerbThirdForm = TryString();

                        var result = verbController.CompareVerbs(verb,
                                                    inputVerbFirstForm,
                                                    inputVerbSecondForm,
                                                    inputVerbThirdForm);

                        statistics.AddStat(result);

                        if(result)
                        {
                            Console.WriteLine("correct");
                            Delay(1500);
                        }
                        else
                        {
                            Console.WriteLine($"IncorrectAnswer - {verb}");
                            Delay(5500);
                        }

                        Console.Clear();
                    }
                }
            }
            #endregion
        }


        // TODO: create specific class for console IO helper.
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

                if (input == "words") 
                { 
                    Console.Clear();  
                    return input; 
                }
                else if (input == "verbs") 
                { 
                    Console.Clear(); 
                    return input; 
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine($"{RM("IncorrectMode")} \n{RM("TryAgain")}");
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Try parse num.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int TryInt(string error, int max)
        {
            while (true)
            {
                var num = Console.ReadLine();

                if (int.TryParse(num, out int result) && result >= 0 && result <= max)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(error);
                    Console.WriteLine();
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

                if (string.IsNullOrEmpty(input))
                { 
                    Console.WriteLine($"{RM("ErrorNullInput")} \n{RM("TryAgain")}");
                }
                else
                {
                    return input;
                }
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