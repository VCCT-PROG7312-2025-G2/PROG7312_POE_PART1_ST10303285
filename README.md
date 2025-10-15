# Community Issue Reporting System

## Overview
This application is a **Windows Forms C# project** that allows users to report issues within their local community as well as stay updated on local events.  
It forms Part 2 of the POE and implements the **Report Issues** feature as well as **Local Events and Announcements** feature.

## Features
### General Features:
- **Main Menu (Form)** – Startup menu that provides access to different sections (Report Issues,Local Events and Announcements, etc).
- **Report Issues Form** – Users can submit details about problems in their area.

### Local Events Features:
- **Events Panel** - View all upcoming events with details
- **Search and Filter** - Filter events by category, date range, or event name
- **Sort Options** - Sort events by date, category or name.
- **Event Details** - Click an event to see full details, location, contact info, and description.
- **Recommendations** - Panel shows events based on your searches and clicks.
- **Smart Tracking** - The application learns your preferences based on what categories and dates you searched for,what events you clicks and recommends events instantly.


## Build and Run

1. Launch the application in Visual Studio (C# .NET Windows Forms).
2. Set the startup project to MunicipalServicesApp.
3. Run the application (F5 or Ctrl+F5).
4. The **Main Menu** will appear – select "Report Issues."
5. Fill in the following fields:
   - Location
   - Category
   - Description (must be detailed, >25 characters recommended)
   - Attachment (optional, but encouraged)
6. Watch the **progress bar** update as you provide more details.
7. Submit the issue – feedback messages confirm success or errors.
8. Go back to main menu, choose "Local Events and Announcements"
9. In the Local Events Form:
   - Search, filter, or sort events.
   - Click events to see details
   - Watch your Recommended for You panel update in real time.
   
## Data Structures Used:
- **Dictionary**: Used to store and manage event data, allowing for efficient retrieval and updates based on user interactions.
- **SortedDictionary<DateTime, List<Event>>**: Stores events grouped by date.
- **PriorityQueue<Event, int>**: Manages event recommendations based on user preferences and interaction history.
- **List<Event>**: Maintains a list of all events for easy iteration and display.
- **HashSet<string>**: Tracks unique categories and locations for filtering options.
- **Custom Event Class**: Represents event details including name, date, category, location, and description.
- **Custom UserPreferences Class**: Tracks user interactions and preferences to tailor event recommendations.
   
## Requirements
- Visual Studio 2019/2022
- .NET Framework (Windows Forms project type)
- Windows OS


## References:
https://learn.microsoft.com/en-us/visualstudio/designers/walkthrough-windows-forms-designer?view=vs-2022
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-9.0
https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.priorityqueue-2?view=net-9.0
https://medium.com/mesciusinc/the-definitive-guide-to-winforms-controls-f7f12196563a 
