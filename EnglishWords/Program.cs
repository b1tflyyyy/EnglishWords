using EnglishWords.BL.Model;
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

            // приветствие
            Console.WriteLine($"{RM("Hello")} \n{RM("ChooseLearn")} \n\t{RM("Words")} \n\t{RM("IrregularVerbs")}");

            var mode = ChooseMode();

            // Кол-во глаголов
            //var countVerbs = 0;

            // контроллер 
            var wordController = new WordController();
            var statisticsController = new StatisticsController();

            var statistics = new Statistics();

            #region Если режим - учить слова

            if (mode == "words")
            {
                #region Записали все слова которые хотим учить
                
                Console.WriteLine("Введіть кількість слів, які ви хочете вивчити.");
                
                var countWords = TryInt();

                Console.Clear();

                // Добаляем n кол-во слов 
                for(int i = 0; i < countWords; i++)
                {
                    Console.Write("Введіть слово на англійській мові: ");
                    var enWord = Console.ReadLine();
                    
                    Console.Write("Введіть переклад: ");
                    var uaWord = Console.ReadLine();

                    // Заполняем модель слова
                    var word = new Word(i, uaWord, enWord);

                    wordController.Add(word);

                    Console.Clear();
                }

                #endregion

                // Закончили ввод слов
                Console.WriteLine("Вітаю! Ви завершили ввід слів :)\nПерейдемо до вивчення!");

                Console.WriteLine();

                while (true)
                {
                    for (int i = 0; i < countWords; i++)
                    {
                        var word = wordController.GetWord(i);

                        Console.Write($"Введіть переклад слова {word.EnWord}: ");

                        var translate = Console.ReadLine();

                        // Получаем результат сравнения слов
                        var result = wordController.CompareWords(word, translate);

                        statistics.WriteStat(result);

                        if (result)
                        {
                            Console.WriteLine("Вірно! Так тримати!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            Console.WriteLine($"Нажаль ви помилилися, правильна відповідь - {word.UaWord}.");
                            Thread.Sleep(5500);
                        }

                        Console.Clear();
                    }


                    var stat = statisticsController.GetStatistics(statistics);
                    statistics.ClearStat();

                    Console.WriteLine($"Вітаємо! \nВи завершили вивчення слів з такими результатами: \n\nКількість правильно перекладених слів: {stat?.CountCorrectAnswer} - {stat?.PercentageCorrectAnswer}%, \nКількість неправильно перекладених слів: {stat?.CountInCorrectAnswer} - {stat?.PercentageInCorrectAnswer}%");
                    
                    Console.WriteLine("\nЗверніть увагу на ці слова: ");
                    
                    // Выводим слова которые быди неправльно переведены
                    foreach(Word word in wordController.GetInCorrectWords())
                    {
                        Console.WriteLine($"\nСлово на англійській мові: {word.EnWord} \nПереклад: {word.UaWord}");
                    }

                    Console.WriteLine("\nЯкщо ви бажаєте знов повторити слова, введіть repeat, інакше натисніть Enter");

                    var input = Console.ReadLine();

                    if (input == "repeat") Console.Clear();
                    else break;
                }
            }

            #endregion
            
            // Если режим неправильных глаголов 
            else
            {

            }
        }

        /// <summary>
        /// Конфиг консоли.
        /// </summary>
        private static void LoadConsoleConfig()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.OutputEncoding = Encoding.Unicode;

            Console.InputEncoding = Encoding.Unicode;
        }

        /// <summary>
        /// Выбрать режим.
        /// </summary>
        /// <returns></returns>
        private static string ChooseMode()
        {
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "words") { Console.Clear();  return input; }
                
                else if (input == "verbs") { Console.Clear(); return input; }

                else Console.WriteLine("Мабуть ви ввели невірний режим! \nСпробуйте ще раз :)");
            }
        }


        /// <summary>
        /// Попробовать спарсить число.
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
                    Console.WriteLine("Мабуть ви ввели не число, або перевищили ліміт слів(500).");
                }
            }
        }

        /// <summary>
        /// Ресурс мененджер.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string? RM(string str) => resourceManager?.GetString(str, culture);
    }
}
