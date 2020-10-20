using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace User.Controllers
{
    
    public class BaseController
    {
        [Authorize(AuthenticationSchemes = "Bearer")]
        public class BaseControllers : ControllerBase
        {
            [NonAction]
            protected string GetCurrentUserName()
            {
                return User.Claims.First(i => i.Type == "Username").Value;
            }

            [NonAction]
            protected string GetCurrentUserPassword()
            {
                return User.Claims.First(i => i.Type == "Password").Value;
            }
        }
    }
}