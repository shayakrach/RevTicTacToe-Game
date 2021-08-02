using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameLogic;

namespace Ex5.WindowsGUI
{
    public class FormGame : Form
    {
        private const int k_CellButtonSize = 50;
        private const int k_CellButtonSizeWithBorder = 55;
        private const int k_FormPaddiingSize = 5;
        private const int k_ScoresAreaHeightSize = 35;
        private FormSettings m_FormSettings = new FormSettings();
        private RevTicTacToe m_Game;
        private Button[,] m_CellsButtons;
        private Label m_Player1NameLabel = new Label();
        private Label m_Player2NameLabel = new Label();

        public FormGame()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "RevTicTacToe";
            this.BackColor = Color.White;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.Size = new Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            bool isSettingsValid = ensureValidSettings();

            if(isSettingsValid)
            {
                startGame();
            }
            else
            {
                this.Close();
            }
        }

        private bool ensureValidSettings()
        {
            bool isSettingsFull = false;

            if(m_FormSettings.ShowDialog() == DialogResult.OK)
            {
                isSettingsFull = true;
            }

            return isSettingsFull;
        }

        private void startGame()
        {
            createGame();
            formResize();
            drawCellsButtons();
            drawScoresArea();
            startNewRound();
        }

        private void createGame()
        {
            Player player1;
            Player player2;
            BoardGame board;
            bool isComputer = m_FormSettings.IsAgainstComputer;

            board = new BoardGame(m_FormSettings.BoardSize);

            player1 = new Player(m_FormSettings.Player1Name, BoardGame.eCoin.Player1);

            player2 = new Player(m_FormSettings.Player2Name, BoardGame.eCoin.Player2, isComputer);

            m_Game = new RevTicTacToe(board, player1, player2);

            m_Game.Board.CellChanged += board_CellChanged;
            m_Game.StatusChanged += game_StatusChanged;
            m_Game.TurnChanged += game_TurnChanged;
        }

        private void formResize()
        {
            int clientWidth = (m_Game.Board.Size * k_CellButtonSizeWithBorder) + k_FormPaddiingSize;
            int clientHeight = clientWidth + k_ScoresAreaHeightSize;

            this.ClientSize = new Size(clientWidth, clientHeight);
        }

        private void drawCellsButtons()
        {
            int boardSize = m_Game.Board.Size;
            m_CellsButtons = new Button[boardSize, boardSize];

            for(int i = 0; i < boardSize; i++)
            {
                for(int j = 0; j < boardSize; j++)
                {
                    initializeCellButton(i, j);
                }
            }
        }

        private void initializeCellButton(int i_Row, int i_Col)
        {
            m_CellsButtons[i_Row, i_Col] = new Button();
            m_CellsButtons[i_Row, i_Col].Enabled = true;
            m_CellsButtons[i_Row, i_Col].Text = string.Empty;
            m_CellsButtons[i_Row, i_Col].Font = new Font("ariel", 20);
            m_CellsButtons[i_Row, i_Col].Height = k_CellButtonSize;
            m_CellsButtons[i_Row, i_Col].Width = k_CellButtonSize;
            m_CellsButtons[i_Row, i_Col].BackColor = Color.White;
            m_CellsButtons[i_Row, i_Col].Click += cellButton_click;
            initializeCellButtonPosition(i_Row, i_Col);
            this.Controls.Add(m_CellsButtons[i_Row, i_Col]);
        }

        private void initializeCellButtonPosition(int i_Row, int i_Col)
        {
            m_CellsButtons[i_Row, i_Col].Top = k_FormPaddiingSize;
            m_CellsButtons[i_Row, i_Col].Left = k_FormPaddiingSize;

            if(i_Row > 0)
            {
                m_CellsButtons[i_Row, i_Col].Top += m_CellsButtons[i_Row - 1, i_Col].Bottom;
            }

            if(i_Col > 0)
            {
                m_CellsButtons[i_Row, i_Col].Left += m_CellsButtons[i_Row, i_Col - 1].Right;
            }
        }

        private void drawScoresArea()
        {
            updateScores();

            initializeScoresLabel(m_Player1NameLabel, FontStyle.Bold);
            initializeScoresLabel(m_Player2NameLabel, FontStyle.Regular);

            int player1NameLabelWidthPosition = (this.ClientSize.Width - (m_Player1NameLabel.Width + m_Player2NameLabel.Width)) / 2;
            int playersNameLabelheightPosition = this.ClientSize.Height - 30;

            m_Player1NameLabel.Location = new Point(player1NameLabelWidthPosition, playersNameLabelheightPosition);
            m_Player2NameLabel.Location = new Point(m_Player1NameLabel.Right + k_FormPaddiingSize, playersNameLabelheightPosition);
        }

        private void initializeScoresLabel(Label i_PlayerNameLabel, FontStyle i_FontStyle)
        {
            i_PlayerNameLabel.AutoSize = true;
            i_PlayerNameLabel.Font = new Font(i_PlayerNameLabel.Font, i_FontStyle);
            this.Controls.Add(i_PlayerNameLabel);
        }

