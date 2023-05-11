using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentsApi.Dataccess.Data;
using StudentsApi.Dataccess.Repository.IRepository;
using StudentsApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsApi.Dataccess.Repository
{
    public class StudentsRepository : Repository<Students>, IStudentsRepository
    {
        private ApplicatioDbContext _db;
        public StudentsRepository(ApplicatioDbContext db):base(db)
        {
            _db = db;
        }

        public async Task<Students> Delete(Guid id)
        {
            var obj = _db.students.FirstOrDefault(u => u.id == id);
            if (obj != null)
            {
               _db.students.Remove(obj);
                return obj;
            }
            return null;
        }

       

        public async Task<Students> Update(Guid id,Students students)
        {
            var obj = _db.students.FirstOrDefault(u => u.id ==id);
            if(obj!=null)
            {
                obj.firstName = students.firstName;
                obj.lastName = students.lastName;
                obj.phone = students.phone;
                obj.email = students.email;
                obj.address = students.address;
                obj.birthOfData = students.birthOfData;
                obj.genderId = students.genderId;
                if (students.imgUrl != null)
                {
                    obj.imgUrl = students.imgUrl;
                }
               // _db.SaveChanges();
                return obj;
            }
            return null;
        }
        public async Task<string> UploadImg(IFormFile file)
        {
            if (file.Length>0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Images");
                var extention = Path.GetExtension(file.FileName);
                /* if (request.imgUrl != null)
                 {
                     var oldimg = Path.Combine(Directory.GetCurrentDirectory(), request.imgUrl.TrimStart('\\'));
                     if (System.IO.File.Exists(oldimg))
                     {
                         System.IO.File.Delete(oldimg);
                     }
                 }*/
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extention), FileMode.Create))
                {
                    await file.CopyToAsync(fileStreams);
                }
                return Path.Combine(@"\Resources\Images" + fileName + extention);
            }
            else
            {
                return null;
            }
        }
        
    }
}
