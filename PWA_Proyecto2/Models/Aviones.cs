//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PWA_Proyecto2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Aviones
    {
        public int Id { get; set; }
        public Nullable<int> MarcaId { get; set; }
        public Nullable<int> ModeloId { get; set; }
        public string NumeroSerie { get; set; }
        public string NombreFantasia { get; set; }
        public string Dimensiones { get; set; }
        public Nullable<double> DistanciaMaximaCombustible { get; set; }
        public Nullable<System.DateTime> FechaIngreso { get; set; }
        public string TecnicoIngreso { get; set; }
        public Nullable<int> CantidadExistencia { get; set; }
    }
}
