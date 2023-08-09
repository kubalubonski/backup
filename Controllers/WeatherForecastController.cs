using Microsoft.AspNetCore.Mvc;

namespace FilesBackupAPI.Controllers;

[ApiController]
[Route("backup-activities")]
public class BackupFilesController : ControllerBase
{
    [HttpGet("kafka")]
    public async Task<ActionResult> GetSendMsg()
    {
        var msg = new SendMessageKafka();
        await msg.SendRentalEventCreateToKafka();
        return Ok();
    }
}
