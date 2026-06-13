# Connect Four — SODV 1202 Term Project

A console-based Connect Four game built in C# using Object-Oriented Programming principles.

**Developer:** ChampagneCODE3D  
**Course:** SODV 1202 — Object-Oriented Programming  

---

## How to Play

1. Run the program
2. Choose a game mode: Human vs Human or Human vs Computer
3. Players take turns entering a column number (1–7) to drop their disc
4. First player to connect 4 in a row (horizontal, vertical, or diagonal) wins!

---

## Project Structure

| File | Class | Description |
|------|-------|-------------|
| `Board.cs` | `Board` | Game grid, drop logic, win/draw detection |
| `Player.cs` | `Player` (abstract) | Base class for all player types |
| `HumanPlayer.cs` | `HumanPlayer` | Reads column input from console |
| `ComputerPlayer.cs` | `ComputerPlayer` | Rule-based AI opponent |
| `GameView.cs` | `GameView` | All console display / UI output |
| `GameController.cs` | `GameController` | Game loop, turn management, state flow |
| `Program.cs` | — | Entry point |

---

## OOP Concepts Used

- **Abstraction** — `Player` abstract class defines the contract for all player types
- **Encapsulation** — `Board` hides internal grid state; only exposes safe methods
- **Inheritance** — `HumanPlayer` and `ComputerPlayer` both extend `Player`
- **Polymorphism** — `GameController` calls `player.GetMove()` without knowing if it's human or AI

---

## Task Breakdown

### Milestone 3 — Repo Setup ✅
- [x] Create GitHub repository
- [x] Initialize project with .NET 10
- [x] Add README and .gitignore
- [x] Make initial commit

### Milestone 4 — Core Classes
- [ ] Implement `Board` class (grid, drop piece, win/draw check)
- [ ] Implement `Player` abstract class
- [ ] Implement `HumanPlayer` class
- [ ] Implement `ComputerPlayer` class with basic AI
- [ ] Implement `GameView` class
- [ ] Implement `GameController` class

### Milestone 5 — Two-Player Mode
- [ ] Human vs Human works end-to-end
- [ ] Win detection working for all directions
- [ ] Draw detection working
- [ ] Play again / return to start screen

### Milestone 6 — AI Mode (Optional)
- [ ] Human vs Computer mode selectable from menu
- [ ] AI blocks opponent winning moves
- [ ] AI takes winning moves when available

### Milestone 7 — Final Submission
- [ ] Full game runs without crashes
- [ ] Code is clean and commented
- [ ] Final commit pushed to GitHub
- [ ] Demo video recorded

---

## How to Run

```bash
dotnet run
```
