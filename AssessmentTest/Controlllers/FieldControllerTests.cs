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
        public async Task Arecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var fields = fixture.Create<Fielddto>();
            var returnData = fixture.Create<Fielddto>();
            fieldInterface.Setup(t => t.Arecords(fields)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.Arecords(fields) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var addedData = result.Value as Fielddto;
            addedData.Should().NotBeNull();

            addedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.Arecords(fields), Times.Once());
        }


        [Fact]
        public void Arecords_ShouldReturnBadRequest_WhenFieldObjectIsNull()
        {
            //Arrange
            Fielddto field = null;
            fieldInterface.Setup(t => t.Arecords(field)).ReturnsAsync((Fielddto)null);

            //Act
            var result = fieldController.Arecords(field);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestResult>();
            fieldInterface.Verify(t => t.Arecords(field), Times.Never());
        }


        [Fact]
        public async Task Arecords_ShouldReturnNotFound_WhenAddFailed()
        {
            var fields = fixture.Create<Fielddto>();
            fieldInterface.Setup(c => c.Arecords(fields)).ReturnsAsync((Fielddto)null);

            // Act
            var result = await fieldController.Arecords(fields);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundResult>(); 

            fieldInterface.Verify(t => t.Arecords(fields), Times.Once());
        }




        [Fact]
        public async Task Arecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var field = fixture.Create<Fielddto>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.Arecords(field)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Arecords(field) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Arecords(field), Times.Once());
        }






        //View Field Table Record


        [Fact]
        public async Task Vrecords_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Fielddto>>();
            fieldInterface.Setup(c => c.Vrecords(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Vrecords(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Fielddto>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Vrecords(id), Times.Once());
        }




        [Fact]
        public async Task Vrecords_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            fieldInterface.Setup(c => c.Vrecords(id)).Returns(Task.FromResult<IEnumerable<Fielddto>>(null));

            // Act
            var result = await fieldController.Vrecords(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("fields not found");

            fieldInterface.Verify(t => t.Vrecords(id), Times.Once());
        }



        [Fact]
        public async Task Vrecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Vrecords(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Vrecords(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Vrecords(id), Times.Once());
        }



        //GetFormNames

        [Fact]
        public async Task Form_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.Form()).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Form() as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

            
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Form(), Times.Once());
        }




       [Fact]
        public async Task Form_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            
            fieldInterface.Setup(c => c.Form()).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.Form() as NotFoundResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();

           

            fieldInterface.Verify(t => t.Form(), Times.Once());
        }



         [Fact]
        public async Task Form_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Form()).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Form() as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Form(), Times.Once());
        }


        //GetFormsinview

        [Fact]
        public async Task Formsincom_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Form>>();
            fieldInterface.Setup(c => c.Formsincom(id)).ReturnsAsync(expectedfields);

            // Act
            var result = await fieldController.Formsincom(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Form>;
            actualfields.Should().NotBeNull();

           
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Formsincom(id), Times.Once());
        }



       [Fact]
        public async Task Formsincom_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.Formsincom(id)).Returns(Task.FromResult<IEnumerable<Form>>(null));

            // Act
            var result = await fieldController.Formsincom(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("forms not found");

            fieldInterface.Verify(t => t.Formsincom(id), Times.Once());
        }


        [Fact]
       public async Task Formsincom_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
           var exceptionMessage = "ExpectedExceptionMessage";
           fieldInterface.Setup(c => c.Formsincom(id)).Throws(new Exception(exceptionMessage));

           // Act
           var result = await fieldController.Formsincom(id) as BadRequestObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<BadRequestObjectResult>();

           var actualExceptionMessage = result.Value as string;
           actualExceptionMessage.Should().NotBeNull();
           actualExceptionMessage.Should().Be(exceptionMessage);

           fieldInterface.Verify(t => t.Formsincom(id), Times.Once());
       }



        //GetDomainTableNames

         [Fact]
         public async Task Table_ShouldReturnOk_WhenSuccess()
         {
             // Arrange

             var expectedfields = fixture.Create<IEnumerable<Aotable>>();
             fieldInterface.Setup(c => c.Table()).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.Table() as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<Aotable>;
             actualfields.Should().NotBeNull();


             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.Table(), Times.Once());
         }




         [Fact]
         public async Task Table_ShouldReturnNotFound_WhenDataNotFound()
         {
             // Arrange

             fieldInterface.Setup(c => c.Table()).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

             // Act
             var result = await fieldController.Table() as NotFoundResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<NotFoundResult>();



             fieldInterface.Verify(t => t.Table(), Times.Once());
         }



         [Fact]
         public async Task Table_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
         {
             // Arrange

             var exceptionMessage = "ExpectedExceptionMessage";
             fieldInterface.Setup(c => c.Table()).Throws(new Exception(exceptionMessage));

             // Act
             var result = await fieldController.Table() as BadRequestObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<BadRequestObjectResult>();

             var actualExceptionMessage = result.Value as string;
             actualExceptionMessage.Should().NotBeNull();
             actualExceptionMessage.Should().Be(exceptionMessage);

             fieldInterface.Verify(t => t.Table(), Times.Once());
         }







        //GetDomaininview
        [Fact]
        public async Task Domaincom_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var expectedfields = fixture.Create<IEnumerable<Aotable>>();
            fieldInterface.Setup(c => c.Domaincom(id)).ReturnsAsync(expectedfields);
           
            // Act
            var result = await fieldController.Domaincom(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualfields = result.Value as IEnumerable<Aotable>;
            actualfields.Should().NotBeNull();

            // Check if the actualColumns and expectedColumns are the same
            actualfields.Should().BeEquivalentTo(expectedfields);

            fieldInterface.Verify(t => t.Domaincom(id), Times.Once());
        }



        [Fact]
        public async Task Domaincom_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();

            fieldInterface.Setup(c => c.Domaincom(id)).Returns(Task.FromResult<IEnumerable<Aotable>>(null));

            // Act
            var result = await fieldController.Domaincom(id) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("tables not found");

            fieldInterface.Verify(t => t.Domaincom(id), Times.Once());
        }


        [Fact]
        public async Task Domaincom_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Domaincom(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Domaincom(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Domaincom(id), Times.Once());
        }

















        //Get Domaintable Fields by passing  tableid




        [Fact]
         public async Task Domain_ShouldReturnOk_WhenSuccess()
         {
             // Arrange
             Guid id = fixture.Create<Guid>();
             var expectedfields = fixture.Create<IEnumerable<DomainTable>>();
             fieldInterface.Setup(c => c.Domain(id)).ReturnsAsync(expectedfields);

             // Act
             var result = await fieldController.Domain(id) as OkObjectResult;

             // Assert
             result.Should().NotBeNull();
             result.Should().BeAssignableTo<OkObjectResult>();

             var actualfields = result.Value as IEnumerable<DomainTable>;
             actualfields.Should().NotBeNull();

             
             actualfields.Should().BeEquivalentTo(expectedfields);

             fieldInterface.Verify(t => t.Domain(id), Times.Once());
         }



        [Fact]
       public async Task Domain_ShouldReturnNotFound_WhenDataNotFound()
       {
           // Arrange
           Guid id = fixture.Create<Guid>();
         
           fieldInterface.Setup(c => c.Domain(id)).Returns(Task.FromResult<IEnumerable<DomainTable>>(null));

           // Act
           var result = await fieldController.Domain(id) as NotFoundObjectResult;

           // Assert
           result.Should().NotBeNull();
           result.Should().BeAssignableTo<NotFoundObjectResult>();

           var errorMessage = result.Value as string;
           errorMessage.Should().NotBeNull();
           errorMessage.Should().Be("records not found");

           fieldInterface.Verify(t => t.Domain(id), Times.Once());
       }





        [Fact]
        public async Task Domain_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            Guid id = fixture.Create<Guid>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Domain(id)).Throws(new Exception(exceptionMessage));

            // Act
            var result = await fieldController.Domain(id) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Domain(id), Times.Once());
        }


        //For fetching Aocolumn name

        [Fact]
        public async Task Found_ShouldReturnOk_WhenSuccess()
        {
            // Arrange
            var search = fixture.Create<string>();
            var expectedTables = fixture.Create<IEnumerable<Aocolumn>>();
            fieldInterface.Setup(c => c.Found(search)).ReturnsAsync(expectedTables);

            // Act
            var result = await fieldController.Found(search) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var actualTables = result.Value as IEnumerable<Aocolumn>;
            actualTables.Should().NotBeNull();

            
            actualTables.Should().BeEquivalentTo(expectedTables);

            fieldInterface.Verify(t => t.Found(search), Times.Once());
        }



        [Fact]
        public async Task Found_ShouldReturnNotFound_WhenDataNotFound()
        {
            // Arrange
            var word = fixture.Create<string>();
           
            fieldInterface.Setup(c => c.Found(word)).Returns(Task.FromResult<IEnumerable<Aocolumn>>(null));

            // Act
            var result = await fieldController.Found(word) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Columns not found");

            fieldInterface.Verify(t => t.Found(word), Times.Once());
        }



        [Fact]
        public async Task Found_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
            var name = fixture.Create<string>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(c => c.Found(name)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.Found(name) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);

            fieldInterface.Verify(t => t.Found(name), Times.Once());
        }








        [Fact]
        public async Task Erecords_ShouldReturnOk_WhenEditSuccess()
        {
            // Arrange
            var table = fixture.Create<Angularassessment.Models.Field>();
            var returnData = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.Erecords( table)).ReturnsAsync(returnData);

            // Act
            var result = await fieldController.Erecords(table) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            var editedData = result.Value as Angularassessment.Models.Field;
            editedData.Should().NotBeNull();

            editedData.Should().BeEquivalentTo(returnData); 

            fieldInterface.Verify(t => t.Erecords(table), Times.Once());
        }



        [Fact]
        public void Erecords_ShouldReturnBadRequestResult_WhenEditFailed()
        {
            
            var Field = new Angularassessment.Models.Field();
            Field = null;
            fieldInterface.Setup(t => t.Erecords(Field)).ReturnsAsync((Angularassessment.Models.Field)null);

            // Act
            var result = fieldController.Erecords(Field);

            // Assert
            result.Should().NotBeNull();
            result.Result.Should().BeAssignableTo<BadRequestResult>();

            fieldInterface.Verify(t => t.Erecords(Field), Times.Never()); 
        }



        



        [Fact]
        public async Task Erecords_ShouldReturnNotFoundObjectResult_WhenDataNotFound()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            fieldInterface.Setup(t => t.Erecords( table)).Returns(Task.FromResult<Angularassessment.Models.Field>(null));

            // Act
            var result = await fieldController.Erecords( table) as NotFoundObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundObjectResult>();

            var errorMessage = result.Value as string;
            errorMessage.Should().NotBeNull();
            errorMessage.Should().Be("Record cannot be editted!");

            fieldInterface.Verify(t => t.Erecords( table), Times.Once());
        }



        [Fact]
        public async Task Erecords_ShouldReturnBadRequestObjectResult_WhenAnExceptionOccurred()
        {
            // Arrange
           
            var table = fixture.Create<Angularassessment.Models.Field>();
            var exceptionMessage = "ExpectedExceptionMessage";
            fieldInterface.Setup(t => t.Erecords( table)).Throws(new Exception(exceptionMessage));
            // Act
            var result = await fieldController.Erecords( table) as BadRequestObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestObjectResult>();

            var actualExceptionMessage = result.Value as string;
            actualExceptionMessage.Should().NotBeNull();
            actualExceptionMessage.Should().Be(exceptionMessage);
            fieldInterface.Verify(t => t.Erecords( table), Times.Once());
        }





    }

}
























