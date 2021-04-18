using System.Linq;
using System.Collections.Generic;
using Botsuana.Covid19.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.EntityFrameworkCore;

namespace Botsuana.Covid19.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacinaController : ControllerBase
    {
        private readonly ILogger<VacinaController> _logger;        

        public VacinaController(ILogger<VacinaController> logger)
        {
            _logger = logger;            
        }

        [HttpGet]
        public IEnumerable<Vacinado> Get([FromServices] EngSoftDoZeroDBContext contexto)
        {
            try
            {
                return contexto.Vacinado.Include(vacinado => vacinado.dose).ToList();
            }
            catch(Exception ex)
            {
                _logger.LogDebug("VacinaController.Get.Error", ex);
                throw;
            }    
        }

        [HttpPost]
        public bool Post([FromServices] EngSoftDoZeroDBContext contexto, [FromBody] Vacinado vacinado)
        {
            try
            {
                var dose = contexto.Dose.Where(x => x.Identificador == vacinado.dose.Identificador).FirstOrDefault();
                vacinado.dose = dose;
                vacinado.dataHora = DateTime.Now;
                contexto.Vacinado.Add(vacinado);
                contexto.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogDebug("VacinaController.Post.Error", ex);
                throw;                
            }            
        }

        [HttpPut]
        public bool Put([FromServices] EngSoftDoZeroDBContext contexto, [FromBody] Vacinado vacinado)
        {
            try
            {
                //se for a mesma pessoa e a mesma dose, vamos apenas atualizar.
                //porém se for a mesma pessoa, mas com dose diferente, vamos adicionar.
                var pessoaVacinada = contexto.Vacinado.Where(x=> x.Identificador == vacinado.Identificador && x.dose.Identificador ==vacinado.dose.Identificador).FirstOrDefault();
                var dose = contexto.Dose.Where(x => x.Identificador == vacinado.dose.Identificador).FirstOrDefault();

                vacinado.dose = dose;
                if (pessoaVacinada == null)                
                    contexto.Vacinado.Add(vacinado);                
                else                
                    contexto.Update(vacinado);
                contexto.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogDebug("VacinaController.Put.Error", ex);
                throw;                
            }            
        }
    }
}