using BusinessLayer.Interface;
using DataBaseLayer.NoteModels;
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
    public class NoteController : ControllerBase
    {
        private readonly FundooContext fundooContext;
        private readonly INoteBL noteBL;
        private readonly ILoggerManager logger;

        public NoteController(FundooContext fundooContext, INoteBL noteBL, ILoggerManager logger)
        {
            this.fundooContext = fundooContext;
            this.noteBL = noteBL;
            this.logger = logger;
        }


        [HttpPost("AddNote")]
        public async Task<IActionResult> AddNote(NotePostModel notePostModel)
        {

            try
            {
                if (notePostModel.Title != "string" && notePostModel.Description != "string" && notePostModel.Bgcolor != "string")
                {
                    var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                    int UserId = int.Parse(userId.Value);
                    await this.noteBL.AddNote(UserId, notePostModel);
                    this.logger.LogInfo($"Note Created Successfully UserId = {userId}");
                    return this.Ok(new { sucess = true, Message = "Note Created Successfully..." });
                }

                return this.BadRequest(new { success = false, message = "Entered Details are similar to Default one" });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }

        }

        [HttpGet("GetALlNotes")]
        public async Task<IActionResult> GetAllNotes()
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var NoteData = await this.noteBL.GetAllNote(UserId);
                if (NoteData.Count == 0)
                {
                    this.logger.LogInfo($"No Notes Exists At Moment!! UserId = {UserId}");
                    return this.BadRequest(new { sucess = false, Message = "Currently You Don't Have Any Notes!!" });
                }

                return this.Ok(new { sucess = true, Message = "Notes Data Retrieved successfully...", data = NoteData });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("UpdateNote")]
        public async Task<IActionResult> UpdateNote(int NoteId, NoteUpdateModel updateModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var check = this.fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (check == null || check.IsTrash == true)
                {
                    return this.BadRequest(new { sucess = false, Message = $"No Note Found for NodeId : {NoteId}" });
                }

                if ((updateModel.Title == string.Empty) || (updateModel.Title == "string" && updateModel.Description == "string" && updateModel.Bgcolor == "string") || (updateModel.IsTrash == true))
                {
                    return this.BadRequest(new { sucess = false, Message = "Enter Valid Data" });
                }

                bool result = await this.noteBL.UpdateNote(UserId, NoteId, updateModel);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, Message = "Note Updated Success Fully!!" });
                }

                return this.BadRequest(new { sucess = false, Message = $"No Note Found for NodeId : {NoteId}" });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpDelete("TrashNote/{noteId}")]
        public async Task<IActionResult> TrashNote(int noteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var check = this.fundooContext.Notes.Where(x => x.NoteId == noteId && x.UserId == UserId).FirstOrDefault();
                if(check == null)
                    return this.BadRequest(new { sucess = false, Message = $"Note Not Found" });

                bool result = await this.noteBL.DeleteNote(UserId, noteId);
                if (result)
                {
                    return this.Ok(new { sucess = true, Message = "Notes Deleted successfully..." });
                }

                return this.BadRequest(new { sucess = true, Message = $"Note : {noteId} Restored Successfully" });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }


        [HttpPut("ArchiveNote/{NoteId}")]
        public async Task<IActionResult> ArchiveNote(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var res = this.fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (res == null)
                {
                    return this.BadRequest(new { sucess = false, Message = "Note not Found" });

                }

                bool result = await this.noteBL.ArchiveNote(UserId, NoteId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, Message = "Note Archive SuccessFully !!" });
                }
                return this.Ok(new { sucess = true, Message = "Note UnArchive SuccessFully !!" });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }


        [HttpPut("PinNote/{NoteId}")]
        public async Task<IActionResult> PinNote(int NoteId)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var res = this.fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (res == null)
                {
                    return this.BadRequest(new { sucess = false, Message = "Note not Found" });

                }

                bool result = await this.noteBL.PinNote(UserId, NoteId);
                if (result == true)
                {
                    return this.Ok(new { sucess = true, Message = "Note Pin SuccessFully !!" });
                }
                return this.Ok(new { sucess = true, Message = "Note UnPin SuccessFully !!" });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }

        [HttpPut("Remainder")]
        public async Task<IActionResult> Remainder(int NoteId, NoteRemainderModel remainderModel)
        {
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                int UserId = int.Parse(userId.Value);
                var remainder = Convert.ToDateTime(remainderModel.Remainder);
                var res = this.fundooContext.Notes.Where(x => x.NoteId == NoteId).FirstOrDefault();
                if (res == null)
                {
                    return this.BadRequest(new { sucess = false, Message = "Note not Found" });

                }

                string result = await this.noteBL.Remainder(UserId, NoteId,remainder);
                if (result != null)
                {
                    return this.Ok(new { sucess = true, Message = "Remainder set SuccessFully !! ", data = result });
                }
                return this.Ok(new { sucess = true, Message = "Remainder Deleted SuccessFully !!" });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex.Message);
                throw ex;
            }
        }

    }
}
