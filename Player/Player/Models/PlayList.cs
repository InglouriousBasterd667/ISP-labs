using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Threading;
using System.Media;
using System.Windows.Input;
using Player.Models;
using System.Diagnostics;
namespace Player.Models
{
    class PlayList : IEnumerable<Composition>
    {
        private const int SECONDS = 5;
        private const int MSECONDS = 1000 * SECONDS;
        private const int SECOND = 1;
        private const int MSECOND = 1000;
        
        private ObservableCollection<Composition> playList = new ObservableCollection<Composition>();

        private int id;
        private string name;
        private TimeSpan length;
        private double rating;
        
        private bool isStop = false;
        private bool isPause = false;
        public PlayList(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public bool IsStop
        {
            get { return isStop; }
            set { isStop = value; }
        }
        public bool IsPause
        {
            get { return isPause; }
            set { isPause = value; }
        }
        public ObservableCollection<Composition> Compositions
        {
            get { return playList; }
            private set { }
        }
        public int ID
        {
            get { return id; }
            private set { }
        }
        public string Name
        {
            get { return name; }
            private set { }
        }
        public void AddComposition(Composition composition)
        {
            playList.Add(composition);
            rating = playList.Average(comp => comp.Rating);
            length = new TimeSpan(playList.Sum(comp=> comp.Length.Ticks));
        }
        public IEnumerator<Composition> GetEnumerator()
        {
            foreach(var comp in playList)
            {
                yield return comp;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public event Action<bool> Complete;
        public event Action<int> GetProcessLength;  
        public event Action<int> Step;
        public event Action Reset;
    
        public void Play()
        {

            foreach(Composition comp in playList)
            {
                if (isStop)
                    break;
                GetProcessLength(comp.Length.Seconds);
                for (int i = 0; i < (comp.Length.Seconds / SECONDS); i++)
                {
                    if (isStop)
                        break;
                    SystemSounds.Beep.Play();
                    var sw = new Stopwatch(); 
                    for (int j = 0; j < SECONDS; j++)
                    {
                        if (isStop)
                            break;
                        Step(SECOND);
                        sw.Reset();
                        sw.Start();
                        while(sw.Elapsed.Seconds < 1 || isPause)
                        {
                            if (IsPause)
                                sw.Stop();
                            else
                                sw.Start();
                            if (isStop)
                                break;
                            Thread.Sleep(10);
                        }
                        sw.Stop();
                    }
                }
                //Reset();
            }
            Reset();
            Complete(isStop);
        }
    }
}
