using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace BusinessLogic.Tests
{
    public class UserServiceTest
    {
        private readonly UserService service;
        private readonly Mock<IUserRepository> userRepositoryMog;

        public UserServiceTest()
        {
            var repositoryWrapperMog = new Mock<IRepositoryWrapper>();
            userRepositoryMog = new Mock<IUserRepository>();

            repositoryWrapperMog.Setup(x => x.Пользователи)
                .Returns(userRepositoryMog.Object);

            service = new UserService(repositoryWrapperMog.Object);
        }

        // Добавьте этот метод для предоставления тестовых данных
        public static IEnumerable<object[]> GetIncorrectUsers()
        {
            return new List<object[]>
    {
        new object[] { new Пользователи { Логин = "", Пароль = "pass1", Email = "email1@test.com", Имя = "Name1", Фамилия = "Last1", Телефон = "111" } },
        new object[] { new Пользователи { Логин = "login2", Пароль = "", Email = "email2@test.com", Имя = "Name2", Фамилия = "Last2", Телефон = "222" } },
        new object[] { new Пользователи { Логин = "login3", Пароль = "pass3", Email = "", Имя = "Name3", Фамилия = "Last3", Телефон = "333" } },
        new object[] { new Пользователи { Логин = "login4", Пароль = "pass4", Email = "email4@test.com", Имя = "", Фамилия = "Last4", Телефон = "444" } },
        new object[] { new Пользователи { Логин = "login5", Пароль = "pass5", Email = "email5@test.com", Имя = "Name5", Фамилия = "", Телефон = "555" } },
        new object[] { new Пользователи { Логин = "login6", Пароль = "pass6", Email = "email6@test.com", Имя = "Name6", Фамилия = "Last6", Телефон = "" } }
    };
        }

        [Theory]
        [MemberData(nameof(GetIncorrectUsers))]
        public async Task CreateAsync_NewUser_ShouldNotCreateNewUser(Пользователи user)
        {
            // arrange
            // Тестовые данные теперь приходят из метода GetIncorrectUsers

            // act
            var ex = await Assert.ThrowsAnyAsync<ArgumentException>(() => service.Create(user));

            // assert
            Assert.IsType<ArgumentException>(ex);
            userRepositoryMog.Verify(x => x.Create(It.IsAny<Пользователи>()), Times.Never);
        }
    }
}