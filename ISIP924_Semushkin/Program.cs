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
                    if (text[i + 1] == ' ' && (i + 1 < text.Length))
                    {
                        cw++;
                    }
                    if (i + 1 <= text.Length && i > 0 && text[i + 1] == ' ' && text[i - 1] == ' ')
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
                if (i + 1 < text.Length && text[i] == '.' && char.IsUpper(text[i + 1]))
                {
                    cs++;
                }
                else if (i + 2 < text.Length && text[i] == '.' && char.IsUpper(text[i + 2]))
                {
                    cs++;
                }
                else if (i + 1 == text.Length && text[i] == '.')
                {
                    cs++;
                }
            }
            return cs;
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