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
        public async Task addRecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var fields = fixture.Create<Fielddto>();
            var returnData = fixture.Create<Fielddto>();
            fieldInterface.Setup(t => t.addRecords(fields)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.addRecords(fields) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var addedData = result.Value as Fielddto;
            addedData.Should().NotBeNull();

            addedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.addRecords(fields), Times.Once());
        }


        [Fact]
        public void addRecords_ShouldReturnBadRequest_WhenFieldObjectIsNull()
        {
            //Arrange
            Fielddto field = null;
            fieldInterface.Setup(t => t.addRecords(field)).ReturnsAsync((Fielddto)null);

            //Act
            var result = fieldController.addRecords(field);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            fieldInterface.Verify(t => t.addRecords(field), Times.Never());
        }


        [Fact]
        public async Task addRecords_ShouldReturnNotFound_WhenAddFailed()
        {
            var fields = fixture.Create<Fielddto>();
            fieldInterface.Setup(c => c.addRecords(fields)).ReturnsAsync((Fielddto)null);

            // Act
            var result = await fieldController.addRecords(fields);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>(); 

            fieldInterface.Verify(t => t.addRecords(fields), Times.Once());
        }




        [Fact]
        public async Task addRecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var field = fixture.Create<Fielddto>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.addRecords(field)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.addRecords(field) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.addRecords(field), Times.Once());
        }






        //View Field Table Record


        [Fact]
        public async Task viewRecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Fielddto>>();
            fieldInterface.Setup(c => c.viewRecords(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.viewRecords(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Fielddto>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.viewRecords(id), Times.Once());
        }




        [Fact]
        public async Task viewRecords_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            fieldInterface.Setup(c => c.viewRecords(id)).Returns(Task.FromResult<IEnumerable<Fielddto>>(null));

            // Act
            var result = await fieldController.viewRecords(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("fields not found");

            fieldInterface.Verify(t => t.viewRecords(id), Times.Once());
        }



        [Fact]
        public async Task viewRecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.viewRecords(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.viewRecords(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.viewRecords(id), Times.Once());
        }



        //GetFormNames

        [Fact]
        public async Task getForm_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.getForm()).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.getForm() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.getForm(), Times.Once());
        }




       [Fact]
        public async Task getForm_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            
            fieldInterface.Setup(c => c.getForm()).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.getForm() as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

           

            fieldInterface.Verify(t => t.getForm(), Times.Once());
        }



         [Fact]
        public async Task getForm_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.getForm()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.getForm() as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.getForm(), Times.Once());
        }


        //GetFormsinview

        [Fact]
        public async Task getFormsView_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.getFormsView(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.getFormsView(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

           
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.getFormsView(id), Times.Once());
        }



       [Fact]
        public async Task getFormsView_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.getFormsView(id)).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.getFormsView(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("forms not found");

            fieldInterface.Verify(t => t.getFormsView(id), Times.Once());
        }


        [Fact]
       public async Task getFormsView_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
           var exceptionMessage = "ExpectedExceptionMessage";
           fieldInterface.Setup(c => c.getFormsView(id)).Throws(new Exception(exceptionMessage));

           // Act
           var result = await fieldController.getFormsView(id) as BadRequestObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<BadRequestObjectResult>();

           var actualExceptionMessage = result.Value as string;
           actualExceptionMessage.Should().NotBeNull();
           actualExceptionMessage.Should().Be(exceptionMessage);

           fieldInterface.Verify(t => t.getFormsView(id), Times.Once());
       }



        //GetDomainTableNames

         [Fact]
         public async Task getTable_ShouldReturnOk_WhenSuccess()
         {
             // Arrange

             var expectedfields = fixture.Create<IEnumerable<Aotable>>();
             fieldInterface.Setup(c => c.getTable()).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.getTable() as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<Aotable>;
             actualfields.Should().NotBeNull();


             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.getTable(), Times.Once());
         }




         [Fact]
         public async Task getTable_ShouldReturnNotFound_WhenDataNotFound()
         {
             // Arrange

             fieldInterface.Setup(c => c.getTable()).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

             // Act
             var result = await fieldController.getTable() as NotFoundResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<NotFoundResult>();



             fieldInterface.Verify(t => t.getTable(), Times.Once());
         }



         [Fact]
         public async Task getTable_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
         {
             // Arrange

             var exceptionMessage = "ExpectedExceptionMessage";
             fieldInterface.Setup(c => c.getTable()).Throws(new Exception(exceptionMessage));

             // Act
             var result = await fieldController.getTable() as BadRequestObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<BadRequestObjectResult>();

             var actualExceptionMessage = result.Value as string;
             actualExceptionMessage.Should().NotBeNull();
             actualExceptionMessage.Should().Be(exceptionMessage);

             fieldInterface.Verify(t => t.getTable(), Times.Once());
         }







        //GetDomaininview
        [Fact]
        public async Task getDomainView_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Aotable>>();
            fieldInterface.Setup(c => c.getDomainView(id)).ReturnsAsync(expectedfields);
           
            // Act
            var result = await fieldController.getDomainView(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Aotable>;
            actualfields.Should().NotBeNull();

            // Check if the actualColumns and expectedColumns are the same
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.getDomainView(id), Times.Once());
        }



        [Fact]
        public async Task getDomainView_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.getDomainView(id)).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

            // Act
            var result = await fieldController.getDomainView(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("tables not found");

            fieldInterface.Verify(t => t.getDomainView(id), Times.Once());
        }


        [Fact]
        public async Task getDomainView_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.getDomainView(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.getDomainView(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.getDomainView(id), Times.Once());
        }

















        //Get Domaintable Fields by passing  tableid




        [Fact]
         public async Task getDomain_ShouldReturnOk_WhenSuccess()
         {
             // Arrange
             Guid id = fixture.Create<Guid>();
             var expectedfields = fixture.Create<IEnumerable<DomainTable>>();
             fieldInterface.Setup(c => c.getDomain(id)).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.getDomain(id) as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<DomainTable>;
             actualfields.Should().NotBeNull();

             
             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.getDomain(id), Times.Once());
         }



        [Fact]
       public async Task getDomain_ShouldReturnNotFound_WhenDataNotFound()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
         
           fieldInterface.Setup(c => c.getDomain(id)).Returns(Task.FromResult<IEnumerable<DomainTable>>(null));

           // Act
           var result = await fieldController.getDomain(id) as NotFoundObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<NotFoundObjectResult>();

           var errorMessage = result.Value as string;
           errorMessage.Should().NotBeNull();
           errorMessage.Should().Be("records not found");

           fieldInterface.Verify(t => t.getDomain(id), Times.Once());
       }





        [Fact]
        public async Task getDomain_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.getDomain(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.getDomain(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.getDomain(id), Times.Once());
        }


        //For fetching Aocolumn name

        [Fact]
        public async Task getColumns_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var search = fixture.Create<string>();
            var expectedTables = fixture.Create<IEnumerable<Aocolumn>>();
            fieldInterface.Setup(c => c.getColumns(search)).ReturnsAsync(expectedTables);

            // Act
            var result = await fieldController.getColumns(search) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualTables = result.Value as IEnumerable<Aocolumn>;
            actualTables.Should().NotBeNull();

            
            actualTables.Should().BeEquivalentTo(expectedTables);

            fieldInterface.Verify(t => t.getColumns(search), Times.Once());
        }



        [Fact]
        public async Task getColumns_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            var word = fixture.Create<string>();
           
            fieldInterface.Setup(c => c.getColumns(word)).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            // Act
            var result = await fieldController.getColumns(word) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Columns not found");

            fieldInterface.Verify(t => t.getColumns(word), Times.Once());
        }



        [Fact]
        public async Task getColumns_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var name = fixture.Create<string>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.getColumns(name)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.getColumns(name) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.getColumns(name), Times.Once());
        }








        [Fact]
        public async Task editRecords_ShouldReturnOk_WhenEditSuccess()
        {
            // Arrange
            var table = fixture.Create<Angularassessment.Models.Field>();
            var returnData = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.editRecords( table)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.editRecords(table) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var editedData = result.Value as Angularassessment.Models.Field;
            editedData.Should().NotBeNull();

            editedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.editRecords(table), Times.Once());
        }



        [Fact]
        public void editRecords_ShouldReturnBadRequestResult_WhenEditFailed()
        {
            
            var Field = new Angularassessment.Models.Field();
            Field = null;
            fieldInterface.Setup(t => t.editRecords(Field)).ReturnsAsync((Angularassessment.Models.Field)null);

            // Act
            var result = fieldController.editRecords(Field);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();

            fieldInterface.Verify(t => t.editRecords(Field), Times.Never()); 
        }



        



        [Fact]
        public async Task editRecords_ShouldReturnNotFoundObjectResult_WhenDataNotFound()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.editRecords( table)).Returns(Task.FromResult<Angularassessment.Models.Field>(null));

            // Act
            var result = await fieldController.editRecords( table) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Record cannot be editted!");

            fieldInterface.Verify(t => t.editRecords( table), Times.Once());
        }



        [Fact]
        public async Task editRecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.editRecords( table)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.editRecords( table) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);
            fieldInterface.Verify(t => t.editRecords( table), Times.Once());
        }





    }

}
























