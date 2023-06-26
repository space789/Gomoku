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

        private int ComputerNumberIdX, ComputerNumberIdY;

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
        public Piece PlaceAPiece(int x, int y)
        {
            Piece piece = board.PlaceAPiece(x, y, currentPlayer);
            if (piece != null)
            {
                CheckWinner();
                currentPlayer = (currentPlayer == PieceType.BLACK) ? PieceType.WHITE : PieceType.BLACK;
                return piece;
            }
            return null;
        }

        public PieceComputer Number(int x, int y)
        {
            PieceComputer number = board.Number(x, y);
            return number;
        }

        public PieceComputer ComputerNumber()
        {
            PieceComputer computernumber = board.ComputerNumber(ComputerNumberIdX, ComputerNumberIdY);
            return computernumber;
        }

        ////////////////////////////////////
        public Piece ComputerPlaceAPiece()
        {
            if (board.PlaceNodeCount == 0)
            {
                Piece initialPiece = board.ComputerPlaceAPiece(8, 8, computerPlayer);
                ComputerNumberIdX = ComputerNumberIdY = 8;
                currentPlayer = (currentPlayer == PieceType.BLACK) ? PieceType.WHITE : PieceType.BLACK;
                return initialPiece;
            }

            // 儲存最佳位置和對應的分數
            int bestScore = int.MinValue;
            List<int[]> bestPositions = new List<int[]>();

            for (int x = 0; x < Board.NODE_COUNT; x++)
            {
                for (int y = 0; y < Board.NODE_COUNT; y++)
                {
                    // 確保該位置沒有棋子
                    if (board.GetPieceType(x, y) != PieceType.NONE)
                        continue;

                    // 評估該位置的分數
                    int score = EvaluatePosition(x, y, computerPlayer);

                    // 如果該位置分數比目前最佳分數高，則更新最佳位置和分數
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestPositions.Clear();
                        bestPositions.Add(new int[] { x, y });
                    }
                    else if (score == bestScore)
                    {
                        bestPositions.Add(new int[] { x, y });
                    }
                }
            }

            if (bestScore == 0)
            {
                Random random_node = new Random();
                int Random_X, Random_Y;
                bestPositions.Clear();
                do
                {
                    Random_X = random_node.Next(7, 9);
                    Random_Y = random_node.Next(7, 9);
                } while (board.GetPieceType(Random_X, Random_Y) != PieceType.NONE);
                bestPositions.Add(new int[] { Random_X, Random_Y });
            }

            // 從最佳位置中隨機選擇一個位置下棋
            Random random = new Random();
            int randomIndex = random.Next(0, bestPositions.Count);
            int[] bestPosition = bestPositions[randomIndex];

            int bestX = bestPosition[0];
            int bestY = bestPosition[1];

            Piece piece = board.ComputerPlaceAPiece(bestX, bestY, computerPlayer);
            ComputerNumberIdX = bestX;
            ComputerNumberIdY = bestY;
            CheckWinner();
            currentPlayer = (currentPlayer == PieceType.BLACK) ? PieceType.WHITE : PieceType.BLACK;
            return piece;
        }

        private int EvaluatePosition(int x, int y, PieceType player)
        {
            int score = 0;

            // 計算在水平、垂直和對角線方向上的連線數量
            int horizontalCount = CountConsecutivePieces(x, y, 1, 0, player) + CountConsecutivePieces(x, y, -1, 0, player);
            int verticalCount = CountConsecutivePieces(x, y, 0, 1, player) + CountConsecutivePieces(x, y, 0, -1, player);
            int diagonalCount1 = CountConsecutivePieces(x, y, 1, 1, player) + CountConsecutivePieces(x, y, -1, -1, player);
            int diagonalCount2 = CountConsecutivePieces(x, y, 1, -1, player) + CountConsecutivePieces(x, y, -1, 1, player);

            // 加權計算連線數量得分
            score += CalculateScore(horizontalCount);
            score += CalculateScore(verticalCount);
            score += CalculateScore(diagonalCount1);
            score += CalculateScore(diagonalCount2);

            // 計算防守分數
            int def_horizontalCount = CountConsecutivePieces(x, y, 1, 0, GetOpponent(player)) + CountConsecutivePieces(x, y, -1, 0, GetOpponent(player));
            int def_verticalCount = CountConsecutivePieces(x, y, 0, 1, GetOpponent(player)) + CountConsecutivePieces(x, y, 0, -1, GetOpponent(player));
            int def_diagonalCount1 = CountConsecutivePieces(x, y, 1, 1, GetOpponent(player)) + CountConsecutivePieces(x, y, -1, -1, GetOpponent(player));
            int def_diagonalCount2 = CountConsecutivePieces(x, y, 1, -1, GetOpponent(player)) + CountConsecutivePieces(x, y, -1, 1, GetOpponent(player));

            // 加權計算防守連線數量得分
            score += CalculateDefensiveScore(def_horizontalCount);
            score += CalculateDefensiveScore(def_verticalCount);
            score += CalculateDefensiveScore(def_diagonalCount1);
            score += CalculateDefensiveScore(def_diagonalCount2);

            // 評估棋形
            score += EvaluateChessPattern(x, y, player);

            return score;
        }

        private int EvaluateChessPattern(int x, int y, PieceType player)
        {
            int score = 0;

            // 雙活三
            if (IsDoubleLiveThree(x, y, player))
            {
                score += 5000;
            }

            // 雙活四
            if (IsDoubleLiveFour(x, y, player))
            {
                score += 10000;
            }

            return score;
        }

        private bool IsDoubleLiveThree(int x, int y, PieceType player)
        {
            // 檢查水平方向
            if (CountConsecutivePieces(x, y, 1, 0, player) + CountConsecutivePieces(x, y, -1, 0, player) == 3 &&
                CountConsecutivePieces(x, y, 2, 0, PieceType.NONE) + CountConsecutivePieces(x, y, -2, 0, PieceType.NONE) >= 2)
            {
                return true;
            }

            // 檢查垂直方向
            if (CountConsecutivePieces(x, y, 0, 1, player) + CountConsecutivePieces(x, y, 0, -1, player) == 3 &&
                CountConsecutivePieces(x, y, 0, 2, PieceType.NONE) + CountConsecutivePieces(x, y, 0, -2, PieceType.NONE) >= 2)
            {
                return true;
            }

            // 檢查對角線方向
            if (CountConsecutivePieces(x, y, 1, 1, player) + CountConsecutivePieces(x, y, -1, -1, player) == 3 &&
                CountConsecutivePieces(x, y, 2, 2, PieceType.NONE) + CountConsecutivePieces(x, y, -2, -2, PieceType.NONE) >= 2)
            {
                return true;
            }

            if (CountConsecutivePieces(x, y, 1, -1, player) + CountConsecutivePieces(x, y, -1, 1, player) == 3 &&
                CountConsecutivePieces(x, y, 2, -2, PieceType.NONE) + CountConsecutivePieces(x, y, -2, 2, PieceType.NONE) >= 2)
            {
                return true;
            }

            return false;
        }

        private bool IsDoubleLiveFour(int x, int y, PieceType player)
        {
            // 檢查水平方向
            if (CountConsecutivePieces(x, y, 1, 0, player) + CountConsecutivePieces(x, y, -1, 0, player) == 4 &&
                CountConsecutivePieces(x, y, 2, 0, PieceType.NONE) + CountConsecutivePieces(x, y, -2, 0, PieceType.NONE) >= 2)
            {
                return true;
            }

            // 檢查垂直方向
            if (CountConsecutivePieces(x, y, 0, 1, player) + CountConsecutivePieces(x, y, 0, -1, player) == 4 &&
                CountConsecutivePieces(x, y, 0, 2, PieceType.NONE) + CountConsecutivePieces(x, y, 0, -2, PieceType.NONE) >= 2)
            {
                return true;
            }

            // 檢查對角線方向
            if (CountConsecutivePieces(x, y, 1, 1, player) + CountConsecutivePieces(x, y, -1, -1, player) == 4 &&
                CountConsecutivePieces(x, y, 2, 2, PieceType.NONE) + CountConsecutivePieces(x, y, -2, -2, PieceType.NONE) >= 2)
            {
                return true;
            }

            if (CountConsecutivePieces(x, y, 1, -1, player) + CountConsecutivePieces(x, y, -1, 1, player) == 4 &&
                CountConsecutivePieces(x, y, 2, -2, PieceType.NONE) + CountConsecutivePieces(x, y, -2, 2, PieceType.NONE) >= 2)
            {
                return true;
            }

            return false;
        }

        private int CountConsecutivePieces(int startX, int startY, int dirX, int dirY, PieceType player)
        {
            int count = 0;
            int x = startX;
            int y = startY;

            x += dirX;
            y += dirY;

            // 向指定方向搜索連線數量，並避免被堵住路
            while (x >= 0 && x < Board.NODE_COUNT && y >= 0 && y < Board.NODE_COUNT)
            {
                if (board.GetPieceType(x, y) == GetOpponent(player))
                {
                    break; // 被對手棋子堵住路，停止搜索
                }

                if (board.GetPieceType(x, y) == PieceType.NONE)
                {
                    // 遇到空位，停止搜索，不計算這個位置
                    return count;
                }

                if (board.GetPieceType(x, y) == player)
                {
                    count++;
                }

                x += dirX;
                y += dirY;
            }

            return count;
        }

        private int CalculateScore(int count)
        {
            if (count >= 5)
            {
                return 1000000; // 連五，必勝局面
            }

            switch (count)
            {
                case 4:
                    return 100000; // 活四
                case 3:
                    return 1000; // 活三
                case 2:
                    return 100; // 活二
                case 1:
                    return 10; // 活一
                default:
                    return 0; // 其他情況
            }
        }

        private int CalculateDefensiveScore(int count)
        {
            if (count >= 5)
            {
                return 1000000; // 對手連五，必敗局面
            }

            switch (count)
            {
                case 4:
                    return 100000; // 對手活四
                case 3:
                    return 1000; // 對手活三
                case 2:
                    return 100; // 對手活二
                case 1:
                    return 10; // 對手活一
                default:
                    return 0; // 其他情況
            }
        }

        private PieceType GetOpponent(PieceType player)
        {
            return (player == PieceType.BLACK) ? PieceType.WHITE : PieceType.BLACK;
        }


        ////////////////////////////////////////////////////


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

        private bool CheckWinner()
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
                    {
                        winner = currentPlayer;
                        return true;
                    }
                }
            }
            return false;
        }

        //紀錄現在看到幾顆相同的棋子
        private int lookPosition(int centerX, int centerY, int xDir, int yDir)
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
