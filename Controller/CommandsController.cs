using AutoMapper;
using Azure;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controller
{
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //GET api/commands
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if (commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDTO>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandCreateDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDTO.Id }, commandReadDTO);
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDTO commandUpdateDTO)
        {
            var commandFromRepo = _repository.GetCommandById(id);
            if(commandFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDTO, commandFromRepo);

            _repository.UpdateCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //Patch api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDTO> patchDocument)
        {
            var commandFromRepo = _repository.GetCommandById(id);

            if(commandFromRepo == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandFromRepo);
            patchDocument.ApplyTo(commandToPatch, ModelState);

            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandFromRepo);
            _repository.UpdateCommand(commandFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
