using DotNetCoreHomeWork.Core.Attributes;
using DotNetCoreHomeWork.Core.Common;
using DotNetCoreHomeWork.Core.Constant;
using DotNetCoreHomeWork.Core.Dto;
using DotNetCoreHomeWork.Core.Model;
using DotNetCoreHomeWork.Core.Repository;
using DotNetCoreHomeWork.Core.Repository.Interface;
using DotNetCoreHomeWork.Core.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DotNetCoreHomeWork.Core.Service
{
    [ServiceMapTo(typeof(ILeaderBoardService))]
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly ILeaderBoardRepository _leaderBoardRepository;
        private readonly IMemoryCache _memoryCache;

        public LeaderBoardService(IMemoryCache memoryCache,
            ILeaderBoardRepository leaderBoardRepository)
        {
            _memoryCache = memoryCache;
            _leaderBoardRepository = leaderBoardRepository;
        }

        private void RemoveCacheData()
        {
            _memoryCache.Remove(CacheKeyConst.LeaderBoardDataKey);
        }

        private async Task<LeaderBoardModel> GetCustomerScoreById(Int64 customerId)
        {
            var customerScore = default(LeaderBoardModel);

            await LockTool.ReadLockFunc(async () =>
            {
                customerScore = await _leaderBoardRepository.GetCustomerScoreById(customerId);
            });
            return customerScore;
        }

        public async Task<int> UpdateScore(UpdateScoreModel updateScoreModel)
        {
            var customerScore = await GetCustomerScoreById(updateScoreModel.CustomerId);

            LockTool.WriteLockAction(async () =>
            {
                RemoveCacheData();
                await _leaderBoardRepository.Remove(customerScore);
                await _leaderBoardRepository.Add(new LeaderBoardModel(updateScoreModel));
                RemoveCacheData();
            });

            return await Task.FromResult(updateScoreModel.Score);
        }

        public async Task<List<LeaderBoardInfoDto>> GetCustomersByRank(int start, int end)
        {
            var cacheData = await GetLeaderBoardCacheData();
            return await GetLeaderBoardRangeData(cacheData, start, end);
        }

        private async Task<SortedSet<LeaderBoardModel>> GetLeaderBoardCacheData()
        {
            return await _memoryCache.GetOrCreateAsync(CacheKeyConst.LeaderBoardDataKey, async entry =>
            {
                var cacheData = await LockTool.ReadLockFunc(() =>
                {
                    return _leaderBoardRepository.GetAllData();
                });
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
                return cacheData;
            });
        }

        private async Task<List<LeaderBoardInfoDto>> GetLeaderBoardRangeData(SortedSet<LeaderBoardModel> leaderBoardData, int start, int end)
        {
            VerifyRankRange(leaderBoardData, start, end);
            var result = TakeLeaderBoardRangeData(leaderBoardData, start, end);
            return await Task.FromResult(result);
        }

        private void VerifyRankRange(SortedSet<LeaderBoardModel> data, int startIndex, int endIndex)
        {
            if (startIndex < 1 || endIndex < 1 || endIndex < startIndex)
            {
                throw new ArgumentOutOfRangeException();
            }
            if (data.Count < endIndex)
            {
                throw new ArgumentException("endNum larger than leaderBoard Count");
            }
        }

        private List<LeaderBoardInfoDto> TakeLeaderBoardRangeData(SortedSet<LeaderBoardModel> leaderBoardData, int start, int end)
        {
            int skipNum = start - 1;
            int takeNum = end - start + 1;
            return leaderBoardData.Skip(skipNum).Take(takeNum).Select(x => new LeaderBoardInfoDto()
            {
                CustomerId = x.CustomerId,
                Score = x.Score,
                Rank = start++
            }).ToList();
        }

        private int GetCustomerRank(SortedSet<LeaderBoardModel> data, Int64 customerId)
        {
            int currentIndex, rank;
            currentIndex = rank = 0;
            data.FirstOrDefault(x =>
            {
                currentIndex++;
                if (x.CustomerId == customerId)
                {
                    rank = currentIndex;
                }
                return x.CustomerId == customerId;
            });
            return rank;
        }

        private bool CustomerInfoNotExist(int customerRank)
        {
            return customerRank < 1;
        }

        public async Task<List<LeaderBoardInfoDto>> GetCustomersByCustomerid(LeaderBoardQuery query)
        {
            query.VerfiyParam();
            var cacheData = await GetLeaderBoardCacheData();
            var customerRank = GetCustomerRank(cacheData, query.CustomerId);
            if (CustomerInfoNotExist(customerRank))
            {
                return new List<LeaderBoardInfoDto>();
            }
            CalculateLeaderBoardSearchRange(query, cacheData, customerRank, out int start, out int end);
            return await GetLeaderBoardRangeData(cacheData, start, end);
        }

        private static void CalculateLeaderBoardSearchRange(LeaderBoardQuery query, SortedSet<LeaderBoardModel> cacheData, int customerRank, out int start, out int end)
        {
            start = customerRank - query.High;
            end = customerRank + query.Low;
            start = start < 1 ? 1 : start;
            end = end > cacheData.Count ? cacheData.Count : end;
        }
    }
}
