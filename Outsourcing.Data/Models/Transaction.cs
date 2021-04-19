using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outsourcing.Data.Models
{
    public class Transaction : BaseEntity
    {
        public int? WalletId { get; set; }
        public string ToAdressPlayer { get; set; }
        public string ToAddressOuter { get; set; }
        public string HashPlayer { get; set; }
        public string HashOuter { get; set; }
        public string FromAdressPlayer { get; set; }
        public string ValueAtPlayer { get; set; }
        public string FeeAt1Percent { get; set; }
        public string StatusAtPlayer { get; set; }
        public DateTime? DateCreate { get; set; }
        public string PriceGasPlayer { get; set; }
        public string PriceGasOuter { get; set; }
        public string GasLimitPlayer { get; set; }
        public string GasLimitOuter { get; set; }
        public int? ConfirmationsOuter { get; set; }
        public int? ConfirmationsPlayer { get; set; }
        public string BalanceAfterTx { get; set; }
        public int? TimestampPlayer { get; set; }
        public string TimestampOuter { get; set; }
        public string GasUsedPlayer { get; set; }
        public string GasUsedOuter { get; set; }
        public string RawDataPlayer { get; set; }
        public string RawDataOuter { get; set; }
        public string Temp1 { get; set; }
        public string Temp2 { get; set; }
        public string Temp3 { get; set; }
        public string Temp4 { get; set; }
        public string Temp5 { get; set; }
        public bool? IsWithdraw { get; set; } 

        public int? Type { get; set; } //1. deposit ETH từ outerETH sang player , 2. withdraw từ player(sẽ dùng fiat chuyển) sang outer
                                        //3. transfer từ ETH sang VIP, 4. transfer từ VIP sang ETH, 5 transfer VIP sang VIP
        public string FeeTransaction { get; set; }
        public string ETH_AMOUNT { get; set; }//số eth được nạp vào, hoặc rút ra phụ thuộc vào type
        public string VIP_AMOUNT { get; set; } //số vip chuyển cho username khác, hoặc nhận từ user khác
        public string ETH_TO_VIP_AMOUNT { get; set; } //số ETH chuyển sang vip, đơn vị là ETH (chưa nhân với tỉ lệ chuyển đổi)
        public string VIP_TO_ETH_AMOUNT { get; set; } //số vip chuyển sang eth, đơn vị là vip ( chưa nhân với tỉ lệ chuyển đổi)
        public string Balance_VIP_AfterTx { get; set; }//balance vip sau khi thực hiện giao dịch, 
        public string ETH_TO_VIP_PERCENT { get; set; }
        public string VIP_TO_ETH_PERCENT { get; set; }
        public string ETH_RATING { get; set; } //tỉ giá 1ETH bằng mấy usd
        public string VIP_RATING { get; set; } // tỉ giá 1 usd bằng mấy VIP
        public int UserId { get; set; }
        public virtual User Users { get; set; }
    }
}
