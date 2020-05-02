using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.CommandHandlers.Successes;
using MyMusic.Application.Queries.Errors;
using MyMusic.Domain.Error;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    public static class ControllerExtensions {
        
        public static ActionResult BuildResponseFrom(this Controller controller, Either<DomainError, CommandResult> result) {
            ActionResult response = null;
            result.IfLeft(error => response = controller.BadRequest(error));
            result.IfRight(commandResult => response = controller.Ok());
            return response;
        }

        public static ActionResult BuildResponseOfType<T, K>(this Controller controller, Either<QueryError, K> result) where T : ResponseBuilder<T, K>, new () {
            ActionResult response = null;
            result.IfLeft(error => response = controller.BadRequest(error));
            result.IfRight(domainObject => {
                var responseBuilder = new T();
                var responseBody = responseBuilder.BuildFrom(domainObject);
                response = controller.Ok(responseBody);
            });
            return response;
        }
    }
}