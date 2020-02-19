using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GroundFrame.Core.SimSig;
using GroundFrame.Core;
using Microsoft.AspNetCore.Http;

namespace GroundFrame.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api")]
    public class Versions : ControllerBase
    {
        private readonly ILogger<GroundFrame.Core.SimSig.Version> _logger;
        private readonly GFSqlConnector _SQLConnection;
        public Versions(ILogger<GroundFrame.Core.SimSig.Version> logger)
        {
            _logger = logger;
            string SQLServer = @"(localdb)\MSSQLLocalDB"; ;
            string DBName = "GroundFrame.sql";
            this._SQLConnection = new GFSqlConnector("testappAPIKEY", "testuserAPIKEY", SQLServer, DBName);
        }

        /// <summary>
        /// Gets a specific Version
        /// </summary>
        /// <param name="id">The ID of the Version to be returned</param>
        /// <response code="200">Returns the requested Version</response> 
        [HttpGet]
        [Route("versions/{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                GroundFrame.Core.SimSig.Version Ver = new GroundFrame.Core.SimSig.Version(id, this._SQLConnection);
                return Ok(Ver);
            }
            catch (Exception Ex)
            {
                _logger.LogError($"An error has occurred: {Ex}");
                return StatusCode(500, Ex.Message);
            }
        }

        /// <summary>
        /// Gets all versions that the user can see. Development versions are limited to editors and administrators and hidden from standard users
        /// </summary>
        /// <response code="200">Returns the requested Version</response> 
        [HttpGet]
        [Route("versions")]
        public IActionResult Get()
        {
            try
            {
                GroundFrame.Core.SimSig.UserSettingCollection VerCollection = new GroundFrame.Core.SimSig.UserSettingCollection(this._SQLConnection);
                return Ok(VerCollection);
            }
            catch (Exception Ex)
            {
                _logger.LogError($"An error has occurred: {Ex}");
                return StatusCode(500, Ex.Message);
            }
        }
    }
}
