using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class DepartmentController : ApiController
    {
        public IHttpActionResult GetDepartment(int id)
        {
            var dep = Presenter.GetDepartments().departments.FirstOrDefault(x => x.id == id);
            if (dep == null)
            {
                return NotFound();
            }
            return Ok(dep);
        }

    }
}
