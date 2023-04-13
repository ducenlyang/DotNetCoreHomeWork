using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreHomeWork.Core.Model
{
    public class LeaderBoardQuery
    {
        public Int64 CustomerId { get; set; }
        public int High { get; set; }
        public int Low { get; set; }

        public void VerfiyParam()
        {
            if (High < 0 || Low < 0)
            {
                throw new ArgumentException("high or low must be greater than zero");
            }
        }
    }
}
