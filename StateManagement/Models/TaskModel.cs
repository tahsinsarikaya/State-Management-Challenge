
namespace StateManagement.Models
{
    public class TaskAddRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public int FlowId { get; set; }
    }
    public class TaskUpdateRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
