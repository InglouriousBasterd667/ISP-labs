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
using System.IO;
using System.IO.Compression;
using Microsoft.Win32;



namespace WpfApplication1
{
  


    public partial class MainWindow : Window
    {
        

        Cells[,] cellsDimA = new Cells[10, 10];
        Cells[,] cellsDimB = new Cells[10, 10];
        TextBox[,] textBoxDimA = new TextBox[10, 10];
        TextBox[,] textBoxDimB = new TextBox[10, 10];
        TextBlock[,] textBoxDimR = new TextBlock[10, 10];
        Matrix result;
        bool sign = false;
        public MainWindow()
        {
            InitializeComponent();
            Matrix matrix = new Matrix(3, 3);
            matrix.Initialize(0);
           // dataGrid.ItemsSource = matrix;

        }

        private void CreateTextBoxes(Matrix matrix, WrapPanel wp, ref TextBox[,] TextBoxDim, ref Cells[,] cellsDim)
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
                    TextBoxDim[i,j].Height = HEIGHT;
                    TextBoxDim[i,j].Margin = new Thickness(MARGIN);
                    TextBoxDim[i,j].Text = vector[j].ToString();
                    
                    TextBoxDim[i,j].SetValue(Grid.ColumnProperty, j);
                    TextBoxDim[i,j].SetValue(Grid.RowProperty, i);
                    wp.Children.Add(TextBoxDim[i, j]);

                    cellsDim[i, j] = new Cells();
                    cellsDim[i, j].Value = matrix[i, j];
                    Binding myBinding = new Binding("Value");
                    myBinding.Source = cellsDim[i,j];
                    myBinding.Mode = BindingMode.TwoWay;
                    TextBoxDim[i, j].SetBinding(TextBox.TextProperty, myBinding);
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
            CreateTextBoxes(matrix, wrapPanel1,ref textBoxDimB, ref cellsDimB);
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            wrapPanel.Children.Clear();
            Matrix matrix = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            matrix.Initialize(1);
            CreateTextBoxes(matrix, wrapPanel, ref textBoxDimA,ref cellsDimA);
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {

            sign = true;
            button3.Background = Brushes.Red;
            button2.Background = Brushes.Green;
            Addition();
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            sign = false;
            button3.Background = Brushes.Green;
            button2.Background = Brushes.Red;
            Multiplication();
        }

        private void Addition()
        {
            wrapPanel2.Children.Clear();
            SortedMatrixPanel.Children.Clear();
            Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixA[i, j] = cellsDimA[i, j].Value;
                }
            }

            Matrix matrixB = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            for (int i = 0; i < matrixB.rows; i++)
            {
                for (int j = 0; j < matrixB.columns; j++)
                {
                    matrixB[i, j] = cellsDimB[i, j].Value;
                }
            }
            
