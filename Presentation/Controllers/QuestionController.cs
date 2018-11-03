
using BusinessLogic.Logic;
using Newtonsoft.Json;
using Presentation.Mapper;
using Presentation.Models;
using Shared.Constants;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Presentation.Controllers
{
    [RoutePrefix("api/question")]
    public class QuestionController : ApiController
    {
        private QuestionLogic questionLogic = new QuestionLogic();
        private UserLogic userLogic = new UserLogic();

        private Guid CurrentUserId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            return userLogic.Find(email).Id;
        }

        /// <summary>
        /// Returns the Email Id of Current Logged in user
        /// </summary>
        /// <returns></returns>
        private string CurrentEmail()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();
            return email;

        }

        /// <summary>
        /// Set tags from string to DTO
        /// </summary>
        /// <param name="stringTags"></param>
        /// <returns></returns>
        private List<TagDTO> SetTags(List<string> stringTags)
        {

            List<TagDTO> tags = new List<TagDTO>();
            foreach (var tagstring in stringTags)
            {
                tags.Add(new TagDTO()
                {
                    Body = tagstring,
                    Id = Guid.NewGuid()

                });
            }
            return tags;
        }

        /// <summary>
        /// All Questions return (with optional page || count )
        /// </summary>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        [Route("all/{searchString=}/{page=1}/{count=10}")]
        [HttpGet]
        public HttpResponseMessage All(int page, int count, string searchString)
        {
            try
            {
                var questions = questionLogic.All(page, count, searchString);
                int totalQuestions = questions.Count();
                if (searchString==string.Empty||searchString==null)
                    totalQuestions = questionLogic.Count();
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { questions, totalQuestions });
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }


        /// <summary>
        /// Find Question Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("find/{Id}")]
        public HttpResponseMessage Find(Guid Id)
        {
            try
            {
                var question = questionLogic.Find(Id);
                if (question == null)
                {
                    throw new NoSuchQuestionFound();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { question });
                return response;
            }
            catch (NoSuchQuestionFound e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { error = e.Message });
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Add new question
        /// </summary>
        /// <param name="questionToAdd"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Add(Question questionToAdd)
        {
            try
            {
                if (questionToAdd != null && ModelState.IsValid)
                {

                    QuestionDTO questionDTO = QuestionMapper.ToDTO(questionToAdd);
                    questionDTO.Tags = SetTags(questionToAdd.Tags);
                    var email = CurrentEmail();
                    UserDTO author = userLogic.Find(email);
                    questionDTO.Author = author;
                    var question = questionLogic.Add(questionDTO);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, question);
                    return response;
                }
                else
                {

                    var validationErrors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            validationErrors.Add((error.ErrorMessage));
                        }
                    }

                    var jsonerrors = JsonConvert.SerializeObject(new
                    {
                        errors = validationErrors
                    });
                    return Request.CreateResponse(HttpStatusCode.Forbidden, JsonConvert.DeserializeObject(jsonerrors));
                }

            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Delete Question 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("delete/{Id}")]
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                
                if(questionLogic.Find(Id).Author.Id != CurrentUserId())
                {
                    throw new Unauthorised();
                }
                var question = questionLogic.Delete(Id);
                if (question == null)
                {
                    throw new NoSuchQuestionFound();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { question });
                return response;
            }
            catch (NoSuchQuestionFound e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { error = e.Message });
                return response;
            }
            catch (Unauthorised e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Forbidden, new { error = e.Message });
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Edit question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("Edit/{Id}")]
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Edit([FromBody]Question questionToEdit,[FromUri]Guid Id )
        {
            try
            {
                if (questionLogic.Find(Id).Author.Id != CurrentUserId())
                {
                    throw new Unauthorised();
                }
                questionToEdit.Id = Id;
                QuestionDTO questionEdit  = QuestionMapper.ToDTO(questionToEdit);
                questionEdit.Tags = SetTags(questionToEdit.Tags);
                QuestionDTO question = questionLogic.Edit(questionEdit);
                if (question == null)
                {
                    throw new NoSuchQuestionFound();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { question });
                return response;
            }
            catch (Unauthorised e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Forbidden, new { error = e.Message });
                return response;
            }
            catch (NoSuchQuestionFound e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, new { error = e.Message });
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
