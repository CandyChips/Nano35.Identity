using System;
using System.Collections.Generic;
using System.Linq;
using Nano35.Contracts.Identity.Artifacts;
using System.Threading;
using System.Threading.Tasks;
using LanguageExt;
using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Nano35.Contracts.Identity.Models;
using Nano35.Identity.Processor.Configurations;
using Nano35.Identity.Processor.Models;
using Nano35.Identity.Processor.Services.Contexts;
using Nano35.Identity.Processor.Services.MappingProfiles;
using Nano35.Identity.Processor.UseCase;
using Nano35.Identity.Processor.UseCase.CreateUser;
using Nano35.Identity.Processor.UseCase.GetAllUsers;
using Nano35.Identity.Processor.UseCase.GetUserById;
using Nano35.Identity.Processor.UseCase.Register;
using Nano35.Identity.Processor.UseCase.UpdateEmail;
using Nano35.Identity.Processor.UseCase.UpdateName;
using Nano35.Identity.Processor.UseCase.UpdatePassword;
using Nano35.Identity.Processor.UseCase.UpdatePhone;
using Xunit;
using Xunit.Abstractions;

namespace Nano35.Identity.Tests
{

    public class ApplicationContextWrapper : IDisposable
    {
        public UserManager<User> UserManager;
        public IServiceCollection ServiceCollection = new ServiceCollection();
        public ApplicationContext Context { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            new Configurator(services, new IdentityConfiguration()).Configure();
            new Configurator(services, new AutoMapperConfiguration()).Configure();
            new Configurator(services,
                    new EntityFrameworkConfiguration("192.168.100.120", "Nano35.Identity.Test.DB", "sa", "Cerber666"))
                .Configure();
        }

        public ApplicationContextWrapper()
        {
            ConfigureServices(ServiceCollection);
            ServiceCollection.AddLogging();
            UserManager = ServiceCollection.BuildServiceProvider().GetService<UserManager<User>>();
            Context = ServiceCollection.BuildServiceProvider().GetService<ApplicationContext>();
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
            Context.Dispose();
        }
    }

    [Collection("Identity")]
    public class CreateTests
    {
        private ApplicationContextWrapper _db = new ApplicationContextWrapper();

        [Fact]
        public async Task CreateUser()
        {

            // Arrange
            Guid testId = Guid.NewGuid();

            var input = new CreateUserRequestContract()
            {
                //input values
                NewId = testId,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };

            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<ICreateUserRequestContract>>();

            //Act
            var result = await
                new LoggedRailPipeNode<ICreateUserRequestContract,ICreateUserSuccessResultContract>(logger,
                new CreateUserUseCase(_db.UserManager)).Ask(input, context.CancellationToken);

            var res = _db.Context.Users.Select(a =>
                new User()
                {
                    PasswordHash = null,
                    SecurityStamp = null,
                    ConcurrencyStamp = null,
                    Id = a.Id,
                    Name = a.Name,
                    Deleted = a.Deleted,
                    UserName = a.UserName,
                    NormalizedUserName = a.NormalizedUserName,
                    Email = a.Email,
                    NormalizedEmail = a.NormalizedEmail,
                    EmailConfirmed = a.EmailConfirmed,
                    PhoneNumber = a.PhoneNumber,
                    PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                    TwoFactorEnabled = a.TwoFactorEnabled,
                    LockoutEnd = a.LockoutEnd,
                    LockoutEnabled = a.LockoutEnabled,
                    AccessFailedCount = a.AccessFailedCount
                }).ToList();

            _db.Dispose();

            Assert.Collection(res, item => Assert.Contains(testId.ToString(), item.Id));
        }

        [Fact]
        public async Task Register()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var input = new RegisterRequestContract()
            {NewUserId = testId,
             Phone = "88005553535",
             Email = "Sasha@ya.ru",
             Password = "qwerty",
             PasswordConfirm = "qwerty"};

