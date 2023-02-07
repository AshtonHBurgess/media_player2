using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.XPath;

namespace media_player
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///
    
    // create an app that can allows user to open up selected mp3 file   play and stop    see tag data   and edit tag data

    //USe  media player control    in WPF

    //reading and writting tags data , you can use 3rd party references that give you the ability to read and write 
    
    //user will be able to open a file dialog and search mp3 file


    //use at least one user control,    suggestion  the editor screen   or now playing

    //layout managers are containsers    grids, stack panels  so you can show that you iunderstand how to layout a formm in a responsive way

    //read and write tag data forL:   Song Title Artist Year

    //we will be using taglib

    //EGNORE THE EDIT MENU
    //ONLY NEED FILE MENU AND A MEDIA MENU
   
    public partial class MainWindow : Window
    {
         


        TagLib.File currentFile;

        public MainWindow()
        {
            InitializeComponent();
            player2.Save += Player2_Save;
        }


        string[] files;
        bool tags;
        OpenFileDialog fileDlg = new OpenFileDialog();
        private void Player2_Save(object? sender, EventArgs e)
        {
            myMediaPlayer.Stop();
            myMediaPlayer.Close();
            currentFile = TagLib.File.Create(fileDlg.FileName);
            currentFile.Tag.Year = Convert.ToUInt32(player2.textBox_Year.Text.ToString());
            currentFile.Tag.Title = player2.textBox_Title.Text.ToString();
            player2.textBox_Title.Text.ToString();
            currentFile.Tag.AlbumArtists[0] = player2.textBox_Band.Text.ToString();
            currentFile.Tag.Album = player2.textBox_Album.Text.ToString();
            currentFile.Save();
            player2.Visibility= Visibility.Collapsed;
            player1.Visibility = Visibility.Visible;

        }





        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            myMediaPlayer.Stop();
            myMediaPlayer.Close();
            currentFile.Dispose();


            //currentFile = TagLib.File.Create(paths[PlayList_ListBox.SelectedIndex]);
            myMediaPlayer.Stop();
            myMediaPlayer.Close();
            currentFile.Dispose();
            currentFile = TagLib.File.Create(PlayList_ListBox.SelectedItem.ToString());
            myMediaPlayer.Source = new Uri(PlayList_ListBox.SelectedItem.ToString());
            try { myMediaPlayer.Play(); } catch { }
            showTags_Click(this, e);
        }

        private void showTags_Click(object sender, RoutedEventArgs e)
        {
            tags = true;
            if (tags)
            {
                player1.Visibility = Visibility.Visible;
                player2.Visibility = Visibility.Collapsed;
                try
                {
                    if (currentFile != null)
                    {
                        var year = currentFile.Tag.Year;
                        var title = currentFile.Tag.Title;
                        var album = currentFile.Tag.Album;
                        var band = currentFile.Tag.AlbumArtists;
                        //tagNameBox.Text = title + " : " + year.ToString();
                        player1.tagTitlePlay.Text ="Title: " + title;
                        player1.tagYearPlay.Text ="Year: " + year.ToString();
                        player1.tagAlbumPlay.Text ="Album: " + album;
                        player1.tagBandPlay.Text ="Band: " + band.FirstOrDefault();
                    }
                }
                catch { }
            }
        
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TagCurrent_Click(object sender, RoutedEventArgs e)
        {
            EditTags_Click_1(this, e);
            try
            {
                if (currentFile != null)
                {
                    var year = currentFile.Tag.Year;
                    var title = currentFile.Tag.Title;

                    //tagNameBox.Text = title + " : " + year.ToString();
                }
            }
            catch { }


        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            //create and instantiate OpenFileDialog  
            fileDlg.Filter = "MP3 file (*.mp3)|*.mp3|(*.MP3)|*.MP3 | All files (*.*)|*.*";
            fileDlg.Multiselect = true;


            //displays dialog on screen
            if (fileDlg.ShowDialog() == true)
            {
                //fileNameBox.Text = fileDlg.FileName;


                //files = fileDlg.SafeFileNames;
                //paths = fileDlg.FileNames;

                files = fileDlg.FileNames;
                for (int i = 0; i < files.Length; i++)
                {
                    PlayList_ListBox.Items.Add(files[i]);
                

                }

                currentFile = TagLib.File.Create(fileDlg.FileName);
                myMediaPlayer.Source = new Uri(fileDlg.FileName);
                try { myMediaPlayer.Play();
                    showTags_Click(this, e);
                } catch { }
            
            }

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            try { myMediaPlayer.Play(); } catch { }
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            try { myMediaPlayer.Pause(); } catch { }
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            try { myMediaPlayer.Stop(); } catch { }
        }

        //private void editTags_Click(object sender, RoutedEventArgs e)
        //{
         
        //}

        private void EditTags_Click_1(object sender, RoutedEventArgs e)
        {
            tags = false;
            if (!tags)
            {
                player1.Visibility = Visibility.Collapsed;
                player2.Visibility = Visibility.Visible;
                try
                {
                    if (currentFile != null)
                    {
                        var year = currentFile.Tag.Year;
                        var title = currentFile.Tag.Title;
                        var album = currentFile.Tag.Album;
                        var band = currentFile.Tag.AlbumArtists;
                        //tagNameBox.Text = title + " : " + year.ToString();
                        //player2.tagTitleEdit.Text = title;
                        //player2.tagYearEdit.Text = year.ToString();
                        //player2.tagAlbumEdit.Text = album;
                        //player2.tagBandEdit.Text = band.FirstOrDefault();

                        player2.textBox_Title.Text = title;
                        player2.textBox_Year.Text = year.ToString();
                        player2.textBox_Album.Text = album;
                        player2.textBox_Band.Text = band.FirstOrDefault();
                        currentFile.Dispose();
                    }
                }
                catch { }
            }
        }
    }
}
