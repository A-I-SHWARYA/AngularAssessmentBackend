using Angularassessment.Controllers;
using Angularassessment.Dto;
using Angularassessment.Models;
using Angularassessment.Services.Interfaces;

using AutoFixture;
using AutoFixture.AutoMoq;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moq;

namespace AssessmentTest.Controlllers
{
    public class FieldControllerTests
    {
        private readonly IFixture fixture;
        private readonly Mock<FieldInterface> fieldInterface;
        private readonly FieldController fieldController;

        public FieldControllerTests()
        {
            fixture = new Fixture();
            


           
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            fieldInterface = fixture.Freeze<Mock<FieldInterface>>();
            fieldController = new FieldController(fieldInterface.Object);
        }



        


        //Adding  Field table record

        [Fact]
        public async Task Addrecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var fields = fixture.Create<Fielddto>();
            var returnData = fixture.Create<Fielddto>();
            fieldInterface.Setup(t => t.Addrecords(fields)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.Addrecords(fields) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var addedData = result.Value as Fielddto;
            addedData.Should().NotBeNull();

            addedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.Addrecords(fields), Times.Once());
        }


        [Fact]
        public void Addrecords_ShouldReturnBadRequest_WhenFieldObjectIsNull()
        {
            //Arrange
            Fielddto field = null;
            fieldInterface.Setup(t => t.Addrecords(field)).ReturnsAsync((Fielddto)null);

            //Act
            var result = fieldController.Addrecords(field);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            fieldInterface.Verify(t => t.Addrecords(field), Times.Never());
        }


        [Fact]
        public async Task Addrecords_ShouldReturnNotFound_WhenAddFailed()
        {
            var fields = fixture.Create<Fielddto>();
            fieldInterface.Setup(c => c.Addrecords(fields)).ReturnsAsync((Fielddto)null);

            // Act
            var result = await fieldController.Addrecords(fields);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>(); 

            fieldInterface.Verify(t => t.Addrecords(fields), Times.Once());
        }




