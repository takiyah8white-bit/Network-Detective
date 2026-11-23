
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

class Program
{
    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Loopback, 9050);
        CaseManager caseManager = new CaseManager();

        server.Start();

        Console.WriteLine("=================================");
        Console.WriteLine("   NETWORK DETECTIVE SERVER");
        Console.WriteLine("=================================");
        Console.WriteLine("Server running on port 9050...");
        Console.WriteLine("Waiting for a detective...");

        while (true)
        {
            using TcpClient client = server.AcceptTcpClient();

            Console.WriteLine("Detective connected!");

            using NetworkStream stream = client.GetStream();
            using StreamReader reader = new StreamReader(stream);
            using StreamWriter writer = new StreamWriter(stream)
            {
                AutoFlush = true
            };

            writer.WriteLine("Welcome to Network Detective!");

            int attempts = 0;
            bool caseSolved = false;
            HashSet<string> discoveredEvidence = new HashSet<string>();

            string? message;

            while ((message = reader.ReadLine()) != null)
            {
                Console.WriteLine("Request received: " + message);

                
string response;



if (message.StartsWith("ACCUSE:"))
{
    string suspect = message.Substring(7);

    if (caseSolved)
    {
        response = "CASE ALREADY CLOSED!\n" +
                   "You've already solved this investigation.\n" +
                   "Your detective ranking has been finalized.";
    }
    else if (!caseManager.Suspects.ContainsKey(suspect))
    {
        response = "Invalid suspect selection.";
    }
    else
    {
        attempts++;

        if (caseManager.CheckAccusation(suspect))
        {
            caseSolved = true;

            string rank;

            if (attempts == 1)
                rank = "MASTER DETECTIVE";
            else if (attempts == 2)
                rank = "SENIOR INVESTIGATOR";
            else if (attempts == 3)
                rank = "DETECTIVE";
            else
                rank = "ROOKIE DETECTIVE";

            
            response = "========== CASE CLOSED ==========\n\n" +
                    "Rachel Morgan has been arrested for the murder of Vanessa Cole.\n\n" +
                    "Excellent work, Detective!\n" +
                    "Attempts: " + attempts + "\n" +
                    "Final Ranking: " + rank + "\n\n" +
                    "CAPTAIN MORGAN HAYES:\n" +
                    "Outstanding work. But something doesn't add up.\n\n" +
                    "DETECTIVE:\n" +
                    "What do you mean, Captain?\n\n" +
                    "CAPTAIN MORGAN HAYES:\n" +
                    "We recovered a message connected to Vanessa's missing phone.\n\n" +
                    "UNKNOWN SENDER:\n" +
                    "Rachel was never supposed to get caught.\n" +
                    "Find out what the detective knows.\n\n" +
                    "CAPTAIN MORGAN HAYES:\n" +
                    "Detective... I don't think Rachel was working alone.\n\n" +
                    "=================================\n" +
                    "       TO BE CONTINUED...\n" +
                    "=================================";

        }
        else
        {
            response = "INCORRECT ACCUSATION!\n" +
                       "Attempts: " + attempts + "\n" +
                       "Continue investigating, Detective.";
        }
    }
}



else if (message.StartsWith("EXAMINE:"))
{
    string evidenceId = message.Substring(8);

    if (caseManager.Evidence.ContainsKey(evidenceId))
    {
        discoveredEvidence.Add(evidenceId);

        response = caseManager.ExamineEvidence(evidenceId);
    }
    else
    {
        response = "Evidence not found.";
    }
}


else if (message.StartsWith("INTERVIEW:"))
{
    string suspectId = message.Substring(10);

    response = caseManager.InterviewSuspect(suspectId);
}



else if (message.StartsWith("FOLLOWUP:"))
{
    string suspectId = message.Substring(9);

    string requiredEvidence = suspectId switch
    {
        "1" => "1",
        "2" => "4",
        "3" => "3",
        _ => ""
    };

    bool hasEvidence = requiredEvidence != "" &&
                       discoveredEvidence.Contains(requiredEvidence);

    response = caseManager.AskFollowUp(suspectId, hasEvidence);
}



else if (message == "BOARD")
{
    if (discoveredEvidence.Count == 0)
    {
        response = "EVIDENCE BOARD\n\n" +
                   "No evidence collected yet.\n" +
                   "Examine the crime scene to discover clues.";
    }
    else
    {
        response = "========== EVIDENCE BOARD ==========\n\n";

        foreach (string evidenceId in discoveredEvidence.OrderBy(
                     id => int.Parse(id)))
        {
            response += "Evidence #" + evidenceId + "\n" +
                        caseManager.Evidence[evidenceId] + "\n\n";
        }

        response += "Total Clues Discovered: " +
                    discoveredEvidence.Count;
    }
}

else
{
    response = message switch
    {
        "BRIEFING" => caseManager.GetBriefing(),
        "SUSPECTS" => caseManager.GetSuspects(),
        "EVIDENCE" => caseManager.GetEvidence(),
        _ => "Unknown investigation request."
    };
}


                // Replace line breaks so each response
                // travels as one line over TCP.
                writer.WriteLine(response.Replace("\r", "")
                                         .Replace("\n", "\\n"));
            }

            Console.WriteLine("Detective disconnected.");
        }
    }
}

