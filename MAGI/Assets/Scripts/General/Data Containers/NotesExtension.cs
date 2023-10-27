using System.Text.RegularExpressions;

namespace General.Data_Containers
{
    public static class NotesExtension
    {
        /// <summary>
        ///  Converts the Notes enum to a formatted string
        /// </summary>
        /// <param name="note"> the name of the note from the enum </param>
        /// <returns> a new string with the formatted note </returns>
        public static string ToFormattedString(this Notes note)
        {
            var noteString = note.ToString();

            var formattedNote = Regex.Replace(noteString, "_", "/")
                .Replace("s", "♯"); 
            
            return formattedNote;
        }
    }
}