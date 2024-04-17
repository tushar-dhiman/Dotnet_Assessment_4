using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using rpgAPI.Controller;
using rpgAPI.Model;
using rpgAPI.Service;

namespace rpgAPITest;


public class CharacterControllerTest
{
    [Fact]
    public void GetCharacterController()
    {
        //arrange
        var cList = new List<Character>()
        {
        new Character(),
        new Character(){Name = "Gollum", Id = 1},
        };

        var serviceResponse = new ServiceResponse<List<Character>>()
        {
            Data = cList
        };

        var mockService = new Mock<ICharacterService>();

        mockService.Setup(x => x.GetAllCharacter()).Returns(serviceResponse);

        var characterController = new CharacterController(mockService.Object);

        //act
        var result = characterController.GetCharacter();

        var okResult = (ObjectResult)result.Result;

        //Assert
        Assert.NotNull(result);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public void GetIdController()
    {
        // Arrange
        int id = 1;
        var character = new Character { Name = "Gollum", Id = id };

        var serviceResponse = new ServiceResponse<Character>
        {
            Data = character
        };

        var mockService = new Mock<ICharacterService>();
        mockService.Setup(x => x.GetCharacterById(id)).Returns(serviceResponse);

        var charController = new CharacterController(mockService.Object);

        //Act
        var result = charController.GetId(id);
        var okResult = (ObjectResult)result.Result;

        // Assert

        var returnedResponse = Assert.IsType<ServiceResponse<Character>>(okResult.Value);
        Assert.Equal(character, returnedResponse.Data);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public void PostCharacterController()
    {
        //Arrange
        var newCharacter = new Character { Name = "Barbarian", Id = 3 };

        var serviceResponse = new ServiceResponse<List<Character>>
        {
            Data = new List<Character> { newCharacter }
        };

        var mockService = new Mock<ICharacterService>();
        mockService.Setup(x => x.AddCharacter(newCharacter)).Returns(serviceResponse);

        var charController = new CharacterController(mockService.Object);

        // Act
        var result = charController.PostCharacter(newCharacter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
        Assert.Contains(newCharacter, returnedResponse.Data);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

    }

    [Fact]
    public void UpdateCharacterController()
    {
        //Arrange
        var newCharacter = new Character { Name = "Dragon", Id = 3 };

        var serviceResponse = new ServiceResponse<List<Character>>
        {
            Data = new List<Character> { newCharacter }
        };

        var mockService = new Mock<ICharacterService>();
        mockService.Setup(x => x.UpdateCharacter(newCharacter)).Returns(serviceResponse);

        var charController = new CharacterController(mockService.Object);

        // Act
        var result = charController.UpdateCharacter(newCharacter);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
        Assert.Contains(newCharacter, returnedResponse.Data);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

    }

    [Fact]
    public void DeleteCharacter_ReturnsOkWithUpdatedCharacterList()
    {
        // Arrange
        var characterToDelete = new Character { Id = 1 }; // Create a character to delete
        var serviceResponse = new ServiceResponse<List<Character>> { Data = new List<Character>() }; // Assuming the character is successfully deleted

        var mockService = new Mock<ICharacterService>();
        mockService.Setup(x => x.DeleteCharacter(characterToDelete)).Returns(serviceResponse);

        var charController = new CharacterController(mockService.Object);

        // Act
        var result = charController.DeleteCharacter(characterToDelete);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponse = Assert.IsType<ServiceResponse<List<Character>>>(okResult.Value);
        Assert.Empty(returnedResponse.Data); // Check if the returned data is empty after deletion
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }
}