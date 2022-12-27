using System;
using BaseService.Entities.Dtos;
using Volo.Abp.Application.Services;

namespace BaseService.Entities;

public interface IUserExtensionAppService :
    ICrudAppService< 
        UserExtensionDto, 
        UserExtensionKey, 
        UserExtensionGetListInput,
        UserExtensionCreateDto,
        UserExtensionUpdateDto>
{

}