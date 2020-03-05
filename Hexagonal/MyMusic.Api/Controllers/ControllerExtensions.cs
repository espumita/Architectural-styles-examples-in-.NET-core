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
        
        
        public static ActionResult BuildResponseOfType<T, K>(this Controller controller, Either<PlayListError, K> result) where T : ResponseMapper<T, K>, new () {
            ActionResult response = null;
            result.Match(
                Left: playListError => response = controller.BadRequest(playListError),
                Right: queryResponse => {
                    var responseMapper = new T();
                    return response = controller.Ok(responseMapper.From(queryResponse));
                });
            return response;
        }
    }
}