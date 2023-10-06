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
        public async Task<IActionResult> Arecords([FromBody] Fielddto field)
        {


            try
            {
                if (field != null)
                {
                    var addedrecord = await fieldInterface.Arecords(field);
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




        [HttpGet("Vrecords/{ColumnId}")]
        public async Task<IActionResult> Vrecords([FromRoute] Guid ColumnId)
        {
            try
            {
                var fields = await fieldInterface.Vrecords(ColumnId);
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



        


        [HttpGet("Domain/{tableid}")]
        public async Task<IActionResult> Domain([FromRoute] Guid tableid)
        {
            try
            {
                var records = await fieldInterface.Domain(tableid);
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


        [HttpGet("Formsincom/{formid}")]
        public async Task<IActionResult> Formsincom([FromRoute] Guid formid)
        {
            try
            {
                var formsview = await fieldInterface.Formsincom(formid);
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




        [HttpGet("Domaincom/{domainid}")]
        public async Task<IActionResult> Domaincom([FromRoute] Guid domainid)
        {
            try
            {
                var domainview = await fieldInterface.Domaincom(domainid);
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






        [HttpGet("Found/{searchWord}")]
        public async Task<IActionResult> Found([FromRoute] string searchWord)
        {

            try
            {
                var Columns = await fieldInterface.Found(searchWord);
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
        public async Task<IActionResult> Erecords([FromBody] Field updatedField)
        {
            try
            {
                if (updatedField != null)
                {
                    
                    var editrecord = await fieldInterface.Erecords( updatedField);
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








        [HttpGet("Table")]
        public async Task<IActionResult> Table()
        {
            try
            {
                var domain = await fieldInterface.Table();
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




        [HttpGet("Form")]
        public async Task<IActionResult> Form()
        {
            try
            {
                var form = await fieldInterface.Form();
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
