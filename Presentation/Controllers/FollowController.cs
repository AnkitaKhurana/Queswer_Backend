using BusinessLogic.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Presentation.Controllers
{
    [RoutePrefix("api/follow")]
    public class FollowController : ApiController
    {
        private FollowLogic followLogic = new FollowLogic();
        private UserLogic userLogic = new UserLogic();

        private Guid CurrentUserId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            if (email == null)
                return Guid.Empty;
            return userLogic.Find(email).Id;
        }

        /// <summary>
        /// Follow Controller
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("true/{Id}")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Follow(Guid Id)
        {
            try
            {
                bool value = followLogic.Follow(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, value);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }


        /// <summary>
        /// UnFollow Controller
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("false/{Id}")]
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage UnFollow(Guid Id)
        {
            try
            {
                bool value = followLogic.UnFollow(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, value);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Users following me
        /// </summary>
        /// <returns></returns>
        [Route("followers")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Followers()
        {
            try
            {
                var followers = followLogic.MyFollowers(CurrentUserId());
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, followers);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Users I am following 
        /// </summary>
        /// <returns></returns>
        [Route("following")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Following()
        {
            try
            {
                var following = followLogic.MyFollowing(CurrentUserId());
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, following);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Questions by my following  
        /// </summary>
        /// <returns></returns>
        [Route("questions")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Questions()
        {
            try
            {
                var questions = followLogic.Questions(CurrentUserId());
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, questions);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

    }
}
