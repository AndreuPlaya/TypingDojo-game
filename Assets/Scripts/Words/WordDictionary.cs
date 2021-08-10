using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class WordDictionary 
{
    private static List<(string word, int value)> _valueWords;
    
    public static List<(string word, int value)> GetWordTouple()
    {
        if (_valueWords == null)
        {
            _valueWords = new List<(string word, int value)>();
            string[] words = ReadFileContents();
            _valueWords = ConstructTouple(words);
        }
        return _valueWords;
    }

 
    private static string[] ReadFileContents()
    {
        string path = "Assets/Resources/en.txt";
        var sr = new StreamReader(path);
        var fileContents = sr.ReadToEnd();
        sr.Close();
        string[] words = fileContents.Split("\n"[0]);
        string[] purgedWords = PurgeCharacters(words);
        return purgedWords;
    }
    private static List<(string word, int value)> ConstructTouple(string[] wordsString)
    {
        List<(string word, int value)> wordsTuple = new List<(string word, int value)>(); ;
        foreach (string word in wordsString)
        {
            int wordPoints = CalculatePoints(word);
            wordsTuple.Add((word, wordPoints));
        }
        wordsTuple.Sort((x, y) => y.value.CompareTo(x.value));
        return wordsTuple;        
    }

    private static string [] PurgeCharacters(string[] words)
    {
        string[] purgedWords = new string[words.Length];

        for (int i = 0; i < words.Length; i++)
        {
            string newWord = words[i];
            foreach (char letter in words[i])
            {
                if (!IsCharacterValid(letter))
                {
                    newWord = newWord.Trim(letter);
                }
            }
            purgedWords[i] = newWord;
        }
        return purgedWords;

    }
    private static bool IsCharacterValid(char letter)
    {
        string validLetters =  "qwertyuiopasdfghjklzxcvbnm";
        foreach(char validLetter in validLetters)
        {
            if (letter == validLetter)
                return true;
        }
        return false;
    }

    private static int CalculatePoints(string word)
    {
        string homeRow = "asdfghjkl";
        string topRow = "qwertyuiop";
        string botRow = "zxcvbnm";
        int value = 0;
        foreach(char letter in word)
        {
            if (homeRow.Contains(letter.ToString()))
            {
                value += 2;
                continue;
            }
            if (topRow.Contains(letter.ToString()))
            {
                value += 3;
                continue;
            }
            if (botRow.Contains(letter.ToString()))
            {
                value += 3;
                continue;
            }

        }
        return value;
        
    }

}
