namespace SampleApp.Common.Contstants
{
    public static class QueryConstants
    {
        public const string GET_PARENT_OF_MAPPED = @"select SectionId from [dbo].[LawCombineBase] where SectionId IN @sectionId OR CombineSectionId IN @sectionId";
        public const string GET_ALL_MAPPED = @"select SectionId from [dbo].[LawCombineBase] where (SectionId = @sectionId OR CombineSectionId = @sectionId) AND IsSubmitted = 1
        Union
        select CombineSectionId from[dbo].[LawCombineBase] where(SectionId = @sectionId OR CombineSectionId = @sectionId) AND IsSubmitted = 1";

        public const string GetAzimuthProcess = @"select AzimuthProcessId from LawProcess where SectionId = @sectionId";

        public const string GetCompanyProcess = @"select Distinct CompanyProcessId from [dbo].[AzimuthProcessMapping] where AzimuthProcessId IN @azimuthProcessId and IsActive = 1";
        public const string UPDATE_PROCESS = @"Update [dbo].[LawProcess]
        Set CompanyProcessId = @companyProcessId, ModifiedDate = GetDate(), ModifiedBy = @ModifiedBy, LawDetailId = @lawDetailId, flag = @flag, UserId = @userId, IsSubmitted = 1
        where SectionId IN @sectionId and AzimuthProcessId = @azimuthProcessId";
        public const string ACCEPT_BRR_LSBM_SQL_QUERY = @"INSERT INTO LawStatusBusinessModule(SectionId,UserId,ApproverId,StatusId,IsCurrentStatus,CreatedDate,CreatedBy,TabIndex,Step, CompanyProcessId, flag)
        VALUES(@SectionId,@UserId,@ApproverId,@StatusId,@IsCurrentStatus,GetDate(),@CreatedBy,@TabIndex,@Step, @companyProcessId, @Flag)";
    }
}
