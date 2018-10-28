using BusinessLogic.Logic;
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

namespace Presentation.Controllers
{
    public class UserController : ApiController
    {
        private UserLogic userLogic = new UserLogic();

        [HttpPost]
        public HttpResponseMessage Register(UserRegister userToRegister)
        {
            try
            {
                if (ModelState.IsValid)
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
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, e.Message);
                return response;
            }
            catch (Exception e)
            {
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.BadRequest, e.Message);
                return response;
            }
        }


        private string Token(User model)
        {
            string token = "";
            var request = HttpContext.Current.Request;
            var tokenServiceUrl = request.Url.GetLeftPart(UriPartial.Authority) + request.ApplicationPath + "token";
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                token = client.UploadString(tokenServiceUrl, "POST", "grant_type=password&email=" + model.Email + "&password=" + model.Password);
            }
            Console.Write(token);
            JObject json = JObject.Parse(token);
            token = json["access_token"].ToString();
            return (token);
        }

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

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Me()
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


        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update([System.Web.Mvc.Bind(Include = "Firstname,Lastname,Password")] User userToUpdate)
        {
            try
            {
                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                       .Select(c => c.Value).SingleOrDefault();
                userToUpdate.Email = email;
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

    }
}
