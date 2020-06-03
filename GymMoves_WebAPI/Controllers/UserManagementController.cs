using GymMoves_WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace GymMoves_WebAPI.Controllers
{
    public class UserManagementController : ApiController
    {
        [Route("api/signup")]
        [HttpPost]
        public HttpResponseMessage SignUp(Users user)
        {

            Boolean successful = false;



            if (user.ID == 123)
            {
                successful = true;
            }


            if (successful)
            {
                string returnMessage = "{ \"Successful\" : \"true\", \"User Type\" : \"Gym Member\", \"Name\" : \"Sally\"}";

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(returnMessage, Encoding.UTF8, "application/json");


                return response;
            }
            else
            {
                string returnMessage = "{ \"Successful\" : \"false\", \"Reason\" : \"The member ID does not exist!\"}";

                var response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(returnMessage, Encoding.UTF8, "application/json");


                return response;
            }
        }

        [Route("api/login")]
        [HttpPost]
        public HttpResponseMessage LogIn(Users user)
        {

            Boolean successful = false;



            if (user.password == "password")
            {
                successful = true;
            }


            if (successful)
            {
                string returnMessage = "{ \"Successful\" : \"true\", \"User Type\" : \"Gym Member\", \"Name\" : \"Sally\"}";

                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new StringContent(returnMessage, Encoding.UTF8, "application/json");


                return response;
            }
            else
            {
                string returnMessage = "{ \"Successful\" : \"false\", \"Reason\" : \"The member ID does not exist!\"}";

                var response = Request.CreateResponse(HttpStatusCode.Unauthorized);
                response.Content = new StringContent(returnMessage, Encoding.UTF8, "application/json");


                return response;
            }
        }
    }
}
