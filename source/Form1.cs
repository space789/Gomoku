using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    public partial class Form1 : Form
    {
        private Game game = new Game();
        private int[] center = new int[1];
        private bool start = false;
        private bool playWithPeople = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.start == true && playWithPeople == true ||
                this.start == true && game.CurrentPlayer != game.ComputerPlayer)
            {
                Piece piece = game.PlaceAPiece(e.X, e.Y);
                PieceComputer number = game.Number(e.X, e.Y);
                if (piece != null)
                {
                    this.Controls.Add(number);
                    this.Controls.Add(piece);
                    displayNumberVisible();

                    lookPosition();
                }


                //檢查是否有人獲勝
                Winner();
                /*檢查是否有快要贏
                AlmostWinner();*/
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (game.CanBePlaced(e.X, e.Y) && this.start == true)
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
            if (this.start == true && playWithPeople == false && game.CurrentPlayer == game.ComputerPlayer)
            {
                Piece piece = game.ComputerPlaceAPiece();
                PieceComputer ComputerNumber = game.ComputerNumber();
                if (piece != null)
                {
                    this.Controls.Add(ComputerNumber);
                    this.Controls.Add(piece);
                    displayNumberVisible();

                    lookPosition();
                }

                //檢查是否有人獲勝
                Winner();
                //檢查是否有快要贏
                // AlmostWinner();
            }
            if ((PlayWithPeople.Checked == true && BlackFirst.Checked == true && this.start == false
                || PlayWithPeople.Checked == true && WhiteFirst.Checked == true && this.start == false
                || PlayWithComputer.Checked == true && ComputerAfter.Checked == true && BlackFirst.Checked == true && this.start == false
                || PlayWithComputer.Checked == true && ComputerAfter.Checked == true && WhiteFirst.Checked == true && this.start == false
                || PlayWithComputer.Checked == true && ComputerFirst.Checked == true && BlackFirst.Checked == true && this.start == false
                || PlayWithComputer.Checked == true && ComputerFirst.Checked == true && WhiteFirst.Checked == true && this.start == false)
                && game.Winner == PieceType.NONE)
                Start.Enabled = true;
            else
                Start.Enabled = false;
        }
        public void CleanPiece(int a)
        {
            for (int x = Board.PlaceNodeCountRead; x >= Board.PlaceNodeCountRead - a; x--)
            {
                foreach (Control item in this.Controls)
                {
                    if (item.Name == "PiecePictureBox" + x)
                    {
                        this.Controls.Remove(item);
                        break;
                    }
                }
                foreach (Control item in this.Controls)
                {
                    if (item.Name == "NumberLabel" + x)
                    {
                        this.Controls.Remove(item);
                        break;
                    }
                }
            }
        }

        public void Winner()
        {
            if (game.Winner == PieceType.BLACK && playWithPeople == true)
            {
                MessageBox.Show("黑色獲勝");
                this.start = false;
                //newRound();
            }
            else if (game.Winner == PieceType.WHITE && playWithPeople == true)
            {
                MessageBox.Show("白色獲勝");
                this.start = false;
                //newRound();
            }
            else if (game.Winner == game.ComputerPlayer && game.Winner != PieceType.NONE && playWithPeople == false)
            {
                MessageBox.Show("電腦獲勝");
                this.start = false;
                //newRound();
            }
            else if (game.Winner != game.ComputerPlayer && game.Winner != PieceType.NONE && playWithPeople == false)
            {
                MessageBox.Show("您獲勝");
                this.start = false;
                //newRound();
            }
        }

        public void AlmostWinner()
        {
            if (game.AlmostWinner == PieceType.WHITE)
                Position.Items.Add("白色快贏了");
            else if (game.AlmostWinner == PieceType.BLACK)
                Position.Items.Add("黑色快贏了");

        }

        private void Previous_MouseDown(object sender, MouseEventArgs e)
        {
            if (Board.PlaceNodeCountRead > 0)
            {
                CleanPiece(1);
                game.Previous(1);
                if (game.CurrentPlayer == PieceType.BLACK)
                    game.SetCurrentPlayer(PieceType.WHITE);
                else if (game.CurrentPlayer == PieceType.WHITE)
                    game.SetCurrentPlayer(PieceType.BLACK);
                Position.Items.RemoveAt(Board.PlaceNodeCountRead);
            }
        }

        private void Previous_MouseMove(object sender, MouseEventArgs e)
        {
            if (Board.PlaceNodeCountRead > 0)
                Previous.Cursor = Cursors.Hand;
            else
                Previous.Cursor = Cursors.Default;

        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
            this.start = true;
            End.Enabled = true;
            WhoFirstPanel.Enabled = false;
            PlayWithWhoPanel.Enabled = false;
            ComputerPanel.Enabled = false;
            if (PlayWithPeople.Checked == true)
                playWithPeople = true;
            else
                playWithPeople = false;
            if (BlackFirst.Checked == true)
                game.SetCurrentPlayer(PieceType.BLACK);
            else
                game.SetCurrentPlayer(PieceType.WHITE);
            if (PlayWithPeople.Checked == false && ComputerFirst.Checked == true && BlackFirst.Checked == true)
                game.SetComputerPlayer(PieceType.BLACK);
            else if (PlayWithPeople.Checked == false && ComputerFirst.Checked == true && WhiteFirst.Checked == true)
                game.SetComputerPlayer(PieceType.WHITE);
            else if (PlayWithPeople.Checked == false && ComputerAfter.Checked == true && BlackFirst.Checked == true)
                game.SetComputerPlayer(PieceType.WHITE);
            else if (PlayWithPeople.Checked == false && ComputerAfter.Checked == true && WhiteFirst.Checked == true)
                game.SetComputerPlayer(PieceType.BLACK);
        }




        private void End_Click(object sender, EventArgs e)
        {
            newRound();
        }
        private void newRound()
        {
            End.Enabled = false;
            Start.Enabled = true;
            WhoFirstPanel.Enabled = true;
            PlayWithWhoPanel.Enabled = true;
            ComputerPanel.Enabled = false;
            PlayWithPeople.Checked = false;
            PlayWithComputer.Checked = false;
            ComputerAfter.Checked = false;
            ComputerFirst.Checked = false;
            BlackFirst.Checked = false;
            WhiteFirst.Checked = false;
            CleanPiece(Board.PlaceNodeCountRead);
            game.NewRound();
            this.start = false;
            Position.Items.Clear();
        }

        private void lookPosition()
        {
            center = game.LookLastPiece();
            if (game.CurrentPlayer == PieceType.BLACK && playWithPeople == true)
                Position.Items.Add(Board.PlaceNodeCountRead + "." + "白棋下在: " + center[0] + " , " + center[1]);
            else if (game.CurrentPlayer == PieceType.WHITE && playWithPeople == true)
                Position.Items.Add(Board.PlaceNodeCountRead + "." + "黑棋下在: " + center[0] + " , " + center[1]);
            else if (game.CurrentPlayer != game.ComputerPlayer && playWithPeople == false)
                Position.Items.Add(Board.PlaceNodeCountRead + "." + "電腦下在: " + center[0] + " , " + center[1]);
            else if (game.CurrentPlayer == game.ComputerPlayer && playWithPeople == false)
                Position.Items.Add(Board.PlaceNodeCountRead + "." + "您下在: " + center[0] + " , " + center[1]);
        }

        private void PlayWithComputer_CheckedChanged(object sender, EventArgs e)
        {
            ComputerPanel.Enabled = true;
        }

        private void PlayWithPeople_CheckedChanged(object sender, EventArgs e)
        {
            ComputerPanel.Enabled = false;
            ComputerFirst.Checked = false;
            ComputerAfter.Checked = false;
        }

        private void DisplayNumber_CheckedChanged(object sender, EventArgs e)
        {
            displayNumberVisible();
        }
        private void displayNumberVisible()
        {
            if (DisplayNumber.Checked)
            {
                for (int x = Board.PlaceNodeCountRead; x >= 0; x--)
                {
                    foreach (Control item in this.Controls)
                    {
                        if (item.Name == "NumberLabel" + x)
                        {
                            item.Visible = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int x = Board.PlaceNodeCountRead; x >= 0; x--)
                {
                    foreach (Control item in this.Controls)
                    {
                        if (item.Name == "NumberLabel" + x)
                        {
                            item.Visible = false;
                            break;
                        }
                    }
                }
            }
        }
    }
}
