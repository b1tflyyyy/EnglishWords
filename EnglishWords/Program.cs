using EnglishWords.BL.Model;
using EnglishWords.BL.Controller;
using System;
using System.Text;
using System.Security.Cryptography.X509Certificates;

namespace View.CMD
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // загружаем конфиг консоли
            LoadConsoleConfig();

            // приветствие
            Console.WriteLine("Вітаємо в програмі EnglishWords! \nСпочатку оберіть режим вивчення: \n\twords - режим вивчення слів \n\tverbs - вивчення правильних та неправильних дієслів");

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
                Console.WriteLine("Введіть кількість слів, які ви хочете вивчити.");
                
                countWords = TryInt(Console.ReadLine());

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
                
                
                Console.WriteLine("Вітаю! Ви завершили ввід слів :)");
            }

            #endregion
            
            else
            {

            }
            
            // Выводим все данные (для теста)
            foreach(var word in wordController)
            {
                Console.WriteLine(word);
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
        /// Попробывть спарсить число.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private static int TryInt(string num)
        {
            while (true)
            {
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
