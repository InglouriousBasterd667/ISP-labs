using System.Windows;
using Player.Models;
using Player.ViewModels;
namespace Player.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
            PlayerMainWindow.DataContext = new MainViewModel();
        }
    }
}
