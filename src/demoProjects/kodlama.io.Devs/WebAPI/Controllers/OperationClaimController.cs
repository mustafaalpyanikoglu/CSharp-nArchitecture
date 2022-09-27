using Application.Features.Languages.Dtos;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetById;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.OperationClaims.Commands.CreateOperationClaim;
using Application.Features.OperationClaims.Commands.UpdateOperationClaim;
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
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateOperationClaimCommand createOperationClaimCommand)
        {
            CreateOperationClaimDto result = await Mediator.Send(createOperationClaimCommand);
            return Created("Eklenen: ", result);
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteOperationClaimCommand deleteOperationClaimCommand)
        {
            DeleteOperationClaimDto result = await Mediator.Send(deleteOperationClaimCommand);
            return Ok($"{result} silindi");
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaimCommand)
        {
            UpdateOperationClaimDto result = await Mediator.Send(updateOperationClaimCommand);
            return Ok($"{result} güncellendi");
        }
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
