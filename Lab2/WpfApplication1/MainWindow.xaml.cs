using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



namespace WpfApplication1
{
  


    public partial class MainWindow : Window
    {
        public class GridHelpers
        {
            #region RowCount Property

            /// <summary>
            /// Adds the specified number of Rows to RowDefinitions. 
            /// Default Height is Auto
            /// </summary>
            public static readonly DependencyProperty RowCountProperty =
                DependencyProperty.RegisterAttached(
                    "RowCount", typeof(int), typeof(GridHelpers),
                    new PropertyMetadata(-1, RowCountChanged));

            // Get
            public static int GetRowCount(DependencyObject obj)
            {
                return (int)obj.GetValue(RowCountProperty);
            }

            // Set
            public static void SetRowCount(DependencyObject obj, int value)
            {
                obj.SetValue(RowCountProperty, value);
            }

            // Change Event - Adds the Rows
            public static void RowCountChanged(
                DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                if (!(obj is Grid) || (int)e.NewValue < 0)
                    return;

                Grid grid = (Grid)obj;
                grid.RowDefinitions.Clear();

                for (int i = 0; i < (int)e.NewValue; i++)
                    grid.RowDefinitions.Add(
                        new RowDefinition() { Height = GridLength.Auto });

                SetStarRows(grid);
            }

            #endregion

            #region ColumnCount Property

            /// <summary>
            /// Adds the specified number of Columns to ColumnDefinitions. 
            /// Default Width is Auto
            /// </summary>
            public static readonly DependencyProperty ColumnCountProperty =
                DependencyProperty.RegisterAttached(
                    "ColumnCount", typeof(int), typeof(GridHelpers),
                    new PropertyMetadata(-1, ColumnCountChanged));

            // Get
            public static int GetColumnCount(DependencyObject obj)
            {
                return (int)obj.GetValue(ColumnCountProperty);
            }

            // Set
            public static void SetColumnCount(DependencyObject obj, int value)
            {
                obj.SetValue(ColumnCountProperty, value);
            }

            // Change Event - Add the Columns
            public static void ColumnCountChanged(
                DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                if (!(obj is Grid) || (int)e.NewValue < 0)
                    return;

                Grid grid = (Grid)obj;
                grid.ColumnDefinitions.Clear();

                for (int i = 0; i < (int)e.NewValue; i++)
                    grid.ColumnDefinitions.Add(
                        new ColumnDefinition() { Width = GridLength.Auto });

                SetStarColumns(grid);
            }

            #endregion

            #region StarRows Property

            /// <summary>
            /// Makes the specified Row's Height equal to Star. 
            /// Can set on multiple Rows
            /// </summary>
            public static readonly DependencyProperty StarRowsProperty =
                DependencyProperty.RegisterAttached(
                    "StarRows", typeof(string), typeof(GridHelpers),
                    new PropertyMetadata(string.Empty, StarRowsChanged));

            // Get
            public static string GetStarRows(DependencyObject obj)
            {
                return (string)obj.GetValue(StarRowsProperty);
            }

            // Set
            public static void SetStarRows(DependencyObject obj, string value)
            {
                obj.SetValue(StarRowsProperty, value);
            }

            // Change Event - Makes specified Row's Height equal to Star
            public static void StarRowsChanged(
                DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                    return;

                SetStarRows((Grid)obj);
            }

            #endregion

            #region StarColumns Property

            /// <summary>
            /// Makes the specified Column's Width equal to Star. 
            /// Can set on multiple Columns
            /// </summary>
            public static readonly DependencyProperty StarColumnsProperty =
                DependencyProperty.RegisterAttached(
                    "StarColumns", typeof(string), typeof(GridHelpers),
                    new PropertyMetadata(string.Empty, StarColumnsChanged));

            // Get
            public static string GetStarColumns(DependencyObject obj)
            {
                return (string)obj.GetValue(StarColumnsProperty);
            }

            // Set
            public static void SetStarColumns(DependencyObject obj, string value)
            {
                obj.SetValue(StarColumnsProperty, value);
            }

            // Change Event - Makes specified Column's Width equal to Star
            public static void StarColumnsChanged(
                DependencyObject obj, DependencyPropertyChangedEventArgs e)
            {
                if (!(obj is Grid) || string.IsNullOrEmpty(e.NewValue.ToString()))
                    return;

                SetStarColumns((Grid)obj);
            }

            #endregion

            private static void SetStarColumns(Grid grid)
            {
                string[] starColumns =
                    GetStarColumns(grid).Split(',');

                for (int i = 0; i < grid.ColumnDefinitions.Count; i++)
                {
                    if (starColumns.Contains(i.ToString()))
                        grid.ColumnDefinitions[i].Width =
                            new GridLength(1, GridUnitType.Star);
                }
            }

