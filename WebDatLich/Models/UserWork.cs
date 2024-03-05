namespace WebDatLich.Models
{
/*Tạo lớp trung gian giữa User và Work*/
    public class UserWork
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int WorkId { get; set; }
        public Work Work { get; set; }
    }
}
