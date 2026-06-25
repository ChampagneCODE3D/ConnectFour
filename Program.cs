using ConnectFour;

/// <summary>
/// Entry point for the Connect Four game.
/// SODV 1202 - Object-Oriented Programming Term Project
/// 
/// This project demonstrates key OOP principles:
/// - ABSTRACTION: Player abstract class defines the contract for all player types
/// - ENCAPSULATION: Board class hides internal grid implementation
/// - INHERITANCE: HumanPlayer and ComputerPlayer extend Player base class
/// - POLYMORPHISM: GetMove() method behaves differently for each player type
/// </summary>

GameController game = new GameController();
game.Start();
