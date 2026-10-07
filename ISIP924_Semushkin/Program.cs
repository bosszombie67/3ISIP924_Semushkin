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
        public int wordCount { get; set; }
        public int sentenceCount { get; set; }
        public List<string> shortestWords { get; set; } = new List<string>();
        public List<string> longestWords { get; set; } = new List<string>();

        public int vowelCount { get; set; }
        public int consonantCount { get; set; }
        public Dictionary<char, int> letterFrequency { get; set; } = new Dictionary<char, int>();
        public TextAnalysis(string inputText)
        {
            Text = inputText;
        }
        public void CountWords(string text)
        {
            int cw = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                {
                    if (i + 1 < text.Length && !char.IsLetterOrDigit(text[i + 1]))
                    {
                        cw++;
                    }
                    else if (i == text.Length - 1)
                    {
                        cw++;
                    }
                }
            }
            wordCount = cw;
        }
        public void CountSentences(string text)
        {
            int cs = 0;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '.' || text[i] == '!' || text[i] == '?')
                {
                    if (i == text.Length - 1)
                    {
                        cs++;
                    }
                    else
                    {
                        int next = i + 1;
                        while (next < text.Length && text[next] == ' ')
                        {
                            next++;
                        }
                        if (next < text.Length && char.IsUpper(text[next]))
                        {
                            cs++;
                        }
                    }
                }
            }
            sentenceCount = cs;
        }
        public void FindShortestWords(string text)
        {
            shortestWords.Clear();
            string word = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                {
                    word += text[i];
                }
                if ((!char.IsLetterOrDigit(text[i]) || i == text.Length - 1) && word.Length > 0)
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
        public void FindLongestWords(string text)
        {
            longestWords.Clear();
            string word = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                {
                    word += text[i];
                }
                if ((!char.IsLetterOrDigit(text[i]) || i == text.Length - 1) && word.Length > 0)
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
        public void CountVowelsAndConsonants(string text)
        {
            int vc = 0;
            int cc = 0;
            string vowels = "аеёиоуыэюя";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    char letter = char.ToLower(text[i]);
                    if (vowels.Contains(letter))
                    {
                        vc++;
                    }
                    else
                    {
                        cc++;
                    }
                }
            }
            vowelCount = vc;
            consonantCount = cc;
        }
    }
        internal class Program
        {
            static void Main(string[] args)
            {
                Console.Write("Введите текст: ");
                TextAnalysis analysis = new TextAnalysis(Console.ReadLine());

            }
        }
}