//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace adoProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ciudade
    {
        public ciudade()
        {
            this.terminales = new HashSet<terminale>();
        }
    
        public int idciudad { get; set; }
        public string nombre { get; set; }
        public int idestado { get; set; }
    
        public virtual estado estado { get; set; }
        public virtual ICollection<terminale> terminales { get; set; }
    }
}