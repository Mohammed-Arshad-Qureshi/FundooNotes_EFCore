using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLogger.Interface;
using RepositoryLayer.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes_EFCore.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly FundooContext fundooContext;
        private readonly ILabelBL labelBL;
        private readonly ILoggerManager logger;

        public LabelController(FundooContext fundooContext, ILabelBL labelBL, ILoggerManager logger)
        {
            this.fundooContext = fundooContext;
            this.labelBL = labelBL;
            this.logger = logger;
        }

        [HttpPost("AddLabel")]
        public async Task<IActionResult> AddNote(int NoteId, string Labelname)
        {

            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var note = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);

                if (note == null || note.IsTrash == true)
                {
                    this.logger.LogInfo($"Enterd invalid Note Id {NoteId}");
                    return this.BadRequest(new { success = false, Message = "Note Not Found Enter valid NoteId" });
                }

                if (Labelname == string.Empty || Labelname == "string")
                {
                    this.logger.LogInfo($"Enterd the default values {NoteId}");
                    return this.BadRequest(new { success = false, Message = "Enter Valid Data" });

                }

                var label = this.fundooContext.Label.FirstOrDefault(x => x.LabelName == Labelname);
                if (label == null)
                {
                    await this.labelBL.AddLabel(UserId, NoteId, Labelname);
                    this.logger.LogInfo($"Label Cread Successfully with noted id = {NoteId}");
                    return this.Ok(new { sucess = true, Message = "Note Created Successfully..." });
                }

                return this.BadRequest(new { sucess = false, Message = "Label with the name already Exists !!" });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
