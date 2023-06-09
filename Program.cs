﻿namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new List<SweEngGloss>();
        class SweEngGloss
        {
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }

        static void Loader(string filePath)
        {
            using (StreamReader fileReader = new StreamReader(filePath))
            {
                dictionary = new List<SweEngGloss>();
                string line = fileReader.ReadLine();
                while (line != null)
                {
                    SweEngGloss glossary = new SweEngGloss(line);
                    dictionary.Add(glossary);
                    line = fileReader.ReadLine();
                }
            }
        }

        static void Translator(string translate)
        {
            foreach (SweEngGloss glossary in dictionary)
            {
                if (glossary.word_swe == translate)
                    Console.WriteLine($"English for {glossary.word_swe} is {glossary.word_eng}");
                if (glossary.word_eng == translate)
                    Console.WriteLine($"Swedish for {glossary.word_eng} is {glossary.word_swe}");
            }
        }

        static void Delete(string deleteSweOrEng1, string deleteSweOrEng2)
        {
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss glossary = dictionary[i];
                if ((glossary.word_swe == deleteSweOrEng1 && glossary.word_eng == deleteSweOrEng2) ||
                    (glossary.word_eng == deleteSweOrEng1 && glossary.word_swe == deleteSweOrEng2))
                {
                    dictionary.RemoveAt(i);
                    i--;
                }
            }
        }

        static string ReadWord(string text)
        {
            Console.WriteLine(text);
            string awnser = Console.ReadLine();
            return awnser;
        }

        static void Main(string[] args)
        {
            var quit = false;
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];

                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    quit = true;
                }

                else if (command == "help")
                {
                    string[] help =
                    {
                        "  help            - display this help",
                        "  load            - load all words from a file to the list",
                        "  list            - display all currently loaded weblinks",
                        "  new             - add new words to the list",
                        "  delete          - delete a word from the list",
                        "  translate       - translate a specific word from the list",
                        "  quit            - quit the program",
                    };
                    foreach (string info in help) Console.WriteLine(info);
                }

                else if (command == "load")
                {
                    if (argument.Length == 2)
                    {
                        try
                        {
                            Loader($"{argument[1]}");
                        }
                        catch (FileNotFoundException ex)
                        {
                            var fileStream = File.Create(argument[1]);
                            fileStream.Close();
                            Loader(argument[1]);
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Loader(defaultFile);
                    }
                }

                else if (command == "list")
                {
                    try
                    {
                        foreach (SweEngGloss glossary in dictionary)
                        {
                            Console.WriteLine($"{glossary.word_swe,-10}  - {glossary.word_eng,-10}");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Nothing to list, please load a file first!");
                    }
                }

                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        string swedish = ReadWord("Write word in Swedish: ");
                        string english = ReadWord("Write word in English: ");
                        dictionary.Add(new SweEngGloss(swedish, english));
                    }
                }                
                
                else if (command == "delete")
                {
                    try
                    {
                        if (argument.Length == 3)
                        {
                            Delete(argument[1], argument[2]);
                        }
                        else if (argument.Length == 2)
                        {
                            string wordToDelete = argument[1];

                            for (int i = 0; i < dictionary.Count; i++)
                            {
                                SweEngGloss glossary = dictionary[i];
                                if (glossary.word_swe == wordToDelete || glossary.word_eng == wordToDelete)
                                {
                                    dictionary.RemoveAt(i);
                                    i--;
                                }
                            }
                        }
                        else if (argument.Length == 1)
                        {
                            string swedish = ReadWord("Write word in Swedish: ");
                            string english = ReadWord("Write word in English: ");

                            Delete(swedish, english);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("There are no words to delete.");
                    }
                }

                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        Translator(argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string translateWord = Console.ReadLine();
                        Translator(translateWord);
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (quit == false);
        }
    }
}