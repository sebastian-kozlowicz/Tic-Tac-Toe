using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Linq;

namespace TicTacToe
{
    /// <summary>
    /// Enum types that represents the value in the field
    /// </summary>
    public enum MarkType
    {
        Free,
        Nought,
        Cross
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        #region Class fields

        /// <summary>
        /// Array that holds marked fields
        /// </summary>
        private MarkType[,] results;

        /// <summary>
        /// True if game has ended
        /// </summary>
        private bool gameEnded;

        /// <summary>
        /// True if player one turn
        /// </summary>
        private bool Player1Turn;

        /// <summary>
        /// Background color of winning fields after win
        /// </summary>
        private SolidColorBrush winColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#B4EEB4"));

        /// <summary>
        /// Background color of all fields after draw
        /// </summary>
        private SolidColorBrush drawColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#f4eb42"));

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            NewGame();
        }

        #endregion

        private void NewGame()
        {
            gameEnded = false;

            // Create two-dimension arrary that holds marked type fields 
            results = new MarkType[3, 3];

            // Fill array with free cells
            for (int i = 0; i < results.GetUpperBound(0); i++)
            {
                for (int j = 0; j < results.GetUpperBound(0); j++)
                {
                    results[i, j] = MarkType.Free;
                }
            }

            // Player 1 starts as first
            Player1Turn = true;

            Board.Children.Cast<Button>().ToList().ForEach(button =>
            {
                // Change background, foreground and content to default values
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gameEnded)
            {
                NewGame();
                return;
            }

            Button button = (Button)sender;

            int row = Grid.GetRow(button);
            int column = Grid.GetColumn(button);

            if (results[row, column] != MarkType.Free)
                return;

            if (Player1Turn)
            {
                results[row, column] = MarkType.Cross;
                button.Content = "X";
            }
            else
            {
                results[row, column] = MarkType.Nought;
                button.Content = "O";
            }

            // Change noughts to red
            if (!Player1Turn)
                button.Foreground = Brushes.Red;

            // Change of player turn
            Player1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }


        private void CheckForWinner()
        {
            #region Horizontal Wins

            // Check for horizontal wins
            //
            //  - Row 0
            //
            if (results[0, 0] != MarkType.Free && (results[0, 0] & results[0, 1] & results[0, 2]) == results[0, 0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button0_1.Background = Button0_2.Background = winColor;

            }
            //
            //  - Row 1
            //
            if (results[1, 0] != MarkType.Free && (results[1, 0] & results[1, 1] & results[1, 2]) == results[1, 0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button1_0.Background = Button1_1.Background = Button1_2.Background = winColor;
            }
            //
            //  - Row 2
            //
            if (results[2, 0] != MarkType.Free && (results[2, 0] & results[2, 1] & results[2, 2]) == results[2, 0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button2_1.Background = Button2_2.Background = winColor;
            }
            #endregion

            #region Vertical Wins

            // Check for vertical wins
            //
            //  - Column 0
            //
            if (results[0, 0] != MarkType.Free && (results[0, 0] & results[1, 0] & results[2, 0]) == results[0, 0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_0.Background = Button2_0.Background = winColor;
            }
            //
            //  - Column 1
            //
            if (results[0, 1] != MarkType.Free && (results[0, 1] & results[1, 1] & results[2, 1]) == results[0, 1])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button0_1.Background = Button1_1.Background = Button2_1.Background = winColor;
            }
            //
            //  - Column 2
            //
            if (results[0, 2] != MarkType.Free && (results[0, 2] & results[1, 2] & results[2, 2]) == results[0, 2])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button0_2.Background = Button1_2.Background = Button2_2.Background = winColor;
            }

            #endregion

            #region Diagonal Wins

            // Check for diagonal wins
            //
            //  - Top Left Bottom Right
            //
            if (results[0, 0] != MarkType.Free && (results[0, 0] & results[1, 1] & results[2, 2]) == results[0, 0])
            {
                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button0_0.Background = Button1_1.Background = Button2_2.Background = winColor;
            }
            //
            //  - Top Right Bottom Left
            //
            if (results[0, 2] != MarkType.Free && (results[0, 2] & results[1, 1] & results[2, 0]) == results[0, 2])
            {

                // Game ends
                gameEnded = true;

                // Highlight winning cells in green
                Button2_0.Background = Button1_1.Background = Button0_2.Background = winColor;

            }
            #endregion

            #region No Winner

            // Check if game hasn't ended and there is no free cells
            if (!gameEnded && !results.Cast<MarkType>().Any(free => free == MarkType.Free) )
            {
                // Game ended
                gameEnded = true;

                // Turn all cells yellow
                Board.Children.Cast<Button>().ToList().ForEach(button =>
                {
                    button.Background = drawColor;
                });
            }

            #endregion
        }
    }
}