            var services = new ServiceCollection();
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingInMemory(cfg =>
                {

                    cfg.ReceiveEndpoint("IRegisterRequestContract", e =>
                    {
                        e.Consumer<RegisterConsumer>(provider);
                    });
                }));
                x.AddConsumer<RegisterConsumer>();
            });
            
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IRegisterRequestContract>>(o => o.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IRegisterRequestContract>>();

            //Act
            var result = await new LoggedPipeNode<IRegisterRequestContract, IRegisterResultContract>(logger, 
                new RegisterUseCase(_db.UserManager)).Ask(input, context.CancellationToken);

            var res = _db.Context.Users.Select(a =>
                new User()
                {
                    PasswordHash = null,
                    SecurityStamp = null,
                    ConcurrencyStamp = null,
                    Id = a.Id,
                    Name = a.Name,
                    Deleted = a.Deleted,
                    UserName = a.Name,
                    NormalizedUserName = a.NormalizedUserName,
                    Email = a.Email,
                    NormalizedEmail = a.NormalizedEmail,
                    EmailConfirmed = a.EmailConfirmed,
                    PhoneNumber = a.PhoneNumber,
                    PhoneNumberConfirmed = a.PhoneNumberConfirmed,
                    TwoFactorEnabled = a.TwoFactorEnabled,
                    LockoutEnd = a.LockoutEnd,
                    LockoutEnabled = a.LockoutEnabled,
                    AccessFailedCount = a.AccessFailedCount
                }).ToList();

            _db.Dispose();

            Assert.Collection(res, item => Assert.Contains(testId.ToString(), item.Id));
        }

    }

    [Collection("Identity")]
    public class GetTests
    {
        private ApplicationContextWrapper _db = new ApplicationContextWrapper();

        [Fact]
        public async Task GetAllUsers()
        {
            // Arrange
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IGetAllUsersRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IGetAllUsersRequestContract>>();

            var Id = new List<Guid>();
            var testId1 = Guid.NewGuid();
            Id.Add(testId1);
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId1,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            var testId2 = Guid.NewGuid();
            Id.Add(testId2);
            var input2 = new RegisterRequestContract()
            {
                //input values
                NewUserId = testId2,
                Phone = "88005553535",
                Email = "Sasha@ya.ru",
                Password = "qwerty",
                PasswordConfirm = "qwerty"
            };
            var registerContext = Mock.Of<ConsumeContext<IRegisterRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new RegisterUseCase(_db.UserManager).Ask(input2, registerContext.CancellationToken);


            var message = new GetAllUsersRequestContract() { };
            var result = await
                new LoggedPipeNode<IGetAllUsersRequestContract, IGetAllUsersResultContract>(logger,
                new GetAllUsersUseCase(_db.UserManager)).Ask(message, context.CancellationToken);
            
            _db.Dispose();

            switch (result)
            {
                case IGetAllUsersSuccessResultContract res:
                    var r = res.Data;
                    Assert.Equal(2,r.Count());
                    Assert.Collection(r, item =>Assert.Contains(item.Id, Id),
                                    item => Assert.Contains(item.Id, Id));
                    break;
                case IGetAllUsersErrorResultContract:
                    throw new Exception("");
            }
        }

        [Fact]
        public async Task GetUserById()
        {
            // Arrange
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IGetUserByIdRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IGetUserByIdRequestContract>>();


            var testId1 = Guid.NewGuid();
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId1,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            var testId2 = Guid.NewGuid();
            var input2 = new RegisterRequestContract()
            {
                //input values
                NewUserId = testId2,
                Phone = "88005553535",
                Email = "Sasha@ya.ru",
                Password = "qwerty",
                PasswordConfirm = "qwerty"
            };
            var registerContext = Mock.Of<ConsumeContext<IRegisterRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new RegisterUseCase(_db.UserManager).Ask(input2, registerContext.CancellationToken);

            var message = new GetUserByIdRequestContract()
            {
                UserId = testId2
            };

            var result = await
                new LoggedPipeNode<IGetUserByIdRequestContract, IGetUserByIdResultContract>(logger,  
                new GetUserByIdUseCase(_db.Context)).Ask(message, context.CancellationToken);

            _db.Dispose();
            
            switch (result)
            {
                case IGetUserByIdSuccessResultContract res:
                    Assert.Equal(message.UserId,res.Data.Id);
                    break;
                case IGetUserByIdErrorResultContract:
                    throw new Exception("Не найдено");
            }
        }

        
    }
    
    [Collection("Identity")]
    public class UpdateTests
    {
        private ApplicationContextWrapper _db = new ApplicationContextWrapper();
    
        [Fact]
        public async Task UpdateEmail()
        {
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IUpdateEmailRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IUpdateEmailRequestContract>>();

            var testId = Guid.NewGuid();
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            
            var message = new UpdateEmailRequestContract()
            {
                UserId = testId,
                Email = "Alex228@ya.ru"
            };
            
            var result = await
                new LoggedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(logger,  
                    new TransactedPipeNode<IUpdateEmailRequestContract, IUpdateEmailResultContract>(_db.Context, 
                        new UpdateEmailUseCase(_db.UserManager))).Ask(message, context.CancellationToken);

            var res = _db.Context.Users.ToList();
            
            _db.Dispose();

            Assert.Equal(message.Email, res[0].Email);
        }
    
        [Fact]
        public async Task UpdateName()
        {
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IUpdateNameRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IUpdateNameRequestContract>>();

            var testId = Guid.NewGuid();
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            
            var message = new UpdateNameRequestContract()
            {
                UserId = testId,
                Name = "Sasha"
            };
            
            var result = await
                new LoggedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(logger, 
                    new TransactedPipeNode<IUpdateNameRequestContract, IUpdateNameResultContract>(_db.Context,
                        new UpdateNameUseCase(_db.UserManager))).Ask(message, context.CancellationToken);

            var res = _db.Context.Users.ToList();
            
            _db.Dispose();

            Assert.Equal(message.Name, res[0].Name);
        }
    
        [Fact]
        public async Task UpdatePassword()
        {
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IUpdatePasswordRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IUpdatePasswordRequestContract>>();

            var testId = Guid.NewGuid();
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            
            var message = new UpdatePasswordRequestContract()
            {
                UserId = testId,
                Password = "qwerty228"
            };
            
            var notExpected = _db.Context.Users.FirstOrDefault(a=> a.Id == testId.ToString())?.PasswordHash;

            var result = await 
                new LoggedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(logger, 
                new TransactedPipeNode<IUpdatePasswordRequestContract, IUpdatePasswordResultContract>(_db.Context,
                    new UpdatePasswordUseCase(_db.UserManager))).Ask(message, context.CancellationToken);

            await _db.Context.Entry(_db.Context.Users.FirstOrDefault()!).ReloadAsync();
            var res = _db.Context.Users.FirstOrDefault(a=> a.Id == testId.ToString())?.PasswordHash;
            
            _db.Dispose();
            
            Assert.NotEqual(notExpected, res);
        }
    
        [Fact]
        public async Task UpdatePhone()
        {
            var source = new CancellationTokenSource();
            var cancellationToken = source.Token;
            var context = Mock.Of<ConsumeContext<IUpdatePhoneRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            var logger = Mock.Of<ILogger<IUpdatePhoneRequestContract>>();

            var testId = Guid.NewGuid();
            var input1 = new CreateUserRequestContract()
            {
                //input values
                NewId = testId,
                Name = "Alex",
                Comment = "comment",
                InstanceId = Guid.Parse("86F7E707-3E34-09B7-A27D-D2016A8B8436"),
                CurrentUnitId = Guid.Parse("69329E05-C72A-E3C6-4C8A-232348B480FC"),
                Phone = "89265123514",
                Email = "Alex@ya.ru",
                Password = "qwerty"
            };
            var createUserContext = Mock.Of<ConsumeContext<ICreateUserRequestContract>>(_ =>
                _.CancellationToken == cancellationToken);
            await new CreateUserUseCase(_db.UserManager).Ask(input1, createUserContext.CancellationToken);

            
            var message = new UpdatePhoneRequestContract()
            {
                UserId = testId,
                Phone = "88005553535"
            };
            
            var result = await
                new LoggedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(logger,  
                    new TransactedPipeNode<IUpdatePhoneRequestContract, IUpdatePhoneResultContract>(_db.Context,
                            new UpdatePhoneUseCase(_db.UserManager))).Ask(message, context.CancellationToken);

            var res = _db.Context.Users.ToList();
            
            _db.Dispose();

            Assert.Equal(message.Phone, res[0].PhoneNumber);
        }
    }
}

