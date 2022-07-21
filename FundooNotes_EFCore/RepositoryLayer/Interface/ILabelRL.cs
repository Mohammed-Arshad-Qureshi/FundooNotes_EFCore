using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        Task AddLabel(int UserId, int NoteId, string LabelName);
    }
}
