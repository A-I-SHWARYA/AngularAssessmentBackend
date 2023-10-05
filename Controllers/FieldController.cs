using Angularassessment.Dto;
using Angularassessment.Models;
using Angularassessment.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.SqlServer.Server;
using System.Linq.Expressions;

namespace Angularassessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FieldController : ControllerBase
    {
        private readonly FieldInterface fieldInterface;

        public FieldController(FieldInterface fieldInterface)
        {
            this.fieldInterface = fieldInterface;
            

        }




        [HttpPost]
        public async Task<IActionResult> addRecords([FromBody] Fielddto field)
        {


            try
            {
                if (field != null)
                {
                    var addedrecord = await fieldInterface.addRecords(field);
                    if (addedrecord != null)
                    {
                        return Ok(addedrecord);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }


        }




        [HttpGet("viewRecords/{ColumnId}")]
        public async Task<IActionResult> viewRecords([FromRoute] Guid ColumnId)
        {
            try
            {
                var fields = await fieldInterface.viewRecords(ColumnId);
                if (fields != null)
                {
                    return Ok(fields);
                }
                else { return NotFound("fields not found"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        


        [HttpGet("getDomain/{tableid}")]
        public async Task<IActionResult> getDomain([FromRoute] Guid tableid)
        {
            try
            {
                var records = await fieldInterface.getDomain(tableid);
                if (records != null)
                {
                    return Ok(records);
                }
                else { return NotFound("records not found"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getFormsView/{formid}")]
        public async Task<IActionResult> getFormsView([FromRoute] Guid formid)
        {
            try
            {
                var formsview = await fieldInterface.getFormsView(formid);
                if (formsview != null)
                {
                    return Ok(formsview);
                }
                else { return NotFound("forms not found"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("getDomainView/{domainid}")]
        public async Task<IActionResult> getDomainView([FromRoute] Guid domainid)
        {
            try
            {
                var domainview = await fieldInterface.getDomainView(domainid);
                if (domainview != null)
                {
                    return Ok(domainview);
                }
                else { return NotFound("tables not found"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        [HttpGet("getColumns/{searchWord}")]
        public async Task<IActionResult> getColumns([FromRoute] string searchWord)
        {

            try
            {
                var Columns = await fieldInterface.getColumns(searchWord);
                if (Columns != null)
                {
                    return Ok(Columns);
                }
                else { return NotFound("Columns not found"); }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }



        [HttpPut]
        public async Task<IActionResult> editRecords([FromBody] Field updatedField)
        {
            try
            {
                if (updatedField != null)
                {
                    
                    var editrecord = await fieldInterface.editRecords( updatedField);
                    if (editrecord != null)
                    {
                        return Ok(editrecord);
                    }
                    else
                    {
                        return NotFound("Record cannot be editted!");
                    }
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }








        [HttpGet("getTable")]
        public async Task<IActionResult> getTable()
        {
            try
            {
                var domain = await fieldInterface.getTable();
                if (domain != null)
                {
                    return Ok(domain);

                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpGet("GetForm")]
        public async Task<IActionResult> getForm()
        {
            try
            {
                var form = await fieldInterface.getForm();
                if (form != null)
                {
                    return Ok(form);

                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
