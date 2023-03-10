using Noted.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noted.Data
{
    public class NoteDatabase
    {
        static SQLiteAsyncConnection database;

        public static readonly AsyncLazy<NoteDatabase> instance = 
            new AsyncLazy<NoteDatabase>(async () =>
            {
                NoteDatabase db = new NoteDatabase();
                try
                {
                    CreateTableResult resutlt = await database.CreateTableAsync<Note>();
                }catch(Exception ex)
                {
                    throw;
                }
                return db;
            });

        public NoteDatabase()
        {
            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Note>> GetNotesAsync()
        {
            return database.Table<Note>().ToListAsync();
        }

        public Task<List<Note>> GetNotes<NotDoneAsync>()
        {
            return database.QueryAsync<Note>("SELECT * FROM  [Note] WHERE [DONE] = 0");
        }

        public Task<Note> GetNotemAsync(int id)
        {
            return database.Table<Note>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Note note)
        {
            if(note.ID != 0)
            {
                return database.UpdateAsync(note);
            }
            else
            {
                return database.InsertAsync(note);
            }
        }

        public Task<int> DeleteNoteAsync(Note note)
        {
            return database.DeleteAsync(note);
        }
    }
}
