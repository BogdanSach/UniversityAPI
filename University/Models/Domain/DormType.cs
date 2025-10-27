namespace UniversityAPI.Models.Domain
{
    public class DormType
    {
        public Guid Id { get; set; }            //PK
        public string TypeName { get; set; }

        // Navigation properties
        public ICollection<Dorm> Dorms { get; set; }
    }
}
