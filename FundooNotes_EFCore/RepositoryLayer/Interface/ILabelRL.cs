using DataBaseLayer.LabelModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task AddLabel(int UserId, int NoteId, string LabelName);

        Task<List<LabelModel>> GetAllLabels(int UserId);

        Task<List<LabelModel>> GetAllLabelsById(int UserId,int NoteId);


    }
}
