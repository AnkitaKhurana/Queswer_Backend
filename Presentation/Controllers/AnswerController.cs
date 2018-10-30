﻿using BusinessLogic.Logic;
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
    public class AnswerController : ApiController
    {
        private AnswerLogic answerLogic = new AnswerLogic();
        private UserLogic userLogic = new UserLogic();

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
        /// Add new Answer to question : id 
        /// </summary>
        /// <param name="answerToAdd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(Answer answerToAdd, Guid id)
        {
            try
            {
                if (answerToAdd != null && ModelState.IsValid && id != null)
                {

                    AnswerDTO answerDTO = AnswerMapper.ToDTO(answerToAdd);
                    answerDTO.QuestionId = id;
                    var email = CurrentEmail();
                    UserDTO author = userLogic.Find(email);
                    answerDTO.Author = author;
                    answerDTO.AuthorId = author.Id;
                    var answer = answerLogic.Add(answerDTO);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
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
        /// Get all answers of a question 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage All(Guid id)
        {
            try
            {
                List<AnswerDTO> answers = answerLogic.Find(id);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answers);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }

        /// <summary>
        /// Put request to edit answer 
        /// </summary>
        /// <param name="answerToEdit"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Edit(Guid Id,Answer answerToEdit)
        {
            try
            {
                answerToEdit.Id = Id;
                AnswerDTO answer = answerLogic.Edit(AnswerMapper.ToDTO(answerToEdit));
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, answer);
                return response;
            }
            catch (NoSuchAnswerFound e)
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
        /// Delete answer 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                var answer = answerLogic.Delete(Id);
                if (answer == null)
                {
                    throw new NoSuchAnswerFound();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { answer });
                return response;
            }
            catch (NoSuchAnswerFound e)
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


    }
}
