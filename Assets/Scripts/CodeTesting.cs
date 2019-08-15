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
        board[0, 0] = 3;
        board[1, 0] = 3;
        board[0, 1] = 3;
        board[1, 1] = 2;
        board[2, 1] = 3;
        board[3, 1] = 3;
        board[3, 2] = 3;
        board[4, 2] = 3;
        board[5, 2] = 3;
        board[3, 3] = 3;
        board[3, 4] = 3;
        board[3, 5] = 3;
        board[3, 6] = 3;
        board[3, 7] = 3;
        board[4, 4] = 3;
        board[4, 5] = 3;
        board[3, 3] = 3;
        board[2, 3] = 2;

        int[,] board2 = new int[8, 8];
        board2[0, 0] = 3;
        board2[1, 0] = 3;
        board2[0, 1] = 3;
        board2[1, 1] = 2;
        board2[2, 1] = 3;
        board2[3, 1] = 3;
        board2[0, 0] = 3;
        board2[1, 2] = 3;
        board2[1, 3] = 3;
        board2[1, 4] = 3;
        board2[1, 5] = 3;
        board2[0, 4] = 3;
        board2[0, 5] = 2;

        print(Nasty2D(board));
        print("Wait time: " + pf.Run(board, 0, 0));
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
