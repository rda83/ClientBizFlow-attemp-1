using System.ComponentModel.DataAnnotations;

namespace ClientBizFlow_attemp_1.Database.Entities.BizFlow
{
    public class Pipeline
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CronExpression { get; set; } = string.Empty;
        public List<PipelineItem> PipelineItems { get; set; } = new List<PipelineItem>();
        public string? Description { get; set; }
        public bool Blocked { get; set; }
    }
}
