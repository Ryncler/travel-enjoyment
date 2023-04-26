using System;
using System.Collections.Generic;
using System.Text;

namespace CommonService.User.Dtos
{
    public class RecentHotDto
    {
        public string Name { get; set; }

        public int[] HourValue { get; set; } = new int[25];
    }
}
