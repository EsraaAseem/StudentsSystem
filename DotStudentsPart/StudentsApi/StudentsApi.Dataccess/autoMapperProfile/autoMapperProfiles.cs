using AutoMapper;
using StudentsApi.Model.ViewModel;
using  datamodel= StudentsApi.Model;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentsApi.Dataccess.AfterMapping;

namespace StudentsApi.Dataccess.autoMapperProfile
{
    public class autoMapperProfiles:Profile
    {
       public autoMapperProfiles()
        {
            CreateMap<datamodel.Students, Students>().ReverseMap();
            CreateMap<datamodel.Gender, Gender>().ReverseMap();
            CreateMap<StudentAddRequest, datamodel.Students>().AfterMap<AddMappingProfile>();

        }
    }
}
