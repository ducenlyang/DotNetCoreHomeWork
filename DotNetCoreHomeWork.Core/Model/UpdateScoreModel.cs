using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCoreHomeWork.Core.Model
{
    public class UpdateScoreModel
    {
        public Int64 CustomerId { get; set; }
        public int  Score { get; set; }
    }
}
