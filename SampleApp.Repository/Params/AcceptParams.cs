namespace SampleApp.Repository.Params
{
    class AcceptParams
    {
        public int LawId { get; set; }
        public int LawDetailId { get; set; }
        public int SectionId { get; set; }
        public int? ApproverId { get; set; }
        public int UserId { get; set; }
        public int CompanyProcessId { get; set; }
        public int Flag { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
        public string Comments { get; set; } = string.Empty;
    }
}
