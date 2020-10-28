using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class EmployeesController : ApiController
    {
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = null;
            foreach (var dep in Presenter.GetDepartments().departments)
            {
                employee = dep.employees.FirstOrDefault(x => x.id == id);
            }

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
