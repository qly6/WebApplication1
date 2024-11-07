namespace WebApplication1.ResponseModel
{
    public class TaiKhoanResponseModel
    {
        public int UserId { get; set; }

        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public string? Email { get; set; }

        public string? Address { get; set; }

        public IEnumerable<HangHoaDto> HangHoas { get; set; }

    }
}
