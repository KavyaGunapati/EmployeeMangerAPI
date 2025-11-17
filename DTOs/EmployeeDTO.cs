namespace EmployeeMangerAPI.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public List<string> Skills { get; set; }
    }
    public class CreateEmployeeDTO
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public List<string> Skills { get; set; }
    }
}
