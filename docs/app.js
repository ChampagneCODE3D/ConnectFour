const ROWS = 6;
const COLS = 7;

const boardEl = document.getElementById('board');
const columnButtonsEl = document.getElementById('columnButtons');
const statusEl = document.getElementById('status');
const modeEl = document.getElementById('mode');
const newGameBtn = document.getElementById('newGameBtn');

let grid = [];
let currentPlayer = 'X';
let gameOver = false;

function createEmptyGrid() {
  return Array.from({ length: ROWS }, () => Array(COLS).fill(' '));
}

function initBoardUi() {
  boardEl.innerHTML = '';
  columnButtonsEl.innerHTML = '';

  for (let c = 0; c < COLS; c++) {
    const btn = document.createElement('button');
    btn.textContent = (c + 1).toString();
    btn.addEventListener('click', () => handleMove(c));
    columnButtonsEl.appendChild(btn);
  }

  for (let r = 0; r < ROWS; r++) {
    for (let c = 0; c < COLS; c++) {
      const cell = document.createElement('div');
      cell.className = 'cell';
      cell.id = `cell-${r}-${c}`;
      boardEl.appendChild(cell);
    }
  }
}

function resetGame() {
  grid = createEmptyGrid();
  currentPlayer = 'X';
  gameOver = false;
  updateStatus();
  render();
}

function updateStatus(message) {
  if (message) {
    statusEl.textContent = message;
    return;
  }

  if (gameOver) {
    return;
  }

  statusEl.textContent = `Current turn: ${currentPlayer}`;
}

function render() {
  for (let r = 0; r < ROWS; r++) {
    for (let c = 0; c < COLS; c++) {
      const cell = document.getElementById(`cell-${r}-${c}`);
      const value = grid[r][c];
      cell.className = 'cell';
      if (value === 'X') cell.classList.add('x');
      if (value === 'O') cell.classList.add('o');
    }
  }
}

function getAvailableRow(col) {
  for (let r = ROWS - 1; r >= 0; r--) {
    if (grid[r][col] === ' ') return r;
  }
  return -1;
}

function dropPiece(col, symbol) {
  const row = getAvailableRow(col);
  if (row === -1) return false;
  grid[row][col] = symbol;
  return true;
}

function isFull() {
  return grid[0].every(cell => cell !== ' ');
}

function checkWin(symbol) {
  for (let r = 0; r < ROWS; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (grid[r][c] === symbol && grid[r][c + 1] === symbol && grid[r][c + 2] === symbol && grid[r][c + 3] === symbol) return true;
    }
  }

  for (let r = 0; r <= ROWS - 4; r++) {
    for (let c = 0; c < COLS; c++) {
      if (grid[r][c] === symbol && grid[r + 1][c] === symbol && grid[r + 2][c] === symbol && grid[r + 3][c] === symbol) return true;
    }
  }

  for (let r = 0; r <= ROWS - 4; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (grid[r][c] === symbol && grid[r + 1][c + 1] === symbol && grid[r + 2][c + 2] === symbol && grid[r + 3][c + 3] === symbol) return true;
    }
  }

  for (let r = 3; r < ROWS; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (grid[r][c] === symbol && grid[r - 1][c + 1] === symbol && grid[r - 2][c + 2] === symbol && grid[r - 3][c + 3] === symbol) return true;
    }
  }

  return false;
}

function cloneGrid(source) {
  return source.map(row => [...row]);
}

function findWinningMove(symbol) {
  for (let c = 0; c < COLS; c++) {
    const row = getAvailableRow(c);
    if (row === -1) continue;

    const testGrid = cloneGrid(grid);
    testGrid[row][c] = symbol;

    if (checkWinOnGrid(testGrid, symbol)) {
      return c;
    }
  }
  return -1;
}

function checkWinOnGrid(testGrid, symbol) {
  for (let r = 0; r < ROWS; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (testGrid[r][c] === symbol && testGrid[r][c + 1] === symbol && testGrid[r][c + 2] === symbol && testGrid[r][c + 3] === symbol) return true;
    }
  }

  for (let r = 0; r <= ROWS - 4; r++) {
    for (let c = 0; c < COLS; c++) {
      if (testGrid[r][c] === symbol && testGrid[r + 1][c] === symbol && testGrid[r + 2][c] === symbol && testGrid[r + 3][c] === symbol) return true;
    }
  }

  for (let r = 0; r <= ROWS - 4; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (testGrid[r][c] === symbol && testGrid[r + 1][c + 1] === symbol && testGrid[r + 2][c + 2] === symbol && testGrid[r + 3][c + 3] === symbol) return true;
    }
  }

  for (let r = 3; r < ROWS; r++) {
    for (let c = 0; c <= COLS - 4; c++) {
      if (testGrid[r][c] === symbol && testGrid[r - 1][c + 1] === symbol && testGrid[r - 2][c + 2] === symbol && testGrid[r - 3][c + 3] === symbol) return true;
    }
  }

  return false;
}

function getComputerMove() {
  const winningMove = findWinningMove('O');
  if (winningMove !== -1) return winningMove;

  const blockingMove = findWinningMove('X');
  if (blockingMove !== -1) return blockingMove;

  const validCols = [];
  for (let c = 0; c < COLS; c++) {
    if (getAvailableRow(c) !== -1) validCols.push(c);
  }

  return validCols[Math.floor(Math.random() * validCols.length)];
}

function handleMove(col) {
  if (gameOver) return;
  if (!dropPiece(col, currentPlayer)) {
    updateStatus('That column is full. Choose another one.');
    return;
  }

  render();

  if (checkWin(currentPlayer)) {
    gameOver = true;
    updateStatus(`Player ${currentPlayer} wins!`);
    return;
  }

  if (isFull()) {
    gameOver = true;
    updateStatus("It's a draw!");
    return;
  }

  currentPlayer = currentPlayer === 'X' ? 'O' : 'X';
  updateStatus();

  if (modeEl.value === 'hvc' && currentPlayer === 'O' && !gameOver) {
    setTimeout(() => {
      const computerCol = getComputerMove();
      handleMove(computerCol);
    }, 350);
  }
}

newGameBtn.addEventListener('click', resetGame);

initBoardUi();
resetGame();
