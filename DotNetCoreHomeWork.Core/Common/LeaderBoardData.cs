using DotNetCoreHomeWork.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreHomeWork.Core.Common
{
    public static class LeaderBoardData
    {
        public static SortedSet<LeaderBoardModel> Data { get; set; } = new SortedSet<LeaderBoardModel>(new LeaderBoardDataComparer());
    }
}
