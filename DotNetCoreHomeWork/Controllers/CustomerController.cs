using DotNetCoreHomeWork.Core.Dto;
using DotNetCoreHomeWork.Core.Model;
using DotNetCoreHomeWork.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace DotNetCoreHomeWork.Api.Controllers
{
    [ApiController]
    [Route("customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;
        public CustomerController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        /// <summary>
        /// Update Score
        /// </summary>
        /// <param name="customerId">customerId</param>
        /// <param name="score">score</param>
        /// <returns>Current score after update</returns>
        [ProducesResponseType(typeof(int), 200)]
        [HttpPost("{customerId}/score/{score}")]
        public async Task<int> UpdateScore([FromRoute] Int64 customerId, [FromRoute][Range(-1000, 1000)] int score, UpdateScoreModel updateScoreModel)
        {
            updateScoreModel.CustomerId = customerId;
            updateScoreModel.Score = score;
            return await _leaderBoardService.UpdateScore(updateScoreModel);
        }

        /// <summary>
        /// Get customers by rank range
        /// </summary>
        /// <param name="start">start rank</param>
        /// <param name="end">end rank</param>
        /// <returns>the found customers with rank and score.</returns>
        [ProducesResponseType(typeof(List<LeaderBoardInfoDto>), 200)]
        [HttpGet("leaderboard")]
        public async Task<List<LeaderBoardInfoDto>> GetCustomersByRank(int start, int end)
        {
            return await _leaderBoardService.GetCustomersByRank(start, end);
        }

        /// <summary>
        /// Get customers by customerid
        /// </summary>
        /// <param name="query">high and low,number of neighbors whose rank is higher than the specified customer</param>
        /// <returns>the found customer and its nearest neighborhoods.</returns>
        [ProducesResponseType(typeof(List<LeaderBoardInfoDto>), 200)]
        [HttpGet("/leaderboard/{customerid}")]
        public async Task<List<LeaderBoardInfoDto>> GetCustomersByCustomerid([FromRoute] int customerid, [FromQuery] LeaderBoardQuery query)
        {
            query.CustomerId = customerid;
            return await _leaderBoardService.GetCustomersByCustomerid(query);
        }
    }
}
