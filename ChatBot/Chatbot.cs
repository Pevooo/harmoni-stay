using System.Collections.Immutable;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using StopWord;

namespace ChatBot;

/*
public class Program{
    public static void Main(string[] args){
        ChatBot Chatbot = new();
        string? input = Console.ReadLine();
        Console.WriteLine(Chatbot.Query(input));
    }
}
*/

public sealed class ChatBot
{

    // Dictionary mapping each word to the corrosponding IDF value
    private readonly Dictionary<string, double> idfs;

    // Dictionary mapping each filename to the corrosponding Dictionary that maps words in the file to the corrosponding TF-IDF value
    private readonly Dictionary<string, Dictionary<string, double>> tfIdfs;

    // Dictionary mapping filenames to a list of the words in the file lowercased excluding stopwords
    private readonly Dictionary<string, List<string>> fileWords;

    // Dictionary mapping filenames to content
    private readonly Dictionary<string, string> files;


    public ChatBot()
    {

        files = ReadData();

        fileWords = new();
        foreach (var file in files)
        {
            fileWords.Add(file.Key, GenerateTokens(file.Value));
        }

        idfs = GetIdfs(fileWords);
        tfIdfs = GetTfIdfs(fileWords);

    }

    public string Query(string prompt)
    {

        List<string> tokenizedQuery = GenerateTokens(prompt);
        string topFileName = GetTopFile(tokenizedQuery, fileWords);


        string topSentence = GetTopSentence(tokenizedQuery, GetSentences(files[topFileName]));

        return topSentence;
    }


    private string GetTopSentence(List<string> tokenizedQuery, Dictionary<string, List<string>> sentences)
    {


        Dictionary<string, double> sentenceIdfs = GetIdfs(sentences);

        Dictionary<string, double> scores = new();
        foreach (var sentence in sentences)
        {
            scores.Add(sentence.Key, 0);
            foreach (var word in tokenizedQuery)
            {
                if (sentence.Value.Contains(word))
                {
                    scores[sentence.Key] += sentenceIdfs[word];
                }
            }
        }

        Dictionary<string, double> MatchingWords = new();
        foreach (var sentence in sentences)
        {
            MatchingWords.Add(sentence.Key, 0);
            foreach (var word in tokenizedQuery)
            {
                if (sentence.Value.Contains(word))
                {
                    MatchingWords[sentence.Key]++;
                }
            }
            MatchingWords[sentence.Key] /= sentence.Value.Count;
        }


        KeyValuePair<string, double> topSentence = scores.OrderByDescending(sentence => sentence.Value).ThenByDescending(sentence => MatchingWords[sentence.Key]).First();


        if (topSentence.Value == 0 && MatchingWords[topSentence.Key] == 0)
        {
            return "Sorry, I couldn't understand what you said.";
        }
        else
        {
            return topSentence.Key;
        }
    }



    private string GetTopFile(List<string> tokenizedQuery, Dictionary<string, List<string>> fileWords)
    {


        var query = fileWords.OrderByDescending(kv => (from s in tfIdfs[kv.Key] where tokenizedQuery.Contains(s.Key) select s.Value).Sum()).ToList();

        return query[0].Key;
    }

    private Dictionary<string, List<string>> GetSentences(string fileData)
    {
        Dictionary<string, List<string>> setnences = new();

        foreach (var rawSentence in fileData.Split("\n"))
        {
            setnences.Add(rawSentence, GenerateTokens(rawSentence));
        }


        return setnences;
    }


    private Dictionary<string, Dictionary<string, double>> GetTfIdfs(Dictionary<string, List<string>> fileWords)
    {
        Dictionary<string, Dictionary<string, double>> TfIdfs = new();

        foreach (var file in fileWords)
        {
            TfIdfs.Add(file.Key, new());
            foreach (var word in file.Value)
            {
                TfIdfs[file.Key].TryAdd(word, 0);
            }
        }

        foreach (var file in fileWords)
        {
            foreach (var word in file.Value)
            {
                TfIdfs[file.Key][word] += idfs[word];
            }
        }


        return TfIdfs;
    }

    private Dictionary<string, double> GetIdfs(Dictionary<string, List<string>> fileWords)
    {
        Dictionary<string, double> idfs = new();

        Dictionary<string, int> wordCounts = new();
        foreach (var file in fileWords)
        {
            foreach (var word in file.Value)
            {
                wordCounts.TryAdd(word, 0);
            }
        }


        foreach (var word in wordCounts)
        {
            int count = 0;

            foreach (var file in fileWords)
            {
                if (file.Value.Contains(word.Key))
                {
                    count++;
                }
            }

            wordCounts[word.Key] = count;
        }

        foreach (var word in wordCounts)
        {
            idfs.Add(word.Key, Math.Log((double)fileWords.Count() / word.Value));
        }

        return idfs;
    }


    private List<string> GenerateTokens(string fileData)
    {

        string words = RemovePunctuation(fileData.Replace("\n", " ").ToLower()).RemoveStopWords("en");


        if (words.Length != 0 && words[words.Length - 1] == ' ')
        {
            words = words.Substring(0, words.Length - 1);
        }

        List<string> tokens = words.Split(" ").ToList();

        return tokens;
    }

    private Dictionary<string, string> ReadData()
    {
        Dictionary<string, string> files = new();
        string[] filenames = Directory.GetFiles(@"Chatbot/data/");

        foreach (string filename in filenames)
        {
            StreamReader reader = new(filename);
            files.Add(filename, reader.ReadToEnd());
        }

        return files;
    }


    private string RemovePunctuation(string s)
    {
        return new string(s.Where(character => !char.IsPunctuation(character)).ToArray());
    }
}
