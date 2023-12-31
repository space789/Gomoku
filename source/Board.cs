﻿using System;
using System.Drawing;

namespace Gomoku
{
    class Board
    {
        public static readonly int NODE_COUNT = 17;

        private static readonly Point NO_MATCH_NODE = new Point(-1, -1);

        private static readonly int OFFSET = 20;
        private static readonly int NODE_RADIUS = 10;
        private static readonly int NODE_DISTANCE = 50;

        public Piece[,] pieces = new Piece[NODE_COUNT, NODE_COUNT];

        private Point[] lastPlaceNode = new Point[289];


        public static int placeNodeCount = 0;
        public static int PlaceNodeCountRead;

        public Point[] LastPlaceNode { get { return lastPlaceNode; } }
        public int PlaceNodeCount { get { return placeNodeCount; } }

        public PieceType GetPieceType(int nodeIdX, int nodeIdY)
        {
            if (pieces[nodeIdX, nodeIdY] == null)
                return PieceType.NONE;
            else
                return pieces[nodeIdX, nodeIdY].GetPieceType();
        }

        public bool CanBePlaced(int x, int y)
        {
            //找出最近的節點 (Node)
            Point nodeId = findTheClosetNode(x, y);

            //如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return false;

            //如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return false;

            return true;
        }

        public Piece PlaceAPiece(int x, int y, PieceType type)
        {
            //找出最近的節點 (Node)
            Point nodeId = findTheClosetNode(x, y);

            //如果沒有的話，回傳 false
            if (nodeId == NO_MATCH_NODE)
                return null;

            //如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;
            Point formPos = convertToFormPosition(nodeId);
            //根據Type產生對應的棋子
            if (type == PieceType.BLACK)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y, placeNodeCount);
            else if (type == PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y, placeNodeCount);
            //紀錄最後下棋子的位置
            lastPlaceNode[placeNodeCount] = nodeId;
            placeNodeCount++;
            PlaceNodeCountRead = placeNodeCount;

            return pieces[nodeId.X, nodeId.Y];
        }

        public PieceComputer Number(int x, int y)
        {
            Point nodeId = findTheClosetNode(x, y);
            Point formPos = convertToFormPosition(nodeId);
            PlaceNumber number = new PlaceNumber(formPos.X, formPos.Y, placeNodeCount);
            return number;
        }
        public PieceComputer ComputerNumber(int x, int y)
        {
            x = x * NODE_DISTANCE + OFFSET;
            y = y * NODE_DISTANCE + OFFSET;
            PlaceNumber computernumber = new PlaceNumber(x, y, placeNodeCount);
            return computernumber;
        }

        public Piece ComputerPlaceAPiece(int x, int y, PieceType type)
        {
            //找出節點 (Node)
            Point nodeId = new Point(x, y);

            //如果有的話，檢查是否已經有棋子存在
            if (pieces[nodeId.X, nodeId.Y] != null)
                return null;

            Point formPos = convertToFormPosition(nodeId);

            //根據Type產生對應的棋子
            if (type == PieceType.BLACK)
                pieces[nodeId.X, nodeId.Y] = new BlackPiece(formPos.X, formPos.Y, placeNodeCount);
            else if (type == PieceType.WHITE)
                pieces[nodeId.X, nodeId.Y] = new WhitePiece(formPos.X, formPos.Y, placeNodeCount);

            //紀錄最後下棋子的位置
            lastPlaceNode[placeNodeCount] = nodeId;
            placeNodeCount++;
            PlaceNodeCountRead = placeNodeCount;

            //設計預測Node

            return pieces[nodeId.X, nodeId.Y];
        }



        public void CleanlastPlaceNode()
        {
            Array.Clear(lastPlaceNode, 0, lastPlaceNode.Length);
            placeNodeCount = 0;
            PlaceNodeCountRead = placeNodeCount;
        }

        public void Previous(int x)
        {
            placeNodeCount -= x;
            PlaceNodeCountRead = placeNodeCount;
        }

        private Point convertToFormPosition(Point nodeId)
        {
            Point formPosition = new Point();
            formPosition.X = nodeId.X * NODE_DISTANCE + OFFSET;
            formPosition.Y = nodeId.Y * NODE_DISTANCE + OFFSET;
            return formPosition;
        }

        private Point findTheClosetNode(int x, int y)
        {
            int nodeIdX = findTheClosetNode(x);
            if (nodeIdX == -1 || nodeIdX >= NODE_COUNT)
                return NO_MATCH_NODE;

            int nodeIdY = findTheClosetNode(y);
            if (nodeIdY == -1 || nodeIdY >= NODE_COUNT)
                return NO_MATCH_NODE;

            return new Point(nodeIdX, nodeIdY);
        }

        private int findTheClosetNode(int pos)
        {
            pos -= OFFSET;
            int quotient = pos / NODE_DISTANCE;
            int remainder = pos % NODE_DISTANCE;

            if (remainder <= NODE_RADIUS)
                return quotient;
            else if (remainder >= NODE_DISTANCE - NODE_RADIUS)
                return quotient + 1;
            else
                return -1;
        }
    }
}
