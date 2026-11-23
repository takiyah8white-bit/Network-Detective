
using System;
using System.Collections.Generic;

class CaseManager
{
    public string CaseTitle = "A Deadly Arrangement";

    public string CaseDescription =
        "Victim: Vanessa Cole, 34, a successful real estate agent.\n" +
        "Location: Her apartment in downtown Los Angeles.\n" +
        "Time of Death: Approximately 9:00 PM.\n\n" +
        "Vanessa was found murdered inside her apartment.\n" +
        "Neighbors reported an argument earlier that evening.\n" +
        "Her phone is missing from the crime scene.";

    public Dictionary<string, string> Suspects = new()
    {
        {
            "1",
            "Daniel Brooks - Vanessa's ex-boyfriend.\n" +
            "Statement: I argued with Vanessa, but I left around 8:30 PM."
        },
        {
            "2",
            "Rachel Morgan - Vanessa's business partner.\n" +
            "Statement: I was working late at the office."
        },
        {
            "3",
            "Ethan Price - Vanessa's neighbor.\n" +
            "Statement: I heard an argument, but I never left my apartment."
        }
    };

    public Dictionary<string, string> Evidence = new()
    {
        {
            "1",
            "Security footage shows Daniel leaving at 8:32 PM."
        },
        {
            "2",
            "Vanessa's phone was missing from the apartment."
        },
        {
            "3",
            "A broken wine glass was discovered near the body."
        },
        {
            "4",
            "Office records show Rachel left work at 8:15 PM."
        },
        {
            "5",
            "A building access log shows Rachel entered at 8:47 PM."
        }
    };

    private readonly string correctSuspect = "2";

    public string GetBriefing()
    {
        return "CAPTAIN MORGAN HAYES:\n\n" +
               "Detective, we've got a homicide.\n" +
               "Review the evidence, question the suspects,\n" +
               "and tell me who you think is responsible.\n\n" +
               CaseTitle + "\n\n" + CaseDescription;
    }

    public string GetSuspects()
    {
        string result = "SUSPECT INTERVIEWS:\n\n";

        foreach (var suspect in Suspects)
        {
            result += suspect.Key + ". " + suspect.Value + "\n\n";
        }

        return result;
    }


public string InterviewSuspect(string suspectId)
{
    if (Suspects.TryGetValue(suspectId, out string? statement))
    {
        return "======= INTERROGATION ROOM =======\n\n" +
               statement + "\n\n" +
               "Interview recorded in case file.";
    }

    return "Suspect not found. Please select a valid suspect.";


}



public string AskFollowUp(string suspectId, bool hasEvidence)
{
    if (suspectId == "1")
    {
        if (hasEvidence)
        {
            return "DETECTIVE:\n" +
                   "Security footage shows you leaving at 8:32 PM. " +
                   "What happened before you left?\n\n" +
                   "DANIEL BROOKS:\n" +
                   "We argued, okay? Vanessa was upset about something " +
                   "at work. But she was alive when I left.\n\n" +
                   "CAPTAIN HAYES:\n" +
                   "An argument gives us a possible motive. Keep investigating.";
        }

        return "DANIEL BROOKS:\n" +
               "I already told you when I left.\n\n" +
               "You need more evidence to question his timeline.";
    }

    if (suspectId == "2")
    {
        if (hasEvidence)
        {
            return "DETECTIVE:\n" +
                   "Office records show you left at 8:15 PM.\n\n" +
                   "RACHEL MORGAN:\n" +
                   "Fine! I left early. I had a meeting " +
                   "I didn't want anyone knowing about.\n\n" +
                   "CAPTAIN HAYES:\n" +
                   "Interesting. Keep digging, Detective.";
        }

        return "RACHEL MORGAN:\n" +
               "I've already told you. I was at the office.\n\n" +
               "You need more evidence to challenge her statement.";
    }

    if (suspectId == "3")
    {
        if (hasEvidence)
        {
            return "DETECTIVE:\n" +
                   "We found a broken wine glass. " +
                   "Did you hear anything besides arguing?\n\n" +
                   "ETHAN PRICE:\n" +
                   "I heard glass shatter. Then everything went quiet. " +
                   "I didn't want to get involved.\n\n" +
                   "CAPTAIN HAYES:\n" +
                   "He may know more than he's telling us.";
        }

        return "ETHAN PRICE:\n" +
               "I heard an argument, that's all.\n\n" +
               "You need more evidence to question his statement.";
    }

    return "Invalid suspect selection.";
}


    public string GetEvidence()
    {
        string result = "CRIME SCENE EVIDENCE:\n\n";

        foreach (var clue in Evidence)
        {
            result += clue.Key + ". " + clue.Value + "\n\n";
        }

        return result;
    }

    public bool CheckAccusation(string suspect)
    {
        return suspect == correctSuspect;
    }


public string ExamineEvidence(string evidenceId)
{
    if (Evidence.TryGetValue(evidenceId, out string? clue))
    {
        return "EVIDENCE #" + evidenceId + "\n\n" +
               clue + "\n\n" +
               "Evidence added to your case file.";
    }

    return "Evidence not found. Please select a valid item.";
}

}
