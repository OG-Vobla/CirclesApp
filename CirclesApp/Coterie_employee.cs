//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CirclesApp
{
    using System;
    using System.Collections.Generic;
    
    public partial class Coterie_employee
    {
        public int Id_coterie_student { get; set; }
        public int Id_coterie { get; set; }
        public int Id_employee { get; set; }
    
        public virtual Coterie Coterie { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
