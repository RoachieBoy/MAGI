using System.Text.RegularExpressions;

namespace General.Data_Containers
{
    public static class NotesExtension
    {
        public static string ToFormattedString(this Notes note)
        {
            var noteString = note.ToString();

            var formattedNote = Regex.Replace(noteString, "_", "/")
                .Replace("s", "♯"); 
            
            return formattedNote;
        }
    }
}