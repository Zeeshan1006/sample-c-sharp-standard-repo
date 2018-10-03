using Dapper;
using SampleApp.Common.Contstants;
using SampleApp.Connection.Interfaces;
using SampleApp.Entities;
using SampleApp.Repository.Interfaces;
using SampleApp.Repository.Params;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Repository.Implementations
{
    public class LawAcceptanceRepository : ILawAcceptanceRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IDbConnection connection;

        public LawAcceptanceRepository()
        {

        }

        public LawAcceptanceRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> AcceptBusinessReq(AcceptBusinessRequirement accept)
        {
            try
            {
                var secIds = await GetListOfAllSectionsAsync(accept);

                var azimuthProcess = new List<int>();
                await GetAllAzimuthProcessBySectionIdAsync(accept.SectionId, azimuthProcess);

                var companyProcessId = await GetCompanyProcessByAzimuthProcess(azimuthProcess);

                foreach (var item in azimuthProcess)
                {
                    await UpdateLawProcess(accept, secIds, item);
                }

                foreach (var item in secIds)
                {
                    await AcceptSectionOfLaw(accept, companyProcessId, item);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private async Task<List<int>> GetListOfAllSectionsAsync(AcceptBusinessRequirement accept)
        {
            var parentSection = await connection.QueryAsync<int>(QueryConstants.GET_PARENT_OF_MAPPED, new
            {
                sectionId = new List<int>()
                {
                    accept.SectionId
                }
            }).ConfigureAwait(false);

            var secIds = new List<int>();

            if (parentSection.FirstOrDefault() > 0)
            {
                secIds = await AllMappedSectionId(parentSection.FirstOrDefault());
            }

            if (secIds != null && secIds.Count() != 0)
            {
                parentSection = await connection.QueryAsync<int>(QueryConstants.GET_PARENT_OF_MAPPED, new
                {
                    sectionId = secIds
                }).ConfigureAwait(false);
                if (parentSection.Count() == 1)
                {
                    accept.SectionId = parentSection.FirstOrDefault();
                }
            }

            else
            {
                secIds = new List<int>
                {
                    accept.SectionId
                };
            }

            return secIds;
        }

        private async Task<List<int>> AllMappedSectionId(int sectionId)
        {
            var mappedLaws = await connection.QueryAsync<int>(QueryConstants.GET_ALL_MAPPED, new
            {
                sectionId
            }).ConfigureAwait(false);

            List<int> sections = new List<int>();
            sections.AddRange(mappedLaws);
            return sections;
        }

        private async Task GetAllAzimuthProcessBySectionIdAsync(int sectionId, List<int> azimuthProcess)
        {
            var azPro = await connection.QueryAsync<int>(QueryConstants.GetAzimuthProcess, new
            {
                sectionId
            }).ConfigureAwait(false);

            azimuthProcess.AddRange(azPro);
        }

        private async Task<IEnumerable<int>> GetCompanyProcessByAzimuthProcess(List<int> azimuthProcess)
        {
            return await connection.QueryAsync<int>(QueryConstants.GetCompanyProcess, new
            {
                azimuthProcessId = azimuthProcess
            });
        }

        private async Task UpdateLawProcess(AcceptBusinessRequirement accept, List<int> secIds, int item)
        {
            var azProcess = new List<int> { item };

            var companyProcessId1 = GetCompanyProcessByAzimuthProcess(azProcess);

            var companyProcesses = string.Join(",", companyProcessId1);

            UpdateParams updateParams = new UpdateParams
            {
                AzimuthProcessId = item,
                CompanyProcessId = companyProcesses,
                Flag = (int)accept.Flag,
                SectionId = secIds,
                LawDetailId = accept.LawDetailId,
                UserId = accept.UserId,
                ModifiedBy = accept.UserName
            };

            await UpdateLawProcesses(updateParams);
        }

        private async Task UpdateLawProcesses(UpdateParams updateParams)
        {
            await connection.ExecuteAsync(QueryConstants.UPDATE_PROCESS,
                                        new
                                        {
                                            companyProcessId = updateParams.CompanyProcessId,
                                            flag = updateParams.Flag,
                                            lawDetailId = updateParams.LawDetailId,
                                            sectionId = updateParams.SectionId,
                                            userId = updateParams.UserId,
                                            azimuthProcessId = updateParams.AzimuthProcessId,
                                            updateParams.ModifiedBy
                                        }).ConfigureAwait(false);
        }

        private async Task AcceptSectionOfLaw(AcceptBusinessRequirement accept, IEnumerable<int> companyProcessId, int sectionId)
        {
            foreach (var item in companyProcessId)
            {
                var chkexst = await CheckExistanceInLawStatusBusinessModule(sectionId, item);

                if (!chkexst)
                {
                    AcceptParams acceptParams = new AcceptParams
                    {
                        SectionId = sectionId,
                        CreatedBy = accept.UserName,
                        LawDetailId = accept.LawDetailId,
                        CompanyProcessId = item,
                        Flag = (int)accept.Flag,
                        UserId = accept.UserId
                    };
                    await AcceptLaw(acceptParams);
                }
            }
        }

        private async Task<bool> CheckExistanceInLawStatusBusinessModule(int SectionId, int CompanyProcessId)
        {
            return await connection.ExecuteScalarAsync<bool>("SELECT Id FROM LawStatusBusinessModule WHERE SectionId = @SectionId AND CompanyProcessId = @CompanyProcessId", new
            {
                SectionId,
                CompanyProcessId
            });
        }

        private async Task AcceptLaw(AcceptParams acceptParams)
        {
            await connection.ExecuteAsync(QueryConstants.ACCEPT_BRR_LSBM_SQL_QUERY,
                                           new
                                           {
                                               acceptParams.LawId,
                                               acceptParams.SectionId,
                                               acceptParams.ApproverId,
                                               StatusId = 3,
                                               IsCurrentStatus = 1,
                                               acceptParams.CreatedBy,
                                               acceptParams.LawDetailId,
                                               TabIndex = 0,
                                               Step = 0,
                                               acceptParams.UserId,
                                               companyProcessId = acceptParams.CompanyProcessId,
                                               acceptParams.Flag
                                           }).ConfigureAwait(false);
        }
    }
}
