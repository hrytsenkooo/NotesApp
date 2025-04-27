namespace NotesApp.Domain.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }  
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}
