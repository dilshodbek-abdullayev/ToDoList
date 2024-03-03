using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities.Models
{
    public class NotePad
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public DateTime To =DateTime.Now.ToLocalTime();
        public DateTime Do = DateTime.Now.ToLocalTime();

    }
}
