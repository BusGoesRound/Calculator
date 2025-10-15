using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{

    public partial class MainWindow : Window
    {

        private readonly string[][] numpadList =
            [
            ["C", "D", "^", "√"],
            ["7", "8", "9", "+"],
            ["4", "5", "6", "-"],
            ["1", "2", "3", "/"],
            ["0", ".", "=", "*"]
            ];

        public MainWindow()
        {
            InitializeComponent();

            CreateKeys();
        }

        private void CreateKeys()
        {
               for (int r = 0; r < numpadList.Length; r++)
               {
                   for (int c = 0; c < numpadList[r].Length; c++)
                   {
                       Button button = new()
                       {
                           Content = numpadList[r][c],
                           FontSize = 24,

                           Background = Application.Current.Resources["ForegroundColor"] as Brush,
                           Foreground = Application.Current.Resources["TextColor"] as Brush,
                           BorderBrush = Brushes.Transparent
                       };

                       button.Click += Button_Click;
                       Grid.SetRow(button, r + 1);
                       Grid.SetColumn(button, c);
                       NumpadGrid.Children.Add(button);
                   }
            }
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                string content = button.Content.ToString();

                if (content == "C")
                {
                    Display.Text = "0";
                    return;
                }

                if (content == "D")
                {
                    if (Display.Text.Length > 1)
                    {
                        Display.Text = Display.Text[..^1];
                    }
                    else
                    {
                        Display.Text = "0";
                    }
                    return;
                }

                if (content == "=")
                {
                    try
                    {
                        var result = new System.Data.DataTable().Compute(Display.Text, null);
                        Display.Text = result.ToString();
                    }
                    catch
                    {
                        Display.Text = "Error";
                    }
                    return;
                }

                if (Display.Text == "0")
                {
                    Display.Text = content;
                }
                else
                {
                    Display.Text += content;
                }
            }
        }
    }
}