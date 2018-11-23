using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Private members

        /// <summary>
        /// Holds the current results of the cells
        /// </summary>
        private MarkType[] Results;

        /// <summary>
        /// Is it's Player one's turn
        /// </summary>
        private bool Player1Turn;

        /// <summary>
        /// True if the game has ended
        /// </summary>
        private bool GameEnd;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion
        

        private void NewGame() {

            // create an array with free cells
            Results = new MarkType[9];
            for (int i = 0; i < Results.Length; i++)
            {
                Results[i] = MarkType.Free;
            }

            //Make it sure it's the first players turn
            Player1Turn = true;

            Container.Children.Cast<Button>().ToList().ForEach(button =>
            {
                button.Content = string.Empty;
                button.Background = Brushes.White;
                button.Foreground = Brushes.Blue;
            });

            //make sure the game has not ended 
            GameEnd = false;
        }
        /// <summary>
        /// Buttnon event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            //start new game
            if (GameEnd)
            {
                NewGame();
                return;
            }

            var button = (Button)sender;

            var Column = Grid.GetColumn(button);
            var Row = Grid.GetRow(button);

            var Index = Column + Row * 3;

            if (Results[Index] != MarkType.Free)
            {
                return;
            }

            //Set the Cell Data to the propper mark
            Results[Index] = Player1Turn ? MarkType.X : MarkType.O;

            // set the button text
            button.Content = Player1Turn ? "X" : "O";

            //Chance the text to red if second player turn

            if (!Player1Turn)
            {
                button.Foreground = Brushes.Red;
            }

            //toggle player turns
            Player1Turn = !Player1Turn;

            CheckForWinner();
        }

        private void CheckForWinner(){
            var Same = false;
            var button = Button0_0;

            //check for hor wins
            for (int i = 0; i < 3; i++)
            {
                Same = (Results[i * 3] & Results[i * 3 + 1] & Results[i * 3 + 2]) == Results[i * 3];
                if (Same && Results[i * 3] != MarkType.Free)
                {
                    GameEnd = true;

                    button = (Button)Container.FindName("Button" + "0" + "_" + (i).ToString());
                    button.Background = Brushes.Green;

                    button = (Button)Container.FindName("Button" + "1" + "_" + (i).ToString());
                    button.Background = Brushes.Green;

                    button = (Button)Container.FindName("Button" + "2" + "_" + (i).ToString());
                    button.Background = Brushes.Green;

                    return;
                }
            }

            for (int i = 0; i < 3; i++)
            {
                Same = (Results[i] & Results[i + 3] & Results[i + 6]) == Results[i];
                if (Same && Results[i] != MarkType.Free)
                {
                    GameEnd = true;

                    button = (Button)Container.FindName("Button" + (i).ToString() + "_" + "0");
                    button.Background = Brushes.Green;

                    button = (Button)Container.FindName("Button" + (i).ToString() + "_" + "1");
                    button.Background = Brushes.Green;

                    button = (Button)Container.FindName("Button" + (i).ToString() + "_" + "2");
                    button.Background = Brushes.Green;

                    return;
                }
            }

            Same = (Results[0] & Results[4] & Results[8]) == Results[0];
            if (Same && Results[0] != MarkType.Free)
            {
                GameEnd = true;

                button = (Button)Container.FindName("Button" + "0" + "_" + "0");
                button.Background = Brushes.Green;

                button = (Button)Container.FindName("Button" + "1" + "_" + "1");
                button.Background = Brushes.Green;

                button = (Button)Container.FindName("Button" + "2" + "_" + "2");
                button.Background = Brushes.Green;

                return;
            }

            Same = (Results[6] & Results[4] & Results[2]) == Results[6];
            if (Same && Results[6] != MarkType.Free)
            {
                GameEnd = true;

                button = (Button)Container.FindName("Button" + "2" + "_" + "0");
                button.Background = Brushes.Green;

                button = (Button)Container.FindName("Button" + "1" + "_" + "1");
                button.Background = Brushes.Green;

                button = (Button)Container.FindName("Button" + "0" + "_" + "2");
                button.Background = Brushes.Green;

                return;
            }

            if (!Results.Any(f => f == MarkType.Free))
            {
                GameEnd = true;

                Container.Children.Cast<Button>().ToList().ForEach(Button =>
                {
                    Button.Background = Brushes.Orange;
                });
                return;
            }

        }
    }
}
