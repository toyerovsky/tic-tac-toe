using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        public MarkerType CurrentMove;
        public List<Button> Buttons;
        public GameObject GameOverOverlay;
        public Text GameOverText;

        private int _rowAndColCount;
        private string[,] _currenMoves;

        public void Start()
        {
            if (Math.Sqrt(Buttons.Count()) % 1 != 0)
                throw new InvalidOperationException("Provided buttons enumerable has to be n * n length. e.g. 1, 4, 9, 16...");

            _rowAndColCount = (int)Math.Sqrt(Buttons.Count());
            _currenMoves = new string[_rowAndColCount, _rowAndColCount];
            // we fill matrix with numbers because "" ruins whole CheckForWin() method idea
            _currenMoves.FillWithNumbers();

            foreach (var button in Buttons)
            {
                button.onClick.AddListener(() =>
                {
                    int index;
                    // it works when button name represents its order in matrix eg. 0, 1, 2 
                    if (int.TryParse(button.name, out index))
                    {
                        var text = button.GetComponentInChildren<Text>();
                        text.text = CurrentMove.ToString();

                        // get the row and coll for button in matrix
                        int row = index / _rowAndColCount;
                        int column = index % _rowAndColCount;
                        _currenMoves[row, column] = CurrentMove.ToString();

                        if (CheckForWin())
                        {
                            GameOver();
                            return;
                        }

                        CurrentMove = CurrentMove == MarkerType.X ? MarkerType.O : MarkerType.X;
                        button.onClick.RemoveAllListeners();
                    }
                });
            }
        }

        private void GameOver()
        {
            gameObject.SetActive(false); // hide board
            GameOverText.text = $"{CurrentMove} WON";
            GameOverOverlay.SetActive(true); // show game over overlay
        }

        /// <summary>
        /// Check horizontal win for n rows
        /// </summary>
        /// <returns></returns>
        private bool CheckHorizontal()
        {
            for (int i = 0; i < _currenMoves.GetLength(0); i++)
                if (CheckHorizontal(i, 0))
                    return true;

            return false;
        }

        /// <summary>
        /// Check vertical for n columns
        /// </summary>
        /// <returns></returns>
        private bool CheckVertical()
        {
            for (int i = 0; i < _currenMoves.GetLength(1); i++)
                if (CheckVertical(0, i))
                    return true;

            return false;
        }

        /// <summary>
        /// Check 2 slants
        /// </summary>
        /// <returns></returns>
        private bool CheckSlant()
        {
            return CheckSlant(0, 0, 1, 1) || CheckSlant(_rowAndColCount - 1, 0, -1, 1);
        }

        /// <summary>
        /// Recursive method that checks for win on x axis
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckHorizontal(int i, int j)
        {
            if (j + 1 == _currenMoves.GetLength(1))
                return true;

            if (_currenMoves[i, j] != _currenMoves[i, j + 1])
                return false;

            return CheckHorizontal(i, j + 1);
        }

        /// <summary>
        /// Recursive method that checks for win on y axis
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        private bool CheckVertical(int i, int j)
        {
            if (i + 1 == _currenMoves.GetLength(0))
                return true;

            if (_currenMoves[i, j] != _currenMoves[i + 1, j])
                return false;

            return CheckVertical(i + 1, j);
        }

        /// <summary>
        /// Recursive method that checks for win on slant
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iDifference"></param>
        /// <param name="jDifference"></param>
        /// <returns></returns>
        private bool CheckSlant(int i, int j, int iDifference, int jDifference)
        {
            if (j + jDifference == _currenMoves.GetLength(0)) // it's square matrix so we haven't to check i dimension
                return true;

            // to move from [0,0] to [3,3] we need to add i + 1, j + 1, but if we want to check the opposite direction we have to use i - 1, j + 1
            if (_currenMoves[i, j] != _currenMoves[i + iDifference, j + jDifference])
                return false;

            return CheckSlant(i + iDifference, j + jDifference, iDifference, jDifference);
        }

        private bool CheckForWin()
        {
            bool verticalWin = CheckVertical();
            bool horizontalWin = CheckHorizontal();
            bool slantWin = CheckSlant();

            return verticalWin || horizontalWin || slantWin;
        }
    }
}
