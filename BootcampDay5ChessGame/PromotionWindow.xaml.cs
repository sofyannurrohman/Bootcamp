using System.Windows;

namespace ChessGame
{
    public partial class PromotionWindow : Window
    {
        public string SelectedPiece { get; private set; }

        public PromotionWindow()
        {
            InitializeComponent();
        }

        private void SelectPiece(string piece)
        {
            SelectedPiece = piece;
            DialogResult = true;
            Close();
        }

        private void Queen_Click(object sender, RoutedEventArgs e)
        {
            SelectPiece("Queen");
        }

        private void Rook_Click(object sender, RoutedEventArgs e)
        {
            SelectPiece("Rook");
        }

        private void Bishop_Click(object sender, RoutedEventArgs e)
        {
            SelectPiece("Bishop");
        }

        private void Knight_Click(object sender, RoutedEventArgs e)
        {
            SelectPiece("Knight");
        }
    }
}