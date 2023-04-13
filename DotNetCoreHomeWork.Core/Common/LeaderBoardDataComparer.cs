using DotNetCoreHomeWork.Core.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace DotNetCoreHomeWork.Core.Common
{
    public class LeaderBoardDataComparer : IComparer<LeaderBoardModel>
    {
        public int Compare([AllowNull] LeaderBoardModel x, [AllowNull] LeaderBoardModel y)
        {
            if (x == y) return 0;
            if (x.Score != y.Score)
            {
                return y.Score - x.Score;
            }
            else
            {
                return x.CustomerId > y.CustomerId ? 1 : -1;
            }
        }
    }
}
