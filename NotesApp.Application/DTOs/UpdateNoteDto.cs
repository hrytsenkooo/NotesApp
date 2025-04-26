namespace NotesApp.Application.DTOs
{
    public class UpdateNoteDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; }
    }
}
