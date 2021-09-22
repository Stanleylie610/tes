using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using SchoolLogin.Controllers;
using SchoolLogin.Data;
using SchoolLogin.Dtos;
using SchoolLogin.Models;
using SchoolLogin.Profiles;
using Xunit;
using Xunit.Abstractions;

namespace SchoolLogin.Tests
{
    public class DBContextTest
    {
        private readonly ITestOutputHelper _outputHelper;

        //ctor for output
        public DBContextTest(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        // [Fact(Skip="Just for testing")]
        [Fact]
        public void EmptyTest()
        {
            _outputHelper.WriteLine("test");
        }

        //only for .net 5.0
        // [Fact]
        // public void ShouldBeAbleToAddInMemoryDb()
        // {
        //     var builder = new DbContextOptionsBuilder<SchoolLoginContext>()
        //                     .EnableSensitiveDataLogging()
        //                     .UseInMemoryDatabase(Guid.NewGuid().ToString());

        //     using (var context = new SchoolLoginContext(builder.Options))
        //     {
        //         context.studentlist.Add(new StudentRoster{StudentName ="Test",email = "test@gmail.com",password=12345});
        //         context.SaveChanges();
        //         Assert.Equal(1,context.studentlist.Count(p=> p.StudentName=="Test"));
        //     }
        // }

        [Theory]
        [InlineData(1, "Stanley")]
        [InlineData(1, "Dandde")]
        [InlineData(1, "bot1")]
        public void FindStudentOnDb(int counta, string nameofstudent)
        {
            var builder = new DbContextOptionsBuilder<SchoolLoginContext>()
                            .EnableSensitiveDataLogging()
                            // .UseSqlite("DataSource=:memory:");
                            .UseSqlServer(connectionString : "server=localhost;Initial Catalog=SchoolDb;User Id = testingapi ; Password = password ;");

            using (var context = new SchoolLoginContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                Assert.Equal(counta,context.studentlist.Count(p=> p.StudentName == nameofstudent)); //(brpabnyk,studentname)
            }
        }

        [Fact(Skip="Create")]
        // [Fact]
        public void CreateStudentOnDb()
        {
            var builder = new DbContextOptionsBuilder<SchoolLoginContext>()
                            .EnableSensitiveDataLogging()
                            // .UseSqlite("DataSource=:memory:");
                            .UseSqlServer(connectionString : "server=localhost;Initial Catalog=SchoolDb;User Id = testingapi ; Password = password ;");

            using (var context = new SchoolLoginContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                string inputstudentname = "bot1";
                string inputemail       = inputstudentname+"@gmail.com";

                context.studentlist.Add(new StudentRoster{StudentName =inputstudentname,email = inputemail,password=456789});
                context.SaveChanges();
                Assert.Equal(1,context.studentlist.Count(p=> p.StudentName == inputstudentname)); //(brpabnyk,studentname)
            }
        }

        [Fact(Skip="Delete")]
        // [Fact]
        public void DeleteStudentOnDb()
        {
            var builder = new DbContextOptionsBuilder<SchoolLoginContext>()
                            .EnableSensitiveDataLogging()
                            // .UseSqlite("DataSource=:memory:");
                            .UseSqlServer(connectionString : "server=localhost;Initial Catalog=SchoolDb;User Id = testingapi ; Password = password ;");

            using (var context = new SchoolLoginContext(builder.Options))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();

                // int tempid = context.studentlist.Find(StudentName == "Bot2");
                int tempid = 10000;
                var tempdelete = context.studentlist.FirstOrDefault(p=>p.ID == tempid );
                context.studentlist.Remove(tempdelete);
                context.SaveChanges();
                Assert.Equal(0,context.studentlist.Count(p=> p.ID == tempid)); //(brpabnyk,studentname)
            }
        }
    }


    public class ControllerTest
    {
        // private readonly Mock<ISchoolloginRepo> repositoryStub = new(); for .net 5.0
        private readonly Mock<ISchoolloginRepo> repositoryStub = new Mock<ISchoolloginRepo>();
        private readonly Mock<IMapper> mapperstub = new Mock<IMapper>();
        private readonly Random rand = new Random();
        [Fact]
        public async Task GetID_Null_ReturnNotFound()
        {
            //Arrange
            // var repositoryStub = new Mock<ISchoolloginRepo>();
            repositoryStub.Setup(repo => repo.GetCommandById(It.IsAny<int>())).ReturnsAsync((StudentRoster)null);

            // var mapperstub = new Mock<IMapper>();

            var controller = new StudentRosterController(repositoryStub.Object,mapperstub.Object);
            //Act
            var result = await controller.GetCommandById(rand.Next(10000));

            //Assert
            // Assert.IsType<NotFoundResult>(result.Result);
            result.Result.Should().BeOfType<NotFoundResult>();      //use FluentAssertion Nuget
        }

