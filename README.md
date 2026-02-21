# Prelim02_Tavera

Name: Rhence Bryan E. Tavera
Course and Block: BSIT 3.2
Scenario: C - The "Eco-Grid" Energy Distributor
Instructor: Ms. Justin Louise R. Neypes

## Programs Used
- Visual Studio 2022	Main IDE for writing and running the C# code
- .NET Framework / .NET Core	Framework used to build and execute the application
- Git	Version control for tracking code changes
- GitHub	Online repository for storing and sharing code
- Windows Terminal / Command Prompt	Running git commands and pushing to GitHub
- eraser.io	Creating UML class diagram
- Google Docs	Writing the PDF documentation

## Software Versions
- IDE: Visual Studio Community 2022 (Version 17.0)
- Framework: .NET 6.0
- Git: Git 2.40
- OS: Windows 11

## Project Description
- An energy distribution system that manages different power sources:
- Base Class: PowerSource (SourceID, BaseOutput)
- Sub-Classes: SolarPanel (Sunlight %) and WindTurbine (Wind Speed)
- Features: GenerateReport() overloaded for summary/detailed view
- Exception: ArgumentException for negative weather variables
- Extra Feature: History log that saves all reports to file
