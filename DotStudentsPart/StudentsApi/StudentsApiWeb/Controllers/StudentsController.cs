using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using StudentsApi.Dataccess.Data;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Model;
using StudentsApi.Model.ViewModel;
using datamodel = StudentsApi.Model;
using Students = StudentsApi.Model.Students;

namespace StudentsApiWeb.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
      
        private readonly IUnitOfWrok _unitOfWork;
       // private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public StudentsController(IUnitOfWrok unitOfWrok, IMapper mapper)
        {
            _unitOfWork = unitOfWrok;
            this._mapper = mapper;
        }

        [HttpGet]
       // [Authorize(AuthenticationSchemes = "Bearer")]
        [Route("[controller]")]
        public async Task<IActionResult> GetStudents()
        {
            var student = await _unitOfWork.Students.GetAll(includeProperity: "gender");
            //_mapper.Map<List<Students>>(student)
            return Ok(_mapper.Map<List<Students>>(student));
        }
        [HttpGet]
        [Route("[controller]/{stdId:guid}")]
        public async Task<IActionResult> GetStudentAysnc([FromRoute] Guid stdId)
        {
            var student = await _unitOfWork.Students.GetFirstOrDefualt(u=>u.id==stdId,includeProperity: "gender");
            if (student == null)
                return NotFound();
            return Ok(_mapper.Map<Students>(student));
        }
        [HttpPost]
        [Route("[controller]/{id:guid}")] 
        public async Task<IActionResult> UpdateStudent([FromRoute]Guid id,[FromBody] StudentAddRequest request)//,IFormFile?file
        {
           
                var student = new Students();
                student.firstName = request.firstName;
                student.lastName = request.lastName;
                student.email = request.email;
                student.phone = request.phone;
                student.birthOfData = request.birthOfData;
                student.address = request.address;
                student.imgUrl = request.imgUrl;
                student.genderId = request.genderId;
                var std = await _unitOfWork.Students.Update(id,student);
                _unitOfWork.save();
                return Ok(std);
            
        }
        [HttpDelete]
        [Route("[controller]/{id:guid}")]

        public async Task<IActionResult> DeletePost([FromRoute]Guid id)
        {
            var obj =await _unitOfWork.Students.GetFirstOrDefualt(u => u.id == id);
            
          
            if (obj == null)
            {
                //  return Json(new { success = false, Message = "Faild To Deleting Data" });
                return NotFound();
            }
       
          await _unitOfWork.Students.Delete(id);
            _unitOfWork.save();

            return Ok(_mapper.Map<Students>(obj));
        }

        [HttpPost]
        [Route("[controller]/upload-img")]
        public async Task<IActionResult> Uploadphoto(IFormFile profimgfile)
        {
            if (profimgfile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images");
                var extention = Path.GetExtension(profimgfile.FileName);
                /* if (request.imgUrl != null)
                 {
                     var oldimg = Path.Combine(Directory.GetCurrentDirectory(), request.imgUrl.TrimStart('\\'));
                     if (System.IO.File.Exists(oldimg))
                     {
                         System.IO.File.Delete(oldimg);
                     }
                 }*/
                var uploadimg = Path.Combine(uploads, fileName + extention);
                using (var fileStreams = new FileStream(uploadimg, FileMode.Create))
                {
                    await profimgfile.CopyToAsync(fileStreams);
                }
                return Ok(Path.Combine(@"\Resources\Images\" + fileName + extention));
               // return Path.Combine(@"\Resources\Images" + fileName + extention);
            }
            else
            {
                return BadRequest();
            }
        }

       [HttpDelete]
        [Route("[controller]/{idimg:guid}/upload-img")]

        public async Task<IActionResult> DeleteImage([FromRoute] Guid idimg)
        {
            var obj = await _unitOfWork.Students.GetFirstOrDefualt(u => u.id == idimg);
            if (obj == null)
            {
                return NotFound();
            }
            if (obj.imgUrl != null)
            {
                var oldimg = Path.Combine(Directory.GetCurrentDirectory(), obj.imgUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldimg))
                {
                    System.IO.File.Delete(oldimg);
                }
            }
            return Ok();
        }
            [HttpPost]
         //[Authorize]
        [Route("[controller]/Add")]
        public async Task<IActionResult> AddStudent([FromBody] StudentAddRequest request)//,IFormFile?file
        {
           /*  if (request.imgFileUrl != null)
             {
                 
                 request.imgUrl = await _unitOfWork.Students.UploadImg(request.imgFileUrl);
             }*/
           var student = new Students();
            student.firstName = request.firstName;
            student.lastName = request.lastName;
            student.email = request.email;
            student.phone = request.phone;
            student.birthOfData = request.birthOfData;
            student.address = request.address;
             student.imgUrl = request.imgUrl;
            student.genderId = request.genderId;
            var std = await _unitOfWork.Students.Add(student);
            _unitOfWork.save();
            return Ok(std);
           


        }

      
    }
}
