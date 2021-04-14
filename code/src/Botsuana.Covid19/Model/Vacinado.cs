using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Botsuana.Covid19.Model
{
    public class Vacinado
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Identificador { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public DateTime dataHora { get; set; }
        public Dose dose { get; set; }
    }
}