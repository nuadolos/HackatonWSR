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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.EventActivity = new HashSet<EventActivity>();
            this.Users = new HashSet<Users>();
        }
    
        public int Id { get; set; }
        public string Title { get; set; }
        public int DirectionId { get; set; }
        public int CityId { get; set; }
        public System.DateTime DateEvent { get; set; }
        public System.TimeSpan StartEvent { get; set; }
        public System.TimeSpan EndEvent { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
    
        public virtual City City { get; set; }
        public virtual Direction Direction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventActivity> EventActivity { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Users> Users { get; set; }
    }
}
