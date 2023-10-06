using Angularassessment.Dto;
using Angularassessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angularassessment.Services.Interfaces
{
    public interface FieldInterface
    {
        public Task<Fielddto> Addrecords(Fielddto field);

        public Task<IEnumerable<Fielddto>> Viewfieldrecords(Guid id);

        public Task<IEnumerable<Aocolumn>> Searchcolumn(string searchWord);

        public Task<Field> Editfieldrecords(Field updatedField);

        public Task<IEnumerable<DomainTable>> Getdomaindata(Guid TableId);

        public Task<IEnumerable<Aotable>> Getaotable();

        public Task<IEnumerable<Form>> Getform();

        public  Task<IEnumerable<Form>> Getformsinviewcomponent(Guid formid);

        public Task<IEnumerable<Aotable>> Getdomaininviewcomponent(Guid domainid);




    }
}
