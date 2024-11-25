using System.Runtime.Serialization;

namespace FoodApp.Data.Entities
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,  
        
        [EnumMember(Value ="InProgress")]
        InProgress, 

        [EnumMember(Value = "Delivered")]
        Delivered,

        [EnumMember(Value = "Rejected")]
        Rejected,

        [EnumMember(Value = "Cancelled")]
        Cancelled,

        [EnumMember(Value = "Completed")]
        Completed
    }
}