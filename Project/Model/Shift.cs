//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Shift
    {
        public Shift()
        {
            this.EmployeeShifts = new HashSet<EmployeeShift>();
        }
    
        public int ShiftID { get; set; }
        public int StartDateTime { get; set; }
        public int EndDateTime { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<EmployeeShift> EmployeeShifts { get; set; }
    }
}
