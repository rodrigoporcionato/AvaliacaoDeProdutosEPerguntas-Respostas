//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BetaViews.Messages.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NPSLojaModel
    {
        public int Id { get; set; }
        public int IdLoja { get; set; }
        public int Nota { get; set; }
        public System.DateTime DtAvaliacao { get; set; }
        public string Canal { get; set; }
    
        public virtual LojaModel LojaModel { get; set; }
    }
}