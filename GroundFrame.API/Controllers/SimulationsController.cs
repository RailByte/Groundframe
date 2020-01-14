using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroundFrame.Classes;
using Microsoft.AspNetCore.Http;

namespace GroundFrame.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api")]
    public class Simulations : ControllerBase
    {
        private readonly ILogger<Simulation> _logger;
        private readonly GFSqlConnector _SQLConnection;
        public Simulations(ILogger<Simulation> logger)
        {
            _logger = logger;
            string SQLServer = @"(localdb)\MSSQLLocalDB"; ;
            string DBName = "GroundFrame.sql";
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testuserAPIKEY", SQLServer, DBName);
        }

        /// <summary>
        /// Gets a specific Simulation
        /// </summary>
        /// <param name="id">The ID of the Simulation to be returned</param>
        /// <response code="200">Returns the requested Simulation</response> 
        [HttpGet]
        [Route("simulations/{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                Classes.Simulation Sim = new Classes.Simulation(id, this._SQLConnection);
                return Ok(Sim);
            }
            catch (Exception Ex)
            {
                _logger.LogError($"An error has occurred: {Ex}");
                return StatusCode(500, Ex.Message);
            }
        }

        /// <summary>
        /// Gets all simulations that the user can see
        /// </summary>
        /// <response code="200">Returns the requested Simulation</response> 
        [HttpGet]
        [Route("simulations")]
        public IActionResult Get()
        {
            try
            {
                Classes.SimulationCollection SimCollection = new Classes.SimulationCollection(this._SQLConnection);
                return Ok(SimCollection);
            }
            catch (Exception Ex)
            {
                _logger.LogError($"An error has occurred: {Ex}");
                return StatusCode(500, Ex.Message);
            }
        }
    }
}
