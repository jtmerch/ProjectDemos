using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorld.BeginTopics
{
    class clsSummarizeText
    {
        public void runSummarizeText()
        {
            var sentence = "This is going to be a really really really really really really really long text. ";

            var summary = funcSummarizeText(sentence, 20);

                Console.WriteLine(summary);
        }


         string funcSummarizeText(string text, int maxLength = 20)
        {

            var wordscount = text.Split(' ');
            var totalCharacters = 0;
            var summaryWords = new List<string>();

            foreach (var word in wordscount)
            {
                summaryWords.Add(word);

                totalCharacters += word.Length + 1; //add 1 for space after the word
                {
                    if (totalCharacters > maxLength) 
                    {
                        break; //don't loop anymore if over maxLength
                    }
                }
            }


            //Join arrays back to string and return
            return String.Join(" ", summaryWords) + "...";


        }
    }



}
