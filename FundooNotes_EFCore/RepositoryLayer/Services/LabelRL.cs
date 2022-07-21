using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
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
    }
}
