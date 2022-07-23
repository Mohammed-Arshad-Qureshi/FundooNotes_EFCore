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

        public async Task<List<LabelModel>> GetLabelByNoteId(int UserId, int NoteId)
        {
            try
            {
                return await this.labelRL.GetLabelByNoteId(UserId, NoteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateLable(int UserId,int LabelId, string Labelname)
        {
            try
            {
                return await this.labelRL.UpdateLable(UserId, LabelId, Labelname);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteLabel(int UserId,int LabelId)
        {
            try
            {
                return await this.labelRL.DeleteLabel(UserId,LabelId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
