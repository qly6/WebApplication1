using WebApplication1.Models;

namespace WebApplication1.ResponseModel
{
    public class UsersDto
    {
        public int UserId { get; set; }
        public List<HangHoa> HangHoas { get; set; }
    }
}
