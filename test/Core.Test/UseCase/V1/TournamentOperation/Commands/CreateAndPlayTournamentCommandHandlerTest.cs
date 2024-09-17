using Core.Common.Interfaces;
using Core.Domain.Dto;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.UseCase.V1.TournamentOperations.Commands.Create;
using System.Net;

namespace Core.Test.UseCase.V1.TournamentOperation.Commands
{
    public class CreateAndPlayTournamentCommandHandlerTest
    {
        private readonly Mock<IRepositoryEF> _repositoryMock;
        private readonly Mock<ITournamentService> _tournamentServiceMock;
        private readonly CreateAndPlayTournamentCommandHandler _handler;

        public CreateAndPlayTournamentCommandHandlerTest()
        {
            _repositoryMock = new Mock<IRepositoryEF>();
            _tournamentServiceMock = new Mock<ITournamentService>();
            _handler = new CreateAndPlayTournamentCommandHandler(_repositoryMock.Object, _tournamentServiceMock.Object);
        }

        [Fact]
        public async Task Handle_AllPlayersFound_ShouldPlayTournament()
        {
            // Arrange
            var command = new CreateAndPlayTournamentCommand
            {
                Gender = 1,
                PlayersId = new List<int> { 1, 2, 3 }
            };

            var players = new List<Player>
        {
            new Player { Id = 1 },
            new Player { Id = 2 },
            new Player { Id = 3 }
        };

            _repositoryMock.Setup(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender))
                .ReturnsAsync(players);

            _tournamentServiceMock.Setup(service => service.PlayTournament(players, EGender.Male))
                .ReturnsAsync(new TournamentResult());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
            Assert.Empty(result.Notifications);

            _repositoryMock.Verify(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender), Times.Once);
            _tournamentServiceMock.Verify(service => service.PlayTournament(players, EGender.Male), Times.Once);
        }

        [Fact]
        public async Task Handle_SomePlayersNotFound_ShouldReturnBadRequestAndNotifications()
        {
            // Arrange
            var command = new CreateAndPlayTournamentCommand
            {
                Gender = 1,
                PlayersId = new List<int> { 1, 2, 3 }
            };

            var players = new List<Player>
        {
            new Player { Id = 1 },
            new Player { Id = 2 }
        };

            _repositoryMock.Setup(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender))
                .ReturnsAsync(players);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Notifications);
            Assert.Equal(1, result.Notifications.Count); // Only one missing player
            Assert.Contains(result.Notifications, n => n.Message.Contains("3"));

            _repositoryMock.Verify(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender), Times.Once);
            _tournamentServiceMock.Verify(service => service.PlayTournament(It.IsAny<List<Player>>(), It.IsAny<EGender>()), Times.Never);
        }

        [Fact]
        public async Task Handle_NoPlayersFound_ShouldReturnBadRequestAndAllNotifications()
        {
            // Arrange
            var command = new CreateAndPlayTournamentCommand
            {
                Gender = 1,
                PlayersId = new List<int> { 1, 2, 3 }
            };

            var players = new List<Player>(); // No players found

            _repositoryMock.Setup(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender))
                .ReturnsAsync(players);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.Null(result.Content);
            Assert.NotEmpty(result.Notifications);
            Assert.Equal(3, result.Notifications.Count); // All players are missing
            Assert.Contains(result.Notifications, n => n.Message.Contains("1"));
            Assert.Contains(result.Notifications, n => n.Message.Contains("2"));
            Assert.Contains(result.Notifications, n => n.Message.Contains("3"));

            _repositoryMock.Verify(repo => repo.GetPlayersByIdsAsync(command.PlayersId, command.Gender), Times.Once);
            _tournamentServiceMock.Verify(service => service.PlayTournament(It.IsAny<List<Player>>(), It.IsAny<EGender>()), Times.Never);
        }

    }
}
