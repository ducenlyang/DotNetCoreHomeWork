using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreHomeWork.Core.Model
{
    public class LeaderBoardModel
    {
        public LeaderBoardModel()
        {

        }
        public LeaderBoardModel(UpdateScoreModel updateScoreModel)
        {
            CustomerId = updateScoreModel.CustomerId;
            Score = updateScoreModel.Score;
        }
        public Int64 CustomerId { get; set; }
        public int Score { get; set; }
    }
}
