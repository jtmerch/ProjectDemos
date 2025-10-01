using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NuveiPayBlazorDemo.Shared
{
    public class NuveiCC
    {
        [Key]
        public Guid Id { get; set; }
        public string? MerchTblId { get; set; } = "00000000-0000-0000-0000-000000000000";
        [MaxLength(200)]
        public string? MerchantName { get; set; }
        [MaxLength(20)]
        public string? merchantID { get; set; }

        [MaxLength(20)]
        public string? merchantSiteID { get; set; }

        [MaxLength(150)]
        public string? merchantSecretKey { get; set; }

        public string? ProcessorType { get; set; }

        public string? UserIdentifier { get; set; }


        [DefaultValue("US")]
        [MaxLength(10)]
        public string? CountryCode { get; set; }

        [DefaultValue("ECommerce")]
        [MaxLength(30)]
        public string? ChannelType { get; set; }

        [DefaultValue("ECommerce")]
        [MaxLength(30)]
        public string? ProcessingMethod { get; set; }

        [DefaultValue("SameDay")]
        public bool SupportsEnhancedData { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AnnualTransactionVolume { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AverageTicketSize { get; set; }

        [DefaultValue("SameDay")]
        [MaxLength(30)]
        public string? SettlementTimeFrame { get; set; }

        [DefaultValue("RealTime")]
        [MaxLength(30)]
        public string? AuthorizationMethod { get; set; }

        [ForeignKey("GroupMember")]
        public Guid? GroupMemberId { get; set; }
      //  public virtual GroupMemberModel GroupMember { get; set; }

        [ForeignKey("ISO")]
        public Guid? ISOId { get; set; }
     //   public virtual ISOModel ISO { get; set; }

        [ForeignKey("Agent")]
        public Guid? AgentId { get; set; }
      //  public virtual AgentModel Agent { get; set; }

        [ForeignKey("MCC")]
        public Guid? MCCId { get; set; }
      //  public virtual MCCModel MCC { get; set; }


        public string? MerchantReturnURL { get; set; }
        public bool Active { get; set; }
        public bool PerformMint { get; set; }

    }
}
