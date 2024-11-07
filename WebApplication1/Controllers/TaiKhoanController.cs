using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.RequestModel;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaiKhoanController : ControllerBase
    {
        private readonly QLBHDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public TaiKhoanController(QLBHDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        /// <summary>
        /// API Get DS TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public IEnumerable<TaiKhoan> Get()
        {
            return _context.TaiKhoans.ToList();
        }

        /// <summary>
        /// API Get DS TaiKhoan
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginTaiKhoan loginTaiKhoan)
        {
            var taikhoan = await _context.TaiKhoans.FirstOrDefaultAsync(taikhoan => taikhoan.Email == loginTaiKhoan.Email && taikhoan.UserPassword == loginTaiKhoan.Password);
            if (taikhoan == null)
            {
                return NotFound();
            }
            string token = _jwtHelper.GenerateToken(taikhoan.Email, "user");
            return Ok(token);
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
