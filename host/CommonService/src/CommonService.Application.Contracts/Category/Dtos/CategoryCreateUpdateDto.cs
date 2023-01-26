using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CommonService.CategoryManage.Dtos;

[Serializable]
public class CategoryCreateUpdateDto
{
    [Required]
    [DisplayName("CategoryName")]
    public string Name { get; set; }
}