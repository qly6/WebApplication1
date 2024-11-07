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
        public async Task<ActionResult<IEnumerable<UsersDto>>> Get()
        {

            //var query = _context.TaiKhoans.Include(tk => tk.HangHoas).Select(tk => new UsersDto()
            //{
            //    UserId = tk.UserId,
            //    HangHoas = tk.HangHoas.Select(h => new HangHoaDto()
            //    {
            //        Code = h.Code,
            //        Name = h.Name,
            //        Price = h.Price,
            //    })
            //});

            var query = from tk in _context.TaiKhoans
                        select new UsersDto
                        {
                            UserId = tk.UserId,
                            HangHoas = from h in tk.HangHoas
                                       select new HangHoaDto
                                       {
                                           Code = h.Code,
                                           Name = h.Name,
                                           Price = h.Price
                                       }
                        };

            return Ok(await query.ToListAsync());
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
