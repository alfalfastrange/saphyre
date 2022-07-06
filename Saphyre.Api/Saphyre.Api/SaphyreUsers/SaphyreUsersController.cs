using MediatR;
using Microsoft.AspNetCore.Mvc;
using Saphyre.Api.SaphyreUsers.Commands;
using Saphyre.Api.SaphyreUsers.Queries;
using Saphyre.Api.SaphyreUsers.ViewModels;

namespace Saphyre.Api.Controllers
{
    [ApiController]
    public class SaphyreUsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SaphyreUsersController(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/saphyreusers")]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateSaphyreUserViewModel model, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateSaphyreUserCommandHandler.Command(model), cancellationToken);
            return result.IsSuccess ? Ok(result.IsSuccess) : BadRequest(result.Errors);
        }

        [HttpGet]
        [Route("api/saphyreusers")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSaphyreUsersQueryHandler.Query(), cancellationToken);
            return Ok(result.Model.SaphyreUsers);
        }

        [HttpGet]
        [Route("api/saphyreusers/{userid:int}")]
        public async Task<IActionResult> GetById([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetSaphyreUserQueryHandler.Query(userId), cancellationToken);
            return result.Model != null ? Ok(result.Model) : NotFound();
        }

        [HttpPut]
        [Route("api/saphyreusers/{userid:int}")]
        public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] CreateOrUpdateSaphyreUserViewModel model, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new UpdateSaphyreUserCommandHandler.Command(userId, model), cancellationToken);
            return result.IsSuccess ? Ok(result.IsSuccess) : BadRequest(result.Errors);
        }

        [HttpDelete]
        [Route("api/saphyreusers/{userid:int}")]
        public async Task<IActionResult> Delete([FromRoute] int userId, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteSaphyreUserCommandHandler.Command(userId), cancellationToken);
            return result.IsSuccess ? Ok(result.IsSuccess) : BadRequest(result.Errors);
        }
    }
}