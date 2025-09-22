namespace CheckersConsoleMVC.Models
{
    public class Move
    {
        public int FromRow { get; }
        public int FromCol { get; }
        public int ToRow { get; }
        public int ToCol { get; }

        // 🔥 Extra fields for capture moves
        public bool IsCapture { get; }
        public int? CapturedRow { get; }
        public int? CapturedCol { get; }

        public Move(int fromRow, int fromCol, int toRow, int toCol, bool isCapture = false, int? capturedRow = null, int? capturedCol = null)
        {
            FromRow = fromRow;
            FromCol = fromCol;
            ToRow = toRow;
            ToCol = toCol;
            IsCapture = isCapture;
            CapturedRow = capturedRow;
            CapturedCol = capturedCol;
        }
    }
}
