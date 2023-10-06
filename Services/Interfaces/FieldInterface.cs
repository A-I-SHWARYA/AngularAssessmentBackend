using Angularassessment.Dto;
using Angularassessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angularassessment.Services.Interfaces
{
    public interface FieldInterface
    {
        public Task<Fielddto> Arecords(Fielddto field);

        public Task<IEnumerable<Fielddto>> Vrecords(Guid id);

        public Task<IEnumerable<Aocolumn>> Found(string searchWord);

        public Task<Field> Erecords(Field updatedField);

        public Task<IEnumerable<DomainTable>> Domain(Guid TableId);

        public Task<IEnumerable<Aotable>> Table();

        public Task<IEnumerable<Form>> Form();

        public  Task<IEnumerable<Form>> Formsincom(Guid formid);

        public Task<IEnumerable<Aotable>> Domaincom(Guid domainid);




    }
}
