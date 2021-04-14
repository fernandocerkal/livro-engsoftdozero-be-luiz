using System.Linq;
using System.Collections.Generic;
using Botsuana.Covid19.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Botsuana.Covid19.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoseController : ControllerBase
    {
        private readonly ILogger<DoseController> _logger;        

        public DoseController(ILogger<DoseController> logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        public IEnumerable<Dose> Get([FromServices] EngSoftDoZeroDBContext contexto)
        {
            try
            {
                return contexto.Dose.ToList();
            }
            catch(Exception ex)
            {
                _logger.LogDebug("DoseController.Get.Error", ex);
                throw;
            }    
        }

        [HttpPost]
        public bool Post([FromServices] EngSoftDoZeroDBContext contexto, [FromBody] Dose dose)
        {
            try
            {
                contexto.Dose.Add(dose);
                contexto.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogDebug("DoseController.Post.Error", ex);
                throw;                
            }            
        }
    }
}