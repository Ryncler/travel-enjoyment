using AutoMapper;
using StorageService.Minio;
using StorageService.Minio.Dtos;

namespace StorageService;

public class StorageServiceApplicationAutoMapperProfile : Profile
{
    public StorageServiceApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<MinioDto, MinioProviderArgs>();
    }
}
