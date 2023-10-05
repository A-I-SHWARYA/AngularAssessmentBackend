using Angularassessment.Dto;
using Angularassessment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Angularassessment.Services.Interfaces
{
    public interface FieldInterface
    {
        public Task<Fielddto> addRecords(Fielddto field);

        public Task<IEnumerable<Fielddto>> viewRecords(Guid id);

        public Task<IEnumerable<Aocolumn>> getColumns(string searchWord);

        public Task<Field> editRecords(Field updatedField);

        public Task<IEnumerable<DomainTable>> getDomain(Guid TableId);

        public Task<IEnumerable<Aotable>> getTable();

        public Task<IEnumerable<Form>> getForm();

        public  Task<IEnumerable<Form>> getFormsView(Guid formid);

        public Task<IEnumerable<Aotable>> getDomainView(Guid domainid);




    }
}