            result = matrixA + matrixB;
            result.FindLengthOfVectors();
            var sortedMatrix = result.OrderBy(vect => vect.mathLength);
            CreateTextBlockses(result, wrapPanel2, ref textBoxDimR);
            Matrix temp = new Matrix(result.rows, result.columns);
            int count = 0;
            foreach (Vector v in sortedMatrix)
            {
                temp[count++] = v;

            }
            CreateTextBlockses(temp, SortedMatrixPanel, ref textBoxDimR);
        }
        private void Multiplication()
        {
            wrapPanel2.Children.Clear();
            SortedMatrixPanel.Children.Clear();
            Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixA[i, j] = cellsDimA[i, j].Value;
                }
            }

            Matrix matrixB = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            for (int i = 0; i < matrixB.rows; i++)
            {
                for (int j = 0; j < matrixB.columns; j++)
                {
                    matrixB[i, j] = cellsDimB[i, j].Value;
                }
            }
            
            result = matrixA * matrixB;
            result.FindLengthOfVectors();
            var sortedMatrix = result.OrderBy(vect => vect.mathLength);
            CreateTextBlockses(result, wrapPanel2, ref textBoxDimR);
            Matrix temp= new Matrix(result.rows,result.columns);
            int count = 0;
            foreach (Vector v in sortedMatrix)
            {
                temp[count++] = v;
                
            }
           CreateTextBlockses(temp, SortedMatrixPanel,ref textBoxDimR);
        }


        private void doAction()
        {
            if (sign)
                Addition();
            else
                Multiplication();
        }

        private void wrapPanel_KeyUp(object sender, KeyEventArgs e)
        {
            doAction();
        }

      
        private void wrapPanel1_KeyUp(object sender, KeyEventArgs e)
        {
            doAction();
        }

        private void wrapPanel1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            doAction();
        }

        private void wrapPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            doAction();
        }
        
        private void buttonSaveToFile_Click(object sender, RoutedEventArgs e)
        {
            Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixA[i, j] = cellsDimA[i, j].value;
                }
            }

            Matrix matrixB = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
            for (int i = 0; i < matrixA.rows; i++)
            {
                for (int j = 0; j < matrixA.columns; j++)
                {
                    matrixB[i, j] = cellsDimB[i, j].value;
                }
            }
            if ((bool)radioButton1.IsChecked)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                string filename;
                fileDialog.DefaultExt = ".txt";
                fileDialog.Filter = "Text files (txt)|*.txt";
                if (fileDialog.ShowDialog() == true)
                    filename = fileDialog.FileName;
                else
                    return;
                FileStream fileStream;
                if (!File.Exists(filename))
                    fileStream = new FileStream(filename, FileMode.CreateNew);
                else
                    fileStream = new FileStream(filename, FileMode.Create);
   

                StreamWriter streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(matrixA.rows);
                streamWriter.WriteLine(matrixA.columns);
                foreach (Vector vect in matrixA)
                {
                    vect[0] = 1.5;
                    streamWriter.WriteLine(vect);
                }

                streamWriter.WriteLine(matrixB.rows);
                streamWriter.WriteLine(matrixB.rows);
                foreach (Vector vect in matrixB)
                {
                    streamWriter.WriteLine(vect);
                }
                streamWriter.Close();
                fileStream.Close();
            }

            if ((bool)radioButton2.IsChecked)
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                string filename;
                fileDialog.DefaultExt = ".data";
                fileDialog.Filter = "Binary files (data)|*.data";
                              
                if (fileDialog.ShowDialog() == true)
                    filename = fileDialog.FileName;
                else
                    return;

                FileStream fileStream = new FileStream(filename, FileMode.CreateNew);
                DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Compress);
                BinaryWriter bw = new BinaryWriter(deflateStream);
                bw.Write(matrixA.rows);
                bw.Write(matrixA.columns);
                foreach (Vector vect in matrixA)
                { 
                    for (int j = 0; j < matrixA.columns; j++)
                    {
                        bw.Write(vect[j]);
                    }
                    
                }
                bw.Write(matrixB.rows);
                bw.Write(matrixB.columns);
                foreach (Vector vect in matrixB)
                {
                    for (int j = 0; j < matrixB.columns; j++)
                    {
                        bw.Write(vect[j]);
                    }
                }
                bw.Close();
                deflateStream.Close();
                fileStream.Close();
                
            }
        }

        private void buttonReadFile_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radioButton1.IsChecked)
            {
                FileDialog fileDialog = new OpenFileDialog();
                string filename;
                fileDialog.DefaultExt = ".txt";
                fileDialog.Filter = "Text documents (txt)|*.txt";
                if (fileDialog.ShowDialog() == true)
                    filename = fileDialog.FileName;
                else
                    return;
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                StreamReader sr = new StreamReader(fileStream);

                textBox.Text = sr.ReadLine();
                textBox1.Text = sr.ReadLine();
                Matrix matrixA = new Matrix(Convert.ToInt32(textBox.Text), Convert.ToInt32(textBox1.Text));
                for (int i = 0; i < matrixA.rows; i++)
                {
                    matrixA[i].StrToVector(sr.ReadLine());
                }
                textBox2.Text = sr.ReadLine();
                textBox3.Text = sr.ReadLine();
                Matrix matrixB = new Matrix(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
                for (int i = 0; i < matrixB.rows; i++)
                {
                    matrixB[i].StrToVector(sr.ReadLine());
                }
                CreateTextBoxes(matrixA, wrapPanel, ref textBoxDimA, ref cellsDimA);
                CreateTextBoxes(matrixB, wrapPanel1, ref textBoxDimB, ref cellsDimB);
                sr.Close();
                fileStream.Close();
            }
            if ((bool)radioButton2.IsChecked)
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                string filename;
                fileDialog.DefaultExt = ".data";
                fileDialog.Filter = "Text documents (data)|*.data";
                if (fileDialog.ShowDialog() == true)
                    filename = fileDialog.FileName;
                else
                    return;
                FileStream fileStream = new FileStream(filename, FileMode.Open);
                DeflateStream deflateStream = new DeflateStream(fileStream, CompressionMode.Decompress);
                BinaryReader binaryReader = new BinaryReader(deflateStream);
                Matrix matrixA = new Matrix(binaryReader.ReadInt32(), binaryReader.ReadInt32());
                textBox.Text = Convert.ToString(matrixA.rows);
                textBox1.Text = Convert.ToString(matrixA.columns);
                foreach (Vector vect in matrixA)
                {
                    for (int i = 0; i < matrixA.columns; i++)
                    {
                        vect[i] = binaryReader.ReadDouble();
                    }
                }
                Matrix matrixB = new Matrix(binaryReader.ReadInt32(), binaryReader.ReadInt32());
                textBox2.Text = Convert.ToString(matrixB.rows);
                textBox3.Text = Convert.ToString(matrixB.columns);
                foreach (Vector vect in matrixB)
                {
                    for (int i = 0; i < matrixB.columns; i++)
                    {
                        vect[i] = binaryReader.ReadDouble();
                    }
                }
                CreateTextBoxes(matrixA, wrapPanel, ref textBoxDimA, ref cellsDimA);
                CreateTextBoxes(matrixB, wrapPanel1, ref textBoxDimB, ref cellsDimB);

            }

        }

        private void buttonLinq_Click(object sender, RoutedEventArgs e)
        {
            groupedMatrix.Children.Clear();
            result.FindLengthOfVectors();
            var matrix = result.Where(vect => vect.mathLength > Convert.ToInt32(minimumLength.Text));
            Matrix temp = new Matrix(matrix.Count(), result.columns);
            int count = 0;
            foreach (Vector v in matrix)
            {
                temp[count++] = v;

            }
            CreateTextBlockses(temp, groupedMatrix, ref textBoxDimR);
            Vector vector = result.Max<Vector>();
        }
    }
}
