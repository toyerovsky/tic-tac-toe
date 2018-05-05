using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Board;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class BoardController : MonoBehaviour
    {
        public MarkerType CurrentMove;
        public List<Button> Buttons;

        public void Start()
        {
            foreach (var button in Buttons)
            {
                button.onClick.AddListener(() =>
                {
                    button.GetComponentInChildren<Text>().text = CurrentMove.ToString();
                    CurrentMove = CurrentMove == MarkerType.X ? MarkerType.O : MarkerType.X;
                });
            }
        }

        public void CheckForWin()
        {
            var texts = Buttons.Select(b => b.GetComponentInChildren<Text>().text);

            int rowAndColCount = (int) Math.Sqrt(texts.Count());
            string[][] winMatrix = new string[rowAndColCount][];

            for (int i = 0; i < texts.Count() / Math.Sqrt(texts.Count()); i++)
            {
                for (int j = 0; j < Math.Sqrt(texts.Count()); j++)
                {
                    winMatrix[i][j] = texts.ElementAt(i + j);
                }
            }
        }
    }
}
