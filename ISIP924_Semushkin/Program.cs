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
                if (string.IsNullOrWhiteSpace(value) || value.Length < 100){
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
        public TextAnalysis(string Text)
        {
            text = Text;
        }
        public int CountWords(string text)
        {
            foreach(char s in text)
            {

            }
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