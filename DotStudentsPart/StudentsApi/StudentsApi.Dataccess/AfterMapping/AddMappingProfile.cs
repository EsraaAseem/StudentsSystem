using AutoMapper;
using StudentsApi.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using datamodel = StudentsApi.Model;


namespace StudentsApi.Dataccess.AfterMapping
{
    internal class AddMappingProfile : IMappingAction<StudentAddRequest, datamodel.Students>
    {
        public void Process(StudentAddRequest source, Model.Students destination, ResolutionContext context)
        {
            destination.id = new Guid();
        }
    }
}
