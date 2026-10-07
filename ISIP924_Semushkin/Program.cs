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
        public string shortestWord { get; set; }
        public string longestWord { get; set; }
        public int vowelCount { get; set; }
        public int consonantCount { get; set; }
        public TextAnalysis(string inputText)
        {
            Text = inputText;
        }
        public int CountWords(string text)
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
            return cw;
        }
        public int CountSentences(string text)
        {
            int cs = 0;
            for(int i = 0; i < text.Length; i++)
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
            return cs;
        }
        public string FindShortestWord(string text)
        {
            string word = "";
            string shortestWord = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                {
                    word += text[i];
                }
                if ((!char.IsLetterOrDigit(text[i]) || i == text.Length - 1) && word.Length > 0)
                {
                    if (shortestWord == "" || word.Length < shortestWord.Length)
                    {
                        shortestWord = word;
                    }

                    word = "";
                }
            }
            return shortestWord;
        }
        public string FindLongestWord(string text)
        {
            string word = "";
            string longestWord = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetterOrDigit(text[i]))
                {
                    word += text[i];
                }
                if ((!char.IsLetterOrDigit(text[i]) || i == text.Length - 1) && word.Length > 0)
                {
                    if (longestWord == "" || word.Length > longestWord.Length)
                    {
                        longestWord = word;
                    }
                    word = "";
                }
            }
            return longestWord;
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
}