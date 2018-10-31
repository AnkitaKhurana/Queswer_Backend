using BusinessLogic.Logic;
using Shared.DTOs;
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
    public class VoteController : ApiController
    {

        private VoteLogic voteLogic = new VoteLogic();
        private UserLogic userLogic = new UserLogic();


        private Guid CurrentUserId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            return userLogic.Find(email).Id;
        }

        /// <summary>
        /// Upvote answer
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Upvote(Guid Id)
        {
            try
            {
                AnswerDTO answer = voteLogic.Upvote(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }

        }


        /// <summary>
        /// Downvote answer 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HttpResponseMessage Downvote(Guid Id)
        {
            try
            {
                AnswerDTO answer = voteLogic.Downvote(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }

        }

        /// <summary>
        /// Un upvote 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HttpResponseMessage UnUpvote(Guid Id)
        {
            try
            {
                AnswerDTO answer = voteLogic.UnUpvote(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }

        }


        /// <summary>
        /// Un Down Vote 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public HttpResponseMessage UnDownvote(Guid Id)
        {
            try
            {
                AnswerDTO answer = voteLogic.UnDownvote(CurrentUserId(), Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
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
