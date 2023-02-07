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

namespace media_player
{
    /// <summary>
    /// Interaction logic for TagEditing.xaml
    /// </summary>
    public partial class TagEditing : UserControl
    {
        public TagEditing()
        {
            InitializeComponent();
        }
        public event EventHandler Save;
        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            if (Save != null)
            {
                this.Save(this, e);//envoke custom event
            }
        }
    }
}
