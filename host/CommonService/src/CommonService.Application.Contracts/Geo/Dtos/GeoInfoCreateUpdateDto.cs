using System;
using System.ComponentModel;

namespace CommonService.GeoManage.Dtos;

[Serializable]
public class GeoInfoCreateUpdateDto
{
    [DisplayName("GeoInfoCode")]
    public int Code { get; set; }

    [DisplayName("GeoInfoName")]
    public string Name { get; set; }

    [DisplayName("GeoInfoProvince")]
    public string Province { get; set; }

    [DisplayName("GeoInfoCity")]
    public string City { get; set; }

    [DisplayName("GeoInfoAera")]
    public string Aera { get; set; }

    [DisplayName("GeoInfoTown")]
    public string Town { get; set; }
}