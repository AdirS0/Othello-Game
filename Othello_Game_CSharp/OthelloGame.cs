﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello_Game_CSharp
{
    public class OthelloGame
    {
        private Player m_Player1;
        private Player m_Player2;
        private GameBoard m_GameBoard;
        private bool m_IsAgainstComputer;
        private List<string> m_ValidMoves;

        public OthelloGame(int i_Size, bool i_IsAgainstComputer, Player i_Player1, Player i_Player2)
        {
            m_Player1 = i_Player1;
            m_Player2 = i_Player2;
            m_IsAgainstComputer = i_IsAgainstComputer;
            m_GameBoard = new GameBoard(i_Size);
            m_ValidMoves = new List<string>();

            UpdateScore();
        }

        public GameBoard Board
        {
            get { return m_GameBoard; }
            set { m_GameBoard = value; }
        }

        public Player Player1
        {
            get { return m_Player1; }
            set { m_Player1 = value; }
        }

        public Player Player2
        {
            get { return m_Player2; }
            set { m_Player2 = value; }
        }

        public List<string> ValidMoves
        {
            get { return m_ValidMoves; }
            set { m_ValidMoves = value; }
        }

        public bool HasValidMoves(Player i_Player)
        {
            bool isValid = false;

            for (int i = 0; i < m_GameBoard.Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_GameBoard.Board.GetLength(0); j++)
                {
                    if (m_GameBoard.IsValidMove(i_Player, i, j))
                    {
                        isValid = true;
                    }
                }
            }

            return isValid;
        }

        public void MakeMove(Player i_Player, string i_Move)
        {
            int col = i_Move[0] - 'A';
            int row = int.Parse(i_Move[1].ToString());

            if (i_Move.Length > 2)
            {
                row = ((row * 10) + int.Parse(i_Move[2].ToString())) - 1;
            }
            else
            {
                row -= 1;
            }

            m_GameBoard.UpdateBoardState(i_Player, row, col);
            m_GameBoard.CountCells();
            UpdateScore();
        }

        public void GetValidMoves(Player i_CurrentPlayer)
        {
            ValidMoves.Clear();

            for (int i = 0; i < m_GameBoard.Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_GameBoard.Board.GetLength(0); j++)
                {
                    if (m_GameBoard.IsValidMove(i_CurrentPlayer, i, j))
                    {
                        string rowToConcat = (i + 1).ToString();
                        string colToConcat = ((char)(j + 'A')).ToString();
                        string validMoveToAdd = string.Concat(colToConcat, rowToConcat);
                        ValidMoves.Add(validMoveToAdd);
                    }
                }
            }
        }

        public void UpdateScore()
        {
            Player1.Score = m_GameBoard.BlackCells;
            Player2.Score = m_GameBoard.WhiteCells;
        }
    }
}

