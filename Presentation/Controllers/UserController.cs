using BusinessLogic.Logic;
using Newtonsoft.Json.Linq;
using Presentation.Mapper;
using Presentation.Models;
using Shared.DTOs;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Presentation.Controllers
{
    public class UserController : ApiController
    {
        private UserLogic userLogic = new UserLogic();

        [HttpPost]
        public HttpResponseMessage Register(User userToRegister)
        {
            try
            {
                UserDTO userDTO = UserMapper.ToDTO(userToRegister);
                var user = userLogic.Register(userDTO);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, user);
                return response;
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
                 token = client.UploadString(tokenServiceUrl, "POST", "grant_type=password&email="+model.Email+"&password="+model.Password);
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
                string token = this.Token(userToLogin);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, new { user, token });
                if (user == null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                }
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

    }
}
