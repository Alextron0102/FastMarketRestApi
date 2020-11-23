using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FastMarketRESTAPI.Commons
{
    public static class Enums
    {
        public enum EstadoPago
        {
            /// <summary>
            /// Si pagó la orden en su totalidad
            /// </summary>
            Pagado, 
            /// <summary>
            /// Si el usuario va a pagar la orden despues(no paga nada)
            /// </summary>
            PorPagar, 
            /// <summary>
            /// Si es anulado el costo de la orden
            /// </summary>
            Pago0, 
            /// <summary>
            /// Si es que el usuario dejó una parte del pago
            /// </summary>
            PagoParte
        }
        public enum EstadoDeuda
        {
            Cancelada, Vigente
        }
        public enum TipoTasa
        {
            TasaNominalMensual, TasaEfectivaMensual
        }
    }
}
