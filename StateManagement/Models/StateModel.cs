
namespace StateManagement.Models
{
    public class StateAddRequestModel
    {
        public string Name { get; set; }
        public int Sequence { get; set; }
        public int FlowId { get; set; }
    }
    public class StateUpdateRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
