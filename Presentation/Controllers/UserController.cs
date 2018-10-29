﻿using BusinessLogic.Logic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Web;
using System.Web.Http;
using Shared.Constants;

namespace Presentation.Controllers
{
    public class UserController : ApiController
    {
        private UserLogic userLogic = new UserLogic();


        /// <summary>
        /// Register new user account 
        /// </summary>
        /// <param name="userToRegister"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Register(UserRegister userToRegister)
        {
            try
            {
                if (userToRegister != null && ModelState.IsValid)
                {
                    UserDTO userDTO = RegisterUserMapper.ToDTO(userToRegister);
                    var user = userLogic.Register(userDTO);
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
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
            catch (UserAlreadyExists e)
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
        /// Request for Token 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private string Token(User model)
        {
            string token = string.Empty;
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "token";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(RequestConstants.CONTENT_TYPE, RequestConstants.FORM_TYPE);
                token = client.UploadString(tokenServiceUrl, RequestConstants.POST, AuthConstants.GenerateRequest(model.Email, model.Password));
            }
            Console.Write(token);
            JObject json = JObject.Parse(token);
            token = json[AuthConstants.ACCESS_TOKEN].ToString();
            return (token);
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="userToLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Login(User userToLogin)
        {
            try
            {
                UserDTO userDTO = UserMapper.ToDTO(userToLogin);
                var user = userLogic.Find(userDTO.Email, userDTO.Password);
                if (user == null)
                {
                    throw new NoSuchUserExists();
                }
                string token = this.Token(userToLogin);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { user, token });
                return response;
            }
            catch (NoSuchUserExists e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, e.Message);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }

        }

        /// <summary>
        /// Return current logged in user profile
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Profile()
        {
            try
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                       .Select(c => c.Value).SingleOrDefault();

                User user = UserMapper.ToViewModel(userLogic.Find(email));
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { user });
                return response;
            }
            catch (NoSuchUserExists e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized, e.Message);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e.Message);
                return response;
            }

        }

        /// <summary>
        /// Update current user account 
        /// </summary>
        /// <param name="userToUpdate"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update([System.Web.Mvc.Bind(Include = "Firstname,Lastname,Password")] User userToUpdate)
        {
            try
            {

                userToUpdate.Email = CurrentEmail();
                User user = UserMapper.ToViewModel(userLogic.Update(UserMapper.ToDTO(userToUpdate)));
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, new { user });
                return response;
            }
            catch (NoSuchUserExists e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized, e.Message);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.ServiceUnavailable, e.Message);
                return response;
            }

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

    }
}
