namespace TaskManager.API.Models
{
    public class Taskitem
    {
        

        // Models/Task.cs
        
        
            public int Id { get; set; }
            public string? Title { get; set; }
            public bool IsCompleted { get; set; }
            public string UserId { get; set; }
            public User User { get; set; }
        

    }
}
