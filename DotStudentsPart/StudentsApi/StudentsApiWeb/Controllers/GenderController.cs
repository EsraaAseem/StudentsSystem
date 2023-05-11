using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Model.ViewModel;

namespace StudentsApiWeb.Controllers
{
    [ApiController]
    public class GenderController : Controller
    {
        private readonly IUnitOfWrok _unitOfWork;
        // private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public GenderController(IUnitOfWrok unitOfWrok, IMapper mapper)
        {
            _unitOfWork = unitOfWrok;
            this._mapper = mapper;
        }
        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetGender()
        {
            // IEnumerable<StudentsApi.Model.Students> objectcategory =await _unitOfWork.Students.GetAll();
            var gender = await _unitOfWork.gender.GetAll();
            if (gender == null ||!gender.Any())
                return NotFound();
            //_mapper.Map<List<Students>>(student)
            return Ok(_mapper.Map<List<Gender>>(gender));
        }
    }
}
