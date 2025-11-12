# Community Issue Reporting System - City Services Hub

## Team Members: Wadiha Boat (ST10303285)
## Video Link : https://youtu.be/kg-91Ma4BgM 

## Overview
This application is a **Windows Forms C# project** that allows users to report issues within their local community as well as track their requested services and stay updated on local events.  
It is split into **three main parts**:
1. **Reporting Issues**
2. **Service Request Tracking and Analysis**
3. **Local Events and Announcements**


## Features

### Main Menu:
This is the home screen basically and gives you access to all the main features.

### Report Issues Form:
Users can report issues in their community using the form. You simply need to fill in:
- **Location**: area where problem is located
- ** Category**: e.g. streetlight, sanitation, etc
- **Description**: what is wrong
- **Attachment** (optional): image or file showing the issue.
A **progress bar** helps you to complete your form so that it is perfect when you submit it.

### Local Events Features:
Users can view all upcoming events and workshops happening in and around their community.
- **Events Panel** - View all upcoming events with details
- **Search and Filter** - Filter events by category, date range, or event name
- **Sort Options** - Sort events by date, category or name.
- **Event Details** - Click an event to see full details, location, contact info, and description.
- **Recommendations** - Panel shows events based on your searches and clicks.
- **Smart Tracking** - The application learns your preferences based on what categories and dates you searched for,what events you clicks and recommends events instantly.

### Service Request Status Feature:
Users can track and analyse their requests through this feature.
It consists of a various options such as:
- **Refresh**: Reload latest data
- **Search for Request by Id**: Find and track a specific request using the request Id
- **Show Priority Order**: Sorts issues by importance using a heap/priority structure.
- **Recent Requests**: View the most recent requests made.
- **Analyse Routes**: Uses graph with Minimum Spanning Tree to show how requests can be linked and optimised by location and category.
Each button opens a embedded card-style popup that makes the data easy to read and visually pleasing.
   
## Data Structures Used:
- **Dictionary**: Used to store and manage event data, allowing for efficient retrieval and updates based on user interactions.
- **SortedDictionary<DateTime, List<Event>>**: Stores events grouped by date.
- **PriorityQueue<Event, int>**: Manages event recommendations based on user preferences and interaction history.
- **List<Event>**: Maintains a list of all events for easy iteration and display.
- **HashSet<string>**: Tracks unique categories and locations for filtering options.
- **Custom Event Class**: Represents event details including name, date, category, location, and description.
- **Custom UserPreferences Class**: Tracks user interactions and preferences to tailor event recommendations.
- **AVL Tree** : To quickly find and display the 10 most recent requests in a sorted order.
- **Min Heap** : To show requests in order of urgency (lowest number = highest priority).
- **Graph (Adjacency List)**: To connect service requests that are similar or nearby.
- **MST (PRIM's algorithm)**: Finds the most efficient path that connect related issues which reduces overlap.
   
### How MST (Graph Analysis) works to make the system better:
The graph connects issues that are related, for example two leaks in the same area or reported around the same time will have a "shorter distance" between them.
Minimum Spanning Tree (MST) on the other hand finds the shortest path that connects all related requests without repeating the same routes.
This helps the municipality see which areas clusters of similar problems, how they can plan routes efficiently and which issues can be handled together.

## Technologies and Tools Used 
- C# (.NET Framework)
- Windows Forms (WinForms)
- Visual Studio 2022
- Custom Classes for data structures.
- System.Drawing for form and UI styling.
- GitHub for version control.

## Build and Run
0. Clone the repo from "https://github.com/VCCT-PROG7312-2025-G2/PROG7312_POE_PART1_ST10303285.git"
1. Launch the application in Visual Studio (C# .NET Windows Forms).
2. Set the startup project to MunicipalServicesApp.
3. Run the application (F5 or Ctrl+F5).
4. The **Main Menu** will appear, access all the features from here.
5. Refer to the video above if confused on how to use the app.


## Changelog from Part 1 & 2 to Past 3
| Area               | Old version                                  | What changed for Part 3 
|-------------------------------------------------------------------|-----------------------------------------------------------------------|
|UI Design           |  Simple form with no customisation           | Added card-based UI with rounded panels and embedded subforms         |
|Data Persistence    |  Only stored issues                          | Used AVL Tree, Priority Heap, Grapgh + MST for advanced operations.   |
|Routing Logic       |  Not implemented                             | Implemented ServiceRouteNetwork with MST calculation.                 |
|Events System       |  Static list                                 | Included filtering, searching and recommendations.                    |
|Error Handling      |  Very little                                 | Added try-catch blocks.                                               |
|-------------------------------------------------------------------------------------------------------------------------------------------|

## KEY LEARNING:
Other than the complex data structures such as AVL Trees, Heaps and Graphs, something else I learnt was to approach problems by breaking them down and trying to understand it in different contexts maybe.I first focused on only getting the features ready but then as i worked, i started thinking about the different structures and how it could be used to ease use of the application as well as how i could make it easier to undersatdn for the users with little to no knowledge of technology. When a function didn’t work, I tracked back to the beginning to see where I could have missed something rather than using github copilot. I tried to understand why it didnt work for example when i implemented the priority heap, i ran into small logical errors, like not linking the the edges properly in the graph so i checked where data was being passed and how the weights were being calculated and whether my methods were returning what i expected.
Overall i started to understand how each part works with each other before moving on which improved my confidence in tackling errors.


## References:
https://learn.microsoft.com/en-us/visualstudio/designers/walkthrough-windows-forms-designer?view=vs-2022
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-9.0
https://medium.com/mesciusinc/the-definitive-guide-to-winforms-controls-f7f12196563a 
https://www.programiz.com/dsa/avl-tree
https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/compiler-messages/overload-resolution?f1url=%3FappId%3Droslyn%26k%3Dk(CS0121)#ambiguous-overloads
https://www.simplilearn.com/tutorials/c-sharp-tutorial/what-is-graphs-in-c-sharp
https://www.programiz.com/dsa/prim-algorithm

## Declaration:
I made use of OpenAI as a helper for doing tasks such as data seeding and fixing minor ui issues. Final work was reviewed by myself.
The chat can be accessed at https://chatgpt.com/share/6914dd4f-5348-8010-81e5-e0f562431828 . 

