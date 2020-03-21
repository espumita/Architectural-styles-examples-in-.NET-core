using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services.Errors;
using MyMusic.Application.Services.Successes;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    public static class ControllerExtensions {
        
        public static ActionResult BuildResponseFrom(this Controller controller, Either<ServiceError, ServiceResponse> result) {
            ActionResult response = null;
            result.Match(
                Left: error => response = controller.BadRequest(error),
                Right: serviceResponse => response = controller.Ok()
            );
            return response;
        }
        
        
        public static ActionResult BuildResponseOfType<T, K>(this Controller controller, Either<ServiceError, K> result) where T : ResponseBuilder<T, K>, new () {
            ActionResult response = null;
            result.Match(
                Left: error => response = controller.BadRequest(error),
                Right: domainObject => {
                    var responseBuilder = new T();
                    var responseBody = responseBuilder.BuildFrom(domainObject);
                    return response = controller.Ok(responseBody);
                });
            return response;
        }
    }
}