using FastMarketRESTAPI.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Dto
{
    /// <summary>
    /// Para crear o actualizar una tasa existente
    /// </summary>
    public class TasaCreateOrUpdateDto
    {
        [Required]
        public double Valor { get; set; }
        public string TipoTasa { get; set; }
        public string Descripcion { get; set; }//Es opcional
    }
    /// <summary>
    /// Para mostrar los detalles de la tasa
    /// </summary>
    public class TasaDto
    {
        public int IdTasa { get; set; }
        public double Valor { get; set; }//En %.
        public string TipoTasa { get; set; }
        public string Descripcion { get; set; }
    }
    /// <summary>
    /// Para listar todas las tasas disponibles
    /// </summary>
    public class TasaSimpleDto
    {
        public int IdTasa { get; set; }
        public double Valor { get; set; }//En %.
        public Enums.TipoTasa TipoTasa { get; set; }
    }
}
