//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WSR_2021.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ActivityDocument
    {
        public int ActivityId { get; set; }
        public int DocumentId { get; set; }
        public string Nothing { get; set; }
    
        public virtual Activity Activity { get; set; }
        public virtual Document Document { get; set; }
    }
}