        private void cellButton_click(object sender, EventArgs e)
        {
            Position nextMove;

            nextMove = getButtonPosition(sender as Button);
            m_Game.NextTurn(nextMove, m_Game.GetCurrentPlayerCoin());

            if(m_Game.Status != RevTicTacToe.eStatus.ExitGame && m_Game.IsCurrentPlayerComputer())
            {
                nextMove = ComputerLogic.GetNextPosition(m_Game.Board, BoardGame.eCoin.Player2);
                m_Game.NextTurn(nextMove, BoardGame.eCoin.Player2);
            }
        }

        private void game_TurnChanged()
        {
            boldCurrentPlayerScore();
        }

        private void game_StatusChanged()
        {
            roundEndsMsg();
        }

        private void board_CellChanged(int i_Row, int i_Col, BoardGame.eCoin i_Coin)
        {
            m_CellsButtons[i_Row, i_Col].Text = getCoinStr(i_Coin);

            m_CellsButtons[i_Row, i_Col].Enabled = i_Coin == BoardGame.eCoin.Empty;
        }

        private Position getButtonPosition(Button i_Button)
        {
            Position tempPosition = new Position(0, 0);

            while(!m_Game.Board.IsPosOutOfRange(tempPosition))
            {
                if(m_CellsButtons[tempPosition.Row, tempPosition.Col] == i_Button)
                {
                    break;
                }

                tempPosition.SetNext(m_Game.Board.Size);
            }

            return tempPosition;
        }

        private void updateGameNameAndRoundCaption()
        {
            this.Text = string.Format("RevTicTacToe - Round {0}", m_Game.RoundNumber);
        }

        private string getCoinStr(BoardGame.eCoin i_Coin)
        {
            string coinStr = string.Empty;

            switch(i_Coin)
            {
                case BoardGame.eCoin.Player1:
                    coinStr = "X";
                    break;
                case BoardGame.eCoin.Player2:
                    coinStr = "O";
                    break;
                case BoardGame.eCoin.Empty:
                    coinStr = string.Empty;
                    break;
            }

            return coinStr;
        }

        private void roundEndsMsg()
        {
            RevTicTacToe.eStatus gameStatus = m_Game.Status;

            if(gameStatus != RevTicTacToe.eStatus.ExitGame && gameStatus != RevTicTacToe.eStatus.NotFinished)
            {
                string messageBoxCaption = "A Win!";
                string messageBoxText = getMessageBoxText(ref messageBoxCaption);

                if(MessageBox.Show(messageBoxText, messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.No)
                {
                    this.Close();
                    m_Game.Status = RevTicTacToe.eStatus.ExitGame;
                }
                else
                {
                    startNewRound();
                }
            }
        }

        private string getMessageBoxText(ref string io_MessageBoxCaption)
        {
            StringBuilder messageBoxText = new StringBuilder();

            switch(m_Game.Status)
            {
                case RevTicTacToe.eStatus.Player1Won:
                    messageBoxText.AppendLine(winnerMsg(m_Game.Player1.Name));
                    break;
                case RevTicTacToe.eStatus.Player2Won:
                    messageBoxText.AppendLine(winnerMsg(m_Game.Player2.Name));
                    break;
                case RevTicTacToe.eStatus.Tie:
                    messageBoxText.AppendLine("Tie!");
                    io_MessageBoxCaption = "A Tie!";
                    break;
            }

            messageBoxText.AppendLine("Would you like to play another round?");

            return messageBoxText.ToString();
        }

        private void updateScores()
        {
            m_Player1NameLabel.Text = getScoresLableName(m_Game.Player1.Name, m_Game.Player1.Score);
            m_Player2NameLabel.Text = getScoresLableName(m_Game.Player2.Name, m_Game.Player2.Score);
        }

        private string getScoresLableName(string i_PlayerName, double i_PlayerScore)
        {
            return string.Format("{0}: {1}", i_PlayerName, i_PlayerScore);
        }

        private string winnerMsg(string i_WinnerName)
        {
            return string.Format("The winner is {0}!", i_WinnerName);
        }

        private void boldCurrentPlayerScore()
        {
            if(m_Game.CurrentTurnIsPlayer1)
            {
                m_Player1NameLabel.Font = new Font(m_Player1NameLabel.Font, FontStyle.Bold);
                m_Player2NameLabel.Font = new Font(m_Player2NameLabel.Font, FontStyle.Regular);
            }
            else
            {
                m_Player1NameLabel.Font = new Font(m_Player1NameLabel.Font, FontStyle.Regular);
                m_Player2NameLabel.Font = new Font(m_Player2NameLabel.Font, FontStyle.Bold);
            }
        }

        private void startNewRound()
        {
            m_Game.NewRound();
            updateGameNameAndRoundCaption();
            updateScores();
        }
    }
}
