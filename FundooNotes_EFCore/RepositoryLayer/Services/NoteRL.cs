﻿using DataBaseLayer.NoteModels;
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
                return await this.fundooContext.Users
                .Where(u => u.UserId == UserId)
                .Join(fundooContext.Notes,
                u => u.UserId,
                n => n.UserId,
                (u, n) => new NoteResponseModel
                {
                    NoteId = n.NoteId,
                    UserId = n.UserId,
                    Title = n.Title,
                    Description = n.Description,
                    Bgcolor = n.Bgcolor,
                    IsTrash = n.IsTrash,
                    IsArchive = n.IsArchive,
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

        public async Task<bool> UpdateNote(int userId, int noteId, NoteUpdateModel updateModel)
        {

            var flag = true;

            try
            {
                var result = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();

                if (result == null || result.IsTrash == true)
                {
                    flag = false;
                    return await Task.FromResult(flag);
                }

                result.Title = updateModel.Title;
                result.Description = updateModel.Description;
                result.Bgcolor = updateModel.Bgcolor;
                result.IsPin = updateModel.IsPin;
                result.IsArchive = updateModel.IsArchive;
                result.IsTrash = updateModel.IsTrash;
                result.ModifiedDate = DateTime.Now;
                this.fundooContext.Notes.Update(result);
                await this.fundooContext.SaveChangesAsync();
                return await Task.FromResult(flag);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteNote(int userId, int noteId)
        {
            try
            {
                var flag = false;
                var note = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (note != null && note.IsTrash == false)
                {
                    flag = true;
                    note.IsTrash = true;
                }
                else
                {
                    note.IsTrash = false;
                }
                await this.fundooContext.SaveChangesAsync();
                return await Task.FromResult(flag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> ArchiveNote(int userId, int noteId)
        {
            try
            {
                var flag = true;
                var note = this.fundooContext.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (note != null && note.IsTrash == false)
                {
                    if (note.IsArchive == false)
                    {
                        note.IsArchive = true;
                    }
                    else
                    {
                        note.IsArchive = false;
                        flag = false;
                    }

                    await this.fundooContext.SaveChangesAsync();
                    return await Task.FromResult(flag);
                }

                return await Task.FromResult(!flag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> PinNote(int userId, int noteId)
        {
            try
            {
                var flag = true;
                var note = this.fundooContext.Notes.Where(x => x.UserId == userId && x.NoteId == noteId).FirstOrDefault();
                if (note != null && note.IsTrash == false)
                {
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                    }
                    else
                    {
                        note.IsPin = false;
                        flag = false;
                    }

                    this.fundooContext.Notes.Update(note);
                    await this.fundooContext.SaveChangesAsync();
                    return await Task.FromResult(flag);
                }

                return await Task.FromResult(!flag);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> Remainder(int userId, int noteId, DateTime Remainder)
        {
            try
            {
                var note = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                if (note != null && note.IsRemainder == false)
                {
                    note.Remainder = Remainder;
                    note.IsRemainder = true;
                    await this.fundooContext.SaveChangesAsync();
                    return "Reminder Set Successfull for date:" + Remainder.Date + " And Time : " + Remainder.TimeOfDay;
                }
                else
                {
                    note.IsRemainder = false;
                    await this.fundooContext.SaveChangesAsync();
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task NoteDeleteforever(int userId, int noteId)
        {
            try
            {
                var note = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();
                this.fundooContext.Notes.Remove(note);
                await this.fundooContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<string> GetAllColors(int userId, int noteId)
        {
           List<string> colors = new List<string>()
           {
               "#ffffff",
               "#f28b82",
               "#fbbc04",
               "#fff475",
               "#ccff90",
               "#a7ffeb",
               "#aecbfa",
               "#d7aefb",
               "#fdcfe8",
           };
           return colors;
        }

        public async Task<bool> UpdateColor(int userId, int noteId, NoteColorModel color)
        {
            try
            {
                var flag = true;
                var result = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == userId).FirstOrDefault();

                if (result == null || result.IsTrash == true)
                {
                    flag = false;
                    return await Task.FromResult(flag);
                }

                result.Bgcolor = color.Bgcolor;
                this.fundooContext.Notes.Update(result);
                await this.fundooContext.SaveChangesAsync();
                return await Task.FromResult(flag);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
