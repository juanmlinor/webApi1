﻿using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SocialMediamContext _context;
        public UserRepository(SocialMediamContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var posts = await _context.Users.ToListAsync();

            return posts;
        }
        public async Task<User> GetUser(int id)
        {
              var post = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            return post;
        }
 
    }
}
