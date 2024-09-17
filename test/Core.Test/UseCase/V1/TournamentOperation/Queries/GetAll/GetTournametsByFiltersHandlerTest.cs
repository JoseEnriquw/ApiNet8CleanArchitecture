using AutoFixture;
using Core.Common.Interfaces;
using Core.Common.Models;
using Core.Domain.Dto;
using Core.Domain.Enums;
using Core.UseCase.V1.TournamentOperations.Queries.GetAll;
using System.Net;

namespace Core.Test.UseCase.V1.TournamentOperation.Queries.GetAll
{
    public class GetTournametsByFiltersHandlerTests
    {
        private readonly Mock<IRepositoryEF> _repositoryMock;
        private readonly GetTournametsByFiltersHandler _handler;

        public GetTournametsByFiltersHandlerTests()
        {
            _repositoryMock = new Mock<IRepositoryEF>();
            _handler = new GetTournametsByFiltersHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnTournamentsWithOKStatusCode_WhenTournamentsFound()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = (int)EGender.Male,
                StartDate = new DateTime(2023, 1, 1),
                Page = 1,
                Size = 10
            };

            var tournaments = new Fixture().CreateMany<TournamentDto>(2).ToList();
            var tournamentsPaginated = new PaginatedList<TournamentDto>(tournaments, 2, 1, 10);

            _repositoryMock.Setup(repo => repo.GetTorneosByFiltersAsync(request))
                .ReturnsAsync(tournamentsPaginated);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
            Assert.Equal(2, result.Content.Items.Count);
            Assert.Equal(tournamentsPaginated, result.Content);

            _repositoryMock.Verify(repo => repo.GetTorneosByFiltersAsync(request), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyTournamentsWithOKStatusCode_WhenNoTournamentsFound()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = (int)EGender.Female,
                StartDate = new DateTime(2023, 1, 1),
                Page = 1,
                Size = 10
            };

            var tournaments = new PaginatedList<TournamentDto>([], 0, 1, 10); 

            _repositoryMock.Setup(repo => repo.GetTorneosByFiltersAsync(request))
                .ReturnsAsync(tournaments);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
            Assert.Empty(result.Content.Items); 

            _repositoryMock.Verify(repo => repo.GetTorneosByFiltersAsync(request), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldReturnTournamentsWithPagination_WhenFiltersAreApplied()
        {
            // Arrange
            var request = new GetTournametsByFilters
            {
                Gender = null,
                StartDate = null, 
                Page = 2,
                Size = 5
            };
            var tournaments = new Fixture().CreateMany<TournamentDto>(2).ToList();

            var tournamentsPaginated = new PaginatedList<TournamentDto>(tournaments, 10, 2, 5); 

            _repositoryMock.Setup(repo => repo.GetTorneosByFiltersAsync(request))
                .ReturnsAsync(tournamentsPaginated);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Content);
            Assert.Equal(2, result.Content.Items.Count); 
            Assert.Equal(10, result.Content.TotalCount); 

            _repositoryMock.Verify(repo => repo.GetTorneosByFiltersAsync(request), Times.Once);
        }
    }
}
