namespace BizLand.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string ImagePath { get; set; }
        public bool isDeleted { get; set; }=false;
    }
}
