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
using System.Windows.Shapes;

namespace firstNoteAttempt
{
    /// <summary>
    /// Interaction logic for NoteWindow.xaml
    /// </summary>
    public partial class NoteWindow : Window
    {
        public Note Note { get; private set; }
        public NoteWindow(Note note)
        {
            InitializeComponent();
            Note = note;
            DataContext = Note;
        }
        void AcceptClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
