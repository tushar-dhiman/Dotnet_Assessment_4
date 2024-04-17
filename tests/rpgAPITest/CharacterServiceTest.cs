using Microsoft.AspNetCore.Authorization.Infrastructure;
using rpgAPI.Model;
using rpgAPI.Service;

namespace rpgAPITest;

public class CharacterServiceTest
{
    [Fact]
    public void GetsAllTheCharacters()
    {
        //Arrange 
        var characterService = new CharacterService();

        //Act
        var result = characterService.GetAllCharacter();

        //Assert 
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
    }

    [Fact]
    public void AddsNewCharacterToList()
    {
        //Arrange 
        var characterService = new CharacterService();
        var newCharacter = new Character
        {
            Id = 2,
            Name = "Stark"
        };

        //Act
        var result = characterService.AddCharacter(newCharacter);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
        Assert.Contains(newCharacter, result.Data);
    }

    [Fact]
    public void GetsCharacterByIdWhenItExist()
    {
        //Arrange
        var characterService = new CharacterService();

        int existingId = 1;

        //Act
        var result = characterService.GetCharacterById(existingId);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
        Assert.Equal(existingId, result.Data.Id);
    }

    [Fact]
    public void GetsCharacterById_ReturnsErrorWhenItDontExist()
    {
        //Arrange
        var characterService = new CharacterService();

        int existingId = 999;

        //Act
        var result = characterService.GetCharacterById(existingId);

        //Assert
        Assert.NotNull(result);
        Assert.Null(result.Data);
        Assert.False(result.Success);
        Assert.NotEmpty(result.Message);
    }

    [Fact]
    public void UpdateExistingCharacter()
    {
        //Arrange
        var characterService = new CharacterService();
        var updateCharacter = new Character { Id = 1, Name = "Stark" };

        //Act
        var result = characterService.UpdateCharacter(updateCharacter);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
        Assert.Contains(updateCharacter, result.Data);
    }

    [Fact]
    public void UpdateExistingCharacter_IfCharacterDontExist()
    {
        //Arrange
        var characterService = new CharacterService();
        var updateCharacter = new Character { Id = 5, Name = "bruce" };

        //Act
        var result = characterService.UpdateCharacter(updateCharacter);

        //Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
    }

    [Fact]
    public void DeleteCharacter_RemovesExistingCharacterFromList()
    {
        // Arrange
        var characterService = new CharacterService();
        var characterToRemove = new Character { Id = 1 };

        // Act
        var result = characterService.DeleteCharacter(characterToRemove);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Data);
        Assert.True(result.Success);
        Assert.Empty(result.Message);
        Assert.DoesNotContain(characterToRemove, result.Data); // Check if removed character is not in the list
    }

    [Fact]
        public void DeleteCharacter_ReturnsErrorForNonExistingCharacter()
        {
            // Arrange
            var characterService = new CharacterService();
            var nonExistingCharacter = new Character { Id = 999 };

            // Act
            var result = characterService.DeleteCharacter(nonExistingCharacter);

            // Assert
            Assert.NotNull(result);
            Assert.Null(result.Data);
            Assert.False(result.Success);
            Assert.NotEmpty(result.Message);
        }
}