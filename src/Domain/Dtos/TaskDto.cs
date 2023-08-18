namespace Domain.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
