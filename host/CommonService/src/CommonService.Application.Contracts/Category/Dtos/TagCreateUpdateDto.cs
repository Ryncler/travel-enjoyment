using System;
using System.ComponentModel;

namespace CommonService.CategoryManage.Dtos;

[Serializable]
public class TagCreateUpdateDto
{
    [DisplayName("TagName")]
    public string Name { get; set; }

    [DisplayName("TagParentCategoryId")]
    public Guid ParentCategoryId { get; set; }
}