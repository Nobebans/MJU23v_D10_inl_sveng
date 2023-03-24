namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary;
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
        static void Main(string[] args)
        {
            string defaultFile = "..\\..\\..\\dict\\sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            do
            {
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                //FIXME: stängs inte av när man skriver "quit"
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
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
                        using (StreamReader fileReader = new StreamReader(argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = fileReader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss glossary = new SweEngGloss(line);
                                dictionary.Add(glossary);
                                line = fileReader.ReadLine();
                            }
                        }
                    }

                    else if (argument.Length == 1)
                    {
                        using (StreamReader fileReader = new StreamReader(defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = fileReader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss glossary = new SweEngGloss(line);
                                dictionary.Add(glossary);
                                line = fileReader.ReadLine();
                            }
                        }
                    }
                }
                //FIXME: programmet krashar när man skriver "list" innan man har loadad in listan
                else if (command == "list")
                {
                    foreach (SweEngGloss glossary in dictionary)
                    {
                        Console.WriteLine($"{glossary.word_swe,-10}  - {glossary.word_eng,-10}");
                    }
                }
                //FIXME: gör en tom text fil om man inte laddad in lista redan
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(swedish, english));
                    }
                }
                //FIXEME: skriv bara "delete ord" inte "delete sveOrd engOrd"
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                    {
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss glossary = dictionary[i];
                            if (glossary.word_swe == argument[1] && glossary.word_eng == argument[2])
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word in Swedish: ");
                        string swedish = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string english = Console.ReadLine();
                        int index = -1;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss glossary = dictionary[i];
                            if (glossary.word_swe == swedish && glossary.word_eng == english)
                                index = i;
                        }
                        dictionary.RemoveAt(index);
                    }
                }
                //FIXME: koddubblett
                else if (command == "translate")
                {
                    if (argument.Length == 2)
                    {
                        foreach (SweEngGloss glossary in dictionary)
                        {
                            if (glossary.word_swe == argument[1])
                                Console.WriteLine($"English for {glossary.word_swe} is {glossary.word_eng}");
                            if (glossary.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {glossary.word_eng} is {glossary.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string translateWord = Console.ReadLine();
                        foreach (SweEngGloss glossary in dictionary)
                        {
                            if (glossary.word_swe == translateWord)
                                Console.WriteLine($"English for {glossary.word_swe} is {glossary.word_eng}");
                            if (glossary.word_eng == translateWord)
                                Console.WriteLine($"Swedish for {glossary.word_eng} is {glossary.word_swe}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
    }
}