using System.Collections.Generic;

namespace SampleApp.Repository.Params
{
    class UpdateParams
    {
        public int LawDetailId { get; set; }
        public List<int> SectionId { get; set; }
        public int AzimuthProcessId { get; set; }
        public string CompanyProcessId { get; set; }
        public int Flag { get; set; }
        public int UserId { get; set; }
        public string ModifiedBy { get; set; }
    }
}
