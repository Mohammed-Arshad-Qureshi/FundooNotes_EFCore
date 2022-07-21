﻿using DataBaseLayer.LabelModels;
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
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration configuration;

        public LabelRL(FundooContext fundooContext, IConfiguration configuration)
        {
            this.fundooContext = fundooContext;
            this.configuration = configuration;
        }

        public async Task AddLabel(int UserId, int NoteId, string LabelName)
        {
            try
            {
                Label label = new Label();
                label.UserId = UserId;
                label.NoteId = NoteId;
                label.LabelName = LabelName;
                this.fundooContext.Label.Add(label);
                await this.fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<LabelModel>> GetAllLabels(int UserId)
        {
            try
            {
                var label = this.fundooContext.Label.FirstOrDefault(x => x.UserId == UserId);
                var result = await (from user in fundooContext.Users
                                    join notes in fundooContext.Notes on user.UserId equals UserId
                                    join labels in fundooContext.Label on notes.NoteId equals labels.NoteId
                                    where labels.UserId == UserId
                                    select new LabelModel
                                    {
                                        LabelId = labels.LabelId,
                                        UserId = UserId,
                                        NoteId = notes.NoteId,
                                        Title = notes.Title,
                                        FirstName = user.FirstName,
                                        LastName = user.LastName,
                                        Email = user.Email,
                                        Description = notes.Description,
                                        LabelName = labels.LabelName,
                                    }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<LabelModel>> GetLabelByNoteId(int UserId,int NoteId)
        {
            try
            {
                var label = this.fundooContext.Label.FirstOrDefault(x => x.UserId == UserId);
                var result = await (from user in fundooContext.Users
                                   join notes in fundooContext.Notes on user.UserId equals UserId //where notes.NoteId == NoteId
                                   join labels in fundooContext.Label on notes.NoteId equals labels.NoteId
                                   where labels.NoteId == NoteId
                                   select new LabelModel
                                   {
                                       LabelId = labels.LabelId,
                                       UserId = UserId,
                                       NoteId = notes.NoteId,
                                       Title = notes.Title,
                                       FirstName = user.FirstName,
                                       LastName = user.LastName,
                                       Email = user.Email,
                                       Description = notes.Description,
                                       LabelName = labels.LabelName,
                                   }).ToListAsync();
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateLable(int NoteId,string Labelname)
        {
            try
            {
                var label = this.fundooContext.Label.FirstOrDefault(x => x.NoteId == NoteId && x.LabelName != Labelname);
                if (label != null)
                {
                    label.LabelName = Labelname;
                    this.fundooContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}