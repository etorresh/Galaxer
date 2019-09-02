using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private int[,] Paths, boardClone;
    private int jumpEnumerator;

    private List<int> blacklist, pathsTime;
    private int timer;

    private int originX, originY;

    public int Run(int[,] boardState, int posX, int posY)
    {
        // Variables are declared and reset.
        Paths = new int[8, 8];
        boardClone = (int[,])boardState.Clone();
        jumpEnumerator = 1;
        blacklist = new List<int>();
        pathsTime = new List<int>();
        timer = 0;
        originX = posX;
        originY = posY;
        // Debug Pathfinder
        print(Nasty2D(Paths));
        CreatePaths(originX, originY);
        print(Nasty2D(Paths));
        FindPath(originX, originY);
        return Mathf.Max(pathsTime.ToArray());
    }

    private void CreatePaths(int posX, int posY)
    {
        // Simulates jumps and creates possible paths.
        if (boardClone[posX, posY] == 3)
        {
            boardClone[posX, posY] = 0;
            Paths[posX, posY] = jumpEnumerator;
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
        int currentPosition = Paths[posX, posY];
        if(posX == originX && posY == originY && timer == 0)
        {
            currentPosition = 1;
        }
        Debug.Log("Findpath cycle: " + currentPosition);
        bool move = false;

        if (posX != 7)
        {
            if (currentPosition < Paths[posX + 1, posY] && !blacklist.Contains(Paths[posX + 1, posY]))
            {
                surroundingValues[0] = Paths[posX + 1, posY];
                move = true;
            }
        }
        if (posX != 0)
        {
            if (currentPosition < Paths[posX - 1, posY] && !blacklist.Contains(Paths[posX - 1, posY]))
            {
                surroundingValues[1] = Paths[posX - 1, posY];
                move = true;
            }
        }
        if (posY != 7)
        {
            if (currentPosition < Paths[posX, posY + 1] && !blacklist.Contains(Paths[posX, posY + 1]))
            {
                surroundingValues[2] = Paths[posX, posY + 1];
                move = true;
            }
        }
        if (posY != 0)
        {
            if (currentPosition < Paths[posX, posY - 1] && !blacklist.Contains(Paths[posX, posY - 1]))
            {
                surroundingValues[3] = Paths[posX, posY - 1];
                move = true;
            }
        }


        if (move)
        {
            int pathIndex = Array.IndexOf(surroundingValues, Mathf.Max(surroundingValues));
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
            
            if (currentPosition == 1 && posX == originX && posY == originY)
            {
                Debug.Log("Findpath end");
                return;
            }
            blacklist.Add(currentPosition);
            pathsTime.Add(timer);
            timer = 0;
            FindPath(originX, originY);
        }
    }

    private string Nasty2D(int[,] nastyArray)
    {
        string boardS = "";
        for (int i = 0; i < nastyArray.GetLength(0); i++)
        {
            for (int j = 0; j < nastyArray.GetLength(1); j++)
            {
                boardS += nastyArray[j, i] + "-";
                if (j == (nastyArray.GetLength(1) - 1))
                {
                    boardS += System.Environment.NewLine;
                }
            }
        }
        return boardS;
    }
}