using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities.DTOs
{
    public class NotePadDTO
    {
        public string? Note { get; set; }
        [JsonIgnore]
        public string? Status { get; set; } = "false";

    }
}
