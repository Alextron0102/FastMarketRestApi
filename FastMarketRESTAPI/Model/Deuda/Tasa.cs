using FastMarketRESTAPI.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Model.Deuda
{
    public class Tasa
    {
        [Key]
        public int IdTasa { get; set; }
        [Required]
        public double Valor { get; set; }//En %.
        [Required]
        public Enums.TipoTasa TipoTasa { get; set; }
        public string Descripcion { get; set; }
        public List<Deuda> Deudas { get; set; }
        /// <summary>
        /// Para indicar si el usuario puede elegir esta tasa al momento de pagar, caso contrario sólo el administrador puede usarla
        /// </summary>
        [Required]
        public bool Publica { get; set; }
    }
}
