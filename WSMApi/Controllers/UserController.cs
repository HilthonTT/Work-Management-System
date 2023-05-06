using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WSMApi.Data;
using WSMApi.Library.DataAccess;
using WSMApi.Library.Models;
using WSMApi.Models;

namespace WSMApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserData _userData;
    private readonly IJobTitleData _jobTitleData;
    private readonly ILogger<UserController> _logger;

    public UserController(ApplicationDbContext context,
					   UserManager<IdentityUser> userManager,
					   RoleManager<IdentityRole> roleManager,
					   IUserData userData,
                       IJobTitleData jobTitleData,
					   ILogger<UserController> logger)
	{
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _userData = userData;
        _jobTitleData = jobTitleData;
        _logger = logger;
    }


    [HttpGet]
    [Route("GetMyId")]
    public ApplicationUserModel GetMyId()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        UserModel userModel = _userData.GetUserById(userId);

        ApplicationUserModel user = new()
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            EmailAddress = userModel.EmailAddress,
            PhoneNumber = userModel.PhoneNumber,
            DateOfBirth = userModel.DateOfBirth,
            DepartmentId = userModel.DepartmentId,
            JobTitleId = userModel.JobTitleId,
            CreatedDate = userModel.CreatedDate,
        };

        var userRoles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        select new { ur.UserId, ur.RoleId, r.Name };

        user.Roles = userRoles.Where(x => x.UserId == user.Id).ToDictionary(key => key.RoleId, val => val.Name);
        user.JobTitles = _jobTitleData.GetJobTitles().Where(x => x.Id == user.JobTitleId).ToList();

        return user;
    }


    public record GetId(
        string Id
    );

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/GetById")]
    public ApplicationUserModel GetById(GetId Id)
    {
        try
        {
            UserModel userModel = _userData.GetUserById(Id.Id);

            if (userModel is not null)
            {
                ApplicationUserModel user = new()
                {
                    Id = userModel.Id,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    EmailAddress = userModel.EmailAddress,
                    PhoneNumber = userModel.PhoneNumber,
                    DateOfBirth = userModel.DateOfBirth,
                    DepartmentId = userModel.DepartmentId,
                    JobTitleId = userModel.JobTitleId,
                    CreatedDate = userModel.CreatedDate,
                };

                var userRoles = from ur in _context.UserRoles
                                join r in _context.Roles on ur.RoleId equals r.Id
                                select new { ur.UserId, ur.RoleId, r.Name };

                user.Roles = userRoles.Where(x => x.UserId == user.Id).ToDictionary(key => key.RoleId, val => val.Name);
                user.JobTitles = _jobTitleData.GetJobTitles().Where(x => x.Id == user.JobTitleId).ToList();

                return user;
            }

            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    [HttpPost]
    [Route("UpdateUser")]
    [Authorize(Roles = "Admin")]
    public async Task UpdateUser(UserModel user)
    {
        try
        {
            _userData.UpdateUser(user);
            var u = _context.Users.Where(x => x.Id == user.Id).First();

            u.Id = user.Id;
            u.Email = user.EmailAddress;
            u.EmailConfirmed = true;
            u.UserName = user.EmailAddress;
            u.PhoneNumber = user.PhoneNumber;

            await _userManager.UpdateAsync(u);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }

    [HttpPut]
    [Route("Admin/UpdateUserJobId")]
    [Authorize(Roles = "Admin")]
    public void UpdateUserJobTitleId(UserModel user)
    {
        try
        {
            _userData.UpdateJobTitleId(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
        }
    }


    [HttpPost]
    [Authorize]
    [Route("Admin/CreateRole")]
    public async Task CreateRole(string roleName)
    {
        IdentityRole identityRole = new()
        {
            Name = roleName
        };

        IdentityResult result = await _roleManager.CreateAsync(identityRole);

        if (result.Succeeded)
        {
            _logger.LogInformation("The role {RoleName} has been created.", identityRole.Name);
        }
    }

    public record UserRegistrationModel(
        string FirstName,
        string LastName,
        string EmailAddress,
        string PhoneNumber,
        DateTime DateOfBirth,
        int? DepartmentId,
        int? JobTitleId,
        string Password);

    [AllowAnonymous]
    [Route("Register")]
    [HttpPost]
    public async Task<IActionResult> Register(UserRegistrationModel user)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);
            if (existingUser is null)
            {
                IdentityUser newUser = new()
                {
                    Email = user.EmailAddress,
                    EmailConfirmed = true,
                    UserName = user.EmailAddress,
                    PhoneNumber = user.PhoneNumber,
                };

                IdentityResult result = await _userManager.CreateAsync(newUser, user.Password);

                if (result.Succeeded)
                {
                    existingUser = await _userManager.FindByEmailAsync(user.EmailAddress);

                    if (existingUser is null)
                    {
                        return BadRequest();
                    }

                    UserModel u = new()
                    {
                        Id = existingUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        EmailAddress = user.EmailAddress,
                        PhoneNumber = user.PhoneNumber,
                        DateOfBirth = user.DateOfBirth,
                        DepartmentId = user.DepartmentId,
                        JobTitleId = user.JobTitleId,
                    };

                    _userData.InsertUser(u);
                    return Ok();
                }
            }
        }

        return BadRequest();
    }

    [HttpGet]
    [Route("Admin/GetAllUsers")]
    [Authorize(Roles = "Admin")]
    public List<ApplicationUserModel> GetAllUsers()
    {
        List<ApplicationUserModel> output = new();

        var users = _userData.GetUsers();
        var userRoles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        select new { ur.UserId, ur.RoleId, r.Name };

        foreach (var user in users)
        {
            ApplicationUserModel u = new()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                PhoneNumber = user.PhoneNumber,
                DateOfBirth = user.DateOfBirth,
                DepartmentId = user.DepartmentId,
                JobTitleId = user.JobTitleId,
                CreatedDate = user.CreatedDate
            };

            u.Roles = userRoles.Where(x => x.UserId == u.Id).ToDictionary(key => key.RoleId, val => val.Name);
            u.JobTitles = _jobTitleData.GetJobTitles().Where(x => x.Id == u.JobTitleId).ToList();

            output.Add(u);
        }

        return output;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("Admin/GetAllRoles")]
    public Dictionary<string, string> GetAllRoles()
    {
        var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);

        return roles;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/AddUserToRole")]
    public async Task AddUserToRole(UserRolePairModel pairing)
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(pairing.UserId);

        _logger.LogInformation("Admin {Admin} added user {User} to role {Role}", 
            loggedInUserId, pairing.UserId, pairing.RoleName);

        await _userManager.AddToRoleAsync(user, pairing.RoleName);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [Route("Admin/RemoveUserFromRole")]
    public async Task RemoveUserFromRole(UserRolePairModel pairing)
    {
        string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userManager.FindByIdAsync(pairing.UserId);

        _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}",
            loggedInUserId, pairing.UserId, pairing.RoleName);

        await _userManager.RemoveFromRoleAsync(user, pairing.RoleName);
    }
}
