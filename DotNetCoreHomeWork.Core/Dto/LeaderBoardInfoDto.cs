using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreHomeWork.Core.Dto
{
    public class LeaderBoardInfoDto
    {
        public Int64 CustomerId { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
    }
}
