using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Color:IEntity
    {
        [Key]
        public int Id { get; set; }
        public string ColorName { get; set; }
    }
}
