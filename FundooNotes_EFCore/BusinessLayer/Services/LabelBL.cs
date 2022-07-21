using BusinessLayer.Interface;
using DataBaseLayer.LabelModels;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;

        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabel(int UserId, int NoteId, string LabelName)
        {
            try
            {
                await this.labelRL.AddLabel(UserId, NoteId, LabelName);
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
                return await this.labelRL.GetAllLabels(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
