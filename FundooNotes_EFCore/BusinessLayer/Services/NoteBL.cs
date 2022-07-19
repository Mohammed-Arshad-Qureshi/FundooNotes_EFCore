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
                await this.noteRL.AddNote(UserId, notePostModel);
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
                return await this.noteRL.GetAllNote(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateNote(int userId, int noteId, NoteUpdateModel updateModel)
        {
            try
            {
                return await this.noteRL.UpdateNote(userId, noteId, updateModel);
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
                return await this.noteRL.DeleteNote(userId, noteId);
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
                return await this.noteRL.ArchiveNote(userId, noteId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
