using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CMon.IoTApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<MenuItem> _mainMenuItems,
            _optionsMenuItems;
        public MainPage()
        {
            this.InitializeComponent();

            _mainMenuItems = MenuItem.GetMainItems();
            _optionsMenuItems = MenuItem.GetOptionsItems();
            hamburgerMenuControl.ItemsSource = _mainMenuItems;
            hamburgerMenuControl.OptionsItemsSource = _optionsMenuItems;

            hamburgerMenuControl.SelectedIndex = 0;
            contentFrame.Navigate(_mainMenuItems.First().PageType);
        }

        private void OnMenuItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = e.ClickedItem as MenuItem;
            contentFrame.Navigate(menuItem.PageType);
        }
    }

    public class MenuItem
    {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public Type PageType { get; set; }

        public static List<MenuItem> GetMainItems() =>
            new List<MenuItem>() {
                new MenuItem() { Icon = Symbol.Clock, Name = "Real Time", PageType = typeof(RealTimePage) }
            };


        public static List<MenuItem> GetOptionsItems() => new List<MenuItem>();
    }
}
