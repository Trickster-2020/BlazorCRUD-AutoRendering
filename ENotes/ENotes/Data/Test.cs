namespace ENotes.Data
{
    public class Test
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string   UserId { get; set; }
        public ApplicationUser User { get; set; }  
    }
}
