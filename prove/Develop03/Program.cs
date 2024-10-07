using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private class Reference
    {
        public string Text { get; private set; }

        public Reference(string text)
        {
            Text = text;
        }
    }

    private class Scripture
    {
        private Reference Reference; 
        private List<Word> Words;

        public Scripture(string referenceText, List<string> words)
        {
            Reference = new Reference(referenceText); 
            Words = words.Select(w => new Word(w)).ToList(); 
        }

        public void HideRandomWords()
        {
            Random randongenerator = new Random();
            int numberToHide = randongenerator.Next(1, 4); 

            
            var visibleWordIndices = Words.Select((word, index) => new { word, index })
                                           .Where(w => !w.word.IsWordHidden())
                                           .Select(w => w.index)
                                           .ToList();

            
            for (int i = 0; i < numberToHide && visibleWordIndices.Count > 0; i++)
            {
                int randomIndex = randongenerator.Next(visibleWordIndices.Count);
                int wordToHideIndex = visibleWordIndices[randomIndex];

                Words[wordToHideIndex].Hide(); 

                
                visibleWordIndices.RemoveAt(randomIndex);
            }
        }

        public string GetDisplayText()
        {
            
            return Reference.Text + " | " + string.Join(" ", Words.Select(w => w.GetDisplayText()));
        }

        public bool IsCompletelyHidden()
        {
            return Words.All(w => w.IsWordHidden()); 
        }
    }

    private class Word
    {
        private string Text;
        private bool isHidden; 

        public Word(string text)
        {
            Text = text;
            isHidden = false; 
        }

        public void Hide()
        {
            isHidden = true;
        }

        public bool IsWordHidden() 
        {
            return isHidden; 
        }

        public string GetDisplayText()
        {
            return isHidden ? new string('_', Text.Length) : Text; 
        }
    }

    static void Main(string[] args)
    {
        
        Scripture escritura = new Scripture("Doctrine and Covenants 6: 33-34",
            "Fear not to do good, my sons, for whatsoever ye sow, that shall ye also reap; therefore, if ye sow good ye shall also reap good for your reward. Therefore, fear not, little flock; do good; let earth and hell combine against you, for if ye are built upon my rock, they cannot prevail."
            .Split(new char[] { ' ', ',', ';', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries)
            .ToList());

        string entry = string.Empty;
        while (entry != "quit")
        {
            Console.Clear();
            Console.WriteLine(escritura.GetDisplayText());

            if (escritura.IsCompletelyHidden())
            {
                break;
            }

            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            entry = Console.ReadLine();

            if (entry == string.Empty) 
            {
                escritura.HideRandomWords(); 
            }
        }
    }
}