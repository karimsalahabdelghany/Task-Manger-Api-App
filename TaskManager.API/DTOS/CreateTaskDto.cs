namespace TaskManager.API.DTOS
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
