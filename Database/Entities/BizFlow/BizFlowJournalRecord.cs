using BizFlow.Core.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientBizFlow_attemp_1.Database.Entities.BizFlow
{
    public class BizFlowJournalRecord
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "timestamp without time zone")]
        public DateTime Period { get; set; }
        public string PipelineName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemSortOrder { get; set; }

        [Column(TypeName = "text")]
        public TypeBizFlowJournaAction TypeAction { get; set; }
        public string TypeOperationId { get; set; }
        public string LaunchId { get; set; }
        public string Message { get; set; }
        public string Trigger { get; set; }
        public bool IsStartNowPipeline { get; set; }
    }
}
