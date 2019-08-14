using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTesting : MonoBehaviour
{
    private PathFinder pf;
    void Start()
    {
        pf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PathFinder>();

        int[,] board = new int[8, 8];
        int rowLength = board.GetLength(0);
        int colLength = board.GetLength(1);
        for (int k = 0; k < 8; k++)
            for (int l = 0; l < 8; l++)
                board[k, l] = 0;

        board[0, 0] = 3;
        board[1, 0] = 3;
        board[0, 1] = 3;
        board[1, 1] = 3;
        board[2, 1] = 3;
        board[3, 1] = 3;
        board[1, 2] = 3;
        board[1, 3] = 3;
        board[1, 4] = 3;
        board[1, 5] = 3;
        board[0, 5] = 3;
        board[0, 4] = 3;






        print(Nasty2D(board));
        print(pf.Run(board, 0, 0));
    }

    private string Nasty2D(int[,] nastyArray)
    {
        string boardS = "";
        for (int i = 0; i < nastyArray.GetLength(0); i++)
        {
            for (int j = 0; j < nastyArray.GetLength(0); j++)
            {
                boardS += nastyArray[j, i] + "-";
                if (j == 7)
                {
                    boardS += System.Environment.NewLine;
                }
            }
        }
        return boardS;
    }
}
