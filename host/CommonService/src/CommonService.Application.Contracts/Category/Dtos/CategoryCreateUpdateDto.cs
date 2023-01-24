using System;
using System.ComponentModel;

namespace CommonService.CategoryManage.Dtos;

[Serializable]
public class CategoryCreateUpdateDto
{
    [DisplayName("CategoryName")]
    public string Name { get; set; }

    [DisplayName("CategoryParentId")]
    public Guid ParentId { get; set; }
}