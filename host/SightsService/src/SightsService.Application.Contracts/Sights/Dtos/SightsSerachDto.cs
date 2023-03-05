using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SightsService.SightsManage.Dtos
{
    public class SightsSerachDto
    {
        [Required]
        public string Name { get; set; }

        public string Address { get; set; } = "";

        public string Ticket { get; set; } = "";
    }
}
