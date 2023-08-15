using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Shared.Queries.Errors;

namespace MyMusic.Shared {

    public static class ControllerExtensions {
        
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