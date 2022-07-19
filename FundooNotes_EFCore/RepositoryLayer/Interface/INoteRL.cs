using DataBaseLayer.NoteModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        Task AddNote(int UserId, NotePostModel notePostModel);

        Task<List<NoteResponseModel>> GetAllNote(int UserId);

        Task<bool> UpdateNote(int userId, int noteId, NoteUpdateModel updateModel);

        Task<bool> DeleteNote(int userId, int noteId);

        Task<bool> ArchiveNote(int userId, int noteId);
    }
}
