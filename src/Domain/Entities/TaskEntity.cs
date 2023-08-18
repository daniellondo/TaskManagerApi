namespace Domain.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class TaskEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
