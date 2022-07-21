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

        [HttpPost("AddLabel/{NoteId}/{Labelname}")]
        public async Task<IActionResult> AddNote(int NoteId, string Labelname)
        {

            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var note = this.fundooContext.Notes.FirstOrDefault(x => x.NoteId == NoteId);
                var label = this.fundooContext.Label.FirstOrDefault(x => x.LabelName == Labelname);

                if (note == null || note.IsTrash == true)
                {
                    this.logger.LogInfo($"Enterd invalid Note Id {NoteId}");
                    return this.BadRequest(new { success = false, Message = "Enter valid NoteId" });
                }

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

        [HttpGet("GetAllLabels")]
        public async Task<IActionResult> GetAllLabels()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var result = await labelBL.GetAllLabels(UserId);
                return this.Ok(new { sucess = true, Message = "Fetch all labels", data = result });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpGet("GetAllLabels/{NoteId}")]
        public async Task<IActionResult> GetAllLabels(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var result = await labelBL.GetLabelByNoteId(UserId, NoteId);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, Message = "Fetch all labels", data = result });
                }
                return this.BadRequest(new { sucess = false, Message = "Enter valid NoteId" });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpPut("UpdateLabel/{NoteId}/{Labelname}")]
        public async Task<IActionResult> UpdatedLabel(int NoteId, string Labelname)
        {
            try
            {
                var label = this.fundooContext.Label.FirstOrDefault(x => x.NoteId == NoteId);
                if (label == null)
                {
                    return this.BadRequest(new { sucess = false, Message = "Enter valid NoteId" });
                }

                bool result = await labelBL.UpdateLable(NoteId, Labelname);
                if (result)
                {
                    return this.Ok(new { sucess = true, Message = "Updated Label Successfully! " });
                }

                return this.BadRequest(new { sucess = false, Message = "Entered Label Name already exsists!!" });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }
    }
}
