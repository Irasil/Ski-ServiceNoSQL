using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ski_ServiceNoSQL.Models;
using Ski_ServiceNoSQL.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Ski_ServiceNoSQL.Controllers
{
    /// <summary>
    /// Kontroller für die Verbindung zu der Tabelle Mitarbeiter
    /// </summary>
   
    [Route("[controller]")]
    [ApiController]
    public class MitarbeiterController : ControllerBase
    {
        
        private readonly IMitarbeiterService _mitarbeiterService;
        public MitarbeiterController(IMitarbeiterService mitarbeiterService)
        {
            this._mitarbeiterService = mitarbeiterService;
        }

        /// <summary>
        /// Alle Mitarbeiter Anzeigen ohne Passwörter
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Mitarbeiter>> AllMitarbeiter()
        {

            try
            {
                List<Mitarbeiter> mitarbeiterList = new List<Mitarbeiter>();
                mitarbeiterList = _mitarbeiterService.AllMitarbeiter();
                foreach (Mitarbeiter mitarbeiter in mitarbeiterList)
                {
                    mitarbeiter.password = "*******";
                }
                return Ok(mitarbeiterList);

               
            }
            catch (Exception ex)
            {
                return NotFound($"Warning --> {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] Mitarbeiter model)
        {
            try
            {
                JsonResult? json = _mitarbeiterService.ProveUser(model);
                string? auswertung = json.Value.ToString();
                bool gespert = false;
                gespert = auswertung.Contains("gespert");
                bool falsch = false;
                falsch = auswertung.Contains("Falsch");

                if (gespert == false && falsch == false)
                    return Ok(json);
                else if (gespert == true)
                {
                    return BadRequest("User ist blockiert");
                }
                else
                {
                    return BadRequest("User oder Passwort sind falsch");
                }
            }
            catch (Exception ex)
            {
                return NotFound($"Warning --> {ex.Message}");
            }
        }


    }
}
