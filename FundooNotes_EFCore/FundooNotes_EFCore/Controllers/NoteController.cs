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
                throw ex;
            }

            return this.BadRequest(new { success = false, message = "Entered Details are same as Default one" });
        }
    }
}
