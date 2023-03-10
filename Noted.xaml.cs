using Noted.Classes;
using Noted.Data;
using SQLite;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace Noted;

public partial class Noted : ContentPage
{
    public Noted()
	{
		InitializeComponent();

        OnAppearing();

        datePicker.MinimumDate = DateTime.Today;
        datePicker.MaximumDate = new DateTime(2100, 12, 31);
    }


    void OnEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        string oldText = e.OldTextValue;
        string newText = e.NewTextValue;
        string myText = task.Text;
    }

    void OnEntryCompleted(object sender, EventArgs e)
    {
        OnSaveClicked(sender, e);
        refreshListView();
    }

    public async void refreshListView()
    {
        lista.ItemsSource = null;
        NoteDatabase database = await NoteDatabase.instance;
        lista.ItemsSource = await database.GetNotesAsync();
    }

    async void OnSaveClicked(object sender, EventArgs e)
    {
        Note note = createNote();

        NoteDatabase database = await NoteDatabase.instance;
        await database.SaveNoteAsync(note);
        await Navigation.PopAsync();

        if (string.IsNullOrWhiteSpace(note.description))
        {
            await DisplayAlert("Alert!", "Description Required", "close");
            return;
        }
        task.Text = "";
    }

    public Note createNote()
    {
        Note note = new Note();
        note.description = task.Text;
        note.date = datePicker.Date;
        note.done = 0;
        return note;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        NoteDatabase database = await NoteDatabase.instance;
        lista.ItemsSource = await database.GetNotesAsync();
    }


    public bool canFormATriangle(int[] sideLengths)
    {
        if (!(sideLengths?.Length > 0))
        {
            throw new ArgumentException();
        }

        foreach(int i in sideLengths)
        {
            if (i <= 0)
            {
                throw new ArgumentException();
            }
        }

        int a = sideLengths[0];
        int b = sideLengths[1];
        int c = sideLengths[2];
        if (a + b > c && a+c>b && b+c>a)
        {
            return true;
        }
        return false;
    }
}