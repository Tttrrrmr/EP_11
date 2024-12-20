//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EP_1.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Concert
    {
        public int ID_concert { get; set; }
        public int ID_type_of_concert { get; set; }
        public int ID_singer { get; set; }
        public int ID_city { get; set; }
        public int ID_organizer { get; set; }
        public string Name { get; set; }
        public System.DateTime Date_concert { get; set; }

        //public string date
        //{
        //    get
        //    {
        //        Console.WriteLine(Date_concert.ToShortDateString());
        //        return Date_concert.ToShortDateString();
        //    }
        //}
        public int Cost { get; set; }
        public string Logo { get; set; }

        public string Pic
        {
            get
            {
                if (string.IsNullOrEmpty($"../../Images/{Logo}"))
                {
                    return "../Images/picture.png";
                }
                return $"../Images/{Logo}";
            }
        }

        public virtual City City { get; set; }
        public virtual Organizer Organizer { get; set; }
        public virtual Singer Singer { get; set; }
        public virtual Type_of_concert Type_of_concert { get; set; }
    }
}
