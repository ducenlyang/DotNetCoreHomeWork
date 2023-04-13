using DotNetCoreHomeWork.Api.Controllers;
using DotNetCoreHomeWork.Core.Dto;
using DotNetCoreHomeWork.Core.Model;
using DotNetCoreHomeWork.Core.Repository.Interface;
using DotNetCoreHomeWork.Core.Service;
using DotNetCoreHomeWork.Core.Service.Interface;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DotNetCoreHomeWork.xUnit
{
    public class LeaderBoardUnitTest
    {
        [Fact]
        public async void UpdateScore()
        {
            var updateScoreModel = new UpdateScoreModel() { CustomerId = 123, Score = 123 };
            var oldScoreModel = new LeaderBoardModel() { CustomerId = 123, Score = 1 };

            var leaderBoardRepository = new Mock<ILeaderBoardRepository>();
            leaderBoardRepository.Setup(f => f.GetCustomerScoreById(123)).ReturnsAsync(oldScoreModel);
            leaderBoardRepository.Setup(f => f.Remove(oldScoreModel)).ReturnsAsync(true);
            leaderBoardRepository.Setup(f => f.Add(new LeaderBoardModel(updateScoreModel))).ReturnsAsync(true);
            
            var memoryCache = new Mock<IMemoryCache>();

            var leaderBoardService = new LeaderBoardService(memoryCache.Object, leaderBoardRepository.Object);

            var controller = new CustomerController(leaderBoardService);
            var score = await controller.UpdateScore(123, 123, updateScoreModel);
            Assert.True(score == 123);
        }
    }
}
