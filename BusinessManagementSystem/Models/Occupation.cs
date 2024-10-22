namespace BusinessManagementSystem.Models
{
    public class Occupation:BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
