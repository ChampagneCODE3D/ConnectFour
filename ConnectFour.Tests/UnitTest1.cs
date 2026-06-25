namespace ConnectFour.Tests;

public class BoardTests
{
    [Fact]
    public void DropPiece_ShouldPlaceAtBottomOfColumn()
    {
        Board board = new();

        bool success = board.DropPiece(1, 'X');
        char[,] grid = board.GetGrid();

        Assert.True(success);
        Assert.Equal('X', grid[5, 0]);
    }

    [Fact]
    public void DropPiece_ShouldStackPiecesInSameColumn()
    {
        Board board = new();

        board.DropPiece(1, 'X');
        board.DropPiece(1, 'O');
        char[,] grid = board.GetGrid();

        Assert.Equal('X', grid[5, 0]);
        Assert.Equal('O', grid[4, 0]);
    }

    [Fact]
    public void IsColumnAvailable_ShouldReturnFalseWhenColumnIsFull()
    {
        Board board = new();

        for (int i = 0; i < 6; i++)
        {
            board.DropPiece(1, i % 2 == 0 ? 'X' : 'O');
        }

        Assert.False(board.IsColumnAvailable(1));
    }

    [Fact]
    public void CheckWin_ShouldDetectHorizontalWin()
    {
        Board board = new();

        board.DropPiece(1, 'X');
        board.DropPiece(2, 'X');
        board.DropPiece(3, 'X');
        board.DropPiece(4, 'X');

        Assert.True(board.CheckWin('X'));
    }

    [Fact]
    public void CheckWin_ShouldDetectVerticalWin()
    {
        Board board = new();

        for (int i = 0; i < 4; i++)
        {
            board.DropPiece(1, 'O');
        }

        Assert.True(board.CheckWin('O'));
    }

    [Fact]
    public void CheckWin_ShouldDetectDiagonalWin()
    {
        Board board = new();

        board.DropPiece(1, 'X');

        board.DropPiece(2, 'O');
        board.DropPiece(2, 'X');

        board.DropPiece(3, 'O');
        board.DropPiece(3, 'O');
        board.DropPiece(3, 'X');

        board.DropPiece(4, 'O');
        board.DropPiece(4, 'O');
        board.DropPiece(4, 'O');
        board.DropPiece(4, 'X');

        Assert.True(board.CheckWin('X'));
    }

    [Fact]
    public void IsFull_ShouldReturnTrueWhenBoardIsFilled()
    {
        Board board = new();

        for (int col = 1; col <= 7; col++)
        {
            for (int row = 0; row < 6; row++)
            {
                board.DropPiece(col, (row + col) % 2 == 0 ? 'X' : 'O');
            }
        }

        Assert.True(board.IsFull());
    }
}
