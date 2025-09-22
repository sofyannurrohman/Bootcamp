using Battleship.Models;
using Battleship.Interfaces;
using System;
using System.Collections.Generic;

public class AIPlayer : IPlayer
{
    private readonly Random _rand = new();
    private readonly HashSet<(int Row, int Col)> _previousAttacks = new();
    private readonly Queue<(int Row, int Col)> _targetQueue = new(); // smart targeting after hit

    public string Name { get; set; }
    public Board Board { get; set; } = new();

    /// <summary>
    /// Select the next attack coordinates.
    /// </summary>
    public (int Row, int Col) SelectAttack()
    {
        // Attack from smart target queue first
        while (_targetQueue.Count > 0)
        {
            var target = _targetQueue.Dequeue();
            if (!_previousAttacks.Contains(target))
            {
                _previousAttacks.Add(target);
                return target;
            }
        }

        // Otherwise, pick a random untried cell
        int row, col;
        do
        {
            row = _rand.Next(Board.Size);
            col = _rand.Next(Board.Size);
        } while (_previousAttacks.Contains((row, col)));

        _previousAttacks.Add((row, col));
        return (row, col);
    }

    /// <summary>
    /// Update AI knowledge based on attack result.
    /// </summary>
    public void ProcessAttackResult(int row, int col, CellStatus result)
    {
        if (result == CellStatus.Hit)
        {
            EnqueueAdjacentTargets(row, col);
        }
    }

    /// <summary>
    /// Add neighboring cells of a hit to the target queue.
    /// </summary>
    private void EnqueueAdjacentTargets(int row, int col)
    {
        var deltas = new (int dRow, int dCol)[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

        foreach (var (dRow, dCol) in deltas)
        {
            int newRow = row + dRow;
            int newCol = col + dCol;

            if (IsWithinBoard(newRow, newCol) && !_previousAttacks.Contains((newRow, newCol)))
            {
                _targetQueue.Enqueue((newRow, newCol));
            }
        }
    }

    private bool IsWithinBoard(int row, int col) => row >= 0 && row < Board.Size && col >= 0 && col < Board.Size;
}
