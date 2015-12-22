using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections;
using System.Windows;
namespace Player
{
   public class CustomListView : ListView
{

    public CustomListView ()
    {
        this.SelectionChanged += CustomListView_SelectionChanged;
    }

    void CustomListView_SelectionChanged (object sender, SelectionChangedEventArgs e)
    {
        this.SelectedItemsList = this.SelectedItems;
    }
    #region SelectedItemsList

    public IList SelectedItemsList
    {
        get { return (IList)GetValue (SelectedItemsListProperty); }
        set { SetValue (SelectedItemsListProperty, value); }
    }

    public static readonly DependencyProperty SelectedItemsListProperty =
            DependencyProperty.Register ("SelectedItemsList", typeof (IList), typeof (CustomListView), new PropertyMetadata (null));

    #endregion
} 
}
