using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StorageService.Storage.Dtos
{
    public class PageListByLinkIdDto : PageListAndSortedRequestDto
    {
        [Required]
        public string LinkId { get; set; }
    }
}
