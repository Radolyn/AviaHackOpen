#region

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using DocRecycle.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace DocRecycle.Database.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        /// <inheritdoc />
        public UserRepository(DocsDatabase context, IConfiguration configuration) : base(context)
        {
            Configuration = configuration.GetSection("AuthOptions");
        }

        public IConfigurationSection Configuration { get; set; }

        /// <inheritdoc />
        public override User GetById(int id)
        {
            var user = _context.Users.Include(x => x.Documents)
                .ThenInclude<User, Document, DocumentType>(document => document.Type)
                .First(x => x.Id == id); // todo: optimize

            return user;
        }

        public User GetBySecret(string secret)
        {
            var user = _context.Users.Include(x => x.Documents)
                .ThenInclude<User, Document, DocumentType>(document => document.Type).First(x => x.Secret == secret);

            return user;
        }

        public string GenerateToken(User user)
        {
            var now = DateTime.Now;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };

            var jwt = new JwtSecurityToken(
                Configuration["ISSUER"],
                Configuration["AUDIENCE"],
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(int.Parse(Configuration["LIFETIME"]))),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["KEY"])),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}