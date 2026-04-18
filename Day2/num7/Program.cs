using System;

class Num7
{
    static void Main()
    {
        string sentence = "хэлоу ворл ай эм дима";
        string[] words = sentence.Split(' ');

        foreach (string word in words)
        {
            Console.WriteLine(word);
        }
    }
}