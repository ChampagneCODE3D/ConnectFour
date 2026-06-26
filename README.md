# Connect Four — SODV 1202 Term Project

A console-based Connect Four game built in C# using Object-Oriented Programming principles.

**Developer:** ChampagneCODE3D  
**Course:** SODV 1202 — Object-Oriented Programming  

---

## How to Play

1. Run the program
2. Choose a game mode: Human vs Human or Human vs Computer
3. Players take turns entering a column number (1–7) to drop their disc
4. Gameplay screen shows active mode and player names (and AI difficulty in Human vs Computer)
5. First player to connect 4 in a row (horizontal, vertical, or diagonal) wins!

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

## PLC Production Line Analogy

For process-control thinking (PLC style), this project maps as follows:

- `Program.cs` + `GameController.cs` = main production control routine that sequences each cycle.
- `Board.cs` = machine status/state memory table (safe read/write rules).
- `Player.cs` + `HumanPlayer.cs` + `ComputerPlayer.cs` = different stations/actors providing the next action.
- `GameView.cs` = HMI layer for operator prompts, status, and alarm-style feedback.

This is why the project uses multiple `.cs` files instead of one monolithic file: each module has one clear job, like separate PLC routines.

### PLC Poka-yoke (Fail-Safe) UI Fix

A recent UI bug fix was implemented using a **Poka-yoke** mindset:

- Problem: accidental key carryover (especially around `Esc`) could advance prompts unexpectedly.
- Fix: progression gates now require **explicit Enter** for affirmative actions.
- Safety path: `Esc` always routes to an **exit confirmation menu** (Return vs Exit), instead of directly advancing state.

This mirrors PLC production safety patterns: deliberate operator acknowledgment, guarded transitions, and explicit confirmation before changing process state.

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
- [x] Replay with same players/settings (`y`) or open post-game options (`n`)

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
- Replay flow after each game: rematch, return to start menu, quit, and HvC difficulty-change path.

---

## Final Submission Checklist

- [x] Console application runs successfully (`dotnet run`)
- [x] Automated tests pass (`dotnet test`)
- [x] Build is successful (`dotnet build`)
- [x] Manual UI checklist completed in `TESTING.md`
- [ ] Demo video recorded and uploaded
- [ ] Final commit pushed to GitHub
- [ ] Repository link submitted in D2L

---

## AI Tool Declaration (Detailed by File)

This project was developed with assistance from **GitHub Copilot**. All generated suggestions were reviewed, edited, tested, and approved by the developer before submission.

### C# Console Application Files

| File | How AI Assisted | Developer Contribution |
|------|------------------|------------------------|
| `Program.cs` | Suggested entry-point scaffold. | Verified startup flow and controller initialization. |
| `Board.cs` | Assisted with board model structure, drop logic, and win/draw checking patterns. | Validated logic correctness, adjusted behavior, and verified with tests. |
| `Player.cs` | Suggested abstract base contract (`GetMove`). | Confirmed abstraction design and naming consistency. |
| `HumanPlayer.cs` | Assisted with input handling loop and validation structure. | Tightened input UX (strict numeric rules + dynamic available-column prompts/messages). |
| `ComputerPlayer.cs` | Suggested rule-based AI approach (win/block/random). | Reviewed and validated game-play behavior manually. |
| `GameView.cs` | Assisted with menu/board display scaffolding. | Refined menu input behavior to accept only valid mode keys. |
| `GameController.cs` | Assisted with game-loop/controller structure. | Tightened player-name normalization and validation rules. |

### Test and Documentation Files

| File | How AI Assisted | Developer Contribution |
|------|------------------|------------------------|
| `ConnectFour.Tests/UnitTest1.cs` | Suggested xUnit test case scaffolds for board behaviors. | Selected test coverage, ran tests, and confirmed passing results. |
| `TESTING.md` | Assisted with manual test checklist structure. | Executed and documented validation workflow. |
| `README.md` | Assisted with documentation formatting and rubric mapping language. | Finalized wording, milestone status, and submission evidence. |

### Web Port Files (Hosted on GitHub Pages)

| File | How AI Assisted | Developer Contribution |
|------|------------------|------------------------|
| `docs/index.html` | Suggested web UI layout scaffold. | Reviewed structure and game controls for assignment goals. |
| `docs/style.css` | Suggested styling baseline for board and controls. | Adjusted visual layout and readability. |
| `docs/app.js` | Assisted with JavaScript port of core game logic and simple AI behavior. | Verified behavior parity with C# version and prepared for GitHub Pages hosting. |

> Note: The live hosted web version is implemented in **JavaScript** (for static GitHub Pages hosting), not Java.

> AI was used as a productivity and learning aid. Final design decisions, validation, testing, and submission responsibility remain with the developer.

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
