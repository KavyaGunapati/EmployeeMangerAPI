namespace EmployeeMangerAPI.DTOs
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
    public class CreateProjectDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
