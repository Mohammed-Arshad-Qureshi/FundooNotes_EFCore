using BusinessLayer.Interface;
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
    }
}
