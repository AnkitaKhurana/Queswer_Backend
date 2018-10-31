using BusinessLogic.Logic;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Presentation.Controllers
{
    [RoutePrefix("api/tag")]
    public class TagController : ApiController
    {
        private TagLogic tagLogic = new TagLogic();

        /// <summary>
        /// Get all Tags 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public HttpResponseMessage All()
        {
            try
            {
                List<TagDTO> tags = tagLogic.FindAll();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, tags);
                return response;

            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }

        }

        /// <summary>
        /// Find Single Tag Details and all it's questions
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("find/{id}")]
        public HttpResponseMessage Find(Guid Id)
        {
            try
            {
                TagDTO tag = tagLogic.Find(Id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, tag);
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
