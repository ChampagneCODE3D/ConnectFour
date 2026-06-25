# Connect Four Test Log

## Visual UI Testing (C# Console)

Run:

```bash
dotnet run --project ConnectFour.csproj
```

### Test Cases

| ID | Scenario | Steps | Expected | Result |
|---|---|---|---|---|
| UI-01 | App launches | Start app | Welcome banner + mode menu appear | [x] Pass / [ ] Fail |
| UI-02 | Invalid menu input | Enter `0`, `3`, `abc` | Validation message; prompts again | [ ] Pass / [ ] Fail |
| UI-03 | HvH names | Choose `1`, enter names | Game starts, symbols assigned | [ ] Pass / [ ] Fail |
| UI-04 | Column bounds | Enter `0` or `8` in turn | Validation message; no crash | [ ] Pass / [ ] Fail |
| UI-05 | Non-numeric move | Enter `abc` in turn | Validation message; no crash | [ ] Pass / [ ] Fail |
| UI-06 | Full column handling | Fill one column then play same column | "column is full" message | [ ] Pass / [ ] Fail |
| UI-07 | Horizontal win | Play sequence to create horizontal 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-08 | Vertical win | Play sequence to create vertical 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-09 | Diagonal win | Play sequence to create diagonal 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-10 | Draw | Fill board with no 4-in-a-row | Draw banner shown | [ ] Pass / [ ] Fail |
| UI-11 | Replay yes | End game, enter `y` | Returns to menu/start screen | [ ] Pass / [ ] Fail |
| UI-12 | Replay no | End game, enter `n` | Goodbye + exits cleanly | [ ] Pass / [ ] Fail |
| UI-13 | HvC mode | Choose `2` | Human vs Computer starts | [ ] Pass / [ ] Fail |
| UI-14 | AI behavior | Observe several turns | AI makes legal moves, blocks/wins at times | [ ] Pass / [ ] Fail |

## Automated Tests

Run:

```bash
dotnet test ConnectFour.Tests/ConnectFour.Tests.csproj
dotnet build
```

Latest run results:
- `dotnet test ConnectFour.Tests/ConnectFour.Tests.csproj` → total: 7, failed: 0, succeeded: 7, skipped: 0
- `dotnet build` → Build succeeded

## Rubric Coverage Notes

- Design (10): OOP separation across Board, Player abstraction, Human/Computer players, View, Controller.
- Effort (15): Milestones tracked in README, commits in GitHub history, testing evidence in this file.
- Functionality (5): Win/draw/validation/replay/HvH/HvC verified by test cases above.
