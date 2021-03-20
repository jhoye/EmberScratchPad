using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ScratchPad.Data;
using ScratchPad.JsonApi;

namespace ScratchPad.Controllers
{
    public abstract class ScratchPadControllerBase : ControllerBase
    {
        protected readonly ScratchPadContext ScratchPadContext;

        public ScratchPadControllerBase(ScratchPadContext scratchPadContext)
        {
            ScratchPadContext = scratchPadContext;
        }

        protected void ThrowNotFoundException(string error)
        {
            throw new JsonApiException(error, JsonApiException.StatusCodes.NotFound);
        }

        protected void ThrowBadRequestException(List<string> errors)
        {
            throw new JsonApiException(errors, JsonApiException.StatusCodes.BadRequest);
        }

        protected string EntryNotFoundMessage(int id)
        {
            return EntityNotFoundMessage("An entry", id);
        }

        protected string CommentNotFoundMessage(int id)
        {
            return EntityNotFoundMessage("A comment", id);
        }

        protected string CategoryNotFoundMessage(int id)
        {
            return EntityNotFoundMessage("A category", id); 
        }

        private string EntityNotFoundMessage(string subject, int id)
        {
            return $"{subject} with id {id} has not been created, or was deleted.";
        }
    }
}
