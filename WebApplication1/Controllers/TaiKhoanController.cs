using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.RequestModel;
using WebApplication1.ResponseModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly QLBHDbContext _context;

        public TaiKhoanController(QLBHDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// API Get DS TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TaiKhoanResponseModel> Get()
        {
            return _context.TaiKhoans
                .Include(taikhoan => taikhoan.HangHoas)
                .Select(taikhoan => new TaiKhoanResponseModel()
                {
                    Address = taikhoan.Address,
                    Email = taikhoan.Email,
                    Firstname = taikhoan.Firstname,
                    Lastname = taikhoan.Lastname,
                    UserId = taikhoan.UserId,
                    HangHoas = taikhoan.HangHoas.Select(hanghoa => new HangHoaDto()
                    {
                        Code = hanghoa.Code,
                        Name = hanghoa.Name,
                        Price = hanghoa.Price,
                    })
                }).ToList();
        }

        /// <summary>
        /// API Them TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public TaiKhoan Post(AddTaiKhoanModel taiKhoan)
        {
            TaiKhoan tk = new TaiKhoan()
            {
                Firstname = taiKhoan.Firstname,
                Lastname = taiKhoan.Lastname,
                Email = taiKhoan.Email,
                UserPassword = taiKhoan.UserPassword,
                Address = taiKhoan.Address,
                CreatedDate = DateTime.Now
            };
            _context.TaiKhoans.Add(tk);
            _context.SaveChanges();
            return tk;
        }

        /// <summary>
        /// API Them TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpPut("id")]
        public TaiKhoan Update(int id,UpdateTaiKhoanModel updateTaiKhoan)
        {
            TaiKhoan taikhoan = _context.TaiKhoans.FirstOrDefault(e => e.UserId == id);
            if (taikhoan != null)
{
                taikhoan.Lastname = updateTaiKhoan.Lastname;
                taikhoan.Firstname = updateTaiKhoan.Firstname;
                _context.SaveChanges();
            }
            return taikhoan;
        }

        /// <summary>
        /// API Them TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpDelete("id")]
        public TaiKhoan Delete(int id)
        {
            TaiKhoan taikhoan = _context.TaiKhoans.FirstOrDefault(e => e.UserId == id);
            if (taikhoan != null)
            {
                _context.Remove(taikhoan);
                _context.SaveChanges();
            }
            return taikhoan;
        }
    }
}
