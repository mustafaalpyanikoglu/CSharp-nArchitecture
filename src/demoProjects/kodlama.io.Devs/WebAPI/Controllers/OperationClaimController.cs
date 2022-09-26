using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetById;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.OperationClaims.Dtos;
using Application.Features.OperationClaims.Models;
using Application.Features.OperationClaims.Queries.GetByIdOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaim;
using Application.Features.OperationClaims.Queries.GetListOperationClaimByDynmaic;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimController : BaseController
    {
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdOperationClaimQuery getByIdOperationClaimQuery)
        {
            GetByIdOperationClaimDto getByIdOperationClaimDto = await Mediator.Send(getByIdOperationClaimQuery);
            return Ok(getByIdOperationClaimDto);
        }
        [HttpGet("getlist")]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListOperationClaimQuery getListOperationClaimQuery = new() { PageRequest = pageRequest };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimQuery);
            return Ok(result);
        }
        [HttpPost("getlist/bydynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] Dynamic dynamic)
        {
            GetListOperationClaimByDynamicQuery getListOperationClaimByDynamicQuery =
                new() { PageRequest = pageRequest, Dynamic = dynamic };
            OperationClaimListModel result = await Mediator.Send(getListOperationClaimByDynamicQuery);
            return Ok(result);

        }
    }
}
