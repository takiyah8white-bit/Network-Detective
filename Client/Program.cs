
using System;
using System.IO;
using System.Net.Sockets;

class Program
{

    static string detectiveRanking = "No cases solved yet.";
    static void Main()
    {
        using TcpClient client = new TcpClient("127.0.0.1", 9050);
        using NetworkStream stream = client.GetStream();
        using StreamReader reader = new StreamReader(stream);
        using StreamWriter writer = new StreamWriter(stream)
        {
            AutoFlush = true
        };

        Console.WriteLine("=================================");
        Console.WriteLine("       NETWORK DETECTIVE");
        Console.WriteLine("       HOMICIDE DIVISION");
        Console.WriteLine("=================================");

        Console.WriteLine(reader.ReadLine());

        bool playing = true;

        


        while (playing)
        {
            Console.WriteLine();
            Console.WriteLine("---------- MAIN MENU ------------");
            Console.WriteLine("1. Start New Case");
            Console.WriteLine("2. View Detective Rankings");
            Console.WriteLine("3. Exit Game");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                
            case "1":
                Console.WriteLine("\n===== SELECT DIFFICULTY =====");
                Console.WriteLine("1. Easy - Rookie Detective");
                Console.WriteLine("2. Medium - Detective");
                Console.WriteLine("3. Hard - Master Detective");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Choose difficulty: ");

                string? difficulty = Console.ReadLine();

                if (difficulty == "1" || difficulty == "2" || difficulty == "3")
                {
                    Console.WriteLine("\nDifficulty selected!");
                    InvestigateCase(reader, writer, difficulty);
                }
                else if (difficulty != "4")
                {
                    Console.WriteLine("Invalid difficulty selection.");
                }
                break;


                
                case "2":
                    Console.WriteLine("\n===== DETECTIVE RANKINGS =====");
                    Console.WriteLine(detectiveRanking);
                    Console.WriteLine("==============================");
                    break;


                case "3":
                    Console.WriteLine("Good work, Detective. Case files closed.");
                    playing = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void InvestigateCase(StreamReader reader, StreamWriter writer, string difficulty)
    {
        bool investigating = true;

        while (investigating)
        {
            
            Console.WriteLine("---------- CASE MENU ------------");
            Console.WriteLine("1. Review Case Briefing");
            Console.WriteLine("2. Interview Suspects");
            Console.WriteLine("3. Examine Evidence");
            Console.WriteLine("4. Review Evidence Board");
            Console.WriteLine("5. Make an Accusation");
            Console.WriteLine("6. Return to Main Menu");
            Console.Write("Choose an action: ");


            string? choice = Console.ReadLine();

            switch (choice)
            {
                
            case "1":
                SendRequest("BRIEFING", reader, writer);

                if (difficulty == "1")
                {
                Console.WriteLine("\nROOKIE DETECTIVE HINT:");
                Console.WriteLine("Compare the suspects' alibis with the timeline.");
                Console.WriteLine("Pay attention to the office records and building access log.");
                }
                else if (difficulty == "2")
                {
                Console.WriteLine("\nDETECTIVE HINT:");
                Console.WriteLine("Someone's timeline doesn't add up.");
                }
                else if (difficulty == "3")
                {
                Console.WriteLine("\nMASTER DETECTIVE MODE:");
                Console.WriteLine("No hints available. Trust your investigation.");
                }
                break;


                
                case "2":
                    Console.WriteLine();
                    Console.WriteLine("======= INTERROGATION ROOM =======");
                    Console.WriteLine("1. Daniel Brooks - Ex-boyfriend");
                    Console.WriteLine("2. Rachel Morgan - Business Partner");
                    Console.WriteLine("3. Ethan Price - Neighbor");
                    Console.WriteLine("4. Return to Case Menu");
                    Console.Write("Who would you like to question? ");

                    string? suspectChoice = Console.ReadLine();

                    if (suspectChoice == "1" ||
                        suspectChoice == "2" ||
                        suspectChoice == "3")
                    {
                        SendRequest("INTERVIEW:" + suspectChoice, reader, writer);
                        
                    Console.Write("\nWould you like to ask a follow-up question? (Y/N): ");
                    string followUpChoice = Console.ReadLine()?.Trim().ToUpper() ?? "";

                    if (followUpChoice == "Y")
                    {
                        SendRequest("FOLLOWUP:" + suspectChoice, reader, writer);
                    }

                


                    }
                    else if (suspectChoice == "4")
                    {
                        Console.WriteLine("Returning to investigation...");
                    }
                    else
                    {
                        Console.WriteLine("Invalid suspect selection.");
                    }
                    break;


                
            case "3":
                Console.WriteLine();
                Console.WriteLine("------ CRIME SCENE EVIDENCE ------");
                Console.WriteLine("1. Security Camera Footage");
                Console.WriteLine("2. Victim's Missing Phone");
                Console.WriteLine("3. Broken Wine Glass");
                Console.WriteLine("4. Office Attendance Records");
                Console.WriteLine("5. Building Access Log");
                Console.WriteLine("6. Return to Case Menu");
                Console.Write("Select evidence to examine: ");

                string? evidenceChoice = Console.ReadLine();

                if (evidenceChoice == "1" ||
                    evidenceChoice == "2" ||
                    evidenceChoice == "3" ||
                    evidenceChoice == "4" ||
                    evidenceChoice == "5")
                {
                    SendRequest("EXAMINE:" + evidenceChoice, reader, writer);
                }
                else if (evidenceChoice == "6")
                {
                    Console.WriteLine("Returning to investigation...");
                }
                else
                {
                    Console.WriteLine("Invalid evidence selection.");
                }
                break;

            case "4":
                SendRequest("BOARD", reader, writer);
                break;

                    
            case "5":
                Console.WriteLine();
                Console.WriteLine("------ MAKE AN ACCUSATION ------");
                Console.WriteLine("1. Daniel Brooks");
                Console.WriteLine("2. Rachel Morgan");
                Console.WriteLine("3. Ethan Price");
                Console.Write("Who do you believe committed the murder? ");

                string? suspect = Console.ReadLine();

                
            if (suspect == "1" || suspect == "2" || suspect == "3")
            {
                string result = SendRequest("ACCUSE:" + suspect, reader, writer);

                if (result.Contains("Master Detective"))
                {
                    detectiveRanking = "Master Detective";
                }
                else if (result.Contains("Senior Investigator"))
                {
                    detectiveRanking = "Senior Investigator";
                }
                else if (result.Contains("Rookie Detective"))
                {
                    detectiveRanking = "Rookie Detective";
                }
                else if (result.Contains("Detective"))
                {
                    detectiveRanking = "Detective";
                }
            }

                else
                {
                    Console.WriteLine("Invalid suspect selection.");
                }
                break;


                case "6":
                    investigating = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static string SendRequest(
        string request,
        StreamReader reader,
        StreamWriter writer)
    {
        writer.WriteLine(request);

        string? response = reader.ReadLine();

        if (response != null)
        {
            Console.WriteLine();
            Console.WriteLine(response.Replace("\\n", "\n"));
        }
        else
        {
            Console.WriteLine("Server disconnected.");
        }
        return response ?? "Error: No response from server.";
    }
}
