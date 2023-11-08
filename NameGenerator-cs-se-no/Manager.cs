using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace Generator_Jmen_05_11_2023_v1
{
    internal class Manager
    {
        public string Enc = "UTF-8";
        //Female names CS
        public static List<string> csFemaleFirst = new List<string>();
        public static List<string> csFemaleLast = new List<string>();
        //Male names CS
        public static List<string> csMaleFirst = new List<string>();
        public static List<string> csMaleLast = new List<string>();

        //Female names SE
        public static List<string> seFemaleFirst = new List<string>();
        public static List<string> seFemaleLast = new List<string>();
        //Male names SE 
        public static List<string> seMaleFirst = new List<string>();
        public static List<string> seMaleLast = new List<string>();

        //Female names NO
        public static List<string> noFemaleFirst = new List<string>();
        public static List<string> noFemaleLast = new List<string>();
        //Male names NO
        public static List<string> noMaleFirst = new List<string>();
        public static List<string> noMaleLast = new List<string>();

        public static List<List<string>> validLists = new List<List<string>>();

        // Create a list of lists for each category
        public static List<List<string>> allLists = new List<List<string>>()
        {
            csFemaleFirst, csFemaleLast, csMaleFirst, csMaleLast,
            seFemaleFirst, seFemaleLast, seMaleFirst, seMaleLast,
            noFemaleFirst, noFemaleLast, noMaleFirst, noMaleLast
        };
        // Create separate lists for FemaleFirsts, MaleFirsts, FemaleLasts, MaleLasts
        public static List<string> FemaleFirsts = new List<string>();
        public static List<string> MaleFirsts = new List<string>();
        public static List<string> FemaleLasts = new List<string>();
        public static List<string> MaleLasts = new List<string>();
        public static List<List<string>> separateLists = new List<List<string>>()
        {
            FemaleFirsts, MaleFirsts, FemaleLasts, MaleLasts
        };

        public void CheckValidLists()
        {
            // Add non-empty lists to validLists
            foreach (List<string> list in allLists)
            {
                if (list.Count > 0)
                {
                    validLists.Add(list);
                }
            }

            //Rozdeleni valid listu do listu podle krestniho / prijmeni a nasledne naplneni listu jmenama z listu
            foreach (var list in validLists)
            {
                // Determine the category based on list name or other criteria
                // For example, if the list name contains "FemaleFirst," it's categorized as FemaleFirst, and so on.
                if (list == csFemaleFirst || list == seFemaleFirst || list == noFemaleFirst)
                {
                    FemaleFirsts.AddRange(list);
                }
                else if (list == csMaleFirst || list == seMaleFirst || list == noMaleFirst)
                {
                    MaleFirsts.AddRange(list);
                }
                else if (list == csFemaleLast || list == seFemaleLast || list == noFemaleLast)
                {
                    FemaleLasts.AddRange(list);
                }
                else if (list == csMaleLast || list == seMaleLast || list == noMaleLast)
                {
                    MaleLasts.AddRange(list);
                }
            }
        }
        public void ClearLists()
        {
            foreach (List<string> list in allLists) list.Clear();
            foreach (List<string> list in validLists) list.Clear();
            foreach (List<string> list in separateLists) list.Clear();
        }
        public void RepairCSV(string fileName, string outPutName)
        {
            try
            {
                /*string csEncoding = "Windows-1250";
                string seEncoding = "ISO-8859-4";
                string noEncoding = "UTF-8";*/
                // Read the CSV data from a file
                string filePath = $"..\\..\\csv\\{fileName}.csv";
                string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding(Enc));

                for (int i = 0; i < lines.Length; i++)
                {
                    // Remove periods from each line
                    lines[i] = lines[i].Replace(".", "");

                    // Remove the last semicolon if it exists
                    if (lines[i].EndsWith(";"))
                    {
                        lines[i] = lines[i].Substring(0, lines[i].Length - 1);
                    }
                    Console.WriteLine(lines[i]);
                }
                File.WriteAllLines($"{outPutName}.csv", lines, Encoding.GetEncoding(Enc));
                Console.WriteLine("Output successful!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred!" + e.Message + " Type: " + e.GetType());
            }
        }
        public void LoadCSV(string gender, List<string> countries)
        {
            ClearLists(); //Vyčistit listy kvůli duplicitě
            try
            {
                foreach (string country in countries)
                {
                    // Read the CSV data from a file
                    string filePath1 = $"..\\..\\csv\\{country + gender}First.csv";
                    string filePath2 = $"..\\..\\csv\\{country + gender}Last.csv";
                    if (country == "cs") Enc = "Windows-1250";
                    string[] linesFirst = File.ReadAllLines(filePath1, Encoding.GetEncoding(Enc));
                    string[] linesLast = File.ReadAllLines(filePath2, Encoding.GetEncoding(Enc));

                    for (int i = 0; i < linesFirst.Length; i++)
                    {
                        string[] line = linesFirst[i].Split(';');
                        switch (country)
                        {
                            case "cs":
                                switch (gender)
                                {
                                    case "Male":
                                        csMaleFirst.Add(line[1]);
                                        break;
                                    case "Female":
                                        csFemaleFirst.Add(line[1]);
                                        break;
                                }
                                break;
                            case "se":
                                switch (gender)
                                {
                                    case "Male":
                                        seMaleFirst.Add(line[1]);
                                        break;
                                    case "Female":
                                        seFemaleFirst.Add(line[1]);
                                        break;
                                }
                                break;
                            case "no":
                                switch (gender)
                                {
                                    case "Male":
                                        noMaleFirst.Add(line[1]);
                                        break;
                                    case "Female":
                                        noFemaleFirst.Add(line[1]);
                                        break;
                                }
                                break;
                        }
                    }

                    for (int i = 0; i < linesLast.Length; i++)
                    {
                        string[] line = linesLast[i].Split(';');
                        switch (country)
                        {
                            case "cs":
                                switch (gender)
                                {
                                    case "Male":
                                        csMaleLast.Add(line[1]);
                                        break;
                                    case "Female":
                                        csFemaleLast.Add(line[1]);
                                        break;
                                }
                                break;
                            case "se":
                                switch (gender)
                                {
                                    case "Male":
                                        seMaleLast.Add(line[1]);
                                        break;
                                    case "Female":
                                        seFemaleLast.Add(line[1]);
                                        break;
                                }
                                break;
                            case "no":
                                switch (gender)
                                {
                                    case "Male":
                                        noMaleLast.Add(line[1]);
                                        break;
                                    case "Female":
                                        noFemaleLast.Add(line[1]);
                                        break;
                                }
                                break;
                        }
                    }
                    Enc = "UTF-8";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occurred!" + e.Message + " Type: " + e.GetType());
            }

        }
        public string GetRandomName(string gender)
        {
            string firstName = "default";
            string lastName = "default";
            Random r = new Random();
            try
            {
                if (gender == "Male")
                {
                    int firstSeed = r.Next(0, MaleFirsts.Count());
                    int lastSeed = r.Next(0, MaleLasts.Count());
                    firstName = MaleFirsts[firstSeed];
                    lastName = MaleLasts[lastSeed];
                }
                else if (gender == "Female")
                {
                    int firstSeed = r.Next(0, FemaleFirsts.Count());
                    int lastSeed = r.Next(0, FemaleLasts.Count());
                    firstName = FemaleFirsts[firstSeed];
                    lastName = FemaleLasts[lastSeed];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception occurred! {e.Message}, Type: {e.GetType()}");
            }
            return firstName + " " + lastName;
        }
    }
}