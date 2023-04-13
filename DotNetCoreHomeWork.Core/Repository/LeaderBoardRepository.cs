using DotNetCoreHomeWork.Core.Attributes;
using DotNetCoreHomeWork.Core.Common;
using DotNetCoreHomeWork.Core.Constant;
using DotNetCoreHomeWork.Core.Model;
using DotNetCoreHomeWork.Core.Repository.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DotNetCoreHomeWork.Core.Repository
{
    [ServiceMapTo(typeof(ILeaderBoardRepository))]
    public class LeaderBoardRepository : ILeaderBoardRepository
    {
        public async Task<bool> Add(LeaderBoardModel model)
        {
            return await Task.FromResult(LeaderBoardData.Data.Add(model));
        }

        public async Task<SortedSet<LeaderBoardModel>> GetAllData()
        {
            return await Task.FromResult(LeaderBoardData.Data);
        }

        public async Task<LeaderBoardModel> GetCustomerScoreById(long customerId)
        {
            return await Task.FromResult(LeaderBoardData.Data.FirstOrDefault(x => x.CustomerId == customerId));
        }

        public async Task<bool> Remove(LeaderBoardModel model)
        {
            if (model != null)
            {
               return await Task.FromResult(LeaderBoardData.Data.Remove(model));
            }
            return await Task.FromResult(false);
        }
    }
}
