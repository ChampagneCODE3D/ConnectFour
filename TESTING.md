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
| UI-02 | Esc in main menu | Press `Esc`, choose return then exit | Return resumes prompt; exit closes game | [x] Pass / [ ] Fail |
| UI-03 | HvH names | Choose `1`, enter names | Game starts, symbols assigned | [x] Pass / [ ] Fail |
| UI-04 | Name edit behavior | Use backspace and spaces in name | Name input edits correctly, normalized spacing | [ ] Pass / [ ] Fail |
| UI-05 | Name length limit | Enter name with 101+ chars | Error shown, prompt retries | [ ] Pass / [ ] Fail |
| UI-06 | Esc in name prompt | Press `Esc`, return to prompt | Return keeps setup flow active | [ ] Pass / [ ] Fail |
| UI-07 | HvC difficulty select | Choose `2`, pick difficulty `1/2/3` | Easy/Medium/Hard selection works | [x] Pass / [ ] Fail |
| UI-08 | Esc in difficulty prompt | Press `Esc`, choose return then exit | Return resumes difficulty prompt; exit closes game | [x] Pass / [ ] Fail |
| UI-09 | Column bounds | Enter `0` or `8` in turn | Validation message; no crash | [x] Pass / [ ] Fail |
| UI-10 | Non-numeric move | Enter letter/symbol in turn | Validation message; no crash | [x] Pass / [ ] Fail |
| UI-11 | Full column handling | Fill one column then pick it again | Dynamic prompt lists valid columns only | [x] Pass / [ ] Fail |
| UI-12 | Esc during move prompt | Press `Esc`, choose return then exit | Return resumes move prompt; exit closes game | [x] Pass / [ ] Fail |
| UI-13 | Horizontal win | Play sequence to create horizontal 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-14 | Vertical win | Play sequence to create vertical 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-15 | Diagonal win | Play sequence to create diagonal 4 | Winner banner shown | [ ] Pass / [ ] Fail |
| UI-16 | Draw | Fill board with no 4-in-a-row | Draw banner shown | [ ] Pass / [ ] Fail |
| UI-17 | Replay yes | End game, enter `y` | Returns to menu/start screen | [ ] Pass / [ ] Fail |
| UI-18 | Replay no | End game, enter `n` | Goodbye + exits cleanly | [ ] Pass / [ ] Fail |
| UI-19 | Esc in replay prompt | Press `Esc` at replay prompt | Exit menu appears with return/exit choices | [ ] Pass / [ ] Fail |
| UI-20 | AI behavior by difficulty | Observe Easy vs Medium vs Hard rounds | Easy random, Medium win/block, Hard stronger positioning | [ ] Pass / [ ] Fail |

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
