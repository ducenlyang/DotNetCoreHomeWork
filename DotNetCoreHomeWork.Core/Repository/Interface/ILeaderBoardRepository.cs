using DotNetCoreHomeWork.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreHomeWork.Core.Repository.Interface
{
    public interface ILeaderBoardRepository
    {
        Task<SortedSet<LeaderBoardModel>> GetAllData();
        Task<LeaderBoardModel> GetCustomerScoreById(Int64 customerId);
        Task<bool> Remove(LeaderBoardModel model);
        Task<bool> Add(LeaderBoardModel model);
    }
}
