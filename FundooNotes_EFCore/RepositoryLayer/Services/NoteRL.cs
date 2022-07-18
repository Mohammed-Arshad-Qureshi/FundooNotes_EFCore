using DataBaseLayer.NoteModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public NoteRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try
            {
                Note note = new Note();
                note.UserId = UserId;
                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.Bgcolor = notePostModel.Bgcolor;
                note.RegisteredDate = DateTime.Now;
                note.ModifiedDate = DateTime.Now;
                this.fundooContext.Notes.Add(note);
                await this.fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<NoteResponseModel>> GetAllNote(int UserId)
        {
            try
            {
                return await fundooContext.Users
               .Where(u => u.UserId == UserId)
               .Join(fundooContext.Notes,
               u => u.UserId,
               n => n.UserId,
               (u, n) => new NoteResponseModel
               {
                   NoteId = n.NoteId,
                   UserId = u.UserId,
                   Title = n.Title,
                   Description = n.Description,
                   Bgcolor = n.Bgcolor,
                   Firstname = u.FirstName,
                   Lasttname = u.LastName,
                   Email = u.Email,
                   CreatedDate = u.CreatedDate,
               }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
