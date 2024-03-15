using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewEtiqa.Models;
using NewEtiqa.Data;

namespace NewEtiqa.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        // Create/ Edit User
        #region
        [HttpPost]
        public JsonResult CreateEdit(User user)
        {
            if(user.Id == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                var userInDb = _context.Users.Find(user.Id);

                if(userInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                userInDb = user;
            }

            _context.SaveChanges();

            return new JsonResult(Ok(user));
        }
        #endregion

        // Get User by Id
        [HttpGet] 
        public JsonResult GetUser(int id) 
        { 
            var result = _context.Users.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(Ok(result));
        }

        // Delete User
        [HttpDelete]
        public JsonResult DeleteUser(int id) 
        {
            var result = _context.Users.Find(id);

            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Users.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());  
        }

        // Get All User
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Users.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
