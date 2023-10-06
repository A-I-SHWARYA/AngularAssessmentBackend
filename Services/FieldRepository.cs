using Angularassessment.Dto;
using Angularassessment.Models;
using Angularassessment.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Angularassessment.Services
{
    public class FieldRepository : FieldInterface
    {
        private readonly DemoContext demoContext;
        private readonly IMapper mapper;

        public FieldRepository(DemoContext demoContext, IMapper mapper)
        {
            this.demoContext = demoContext;
            this.mapper = mapper;
        }

        public async Task<Fielddto> Addrecords(Fielddto field)
        {
            var record = mapper.Map<Field>(field);
            record.Id = Guid.NewGuid();
            await demoContext.Fields.AddAsync(record);
            await demoContext.SaveChangesAsync();
            return field;
        }

      

        public async Task<IEnumerable<Fielddto>> Viewfieldrecords(Guid id)
        {
            var fields = await demoContext.Fields
                .Include(f => f.Column)
                .Where(f => f.Column.Id == id)
                .ToListAsync();

           
            var fieldDtos = mapper.Map<IEnumerable<Fielddto>>(fields);

            return fieldDtos;
        }

        public async Task<IEnumerable<Aocolumn>> Searchcolumn(string searchWord)
        {
            var columns = await demoContext.Aocolumns
            .Where(c => c.Name.Contains(searchWord))
            .ToListAsync();

            return columns;
        }

        public async Task<Field> Editfieldrecords( Field updatedField)
        {
            
            
                 var modifieddata= demoContext.Fields.Entry(updatedField).State = EntityState.Modified;
                await demoContext.SaveChangesAsync();
                
               
                return updatedField;
           
           

        }



        public async Task<IEnumerable<DomainTable>> Getdomaindata(Guid TableId)
        {

            var domainTableList = await demoContext.DomainTables
            .Where(dt => dt.TableId == TableId)
            .ToListAsync();

            return domainTableList;

           
           
            

        }


        public async Task<IEnumerable<Form>> Getformsinviewcomponent(Guid formid)
        {
            var formsforview = await demoContext.Forms
           .Where(dt => dt.Id == formid)
           .ToListAsync();

            return formsforview;
        }




        public async Task<IEnumerable<Aotable>> Getdomaininviewcomponent(Guid domainid)
        {
            var domainTable = await demoContext.DomainTables
                .Where(dt => dt.Id == domainid)
                .FirstOrDefaultAsync();

            if (domainTable == null)
            {
               
                return null;
            }

            var aotables = await demoContext.Aotables
                .Where(a => a.Id == domainTable.TableId)
                .ToListAsync();

            return aotables;
        }








        public async Task<IEnumerable<Aotable>> Getaotable()
         {
             var tables = await demoContext.Aotables.ToListAsync();
             return tables;

         }

        public async Task<IEnumerable<Form>> Getform()
        {
            var Forms = await demoContext.Forms.ToListAsync();
            return Forms;


        }




    }
}
