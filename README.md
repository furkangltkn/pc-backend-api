# PC Backend Server

Backend server that manages communication between the UI and Raspberry Pi, processing telemetry data and forwarding control commands.

## Description
This project provides the communication layer between a user interface (UI) and a Raspberry Pi.  
It receives telemetry data from the Raspberry Pi, processes it, and sends it to the UI.  
It also forwards control commands from the UI to the Raspberry Pi.

## Features
- Bidirectional communication between UI and Raspberry Pi
- Telemetry data processing
- Command forwarding from UI to Raspberry Pi
- ASP.NET Core Web API based backend
- Logging system for system events and errors

## Technologies
- ASP.NET Core Web API
- C#
- TCP / Socket Communication
- Raspberry Pi

## Usage
1. Run the backend server.
2. Connect the Raspberry Pi client to the backend.
3. Connect the UI client to the backend.
4. Monitor telemetry data and send control commands via the UI.

## Notes
Developed as part of a TEKNOFEST autonomous vehicle project.
Backend server that manages communication between the UI and Raspberry Pi, processing telemetry data and forwarding control commands.
