using backend.Models;
using Microsoft.AspNetCore.Mvc;



namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly AppDbContext _context;

    public UsersController(ILogger<UsersController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
        return _context.Users.ToList();
    }

    [HttpGet("{id}")]
    public User Get(int id)
    {
        return _context.Users.Find(id);
    }

  [HttpPost]
    public IActionResult Post([FromBody] UserDTO userDTO)
    {
        var userExists = _context.Users.Any(u => u.Email == userDTO.Email);
        if (userExists)
        {
            return BadRequest("User already exists.");
        }
        var user = new User(userDTO.Email, userDTO.Password);
        _context.Users.Add(user);
        _context.SaveChanges();
        return Ok(new { Message = "User created" });
    }

    [HttpPost("signin")]
    public IActionResult Login([FromBody] UserDTO userDTO)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == userDTO.Email);
        if (user == null)
        {
            return BadRequest("User does not exist.");
        }
        if (user.PasswordHash != userDTO.Password)
        {
            return BadRequest("Incorrect password.");
        }
        return
            Ok(new
            {
                Message = "Login successful",
                user.Id,
                user.Email
            });
    }


    [HttpDelete("{id}")]
    public User Delete(int id)
    {
        var userToDelete = _context.Users.Find(id);
        _context.Users.Remove(userToDelete);
        _context.SaveChanges();
        return userToDelete;
    }

}
