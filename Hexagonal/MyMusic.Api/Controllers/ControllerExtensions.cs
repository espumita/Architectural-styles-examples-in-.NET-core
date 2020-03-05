using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using MyMusic.Application.Services.Errors;

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
    }
}