using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class OrgStructureController : ApiController
    {
        public Departments GetDepartments() 
        {
            return Presenter.GetDepartments();
        } 
    }
}
