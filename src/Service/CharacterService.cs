using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rpgAPI.Service
{
    public class CharacterService : ICharacterService
    {


        private static List<Character> _characterList = new List<Character>()
        {
            new Character(),
            new Character(){Name = "Gollum", Id = 1},
        };


        public ServiceResponse<List<Character>> GetAllCharacter()
        {
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;
        }

        public ServiceResponse<List<Character>> AddCharacter(Character newCharacter)
        { 
             _characterList.Add(newCharacter);
            
            var serviceResponse = new ServiceResponse<List<Character>>()
            {
                Data = _characterList
            };
            return serviceResponse;

        }

        public ServiceResponse<Character> GetCharacterById(int id)
        {
            var character = _characterList.FirstOrDefault(c=>c.Id==id);

            var serviceResponse = new ServiceResponse<Character>();

            if (character == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id Doesn't Exist";

                return serviceResponse;
            }

            serviceResponse.Data = character;

            return serviceResponse;
        }
        
        public ServiceResponse<List<Character>> UpdateCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            var oldCharacter = _characterList.FirstOrDefault(c => c.Id == character.Id);

            if (oldCharacter != null)
            {
                _characterList.RemoveAll(x => x.Id == character.Id);
            }

            _characterList.Add(character);

            serviceResponse.Data = _characterList;

            return serviceResponse;
        }

        public ServiceResponse<List<Character>> DeleteCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();

            var characterToRemove = _characterList.FirstOrDefault(c => c.Id == character.Id);

            if (characterToRemove != null)
            {
                _characterList.Remove(characterToRemove);
                serviceResponse.Data = _characterList;
                serviceResponse.Success = true;
            }
            else
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Character with ID {character.Id} not found.";
            }

            return serviceResponse;
        }        
    }
}