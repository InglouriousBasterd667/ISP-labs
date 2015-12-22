using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player.ViewModels;
using System.Windows.Input;
using Player.Models;
using System.Collections.ObjectModel;
using System.Threading;
using System.Collections;

namespace Player.ViewModels
{
    class PlayListViewModel:BaseViewModel
    {
        private PlayList playlist;
        private PlayList selectedList;
        private IList selectedModels = new ArrayList ();
        private Thread thread;
        public IList SelectedModels
        {
            get { return selectedModels; }
            set
            {
                selectedModels = value;
                NotifyPropertyChanged("TestSelected");
            }
        }
      /*  public string Name { get; set; }
        public int ID { get; set; }*/
        public string PlayButtonContent { get; set; }
        public ICommand PlayCommand { get; set; }
        public ICommand StopCommand { get; set; }
        public bool ButtonEnabled{ get; set; }
        public int Maximum { get; set; }
        public int Value { get; set; }
        public ObservableCollection<Composition> PlayList {
            get { return playlist.Compositions; }
            private set { }
        }
        public PlayListViewModel(PlayList playList,string name)
        {
            Maximum = 5;
            
            ButtonEnabled = true;
            PlayCommand = new Command(action => Play());
            StopCommand = new Command(action => Stop());
            PlayButtonContent = "Play";
            this.playlist = playList;
            header = name;
        }
        public void Play()
        {
            selectedList = new PlayList(0, null);
            if (selectedModels.Count == 0)
                selectedList = playlist;
            else
            {
                foreach(Composition comp in selectedModels)
                {
                    selectedList.AddComposition(comp);
                }
            }
           // ButtonEnabled = false;
            selectedList.IsStop = false;
            NotifyPropertyChanged("ButtonEnabled");
            selectedList.Complete += Stop;
            selectedList.GetProcessLength += Process;
            selectedList.Step += Step;
            selectedList.Reset += Reset;
            thread = new Thread(selectedList.Play);
            thread.IsBackground = true;
            thread.Start();
            PlayCommand = new Command(action => Pause());
            NotifyPropertyChanged("PlayCommand");
            PlayButtonContent = "Pause";
            NotifyPropertyChanged("PlayButtonContent");
        }
        
        private void Stop(bool stop)
        {
            // ButtonEnabled = true;
            PlayCommand = new Command(action => Play());
            NotifyPropertyChanged("PlayCommand");
            PlayButtonContent = "Play";
            NotifyPropertyChanged("PlayButtonContent");
            thread.Abort();
        }
        private void Process(int maximum)
        {
            Value = 0;
            Maximum = maximum;
            NotifyPropertyChanged("Maximum");
            
        }
        private void Step(int step)
        {
            Value += step;
            NotifyPropertyChanged("Value");
        }
        private void Reset()
        {
            Value = 0;
            NotifyPropertyChanged("Value");
        }

        private void Stop()
        {
            selectedList.IsStop = true;
        }
        private void Pause()
        {
            selectedList.IsPause = true;
            PlayCommand = new Command(action => Resume());
            NotifyPropertyChanged("PlayCommand");
            PlayButtonContent = "Resume";
            NotifyPropertyChanged("PlayButtonContent");
        }
        private void Resume()
        {
            selectedList.IsPause = false;
            PlayCommand = new Command(action => Pause());
            NotifyPropertyChanged("PlayCommand");
            PlayButtonContent = "Pause";
            NotifyPropertyChanged("PlayButtonContent");
        }

    }   
}
