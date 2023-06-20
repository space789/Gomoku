using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gomoku
{
    class Game
    { 
        private Board board = new Board();

        private PieceType currentPlayer = PieceType.NONE;
        private PieceType computerPlayer = PieceType.NONE;
        private PieceType Player = PieceType.NONE;

        private int[] ComputerIdX = new int[289], ComputerIdY = new int[289];
        private int ComputerNumberIdX, ComputerNumberIdY;
        private int ComputerScore, DetermineComputerScore;
        private int ComputerScoreRepeatCount, ComputerScoreRepeatRandom;
        private int ComputerDirCanPutPieceCount;
        private bool ComputerCanNotPutPiece;

        private PieceType winner = PieceType.NONE;
        private PieceType almostWinner = PieceType.NONE;


        public PieceType Winner { get { return winner; } }
        public PieceType AlmostWinner { get { return almostWinner; } }
        public PieceType CurrentPlayer { get { return currentPlayer; } }
        public PieceType ComputerPlayer { get { return computerPlayer; } }

        public bool CanBePlaced(int x, int y)
        {
            return board.CanBePlaced(x, y);
        }
        public Piece PlaceAPiece(int x,int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                CheckWinner();
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;
                return piece;
            }
            return null;
        }

        public PieceComputer Number(int x,int y)
        {
            PieceComputer number = board.Number(x, y);
            return number;
        }

        public PieceComputer ComputerNumber()
        {
            PieceComputer computernumber = board.ComputerNumber(ComputerNumberIdX, ComputerNumberIdY);
            return computernumber;
        }

        public Piece ComputerPlaceAPiece()
        {
            while (currentPlayer == computerPlayer)
            {
                //演算法
                //如果場上沒棋子先下中間
                if (board.PlaceNodeCount == 0)
                {
                    Piece piece = board.CumputerPlaceAPiece(8, 8, computerPlayer);
                    ComputerNumberIdX = ComputerNumberIdY = 8;
                    if (currentPlayer == PieceType.BLACK)
                        currentPlayer = PieceType.WHITE;
                    else if (currentPlayer == PieceType.WHITE)
                        currentPlayer = PieceType.BLACK;
                    return piece;
                }
                //預設座標基本分數
                DetermineComputerScore = 0;
                //相同分數計數
                ComputerScoreRepeatCount = 0;
                //玩家棋子顏色
                if (computerPlayer == PieceType.BLACK)
                    Player = PieceType.WHITE;
                if (computerPlayer == PieceType.WHITE)
                    Player = PieceType.BLACK;

                for (int i = 0;i < board.PlaceNodeCount;i++)
                {
                int centerX = board.LastPlaceNode[i].X;
                int centerY = board.LastPlaceNode[i].Y;
                    for (int xDir = 1; xDir >= -1; xDir--)
                    {
                        //檢查八個不同方向
                        for (int yDir = -1; yDir <= 1; yDir++)
                        {
                            //更新座標點分數
                            ComputerScore = 0;
                            //預設空間足夠
                            ComputerCanNotPutPiece = false;
                            //預設可以擺棋
                            ComputerDirCanPutPieceCount = 0;

                            //更新預測座標
                            centerX += xDir;
                            centerY += yDir;

                            //不能預測棋子在棋盤外
                            if (centerX < 0 || centerX >= Board.NODE_COUNT ||
                               centerY < 0 || centerY >= Board.NODE_COUNT)
                            {
                                //提早回歸座標
                                centerX -= xDir;
                                centerY -= yDir;
                                continue;
                            }

                            //判斷預測棋位是否有棋子
                            if(board.GetPieceType(centerX, centerY) != PieceType.NONE)
                            {
                                //提早回歸座標
                                centerX -= xDir;
                                centerY -= yDir;
                                continue;
                            }
                            for (int xDir2 = 1; xDir2 >= 0; xDir2--)
                            {
                                for(int yDir2 = -1; yDir2 <= 1; yDir2++)
                                {
                                    //排除中間的情況
                                    if (xDir == 0 && yDir == 0)
                                        continue;
                                    if (xDir2 == 0 && yDir2 == 0)
                                        continue;
                                    if (xDir2 == 0 && yDir2 == 1)
                                        continue;

                                    //紀錄現在看到幾顆相同的棋子
                                    int count1 = 1;
                                    int count2 = 1;
                                    //判斷空間
                                    while (count1 < 5)
                                    {
                                        int targetX = centerX + count1 * xDir2;
                                        int targetY = centerY + count1 * yDir2;

                                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                                           targetY < 0 || targetY >= Board.NODE_COUNT ||
                                           board.GetPieceType(targetX, targetY) == Player)
                                            break;

                                        count1++;
                                    }
                                    //判斷空間
                                    while (count2 < 5)
                                    {
                                        int targetX = centerX - count2 * xDir2;
                                        int targetY = centerY - count2 * yDir2;

                                        if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                                           targetY < 0 || targetY >= Board.NODE_COUNT ||
                                           board.GetPieceType(targetX, targetY) == Player)
                                            break;

                                        count2++;
                                    }
                                    //判斷空間夠不夠
                                    if (count1 + count2 - 1 < 5)
                                        ComputerDirCanPutPieceCount++;
                                    //給予每個節點(Node)的評分
                                    switch (lookPosition(centerX, centerY, xDir, yDir))
                                    {
                                        case 1:
                                            ComputerScore += 1;
                                            break;
                                        case 2:
                                            ComputerScore += 20;
                                            break;
                                        case 3:
                                            ComputerScore += 100;
                                            break;
                                        case 4:
                                            ComputerScore += 1000;
                                            break;
                                        case 5:
                                            ComputerScore += 1000000;
                                            break;
                                    }
                                }  
                            }
                            if (ComputerDirCanPutPieceCount == 2)
                                ComputerCanNotPutPiece = true;
                            //判斷分數高低
                            if (ComputerScore >= DetermineComputerScore && ComputerCanNotPutPiece == false)
                            {
                                if (ComputerScore == DetermineComputerScore)
                                    ComputerScoreRepeatCount++;
                                if (ComputerScore > DetermineComputerScore)
                                    ComputerScoreRepeatCount = 0;
                                ComputerIdX[ComputerScoreRepeatCount] = centerX;
                                ComputerIdY[ComputerScoreRepeatCount] = centerY;
                                DetermineComputerScore = ComputerScore;
                            }

                            //回歸座標
                            centerX -= xDir;
                            centerY -= yDir;
                        }
                    }

                }
                //電腦下棋
                Random random = new Random();
                ComputerScoreRepeatRandom = random.Next(0, ComputerScoreRepeatCount);
                Piece pieces = board.CumputerPlaceAPiece(ComputerIdX[ComputerScoreRepeatRandom], ComputerIdY[ComputerScoreRepeatRandom], computerPlayer);
                ComputerNumberIdX = ComputerIdX[ComputerScoreRepeatRandom];
                ComputerNumberIdY = ComputerIdY[ComputerScoreRepeatRandom];
                CheckWinner();
                if (currentPlayer == PieceType.BLACK)
                    currentPlayer = PieceType.WHITE;
                else if (currentPlayer == PieceType.WHITE)
                    currentPlayer = PieceType.BLACK;
                return pieces;
            }
            return null;
        }
        public int[] LookLastPiece()
        {
            int[] center = new int[2];
            if (board.PlaceNodeCount > 0)
            {
                center[0] = board.LastPlaceNode[board.PlaceNodeCount - 1].X;
                center[1] = board.LastPlaceNode[board.PlaceNodeCount - 1].Y;
            }
            return center;
        }

        public void Previous(int x)
        {
            for (int i = 0; i < x; i++)
                board.pieces[board.LastPlaceNode[board.PlaceNodeCount - 1 - i].X, board.LastPlaceNode[board.PlaceNodeCount - 1 - i].Y] = null;
            board.Previous(x);
        }

        public void SetCurrentPlayer(PieceType type)
        {
            if (type == PieceType.BLACK)
                currentPlayer = PieceType.BLACK;
            else if (type == PieceType.WHITE)
                currentPlayer = PieceType.WHITE;
        }

        public void SetComputerPlayer(PieceType type)
        {
            if (type == PieceType.BLACK)
                computerPlayer = PieceType.BLACK;
            else if (type == PieceType.WHITE)
                computerPlayer = PieceType.WHITE;
        }

        private void CheckWinner()
        {
            int centerX = board.LastPlaceNode[board.PlaceNodeCount - 1].X;
            int centerY = board.LastPlaceNode[board.PlaceNodeCount - 1].Y;

            for (int xDir = 1; xDir >= 0; xDir--)
            {
                //檢查八個不同方向
                for (int yDir = -1; yDir <= 1; yDir++)
                {
                    //排除中間的情況
                    if (xDir == 0 && yDir == 0)
                        continue;

                    //檢查是否看到五顆棋子
                    if (lookPosition(centerX, centerY, xDir, yDir) >= 5)
                        winner = currentPlayer;
                }
            }
        }

        //紀錄現在看到幾顆相同的棋子
        private int lookPosition(int centerX,int centerY,int xDir,int yDir)
        {
            int count1 = 1;
            int count2 = 1;
            while (count1 < 5)
            {
                int targetX = centerX + count1 * xDir;
                int targetY = centerY + count1 * yDir;

                if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                    targetY < 0 || targetY >= Board.NODE_COUNT ||
                    board.GetPieceType(targetX, targetY) != currentPlayer)
                    break;

                count1++;
            }
            //同時判斷左右
            while (count2 < 5)
            {
                int targetX = centerX - count2 * xDir;
                int targetY = centerY - count2 * yDir;

                if (targetX < 0 || targetX >= Board.NODE_COUNT ||
                    targetY < 0 || targetY >= Board.NODE_COUNT ||
                    board.GetPieceType(targetX, targetY) != currentPlayer)
                    break;

                count2++;
            }
            return count1 + count2 - 1;
        }

        public void NewRound()
        {
            winner = PieceType.NONE;
            for (int x = 0; x < Board.NODE_COUNT; x++)
            {
                for (int y = 0; y < Board.NODE_COUNT; y++) 
                {
                    board.pieces[x, y] = null;
                }
            }
            currentPlayer = PieceType.NONE;
            almostWinner = PieceType.NONE;
            computerPlayer = PieceType.NONE;
            board.CleanlastPlaceNode();
            
        }
    }
}
