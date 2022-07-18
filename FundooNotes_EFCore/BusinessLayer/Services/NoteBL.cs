using BusinessLayer.Interface;
using DataBaseLayer.NoteModels;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;

        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }

        public async Task AddNote(int UserId, NotePostModel notePostModel)
        {
            try
            {
                this.noteRL.AddNote(UserId, notePostModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<List<NoteResponseModel>> GetAllNote(int UserId)
        {
            try
            {
                return this.noteRL.GetAllNote(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
