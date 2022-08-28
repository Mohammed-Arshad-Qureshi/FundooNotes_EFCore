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

        Task NoteDeleteforever(int userId, int noteId);

        Task<bool> ArchiveNote(int userId, int noteId);

        Task<bool> PinNote(int userId, int noteId);

        Task<string> Remainder(int userId, int noteId, DateTime Remainder);

        List<string> GetAllColors(int userId, int noteId);

        Task<bool> UpdateColor(int userId, int noteId, NoteColorModel color);
    }

}
