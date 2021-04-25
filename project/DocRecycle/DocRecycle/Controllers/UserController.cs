#region

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DocRecycle.Database.Models;
using DocRecycle.Database.Repositories;
using DocRecycle.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace DocRecycle.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(UserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public UserRepository UserRepository { get; }

        [HttpPost("pnr")]
        public async Task<IActionResult> AuthPnr([FromBody] AuthPnr data)
        {
            var res = ExternalService.GetData(data.Pnr);

            if (res == null)
                return BadRequest(AuthResult.FromError("Неправильный PNR код"));

            var dbUser = UserRepository.Find(x => x.Email == res.Email).FirstOrDefault();

            if (dbUser == null)
            {
                dbUser = new User
                {
                    Email = res.Email,
                    Phone = res.Phone,
                    FirstName = res.FirstName,
                    MiddleName = res.MiddleName,
                    LastName = res.LastName
                };

                // todo: load passport, etc.

                UserRepository.Add(dbUser);
                await UserRepository.Save();
            }

            var token = UserRepository.GenerateToken(dbUser);

            return Ok(AuthResult.FromSuccess(token));
        }

        [HttpPost("phone")]
        public async Task<IActionResult> AuthPhone([FromBody] AuthPhone data)
        {
            // todo: send sms

            var dbUser = UserRepository.Find(x => x.Phone == data.Phone).FirstOrDefault();

            if (dbUser == null)
            {
                dbUser = new User
                {
                    Phone = data.Phone,
                };

                UserRepository.Add(dbUser);
                await UserRepository.Save();
            }

            var token = UserRepository.GenerateToken(dbUser);

            return Ok(AuthResult.FromSuccess(token));
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var list = new List<Document>();

            foreach (var document in
                UserRepository.GetById(int.Parse(User.FindFirst(ClaimTypes.Sid).Value)).Documents) // todo: optimize
                list.Add(new Document
                {
                    Id = document.Id,
                    Type = document.Type,
                    Value = document.Value
                });

            return Ok(list);
        }
    }
}