using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static readonly List<User> _users = new List<User>
    {
            new User {
                Id= 1,
                Username= "Title",
                Email= "test@gmail.com",
                Fullname= "Title Dude"
            },
            new User {
                Id= 2,
                Username= "Dude",
                Email= "test2@gmail.com",
                Fullname= "Dude Mak"
            },
    };

    [HttpGet]

    //Get all User
    public ActionResult<IEnumerable<User>> GeyUsers()
    {
        return Ok(_users);
    }

    //Get User By Id

    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);

    }

    //Create new user
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        //Validate User Id
        if (id != user.Id)
        {
            return BadRequest();
        }

        //Find existing user
        var exsitingUser = _users.Find(u => u.Id == id);
        if (exsitingUser == null)
        {
            return NotFound();
        }

        exsitingUser.Username = user.Username;
        exsitingUser.Email = user.Email;
        exsitingUser.Fullname = user.Fullname;

        return Ok(exsitingUser);
    }

    [HttpDelete("{id}")]
    public ActionResult DelUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        else
        {
            _users.Remove(user);
            return NoContent();
        }

    }

}