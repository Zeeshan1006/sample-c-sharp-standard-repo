namespace SampleApp.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class AcceptBusinessRequirement
    {
        [Required]
        public int SectionId { get; set; }
        public bool? IsSubmitted { get; set; }
        public int UserId { get; set; }
        [Required]
        public int LawDetailId { get; set; }
        [Required]
        public AcceptanceFlag Flag { get; set; }
        public int StatusId { get; set; }
        public string UserName { get; set; }
    }

    public enum AcceptanceFlag
    {
        NoChange = 1,
        Change,
        RequirementEdited,
        NewRequirement
    }
}
