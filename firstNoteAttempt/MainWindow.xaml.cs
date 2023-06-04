using Microsoft.EntityFrameworkCore;
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

namespace firstNoteAttempt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ApplicationContext db = new ApplicationContext();
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // гарантируем, что база данных создана
            db.Database.EnsureCreated();
            db.Notes.Load();
            DataContext = db.Notes.Local.ToObservableCollection();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            NoteWindow NoteWindow = new NoteWindow(new Note());
            if (NoteWindow.ShowDialog() == true)
            {
                Note Note = NoteWindow.Note;
                db.Notes.Add(Note);
                try { db.SaveChanges(); } catch { MessageBox.Show("ALL INPUTS MUST BE FILLED"); db.Notes.Remove(Note); }
                
            }
        }
        // редактирование
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Note note = notesList.SelectedItem as Note;
            // если ни одного объекта не выделено, выходим
            if (note is null) return;

            NoteWindow NoteWindow = new NoteWindow(new Note
            {
                Id = note.Id,
                Header = note.Header,
                Content = note.Content
            });

            if (NoteWindow.ShowDialog() == true)
            {
                // получаем измененный объект
                note = db.Notes.Find(NoteWindow.Note.Id);
                if (note != null)
                {
                    note.Header = NoteWindow.Note.Header;
                    note.Content = NoteWindow.Note.Content;
                    db.SaveChanges();
                    notesList.Items.Refresh();
                }
            }
        }
        // удаление
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Note note = notesList.SelectedItem as Note;
            // если ни одного объекта не выделено, выходим
            if (note is null) return;
            db.Notes.Remove(note);
            db.SaveChanges();
        }
    }
}
