using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Player.Models;
namespace Player.ViewModels
{
    class MainViewModel
    {
        private ObservableCollection<Composition> library;
        private ObservableCollection<BaseViewModel>  viewModels;
        public ObservableCollection<BaseViewModel> ViewModels
        {
            get { return viewModels; }
            private set { }
        }


        public  MainViewModel()
        {
            viewModels = new ObservableCollection<BaseViewModel>();
            library = new ObservableCollection<Composition>();
            AddSongLoader();
        }
        

        public void AddSongLoader()
        {
            viewModels.Add(new CompositionLoader(library,viewModels));
        }
}
}
