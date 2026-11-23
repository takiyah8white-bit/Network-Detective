
# 🔎 Network Detective
### A C# Client-Server Murder Mystery Game

Network Detective is an interactive detective mystery game developed in C# using .NET and TCP socket programming.

Players take on the role of a homicide detective investigating a murder by gathering evidence, interviewing suspects, challenging alibis, and making accusations.

The project demonstrates client-server communication through a playable investigation.

## 🎮 Storyline

### Episode 1: A Deadly Arrangement

Vanessa Cole, a successful real estate agent, is found murdered inside her downtown Los Angeles apartment.

Three suspects are brought in for questioning:

- Daniel Brooks — Vanessa's ex-boyfriend
- Rachel Morgan — Vanessa's business partner
- Ethan Price — Vanessa's neighbor

As the investigation progresses, the detective must examine evidence, identify inconsistencies, and determine who is responsible.

But solving the murder may uncover something much bigger...

**To Be Continued.**

## 🕵️ Game Features

- Interactive command-line investigation
- Three selectable difficulty levels
- Suspect interviews and follow-up questioning
- Evidence collection and examination
- Evidence-dependent interrogation responses
- Detective evidence board
- Accusation system
- Detective rankings based on accusation attempts
- Cliffhanger ending

## 💻 Technologies Used

- C#
- .NET
- TCP Sockets
- Visual Studio Code
- Git and GitHub

## ⚙️ How to Run

### Requirements
- .NET SDK
- A terminal or Visual Studio Code

### 1. Clone the repository

```bash
git clone https://github.com/takiyah8white-bit/Network-Detective.git
cd Network-Detective
```

### 2. Start the Server

Open a terminal and run:

```bash
dotnet run --project Server
```

The Server listens for connections on port 9050.

### 3. Start the Client

Open a second terminal and run:

```bash
dotnet run --project Client
```

Follow the on-screen menu to begin investigating.

## 🌐 How Client-Server Communication Works

Network Detective uses TCP sockets to exchange information between two separate C# applications.

The Client sends investigation requests to the Server, including:

- `BRIEFING`
- `INTERVIEW`
- `EXAMINE`
- `FOLLOWUP`
- `BOARD`
- `ACCUSE`

The Server processes each request, retrieves the appropriate case information, and sends a response back to the Client.

The Client then displays that response to the player.

## 🚧 Future Development

This project currently focuses on Episode 1.

Planned improvements include:

- Additional murder mystery cases
- Expanded difficulty-based investigations
- More complex suspect interactions
- Connected storylines and a larger conspiracy
- Persistent detective rankings and progress tracking
- Additional evidence and investigation mechanics

## 👩🏽‍💻 About the Project

Network Detective was created as a Computer Science Network Programming project to demonstrate practical TCP client-server communication through an interactive game.

The goal was to combine technical networking concepts with creative storytelling and problem-solving.
