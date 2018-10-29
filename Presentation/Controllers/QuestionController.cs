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
    public class QuestionController : ApiController
    {
        private QuestionLogic questionLogic = new QuestionLogic();
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
        /// Find Question Via Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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
        public HttpResponseMessage Delete(Guid Id)
        {
            try
            {
                var question = questionLogic.Delete(Id);
                if (question == null)
                {
                    throw new NoSuchUserExists();
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
        /// Edit question
        /// </summary>
        /// <param name="question"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Edit(Question question,Guid Id )
        {
            try
            {
                question.Id = Id;
                QuestionDTO questionToEdit  = QuestionMapper.ToDTO(question);
                questionToEdit.Tags = SetTags(question.Tags);
                QuestionDTO questionDTO = questionLogic.Edit(questionToEdit);
                if (questionDTO == null)
                {
                    throw new NoSuchUserExists();
                }
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { questionDTO });
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

    }
}
