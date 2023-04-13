using DotNetCoreHomeWork.Core.Dto;
using DotNetCoreHomeWork.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreHomeWork.Core.Service.Interface
{
    public interface ILeaderBoardService
    {
        Task<int> UpdateScore(UpdateScoreModel updateScoreModel);
        Task<List<LeaderBoardInfoDto>> GetCustomersByRank(int start, int end);
        Task<List<LeaderBoardInfoDto>> GetCustomersByCustomerid(LeaderBoardQuery query);
    }
}
