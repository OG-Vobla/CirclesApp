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
    
    public partial class Class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Class()
        {
            this.Student_Check = new HashSet<Student_Check>();
        }
    
        public int Id_class { get; set; }
        public int Number_cabinet { get; set; }
        public System.DateTime Date { get; set; }
        public System.TimeSpan Time_start { get; set; }
        public System.TimeSpan Time_end { get; set; }
        public int Id_employee { get; set; }
        public Nullable<int> Id_coterie { get; set; }
    
        public virtual Coterie Coterie { get; set; }
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student_Check> Student_Check { get; set; }
    }
}
