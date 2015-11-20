# PingPongCsharp

## PongC# Game next Gen. 

PongC# is a simple that is based on the traditionnal Pong Game. We only create a bluetooth connection between two laptops. The ball go to one screen to a another. You can supprise your opponent by playing difficult move. 

# Why the language C# ?

PongC# is a school project. The objective of our project is to learn C# language. 
With [LirycMoaners](https://github.com/LirycMoaners), we decide to take an mytic game and update it with new technology as bluetooth. 

# Library 

PongC# use [32feet's library](https://32feet.codeplex.com/), that allowed us to support bluetooth technology. 
We create all interactions inside the program. 
We also use the Entity's Framework for the score management. 

# How does it work ?

PongC# is based on client/server architecture. When this game starts, you have the opportunity to choose which of you is the server and the client. 

# Architecture 

PingPongC#
|
|------>Class
|----------->Balle.cs --> Ball object
|----------->ClientClass.cs --> Client for bluetooth connection
|----------->DataTransit.cs --> Object that will be send between computer
|----------->Program.cs --> Main
|----------->ServerClass.cs --> Server for bluetooth connection
|----------->SingletonDb.cs --> Singleton that assura that we only have a unique connection into the database.
|------>Windows
|----------->About.cs --> About Us panel
|----------->ConfigurationPanel.cs --> Configuration panel is a the panel that create the link between the Server/Client
|----------->Partie.cs --> Game panel
|----------->ScoreResult.cs --> Score Panel
|----------->Welcome.cs --> Menu / Main panel
