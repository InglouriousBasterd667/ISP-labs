
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Win32;

namespace WpfApplication1
{
    public class Hotkeys 
    {
        private static RoutedUICommand addition;

        static Hotkeys()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.A, ModifierKeys.Control, "Ctrl+A"));
            addition = new RoutedUICommand("Addition", "Addition", typeof(Hotkeys), inputs);
        }

        public static RoutedUICommand Addition
        {
            get { return addition; }
        }
    }
    
}
