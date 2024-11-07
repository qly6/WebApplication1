using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using WebApplication1.Models;
using WebApplication1.RequestModel;
using WebApplication1.ResponseModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public IEnumerable<TaiKhoanResponseModel> Get()
        {

        // Cach1 : Linq Extension
        //return _context.TaiKhoans
        //    .Include(taikhoan => taikhoan.HangHoas)
        //    .Select(taikhoan => new TaiKhoanResponseModel()
        //    {
        //        Address = taikhoan.Address,
        //        Email = taikhoan.Email,
        //        Firstname = taikhoan.Firstname,
        //        Lastname = taikhoan.Lastname,
        //        UserId = taikhoan.UserId,
        //        HangHoas = taikhoan.HangHoas.Select(hanghoa => new HangHoaDto()
        //        {
        //            Code = hanghoa.Code,
        //            Name = hanghoa.Name,
        //            Price = hanghoa.Price,
        //        })
        //    }).ToList();

            //Cach2: Linq Query
         var query = from taikhoan in _context.TaiKhoans
                       select new TaiKhoanResponseModel
                       {
                           UserId = taikhoan.UserId,
                           Email = taikhoan.Email,
                           Firstname = taikhoan.Firstname,
                           Lastname = taikhoan.Lastname,
                           HangHoas = from h in taikhoan.HangHoas
                               select new HangHoaDto
                                {
                                    Code = h.Code,
                                    Name = h.Name,
                                    Price = h.Price
                                }
                        };
            return query.ToList();
        }

        [HttpPost("login")]
        public ActionResult<string> Login([FromBody] LoginRequestModel loginRequest)
        {
            var taiKhoan = _context.TaiKhoans.FirstOrDefault(taiKhoan => taiKhoan.Email == loginRequest.Email && taiKhoan.UserPassword == loginRequest.Password);
            if (taiKhoan == null)
            {
                return NotFound();
            }

            return _jwtHelper.GenerateToken(taiKhoan.Email, "user");
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
