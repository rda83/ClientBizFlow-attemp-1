using System.ComponentModel.DataAnnotations;

namespace ClientBizFlow_attemp_1.Database.Entities.BizFlow
{
    public class CancelPipelineRequest
    {
        [Key]
        public long Id { get; set; }
        public string PipelineName { get; set; } = string.Empty;
        public DateTime ExpirationTime { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool ClosingByExpirationTimeOnly { get; set; }
        public DateTime Created { get; set; }
    }
}
