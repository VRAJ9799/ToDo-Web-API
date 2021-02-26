using System.Text.Json.Serialization;

namespace ToDo_Application.Models
{
    public class ToDo
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
    }
}
