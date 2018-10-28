using BusinessLogic.Logic;
using Newtonsoft.Json;
using Presentation.Mapper;
using Presentation.Models;
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

namespace Presentation.Controllers
{
    public class QuestionController : ApiController
    {
        private QuestionLogic questionLogic = new QuestionLogic();
        private UserLogic userLogic = new UserLogic();

        [HttpGet]
        public HttpResponseMessage Find(Guid Id)
        {
            try
            {
                
                var question = questionLogic.Find(Id);
                if (question == null)
                {
                    throw new NoSuchUserExists();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { question });
                return response;
            }
            catch (NoSuchQuestionFound e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,new { error=  e.Message });
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(Question questionToAdd)
        {
            try
            {
                if (questionToAdd != null && ModelState.IsValid)
                {
                    List<TagDTO> tags = new List<TagDTO>(); 
                    foreach(var tagstring in questionToAdd.Tags)
                    {
                        tags.Add(new TagDTO()
                        {
                            Body = tagstring,
                            Id = Guid.NewGuid()
                            
                        });
                    }
                    
                    QuestionDTO questionDTO = QuestionMapper.ToDTO(questionToAdd);
                    questionDTO.Tags = tags;
                    var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                           .Select(c => c.Value).SingleOrDefault();
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
    }
}
