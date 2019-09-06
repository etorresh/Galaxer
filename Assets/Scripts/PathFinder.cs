using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinder : MonoBehaviour
{
    private int[,] boardClone;
    private int jumpEnumerator;

    private List<int> pathsTime, preventBacktrack, blacklist;
    private List<int>[,] Paths = new List<int>[8, 8];
    private int timer;

    private int originX, originY;

    int countJumps;


    public int Run(int[,] boardState, int posX, int posY)
    {
        // Assign empty int lists to Paths
        for (int i = 0; i < Paths.GetLength(0); i++)
        {
            for (int j = 0; j < Paths.GetLength(1); j++)
            {
                Paths[i, j] = new List<int>();
            }
        }

        pathsTime = new List<int>();
        preventBacktrack = new List<int>();

        // Values reset for CreatePaths and FindPath
        boardClone = (int[,])boardState.Clone();
        timer = 0;
        jumpEnumerator = 1;
        originX = posX;
        originY = posY;

        // Run main functions
        CreatePaths(originX, originY);
        CheckConsecutive();
        FindPath(originX, originY);

        return Mathf.Max(pathsTime.ToArray());
    }

    private void CreatePaths(int posX, int posY)
    {
        // Simulates jumps and creates possible paths.
        if (boardClone[posX, posY] == 3)
        {
            boardClone[posX, posY] = 0;
            Paths[posX, posY].Add(jumpEnumerator);
            jumpEnumerator += 1;
            if (posX != 7)
            {
                CreatePaths(posX + 1, posY);
            }
            if (posX != 0)
            {
                CreatePaths(posX - 1, posY);
            }
            if (posY != 7)
            {
                CreatePaths(posX, posY + 1);
            }
            if (posY != 0)
            {
                CreatePaths(posX, posY - 1);
            }
        }
        else
        {
            boardClone[posX, posY] += 1;
        }
    }

    private void FindPath(int posX, int posY)
    {
        // Runs throughout all possible path deadends and returns the size of the longest one.
        // if a deadend is found: send positon value to blacklist and timer to pathsTime, then reset timer.
        int[] surroundingValues = new int[4];
        int currentPosition = LowestValue(Paths[posX, posY]); 
        preventBacktrack.Add(currentPosition);
        Debug.Log("Vamos en: " + currentPosition);

        bool move = false;

        if (posX != 7)
        {
            if (currentPosition < LowestValue(Paths[posX + 1, posY]))
            {
                surroundingValues[0] = LowestValue(Paths[posX + 1, posY]);
                move = true;
            }
        }
        if (posX != 0)
        {
            if (currentPosition < LowestValue(Paths[posX - 1, posY]))
            {
                surroundingValues[1] = LowestValue(Paths[posX - 1, posY]);
                move = true;
            }
        }
        if (posY != 7)
        {
            if (currentPosition < LowestValue(Paths[posX, posY + 1]))
            {
                surroundingValues[2] = LowestValue(Paths[posX, posY + 1]);
                move = true;
            }
        }
        if (posY != 0)
        {
            if (currentPosition < LowestValue(Paths[posX, posY - 1]))
            {
                surroundingValues[3] = LowestValue(Paths[posX, posY - 1]);
                move = true;
            }
        }


        if (move)
        {
            int[] surroundingSorted = (int[])surroundingValues.Clone();
            Array.Sort(surroundingSorted);
            int sIndex = Array.BinarySearch(surroundingSorted, currentPosition);
            int nearest = surroundingSorted[~sIndex];
            int pathIndex = Array.IndexOf(surroundingValues, nearest);

            timer++;

            if (pathIndex == 0)
            {
                FindPath(posX + 1, posY);
            }
            else if (pathIndex == 1)
            {
                FindPath(posX - 1, posY);
            }
            else if (pathIndex == 2)
            {
                FindPath(posX, posY + 1);
            }
            else if (pathIndex == 3)
            {
                FindPath(posX, posY - 1);
            }
        }
        else
        {
            Debug.Log("Se acabo en: " + currentPosition);
            if (currentPosition == 1)
            {
                return;
            }
            Paths[posX, posY].Remove(currentPosition);
            pathsTime.Add(timer);
            preventBacktrack.Clear();
            timer = 0;
            FindPath(originX, originY);
        }
    }

    private int LowestValue(List<int> x)
    {
        int minValue = 0;

        List<int> temp = new List<int>();
        while (x.Any() && preventBacktrack.Contains(x.Min()) )
        {
            temp.Add(x.Min());
            x.Remove(x.Min());
        }
        if (x.Any())
        {
            minValue = x.Min();
        }
        x.AddRange(temp);
        return minValue;
    }

    private void CheckConsecutive()
    {
        int totalJumps = jumpEnumerator - 1;
        int currentJump = 1;
        while (currentJump != 14)
        {

        }
    }



    // Nasty way to debug a 2d list that contains a list
    private string Nasty2D(List<int>[,] nastyArray)
    {
        string boardS = "";
        for (int i = 0; i < nastyArray.GetLength(0); i++)
        {
            for (int j = 0; j < nastyArray.GetLength(1); j++)
            {
                foreach (int k in Paths[j, i])
                {
                    boardS += k + "/";
                }
                boardS += " - ";
                if (j == (nastyArray.GetLength(1) - 1))
                {
                    boardS += System.Environment.NewLine;
                }
            }
        }
        return boardS;
    }
}