using System;


namespace DominoConsoleMVC.Models
{
    public record DominoTile(int Left, int Right)
    {
        public bool IsDouble => Left == Right;
        public DominoTile Flipped() => new DominoTile(Right, Left);
        public override string ToString() => $"[{Left}|{Right}]";
        public static List<DominoTile> GenerateAllTiles()
        {
            var tiles = new List<DominoTile>();
            for (int i = 0; i <= 6; i++)
            {
                for (int j = i; j <= 6; j++)
                {
                    tiles.Add(new DominoTile(i, j));
                }
            }
            return tiles;
        }

    }
}