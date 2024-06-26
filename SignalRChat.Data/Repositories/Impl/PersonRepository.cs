﻿using Microsoft.EntityFrameworkCore;
using SignalRChat.Data.Repositories.Interfaces;
using SignalRChat.Domain.Entities;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRChat.Data.Repositories.Impl
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context) { }
        public async Task<Person?> FindByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
        }
        public async Task<IEnumerable<Person>> GetAllUsersAsync(int personId)
        {
            var dialogs = await _context.PersonalMessages
            .Where(m => m.SenderId == personId || m.RecipientId == personId)
            .Select(m => m.SenderId)
            .Union(_context.PersonalMessages.Where(m => m.SenderId == personId || m.RecipientId == personId)
            .Select(m => m.RecipientId))
            .Distinct()
            .Where(id => id != personId)
            .ToListAsync();
            var users = await _context.Users.Where(x => x.Id != personId).ToListAsync();
            return users.Except(await _context.Users.Where(u => dialogs.Contains(u.Id)).ToListAsync());
        }

        public async Task<IEnumerable<Person>> GetAllUsersToAddGroupAsync(int groupId, int personId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(x => x.Id == groupId);
            if (group != null)
            {
                var members = await _context.GroupMembers.Where(x => x.GroupId == groupId).Select(m => m.PersonId).ToListAsync();
                var deletedMembers = await _context.GroupMembers.Where(x => x.GroupId == groupId && x.DeleteDate != null && x.IsLeaved == false).Select(m => m.PersonId).ToListAsync();
                var users = await _context.Users.Where(x => x.Id != personId).ToListAsync();
                return users.Except(await _context.Users.Where(x => members.Contains(x.Id) || deletedMembers.Contains(x.Id) || x.Id == group.PersonId).ToListAsync());
            }
            return await _context.Users.Where(x => x.Id != personId).ToListAsync();
        }

        public async Task<string?> GetLoginByIdAsync(int id)
        {
            return await _context.Users.Where(x => x.Id == id).Select(x => x.Login).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAvatarAsync(int id, string avatar)
        {
            var updatedPerson = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (updatedPerson != null)
            {
                updatedPerson.Avatar = avatar;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> SaveResetPassCodeAsync(int personId, int code)
        {
            var person = await _context.Users.FirstOrDefaultAsync(x => x.Id == personId);
            if (person != null)
            {
                person.ResetPassCode = code;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPasswordAsync(int personId, string passHash)
        {
            var person = await _context.Users.FirstOrDefaultAsync(x => x.Id == personId);
            if (person != null)
            {
                person.PasswordHash = passHash;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
