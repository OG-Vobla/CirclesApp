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
    
    public partial class Student_Check
    {
        public int Id_student_scheck { get; set; }
        public int Id_student_coterie { get; set; }
        public int Id_class { get; set; }
        public int IsAttended { get; set; }
    
        public virtual Class Class { get; set; }
        public virtual Student_coterie Student_coterie { get; set; }
    }
}