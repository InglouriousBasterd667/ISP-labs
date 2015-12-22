using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player.ViewModels;
using System.Windows.Input;
using Player.Models;
using System.Collections.ObjectModel;
using System.Collections;
//using Player.ViewModels;

namespace Player.ViewModels
{
    class CompositionLoader : BaseViewModel
    {

        private ObservableCollection<Composition> library;
        private ObservableCollection<BaseViewModel> viewModels;
        private IList selectedModels = new ArrayList ();
        public string Name { get; set; }
        public int ID { get; set; }
        public ICommand AddPlayListCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
       
        public IList SelectedModels
        {
            get { return selectedModels; }
            set
            {
                selectedModels = value;
                NotifyPropertyChanged("TestSelected");
            }
        }
        
        public int txtID { get; set; }
        public string txtArtist { get; set; }
        public string txtTitle { get; set; }
        public TimeSpan txtLength { get; set; }
        public int txtRating { get; set; }
        public Composition.Genres txtGenre { get; set; }

        public ObservableCollection<Composition> Library
        {
            get
            {
                return library;
            }
            private set { }
        }

        public CompositionLoader(ObservableCollection<Composition> library,ObservableCollection<BaseViewModel> viewModel)
        {
            AddCommand = new Command(action => Add());
            DeleteCommand = new Command(action => Delete());
            AddPlayListCommand = new Command(action => AddPlayList());
            this.library = library;
            this.viewModels = viewModel;
            header = "Library";
        }
        public void AddPlayList()
        {
            if (SelectedModels.Count != 0)
            { 
                PlayList list = new PlayList(ID, Name);
                foreach (Composition comp in SelectedModels)
                {
                    list.AddComposition(comp);
                }
                PlayListViewModel playlist = new PlayListViewModel(list, Name);
                viewModels.Add(playlist);
                NotifyPropertyChanged(String.Empty);
            }
        }
        
        private void Add()
        {
            Composition composition = new Composition(txtID, txtTitle, Convert.ToString(txtLength), 
                                                      txtArtist, txtGenre, txtRating);
            library.Add(composition);
            NotifyPropertyChanged("Library");
        }
        private void Delete()
        {
            int i = 0;
            int count = selectedModels.Count;
            Composition[] temp = new Composition[count];
            foreach(Composition comp in selectedModels)
            {
                temp[i++] = comp;
            }
            for (i = 0; i < count; i++)
            {
                library.Remove(temp[i]);
            }
        }


      
    }
}
