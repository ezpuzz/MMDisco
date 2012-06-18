using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MMDisco
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Load in mediamonkey automation object
            SongsDB.SDBApplication SDB = new SongsDB.SDBApplication();

            // Only get the first album selected
            SongsDB.SDBAlbum mmAlbum = SDB.SelectedSongList.Albums.get_Item(0);

            // Add tracks to mediamonkey track list
            for (int i = 0; i < mmAlbum.Tracks.Count; i++)
            {
                mmTrackList.Items.Add(new MMTrack(mmAlbum.Tracks.get_Item(i)));
            }

            // set search box to album and artist
            searchBox.Text = mmAlbum.Artist.Name + ' ' + mmAlbum.Name;
        }

        private void releaseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (releaseList.HasItems)
            {
                releaseTrackList.ItemsSource = DiscogsHelper.loadTracks((DiscogsSearchResult)e.AddedItems[0]);

                // set primary art to main art display
                releaseCoverArt.Source 
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            releaseList.ItemsSource = DiscogsHelper.search(searchBox.Text);
        }

        private void image1_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