            private static void SetStarRows(Grid grid)
            {
                string[] starRows =
                    GetStarRows(grid).Split(',');

                for (int i = 0; i < grid.RowDefinitions.Count; i++)
                {
                    if (starRows.Contains(i.ToString()))
                        grid.RowDefinitions[i].Height =
                            new GridLength(1, GridUnitType.Star);
                }
            }
        }

        TextBox[,] textBoxDimA = new TextBox[10, 10];
        TextBox[,] textBoxDimB = new TextBox[10, 10];
        TextBlock[,] textBoxDimR = new TextBlock[10, 10];
        public MainWindow()
        {
            InitializeComponent();
           
        }
        
        private void CreateTextBoxes(Matrix matrix, WrapPanel wp, ref TextBox[,] TextBoxDim)
        {
            const double MARGIN = 1d;
            const double WIDTH = 40d;
            const double HEIGHT = 40d;
            int i = 0;
            wp.Width = matrix.columns * WIDTH + 2*MARGIN*matrix.columns;
            wp.Height = matrix.rows * HEIGHT + 2 * MARGIN * matrix.rows;
            foreach (Vector vector in matrix)
            {
                for (int j = 0; j < vector.length; j++)
                {
                    TextBoxDim[i,j] = new TextBox();
                    TextBoxDim[i,j].Width = WIDTH;
                    TextBoxDim[i, j].Height = HEIGHT;
                    TextBoxDim[i, j].Margin = new Thickness(MARGIN);
                    TextBoxDim[i,j].Text = vector[j].ToString();
                  
                    TextBoxDim[i,j].SetValue(Grid.ColumnProperty, j);
                    TextBoxDim[i,j].SetValue(Grid.RowProperty, i);
                    wp.Children.Add(TextBoxDim[i, j]);
                }
                i++;
            }
        }

        private void CreateTextBlockses(Matrix matrix, WrapPanel wp, ref TextBlock[,] TextBlockDim)
        {
            const double MARGIN = 1d;
            const double WIDTH = 40d;
            const double HEIGHT = 40d;
            int i = 0;
            wp.Width = matrix.columns * WIDTH + 2 * MARGIN * matrix.columns;
            wp.Height = matrix.rows * HEIGHT + 2 * MARGIN * matrix.rows;
            foreach (Vector vector in matrix)
            {
                for (int j = 0; j < vector.length; j++)
                {
                    TextBlockDim[i, j] = new TextBlock();
                    TextBlockDim[i, j].Width = WIDTH;
                    TextBlockDim[i, j].Height = HEIGHT;
                    TextBlockDim[i, j].Margin = new Thickness(MARGIN);
                    TextBlockDim[i, j].Text = vector[j].ToString();
                    TextBlockDim[i, j].Background = Brushes.White;
                    TextBlockDim[i, j].SetValue(Grid.ColumnProperty, j);
                    TextBlockDim[i, j].SetValue(Grid.RowProperty, i);
                    wp.Children.Add(TextBlockDim[i, j]);
                }
                i++;
            }
        }
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        private void textBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void wrapPanel_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        private void wrapPanel1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel1.Children.Clear();
            Matrix matrix = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            matrix.Initialize(2);
            CreateTextBoxes(matrix, wrapPanel1, ref textBoxDimB);
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel.Children.Clear();
            Matrix matrix = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            matrix.Initialize(1);
            CreateTextBoxes(matrix, wrapPanel, ref textBoxDimA);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel2.Children.Clear();
            Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixA[i, j] = Convert.ToDouble(textBoxDimA[i, j].Text);
                }
            }

            Matrix matrixB = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            for (int i = 0; i < matrixB.rows; i++)
            {
                for (int j = 0; j < matrixB.columns; j++)
                {
                    matrixB[i, j] = Convert.ToDouble(textBoxDimB[i, j].Text);
                }
            }
            Matrix result;
            result = matrixA + matrixB;
            CreateTextBlockses(result, wrapPanel2, ref textBoxDimR);
        }

        //Multiplication
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel2.Children.Clear();
            Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixA[i, j] = Convert.ToDouble(textBoxDimA[i, j].Text);
                }
            }

            Matrix matrixB = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            for (int i = 0; i < matrixB.rows; i++)
            {
                for (int j = 0; j < matrixB.columns; j++)
                {
                    matrixB[i, j] = Convert.ToDouble(textBoxDimB[i, j].Text);
                }
            }
            Matrix result;
            result = matrixA * matrixB;
            CreateTextBlockses(result, wrapPanel2, ref textBoxDimR);
        }

        
    }
}
