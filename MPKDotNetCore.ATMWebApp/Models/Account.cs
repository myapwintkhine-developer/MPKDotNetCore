using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MPKDotNetCore.ATMWebApp.Models
{
    [Table("Account")]
    public class Account
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public string NRCNo {  get; set; }
        public int AccountNo { get; set; }
        public int Pin { get; set; }
        public decimal Balance { get; set; }
    }

    public class MessageModel
    {
        public MessageModel() { }
        public MessageModel(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