        [Fact]
        public async Task Addrecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var field = fixture.Create<Fielddto>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.Addrecords(field)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Addrecords(field) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Addrecords(field), Times.Once());
        }






        //View Field Table Record


        [Fact]
        public async Task Viewfieldrecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Fielddto>>();
            fieldInterface.Setup(c => c.Viewfieldrecords(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Viewfieldrecords(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Fielddto>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Viewfieldrecords(id), Times.Once());
        }




        [Fact]
        public async Task Viewfieldrecords_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            fieldInterface.Setup(c => c.Viewfieldrecords(id)).Returns(Task.FromResult<IEnumerable<Fielddto>>(null));

            // Act
            var result = await fieldController.Viewfieldrecords(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("fields not found");

            fieldInterface.Verify(t => t.Viewfieldrecords(id), Times.Once());
        }



        [Fact]
        public async Task Viewfieldrecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Viewfieldrecords(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Viewfieldrecords(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Viewfieldrecords(id), Times.Once());
        }



        //GetFormNames

        [Fact]
        public async Task Getform_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.Getform()).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Getform() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Getform(), Times.Once());
        }




       [Fact]
        public async Task Getform_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            
            fieldInterface.Setup(c => c.Getform()).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.Getform() as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

           

            fieldInterface.Verify(t => t.Getform(), Times.Once());
        }



         [Fact]
        public async Task Getform_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Getform()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Getform() as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Getform(), Times.Once());
        }


        //GetFormsinview

        [Fact]
        public async Task Getformsinviewcomponent_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.Getformsinviewcomponent(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Getformsinviewcomponent(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

           
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Getformsinviewcomponent(id), Times.Once());
        }



       [Fact]
        public async Task Getformsinviewcomponent_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.Getformsinviewcomponent(id)).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.Getformsinviewcomponent(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("forms not found");

            fieldInterface.Verify(t => t.Getformsinviewcomponent(id), Times.Once());
        }


        [Fact]
       public async Task Getformsinviewcomponent_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
           var exceptionMessage = "ExpectedExceptionMessage";
           fieldInterface.Setup(c => c.Getformsinviewcomponent(id)).Throws(new Exception(exceptionMessage));

           // Act
           var result = await fieldController.Getformsinviewcomponent(id) as BadRequestObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<BadRequestObjectResult>();

           var actualExceptionMessage = result.Value as string;
           actualExceptionMessage.Should().NotBeNull();
           actualExceptionMessage.Should().Be(exceptionMessage);

           fieldInterface.Verify(t => t.Getformsinviewcomponent(id), Times.Once());
       }



        //GetDomainTableNames

         [Fact]
         public async Task Getaotable_ShouldReturnOk_WhenSuccess()
         {
             // Arrange

             var expectedfields = fixture.Create<IEnumerable<Aotable>>();
             fieldInterface.Setup(c => c.Getaotable()).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.Getaotable() as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<Aotable>;
             actualfields.Should().NotBeNull();


             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.Getaotable(), Times.Once());
         }




         [Fact]
         public async Task Getaotable_ShouldReturnNotFound_WhenDataNotFound()
         {
             // Arrange

             fieldInterface.Setup(c => c.Getaotable()).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

             // Act
             var result = await fieldController.Getaotable() as NotFoundResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<NotFoundResult>();



             fieldInterface.Verify(t => t.Getaotable(), Times.Once());
         }



         [Fact]
         public async Task Getaotable_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
         {
             // Arrange

             var exceptionMessage = "ExpectedExceptionMessage";
             fieldInterface.Setup(c => c.Getaotable()).Throws(new Exception(exceptionMessage));

             // Act
             var result = await fieldController.Getaotable() as BadRequestObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<BadRequestObjectResult>();

             var actualExceptionMessage = result.Value as string;
             actualExceptionMessage.Should().NotBeNull();
             actualExceptionMessage.Should().Be(exceptionMessage);

             fieldInterface.Verify(t => t.Getaotable(), Times.Once());
         }







        //GetDomaininview
        [Fact]
        public async Task Getdomaininviewcomponent_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Aotable>>();
            fieldInterface.Setup(c => c.Getdomaininviewcomponent(id)).ReturnsAsync(expectedfields);
           
            // Act
            var result = await fieldController.Getdomaininviewcomponent(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Aotable>;
            actualfields.Should().NotBeNull();

            // Check if the actualColumns and expectedColumns are the same
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Getdomaininviewcomponent(id), Times.Once());
        }



        [Fact]
        public async Task Getdomaininviewcomponent_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.Getdomaininviewcomponent(id)).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

            // Act
            var result = await fieldController.Getdomaininviewcomponent(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("tables not found");

            fieldInterface.Verify(t => t.Getdomaininviewcomponent(id), Times.Once());
        }


        [Fact]
        public async Task Getdomaininviewcomponent_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Getdomaininviewcomponent(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Getdomaininviewcomponent(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Getdomaininviewcomponent(id), Times.Once());
        }

















        //Get Domaintable Fields by passing  tableid




        [Fact]
         public async Task Getdomaindata_ShouldReturnOk_WhenSuccess()
         {
             // Arrange
             Guid id = fixture.Create<Guid>();
             var expectedfields = fixture.Create<IEnumerable<DomainTable>>();
             fieldInterface.Setup(c => c.Getdomaindata(id)).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.Getdomaindata(id) as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<DomainTable>;
             actualfields.Should().NotBeNull();

             
             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.Getdomaindata(id), Times.Once());
         }



        [Fact]
       public async Task Getdomaindata_ShouldReturnNotFound_WhenDataNotFound()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
         
           fieldInterface.Setup(c => c.Getdomaindata(id)).Returns(Task.FromResult<IEnumerable<DomainTable>>(null));

           // Act
           var result = await fieldController.Getdomaindata(id) as NotFoundObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<NotFoundObjectResult>();

           var errorMessage = result.Value as string;
           errorMessage.Should().NotBeNull();
           errorMessage.Should().Be("records not found");

           fieldInterface.Verify(t => t.Getdomaindata(id), Times.Once());
       }





        [Fact]
        public async Task Getdomaindata_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Getdomaindata(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Getdomaindata(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Getdomaindata(id), Times.Once());
        }


        //For fetching Aocolumn name

        [Fact]
        public async Task Searchcolumn_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var search = fixture.Create<string>();
            var expectedTables = fixture.Create<IEnumerable<Aocolumn>>();
            fieldInterface.Setup(c => c.Searchcolumn(search)).ReturnsAsync(expectedTables);

            // Act
            var result = await fieldController.Searchcolumn(search) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualTables = result.Value as IEnumerable<Aocolumn>;
            actualTables.Should().NotBeNull();

            
            actualTables.Should().BeEquivalentTo(expectedTables);

            fieldInterface.Verify(t => t.Searchcolumn(search), Times.Once());
        }



        [Fact]
        public async Task Searchcolumn_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            var word = fixture.Create<string>();
           
            fieldInterface.Setup(c => c.Searchcolumn(word)).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            // Act
            var result = await fieldController.Searchcolumn(word) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Columns not found");

            fieldInterface.Verify(t => t.Searchcolumn(word), Times.Once());
        }



        [Fact]
        public async Task Searchcolumn_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var name = fixture.Create<string>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Searchcolumn(name)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.Searchcolumn(name) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Searchcolumn(name), Times.Once());
        }








        [Fact]
        public async Task Editfieldrecords_ShouldReturnOk_WhenEditSuccess()
        {
            // Arrange
            var table = fixture.Create<Angularassessment.Models.Field>();
            var returnData = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.Editfieldrecords( table)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.Editfieldrecords(table) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var editedData = result.Value as Angularassessment.Models.Field;
            editedData.Should().NotBeNull();

            editedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.Editfieldrecords(table), Times.Once());
        }



        [Fact]
        public void Editfieldrecords_ShouldReturnBadRequestResult_WhenEditFailed()
        {
            
            var Field = new Angularassessment.Models.Field();
            Field = null;
            fieldInterface.Setup(t => t.Editfieldrecords(Field)).ReturnsAsync((Angularassessment.Models.Field)null);

            // Act
            var result = fieldController.Editfieldrecords(Field);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();

            fieldInterface.Verify(t => t.Editfieldrecords(Field), Times.Never()); 
        }



        



        [Fact]
        public async Task Editfieldrecords_ShouldReturnNotFoundObjectResult_WhenDataNotFound()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.Editfieldrecords( table)).Returns(Task.FromResult<Angularassessment.Models.Field>(null));

            // Act
            var result = await fieldController.Editfieldrecords( table) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Record cannot be editted!");

            fieldInterface.Verify(t => t.Editfieldrecords( table), Times.Once());
        }



        [Fact]
        public async Task Editfieldrecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.Editfieldrecords( table)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.Editfieldrecords( table) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);
            fieldInterface.Verify(t => t.Editfieldrecords( table), Times.Once());
        }





    }

}
























