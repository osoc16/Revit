using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Revit.Api.Azure.Controllers
{


    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {


        [Route("{testId}/User/{userId}")]
        // GET: api/Candidates
        public object  Get(int testId,int userId )
        {
            



            return userId;
        }



    }
}
