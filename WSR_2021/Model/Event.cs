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
    
    public partial class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DirectionId { get; set; }
        public System.DateTime DateEvent { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    
        public virtual Direction Direction { get; set; }
    }
}
