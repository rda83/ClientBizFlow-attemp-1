using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ClientBizFlow_attemp_1.Database.Entities.BizFlow
{
    public class PipelineItem
    {
        [Key]
        public long Id { get; set; }
        
        [ForeignKey("PipelineId")]
        public Pipeline Pipeline { set; get; }
        
        [Required]
        public long PipelineId { get; set; }

        public string? TypeOperationId { get; set; }
        public int SortOrder { get; set; }
        public string? Description { get; set; }
        public bool Blocked { get; set; }
        public JsonElement Options { get; set; }
    }
}
