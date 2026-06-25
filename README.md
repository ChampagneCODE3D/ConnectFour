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

### Milestone 4 — Core Classes ✅
- [x] Implement `Board` class (grid, drop piece, win/draw check)
- [x] Implement `Player` abstract class
- [x] Implement `HumanPlayer` class
- [x] Implement `ComputerPlayer` class with basic AI
- [x] Implement `GameView` class
- [x] Implement `GameController` class

### Milestone 5 — Two-Player Mode ✅
- [x] Human vs Human works end-to-end
- [x] Win detection working for all directions
- [x] Draw detection working
- [x] Play again / return to start screen

### Milestone 6 — AI Mode (Optional) ✅
- [x] Human vs Computer mode selectable from menu
- [x] AI blocks opponent winning moves
- [x] AI takes winning moves when available

### Milestone 7 — Final Submission
- [x] Full game runs without crashes
- [x] Code is clean and commented
- [ ] Final commit pushed to GitHub
- [ ] Demo video recorded

---

## Rubric Coverage (SODV 1202)

### A) Design (10 Marks)
- Clean class structure with clear responsibilities (`Board`, `Player`, `HumanPlayer`, `ComputerPlayer`, `GameView`, `GameController`).
- Abstraction via `Player` abstract class.
- Encapsulation via private board grid and controlled board operations.
- Inheritance via human/computer player subclasses.
- Polymorphism via shared `GetMove(Board board)` contract.

### B) Effort (15 Marks)
- Milestones tracked with completion checklist in this README.
- Public GitHub repository with iterative commits.
- Automated tests and manual UI test checklist documented in `TESTING.md`.
- Codebase follows consistent naming and style.

### C) Functionality (5 Marks)
- 7x6 board with move validation.
- Win detection for horizontal, vertical, and both diagonal directions.
- Draw detection.
- Human vs Human and Human vs Computer game modes.
- Replay/start-screen flow after each game.

---

## Final Submission Checklist

- [x] Console application runs successfully (`dotnet run`)
- [x] Automated tests pass (`dotnet test`)
- [x] Build is successful (`dotnet build`)
- [ ] Manual UI checklist completed in `TESTING.md`
- [ ] Demo video recorded and uploaded
- [ ] Final commit pushed to GitHub
- [ ] Repository link submitted in D2L

---

## AI Tool Declaration

This project was developed with the assistance of **GitHub Copilot** (an AI programming assistant by Microsoft/GitHub).

| What AI was used for | What the student did |
|----------------------|----------------------|
| Setting up the GitHub repository and git structure | Reviewed, approved, and submitted all steps |
| Generating the initial OO design plan and class breakdown | Evaluated the design against assignment requirements |
| Writing boilerplate/scaffold code for classes | Will review, understand, modify, and extend all code |
| Suggesting task breakdown and milestone structure | Made final decisions on scope and priorities |

> All code submitted is understood by the developer. AI was used as a learning and productivity tool, not to bypass understanding of the material.

---

## Testing

Run automated tests for core board logic:

```bash
dotnet test ConnectFour.Tests/ConnectFour.Tests.csproj
```

## Web Version (JavaScript + GitHub Pages)

A browser version of the game is included in the `docs/` folder.

### Run locally

Open `docs/index.html` in your browser.

### Publish on GitHub Pages

1. Push the repository to GitHub.
2. In repository settings, open **Pages**.
3. Set source to **Deploy from a branch**.
4. Select branch `master` and folder `/docs`.
5. Save. Your live game will be available at:

`https://champagnecode3d.github.io/ConnectFour/`

## How to Run (Console C# App)

```bash
dotnet run
```
