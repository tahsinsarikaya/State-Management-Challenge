
namespace StateManagement.Models
{
    public class FlowAddRequestModel
    {
        public string Name { get; set; }
    }

    public class FlowUpdateRequestModel: FlowAddRequestModel
    {
        public int Id { get; set; }
    }
}
