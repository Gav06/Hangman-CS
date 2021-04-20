using System;
using System.Collections.Generic;

namespace Hangman {
    class Program {
        static void Main(string[] args) {
            new Program();
        }

        String[] words = { "java", "microsoft", "oracle", "kotlin", "python", "groovy", "scala", "javascript" };
        String word;
        Boolean shouldStop = false;
        int chancesLeft = 5;

        List<char> guessedLetters = new List<char>();
        List<char> knownLetters = new List<char>();

        public Program() {
            Random random = new Random();

            double probability = random.NextDouble() * 2;
            int chosenWord = (int) Math.Round(probability);
            word = words[chosenWord];

            Console.WriteLine("Welcome to hangman by Gav06\n\n");

            //Console.WriteLine(word);
            Console.WriteLine("The word is " + word.Length + " characters long\n");

            while (!shouldStop) {
                runTurn();
            }
        }

        void giveInfo() {
            Console.WriteLine("Chances remaining: " + chancesLeft);
            String text = "Incorrect letters: ";
            for (int i = 0; i < guessedLetters.Count; i++) {
                text += guessedLetters[i] + ", ";
            }
            text += "\n";
            Console.WriteLine(text.Substring(0, text.Length - 2));
        }


        void runTurn() {
            if (chancesLeft == 0) {
                Console.WriteLine("Game over!\nThe word was " + word);
                shouldStop = true;
                return;
            }
            giveInfo();
            Console.WriteLine("Please guess a letter: ");
            String rawInput = Console.ReadLine();
            if (rawInput.Length == 0) {
                return;
            }

            // make everything lowercase
            char guessedLetter = rawInput.ToLower().ToCharArray()[0];
            Console.WriteLine("You guessed " + guessedLetter);
            int instances = instancesOfLetterInWord(guessedLetter);
            Console.WriteLine("There are " + instances + " instances of " + guessedLetter + " in the word");
            if (instances == 0) {
                guessedLetters.Add(guessedLetter);
                chancesLeft--;
            } else {
                knownLetters.Add(guessedLetter);
            }

            String s = getFormattedWord();
            Console.WriteLine("Word: " + s + "\n\n");
            if (s.Equals(word))
            {
                Console.WriteLine("You win!");
                shouldStop = true;
            }
        }

        int instancesOfLetterInWord(char charIn) {
            char[] str = word.ToLower().ToCharArray();
            int amount = 0;
            for (int i = 0; i < str.Length; i++) {
                if (str[i] == charIn)
                    amount++;
            }

            return amount;
        }

        String getFormattedWord() {
            String formattedWord = "";
            foreach (char c in word.ToCharArray()) {
                // :sunglasses:
                if (knownLetters.Contains(c.ToString().ToLower().ToCharArray()[0])) {
                    formattedWord += c;
                } else {
                    formattedWord += "_";
                }
            }

            return formattedWord;
        }
    }
}
