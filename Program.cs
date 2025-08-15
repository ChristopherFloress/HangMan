using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static readonly string[] HangmanStages = new[]
    {
        @"
  +---+
  |   |
      |
      |
      |
      |
=========",
        @"
  +---+
  |   |
  O   |
      |
      |
      |
=========",
        @"
  +---+
  |   |
  O   |
  |   |
      |
      |
=========",
        @"
  +---+
  |   |
  O   |
 /|   |
      |
      |
=========",
        @"
  +---+
  |   |
  O   |
 /|\  |
      |
      |
=========",
        @"
  +---+
  |   |
  O   |
 /|\  |
 /    |
      |
=========",
        @"
  +---+
  |   |
  O   |
 /|\  |
 / \  |
      |
========="
    };

    static void Main()
    {
        string[] wordList = { "cat", "dog", "sun", "car", "tree", "book", "fish", "bird", "milk", "star", "cake", "rain", "moon", "shoe", "frog" };
        int winStreak = 0;

        while (true)
        {
            var random = new Random();
            string wordToGuess = wordList[random.Next(wordList.Length)];
            var guessedLetters = new HashSet<char>();
            int wrongGuesses = 0;
            int maxWrongGuesses = HangmanStages.Length - 1;
            var guessHistory = new List<string>();
            bool won = false;

            while (wrongGuesses < maxWrongGuesses)
            {
                Console.Clear();

                Console.WriteLine($"Win streak: {winStreak}");
                Console.WriteLine("Previous guesses: " + string.Join(", ", guessHistory));
                Console.WriteLine(new string('-', 24));

                Console.Write("Word: ");
                foreach (char c in wordToGuess)
                {
                    Console.Write(guessedLetters.Contains(c) ? c : '_');
                    Console.Write(' ');
                }
                Console.WriteLine();
                Console.WriteLine(new string('-', 24));

                Console.WriteLine(HangmanStages[wrongGuesses]);
                Console.WriteLine($"Attempts left: {maxWrongGuesses - wrongGuesses}");
                Console.WriteLine(new string('-', 24));

                if (IsWordGuessed(wordToGuess, guessedLetters))
                {
                    Console.WriteLine("Congratulations! You guessed the word!");
                    winStreak++;
                    won = true;
                    break;
                }

                Console.Write("Guess a letter: ");
                string input = Console.ReadLine()?.ToLower();
                if (string.IsNullOrWhiteSpace(input) || input.Length != 1 || !char.IsLetter(input[0]))
                {
                    Console.WriteLine("Invalid input.");
                    Thread.Sleep(1000);
                    continue;
                }

                char guess = input[0];
                if (guessedLetters.Contains(guess))
                {
                    Console.WriteLine("You already guessed that letter bro");
                    Thread.Sleep(1000);
                    continue;
                }

                guessedLetters.Add(guess);
                guessHistory.Add(guess.ToString());
                if (!wordToGuess.Contains(guess))
                {
                    wrongGuesses++;
                }
            }

            if (!won)
            {
                Console.Clear();
                Console.WriteLine(HangmanStages[HangmanStages.Length - 1]);
                Console.WriteLine($"Game over! The word was: {wordToGuess}");
                winStreak = 0;
            }

            Console.WriteLine($"Current win streak: {winStreak}");
            Console.Write("Play again? (y/n): ");
            string playAgain = Console.ReadLine()?.ToLower();
            if (playAgain != "y")
                break;
        }
    }

    static bool IsWordGuessed(string word, HashSet<char> guessedLetters)
    {
        foreach (char c in word)
        {
            if (!guessedLetters.Contains(c))
                return false;
        }
        return true;
    }
}
