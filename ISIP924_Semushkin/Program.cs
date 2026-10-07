using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    class TextAnalysis
    {
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 100)
                {
                    throw new ArgumentException("Текст должен содержать как минимум 100 символов");
                }
                text = value;
            }
        }
        public int wordCount { get; private set; }
        public int sentenceCount { get; private set; }
        public List<string> shortestWords { get; private set; } = new List<string>();
        public List<string> longestWords { get; private set; } = new List<string>();

        public int vowelCount { get; private set; }
        public int consonantCount { get; private set; }
        public Dictionary<char, int> letterFrequency { get; private set; } = new Dictionary<char, int>();
        public TextAnalysis(string inputText)
        {
            Text = inputText;
        }
        public void Analyze()
        {
            CountWords();
            CountSentences();
            FindShortestWords();
            FindLongestWords();
            CountVowelsAndConsonants();
            CountLetterFrequency();
        }
        public void PrintStatistics()
        {
            Console.WriteLine("Текст:");
            Console.WriteLine(Text);
            Console.WriteLine($"Количество слов: {wordCount}");
            Console.WriteLine($"Количество предложений: {sentenceCount}");
            Console.WriteLine($"Количество гласных: {vowelCount}");
            Console.WriteLine($"Количество согласных: {consonantCount}" );
            Console.WriteLine();
            Console.WriteLine("Самые короткие слова:");
            foreach (string word in shortestWords)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
            Console.WriteLine("Самые длинные слова:");
            foreach (string word in longestWords)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
            Console.WriteLine("Частота встречаемости букв:");
            foreach (var letter in letterFrequency)
            {
                Console.WriteLine(letter.Key + " - " + letter.Value);
            }
        }
        private void CountWords()
        {
            int cw = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetterOrDigit(Text[i]))
                {
                    if (i + 1 < Text.Length && !char.IsLetterOrDigit(Text[i + 1]))
                    {
                        cw++;
                    }
                    else if (i == Text.Length - 1)
                    {
                        cw++;
                    }
                }
            }
            wordCount = cw;
        }
        private void CountSentences()
        {
            int cs = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] == '.' || Text[i] == '!' || Text[i] == '?')
                {
                    if (i + 1 < Text.Length && (Text[i + 1] == '.' || Text[i + 1] == '!' || Text[i + 1] == '?'))
                    {
                        continue;
                    }
                    int next = i + 1;
                    while (next < Text.Length && Text[next] == ' ')
                    {
                        next++;
                    }
                    if (next == Text.Length)
                    {
                        cs++;
                    }
                    else if (char.IsUpper(Text[next]))
                    {
                        cs++;
                    }
                }
            }
            sentenceCount = cs;
        }
        private void FindShortestWords()
        {
            shortestWords.Clear();
            string word = "";
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetter(Text[i]))
                {
                    word += Text[i];
                }
                if ((!char.IsLetter(Text[i]) || i == Text.Length - 1) && word.Length > 0)
                {
                    if (shortestWords.Count == 0 || word.Length < shortestWords[0].Length)
                    {
                        shortestWords.Clear();
                        shortestWords.Add(word);
                    }
                    else if (word.Length == shortestWords[0].Length)
                    {
                        shortestWords.Add(word);
                    }
                    word = "";
                }
            }
        }
        private void FindLongestWords()
        {
            longestWords.Clear();
            string word = "";
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetter(Text[i]))
                {
                    word += Text[i];
                }
                if ((!char.IsLetter(Text[i]) || i == Text.Length - 1) && word.Length > 0)
                {
                    if (longestWords.Count == 0 || word.Length > longestWords[0].Length)
                    {
                        longestWords.Clear();
                        longestWords.Add(word);
                    }
                    else if (word.Length == longestWords[0].Length)
                    {
                        longestWords.Add(word);
                    }
                    word = "";
                }
            }
        }
        private void CountVowelsAndConsonants()
        {
            vowelCount = 0;
            consonantCount = 0;
            string vowels = "аеёиоуыэюя";
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetter(Text[i]))
                {
                    char letter = char.ToLower(Text[i]);
                    if (vowels.Contains(letter))
                    {
                        vowelCount++;
                    }
                    else
                    {
                        consonantCount++;
                    }
                }
            }
        }
        private void CountLetterFrequency()
        {
            letterFrequency.Clear();
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetter(Text[i]))
                {
                    char letter = char.ToLower(Text[i]);
                    if (letterFrequency.ContainsKey(letter))
                    {
                        letterFrequency[letter]++;
                    }
                    else
                    {
                        letterFrequency.Add(letter, 1);
                    }
                }
            }
        }

    }
        internal class Program
        {
            static void Main(string[] args)
            {
                List<TextAnalysis> stats = new List<TextAnalysis>();
                int choice = -67;
                while(choice != 0)
                {
                    Console.WriteLine("1 - Ввести новый текст");
                    Console.WriteLine("2 - Вывести статистику прошлых текстов");
                    Console.WriteLine("0 - Выход");
                    Console.Write("Выберите действие: ");
                    if (!int.TryParse(Console.ReadLine(), out choice))
                    {
                        Console.WriteLine("Введите число.");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    Console.Clear();
                    switch (choice)
                    {
                        case 1:
                            try
                            {
                                Console.Write("Введите текст: ");
                                TextAnalysis analysis = new TextAnalysis(Console.ReadLine());
                                analysis.Analyze();
                                stats.Add(analysis);
                                Console.WriteLine();
                                analysis.PrintStatistics();
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            break;
                        case 2:
                            if (stats.Count == 0)
                            {
                                Console.WriteLine("Статистики пока нет.");
                            }
                            else
                            {
                                for (int i = 0; i < stats.Count; i++)
                                {
                                    Console.WriteLine($"----- Текст №{i + 1} -----");
                                    stats[i].PrintStatistics();
                                    Console.WriteLine();
                                }
                            }
                            break;
                        case 0:
                            Console.WriteLine("Выход из программы.");
                            break;
                        default:
                            Console.WriteLine("Такого пункта нет.");
                            break;
                    }
                    if (choice != 0)
                    {
                        Console.WriteLine("Нажмите любую клавишу...");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }
        }
}