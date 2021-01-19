using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Chat.Common;
using Chat.Models;

namespace Chat.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private ChatDBContext dbContext;

        public RegistrationController(ChatDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("AddUser")]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]User user)
        {
            if (user.Name == null || user.Password == null)
            {
                return BadRequest("Username and Password must be not null.");
            }
            else
            {
                User _user = await dbContext.Users.FirstOrDefaultAsync(u => u.Name == user.Name);
                if (_user == null)
                {
                    var __user = new User { Name = user.Name, Password = user.Password };
                    dbContext.Users.Add(__user);
                    await dbContext.SaveChangesAsync();
                    return new JsonResult(__user);
                }
                else
                {
                    return BadRequest("Username already exist.");
                }
            }
        }
    }
}