using EnglishWords.BL.Model;
using EnglishWords.BL.Controller;
using System;
using System.Text;
using System.Threading;

namespace View.CMD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // загружаем конфиг консоли
            LoadConsoleConfig();

            // приветствие
            Console.WriteLine("Вітаємо в програмі EnglishWords! \nСпочатку оберіть режим вивчення: \n\twords - режим вивчення слів \n\tverbs - вивчення неправильних дієслів");

            // Режим обучения
            var mode = "";

            // Кол-во слов
            var countWords = 0;

            // Кол-во глаголов
            var countVerbs = 0;

            // контроллер 
            var wordController = new WordController();

            #region Выбираем режим
            
            while (true)
            {
                var input = Console.ReadLine();

                if (input == "words")
                {
                    mode = input;
                    Console.Clear();
                    break;
                }
                else if (input == "verbs")
                {
                    mode = input;
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Мабуть ви ввели невірний режим! \nСпробуйте ще раз :)");
                }
            }

            #endregion


            #region Если режим - учить слова

            if (mode == "words")
            {
                #region Записали все слова которые хотим учить
                
                Console.WriteLine("Введіть кількість слів, які ви хочете вивчити.");
                
                countWords = TryInt();

                Console.Clear();

                // Добаляем n кол-во слов 
                for(int i = 0; i < countWords; i++)
                {
                    Console.Write("Введіть слово на англійській мові: ");
                    var enWord = Console.ReadLine();
                    
                    Console.Write("Введіть переклад: ");
                    var uaWord = Console.ReadLine();

                    // Заполняем модель слова
                    var word = new Word() { Id = i, EnWord = enWord, UkWord = uaWord };

                    wordController.Add(word);

                    Console.Clear();
                }

                #endregion

                // Закончили ввод слов
                Console.WriteLine("Вітаю! Ви завершили ввід слів :)\nПерейдемо до вивчення!");

                Console.WriteLine();

                while (true)
                {
                    var listErrorWords = new List<Word>();

                    // кол-во правильных ответов
                    var countCorrectAnswer = 0;

                    // кол-во неправильных ответов
                    var countInCorrectAnswer = 0;

                    // процент правльных ответов
                    var interestCorrectAnswer = 0;

                    // процент неправильных ответов
                    var interestInCorrectAnswer = 0;

                    // кол-во всех ответов
                    var allAnswers = 0;

                    for (int i = 0; i < countWords; i++)
                    {
                        var word = wordController[i];

                        Console.Write($"Введіть переклад слова {word.EnWord}: ");

                        var translate = Console.ReadLine();

                        if (translate == word.UkWord)
                        {
                            // считаем сколько было правильных ответов
                            countCorrectAnswer++;
                            Console.WriteLine("Вірно! Так тримати!");
                            Thread.Sleep(1500);
                        }
                        else
                        {
                            // считаем сколько было неправльных ответов
                            countInCorrectAnswer++;
                            listErrorWords.Add(word);
                            Console.WriteLine($"Нажаль ви помилилися, правильна відповідь - {word.UkWord}.");
                            Thread.Sleep(5500);
                        }

                        Console.Clear();
                    }
                    
                    // Кол-во всех ответов
                    allAnswers = countCorrectAnswer + countInCorrectAnswer;

                    // процент правильных ответов
                    interestCorrectAnswer = countCorrectAnswer * 100 / allAnswers;

                    // процент неверных ответов
                    interestInCorrectAnswer = 100 - interestCorrectAnswer;

                    Console.WriteLine($"Вітаємо! \nВи завершили вивчення слів з такими результатами: \n\nКількість правильно перекладених слів: {countCorrectAnswer} - {interestCorrectAnswer}%, \nКількість неправильно перекладених слів: {countInCorrectAnswer} - {interestInCorrectAnswer}%");
                    
                    Console.WriteLine("\nЗверніть увагу на ці слова: ");
                    
                    // Выводим слова которые быди неправльно переведены
                    foreach(Word word in listErrorWords)
                    {
                        Console.WriteLine($"\nСлово на англійській мові: {word.EnWord} \nПереклад: {word.UkWord}");
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
    }
}