       [Fact]
        public async Task GetID_Found_OK()
        {
            //Arrange
            var expectedItem = CreateRandomItem();
            
            repositoryStub.Setup(repo => repo.GetCommandById(It.IsAny<int>())).ReturnsAsync(expectedItem);
            //mockmapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchoolLoginProfiles()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();
            var controller = new StudentRosterController(repositoryStub.Object,mapper);
            //Act
            var result = await controller.GetCommandById(rand.Next(1000));
            //Assert
            
            result.Value.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<SchoolLoginReadDto>());
            // Assert.IsType<OkObjectResult>(result.Result);

            // Assert.IsType<OkResult>(result.Value);
            // var dto = (result as ActionResult<SchoolLoginReadDto>).Value;
            // Assert.Equal(expectedItem.StudentName, dto.StudentName);
            // Assert.Equal(expectedItem.email, dto.email);
            // Assert.Equal(expectedItem.password, dto.password);
        }

       [Fact]
        public async Task GetAll_Found_OK()
        {
            //Arrange
            var expectedItem = new[]{CreateRandomItem(),CreateRandomItem(),CreateRandomItem()};
            
            repositoryStub.Setup(repo => repo.GetAllCommands()).ReturnsAsync(expectedItem);

            //auto mapper configuration
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchoolLoginProfiles()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var controller = new StudentRosterController(repositoryStub.Object, mapper);
            //Act
            var result = await controller.GetAllCommands();
            
            //Assert
            result.Should().BeEquivalentTo(expectedItem, options => options.ComparingByMembers<SchoolLoginReadDto>());
        }

        [Fact]
        public async Task Create_Success_POST()
        {
            //Arrange
            var itemtocreate = new SchoolLoginCreateDto()
            {
                StudentName = Guid.NewGuid().ToString(),
                email = Guid.NewGuid().ToString(),          //if empty use Null
                password = rand.Next(10000)                 //if empty use 0
            };

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchoolLoginProfiles()); //your automapperprofile 
            });
            var mapper = mockMapper.CreateMapper();

            var controller = new StudentRosterController(repositoryStub.Object,mapper);
            //Act
            var result = await controller.CreateCommand(itemtocreate);
            //Assert
            var CreatedItem =(result.Result as CreatedAtActionResult).Value as SchoolLoginCreateDto;
            result.Should().BeEquivalentTo(CreatedItem, options => options.ComparingByMembers<SchoolLoginCreateDto>().ExcludingMissingMembers());
            // CreatedItem.StudentName.Should().NotBeEmpty();
            CreatedItem.StudentName.Should().NotBe(null);
            CreatedItem.email.Should().NotBe(null);
            CreatedItem.password.Should().NotBe(null);
        }

        [Fact]
        public async Task Update_Success_PUT()
        {
            //arrange
            var existingItem = CreateRandomItem();
            
            repositoryStub.Setup(repo => repo.GetCommandById(It.IsAny<int>())).ReturnsAsync(existingItem);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchoolLoginProfiles()); //your automapperprofile 
            });

            var studentid = existingItem.ID;
            var itemToUpdate = new SchoolLoginUpdateDto()
            {
                StudentName = Guid.NewGuid().ToString(),
                email = Guid.NewGuid().ToString(),
                password = rand.Next(1000) + 3
            };

            var mapper = mockMapper.CreateMapper();            
            var controller = new StudentRosterController(repositoryStub.Object,mapper);
            //act
            var result = await controller.UpdateCommand(studentid,itemToUpdate);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }


        [Fact]
        public async Task Remove_Success_Delete()
        {
            //arrange
            var existingItem = CreateRandomItem();
            
            repositoryStub.Setup(repo => repo.GetCommandById(It.IsAny<int>())).ReturnsAsync(existingItem);
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SchoolLoginProfiles()); //your automapperprofile 
            });

            var mapper = mockMapper.CreateMapper();            
            var controller = new StudentRosterController(repositoryStub.Object,mapper);
            //act
            var result = await controller.DeleteCommand(existingItem.ID);
            //assert
            result.Should().BeOfType<NoContentResult>();
        }
        private StudentRoster CreateRandomItem()
        {
            var studentlist = new StudentRoster
            {
                ID =rand.Next(1000),StudentName =Guid.NewGuid().ToString(),email = Guid.NewGuid().ToString(),password=rand.Next(10000)
            };
            return studentlist;
            //only for c#9
            // return new()
            // {
            //     ID = rand.Next(1000),
            //     StudentName = Guid.NewGuid().ToString(),
            //     email = Guid.NewGuid().ToString(),
            //     password = rand.Next(10000)
            // };
        }
    }
}
