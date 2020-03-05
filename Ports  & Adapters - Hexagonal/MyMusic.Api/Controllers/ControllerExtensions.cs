using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services.Errors;
using MyMusic.Responses;

namespace MyMusic.Controllers {

    public static class ControllerExtensions {
        
        public static ActionResult BuildResponseFrom(this Controller controller, Either<PlayListError, string> result) {
            ActionResult response = null;
            result.Match(
                Left: playListError => response = controller.BadRequest(playListError),
                Right: success => response = controller.Ok()
            );
            return response;
        }
        
        
        public static ActionResult BuildResponseOfType<T, K>(this Controller controller, Either<PlayListError, K> result) where T : ResponseBuilder<T, K>, new () {
            ActionResult response = null;
            result.Match(
                Left: playListError => response = controller.BadRequest(playListError),
                Right: domainObject => {
                    var responseBuilder = new T();
                    var responseBody = responseBuilder.BuildFrom(domainObject);
                    return response = controller.Ok(responseBody);
                });
            return response;
        }
    }
}